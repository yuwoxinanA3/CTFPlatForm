using CTFPlatForm.Core.Interface;
using CTFPlatForm.Service.Upload;
using Microsoft.Extensions.DependencyInjection;

namespace CTFPlatForm.Service
{
    /// <summary>
    /// 文件上传服务接口类
    /// </summary>
    public interface IFileUploadServiceFactory
    {
        IFileUploadService GetUploadService(string type);
    }
    /// <summary>
    /// 文件上传服务工厂类
    /// </summary>

    public class FileUploadServiceFactory : IFileUploadServiceFactory
    {
        #region 构造函数
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public FileUploadServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion

        /// <summary>
        /// 获取文件上传服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IFileUploadService GetUploadService(string type)
        {
            return type.ToLower() switch
            {
                "local" => _serviceProvider.GetRequiredService<LocalFileUploadService>(),//本地上传
                "cloud" => _serviceProvider.GetRequiredService<CloudFileUploadService>(),//云服务上传
                _ => _serviceProvider.GetRequiredService<LocalFileUploadService>() // 默认使用本地存储
            };
        }
    }
}
