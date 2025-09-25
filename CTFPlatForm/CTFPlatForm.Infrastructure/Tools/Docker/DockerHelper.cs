using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Infrastructure.Tools.Docker
{
    public static class DockerHelper
    {
        /// <summary>
        /// 测试docker服务
        /// </summary>
        /// <returns></returns>
        public static async Task<(string url, string errorMessage)> DockerTest()
        {
            try
            {
                // 创建 Docker 客户端
                var client = new DockerClientConfiguration(
                    new Uri(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                        ? "npipe://./pipe/docker_engine"
                        : "unix:///var/run/docker.sock"))
                    .CreateClient();

                // 检查 Docker 服务是否可用
                try
                {
                    var version = await client.System.GetVersionAsync();
                    Console.WriteLine($"Docker 版本: {version.Version}");
                }
                catch (Exception ex)
                {
                    return (string.Empty, $"无法连接到 Docker 服务: {ex.Message}");
                }

                // 拉取 nginx 镜像（如果还没有的话）
                Console.WriteLine("正在拉取 nginx 镜像...");
                try
                {
                    await client.Images.CreateImageAsync(
                        new ImagesCreateParameters { FromImage = "nginx", Tag = "latest" },
                        null,
                        new Progress<JSONMessage>(message =>
                        {
                            if (!string.IsNullOrEmpty(message.Status))
                                Console.WriteLine(message.Status);
                        })
                    );
                }
                catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return (string.Empty, "无法找到 nginx 镜像，请检查网络连接或镜像名称");
                }
                catch (Exception ex)
                {
                    return (string.Empty, $"拉取镜像时发生错误: {ex.Message}");
                }

                // 生成唯一的容器名称，避免命名冲突
                string containerName = $"test-nginx-container-{DateTime.UtcNow.ToString("yyyyMMdd-HHmmss")}";

                // 创建容器
                Console.WriteLine("正在创建容器...");
                CreateContainerResponse container;
                try
                {
                    container = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
                    {
                        Image = "nginx:latest",
                        Name = containerName,
                        HostConfig = new HostConfig()
                        {
                            PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {
                            "80/tcp",
                            new List<PortBinding>
                            {
                                new PortBinding
                                {
                                    HostPort = "8080"
                                }
                            }
                        }
                    }
                        }
                    });
                }
                catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    return (string.Empty, "容器名称冲突，请稍后重试");
                }
                catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return (string.Empty, "镜像不存在，请确认镜像已正确拉取");
                }
                catch (Exception ex)
                {
                    return (string.Empty, $"创建容器时发生错误: {ex.Message}");
                }

                Console.WriteLine($"容器创建成功，ID: {container.ID.Substring(0, 12)}");

                // 启动容器
                Console.WriteLine("正在启动容器...");
                try
                {
                    await client.Containers.StartContainerAsync(container.ID, new ContainerStartParameters());
                }
                catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    // 如果启动失败，尝试删除已创建的容器
                    try
                    {
                        await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
                    }
                    catch { /* 忽略清理错误 */ }

                    return (string.Empty, "启动容器失败，可能是端口被占用或其他系统错误");
                }
                catch (Exception ex)
                {
                    // 如果启动失败，尝试删除已创建的容器
                    try
                    {
                        await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
                    }
                    catch { /* 忽略清理错误 */ }

                    return (string.Empty, $"启动容器时发生错误: {ex.Message}");
                }

                Console.WriteLine("容器已启动");

                // 验证容器是否正在运行
                try
                {
                    var containerInfo = await client.Containers.InspectContainerAsync(container.ID);
                    if (containerInfo.State.Running != true)
                    {
                        return (string.Empty, "容器启动后未处于运行状态");
                    }
                }
                catch (Exception ex)
                {
                    return (string.Empty, $"验证容器状态时发生错误: {ex.Message}");
                }

                // 列出正在运行的容器
                try
                {
                    var containers = await client.Containers.ListContainersAsync(new ContainersListParameters());
                    Console.WriteLine("\n当前运行的容器:");
                    foreach (var c in containers)
                    {
                        Console.WriteLine($"- {c.Names.FirstOrDefault()} ({c.Image}) - {c.Status}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"列出容器时发生警告: {ex.Message}");
                }

                Console.WriteLine("\n5分钟内,你可以在浏览器中访问 http://localhost:8080 来查看 nginx 页面");

                // 启动一个后台任务，5分钟后自动清理容器
                _ = Task.Run(async () =>
                {
                    try
                    {
                        // 等待5分钟
                        await Task.Delay(TimeSpan.FromMinutes(5));

                        // 停止容器
                        Console.WriteLine("正在停止容器...");
                        try
                        {
                            await client.Containers.StopContainerAsync(container.ID, new ContainerStopParameters());
                        }
                        catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            Console.WriteLine("容器已不存在，无需停止");
                        }

                        // 删除容器
                        Console.WriteLine("正在删除容器...");
                        try
                        {
                            await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters());
                        }
                        catch (DockerApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            Console.WriteLine("容器已不存在，无需删除");
                        }

                        Console.WriteLine("容器已清理完成");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"自动清理容器时发生错误: {ex.Message}");
                    }
                });

                return ("http://localhost:8080", string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生未预期的错误: {ex.Message}");
                return (string.Empty, $"系统错误: {ex.Message}");
            }
        }







    }
}