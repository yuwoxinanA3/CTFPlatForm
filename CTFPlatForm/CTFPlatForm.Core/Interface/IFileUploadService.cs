using Microsoft.AspNetCore.Http;

namespace CTFPlatForm.Core.Interface
{
    /// <summary>
    /// 文件上传服务接口类
    /// </summary>
    public interface IFileUploadService
    {
        /// <summary>
        /// 头像/战队图标上传服务
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<string> UploadAvatarAsync(IFormFile file);
    }

}
