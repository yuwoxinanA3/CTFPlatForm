using Autofac;
using CTFPlatForm.Service;
using CTFPlatForm.Service.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Infrastructure.Tools
{
    /// <summary>
    /// Autofac自动依赖注入辅助类
    /// </summary>
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 获取所有相关的程序集
            var assemblies = new Assembly[]
            {
            Assembly.GetExecutingAssembly(), // CTFPlatForm.Infrastructure.Tools
            Assembly.Load("CTFPlatForm.Core"), // 包含接口定义的程序集
            Assembly.Load("CTFPlatForm.Service"), // 包含服务实现的程序集
            Assembly.Load("CTFPlatForm.Repository") // 包含仓储实现的程序集
            };

            // 自动注册所有 Repository（按接口匹配）
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsSelf()
                .InstancePerLifetimeScope();

            // 自动注册所有 Service 接口和实现
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service") && t.Name != "FileUploadServiceFactory")
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // 单独注册 Factory
            builder.RegisterType<FileUploadServiceFactory>()
                .As<IFileUploadServiceFactory>()
                .InstancePerLifetimeScope();

            // 注册具体的上传服务实现
            builder.RegisterType<LocalFileUploadService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<CloudFileUploadService>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
