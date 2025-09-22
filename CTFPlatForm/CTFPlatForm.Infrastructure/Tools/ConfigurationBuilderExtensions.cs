using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CTFPlatForm.Infrastructure.Tools
{
    /// <summary>
    /// 配置文件加载扩展类
    /// </summary>
    public static class ConfigurationBuilderExtensions
    {
        /// <summary>
        /// json配置文件加载
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        /// <param name="filePathInAssembly"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static IConfigurationBuilder AddEmbeddedJsonFile(this IConfigurationBuilder builder, Assembly assembly, string filePathInAssembly)
        {
            var resourceName = $"{assembly.GetName().Name}.{filePathInAssembly.Replace("/", ".")}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"Embedded file '{resourceName}' not found in assembly '{assembly.FullName}'.");

                builder.AddJsonStream(stream);
            }

            return builder;
        }
    }
}
