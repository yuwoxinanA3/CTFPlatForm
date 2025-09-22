using CTFPlatForm.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CTFPlatForm.Service.Upload
{
    /// <summary>
    /// 云服务上传 //to do
    /// </summary>
    public class CloudFileUploadService : IFileUploadService
    {
        #region 构造函数
        private readonly string _accessKeyId;
        private readonly string _accessKeySecret;
        private readonly string _endpoint;
        private readonly string _bucketName;

        public CloudFileUploadService(IConfiguration configuration)
        {
            _accessKeyId = configuration["OSS:AccessKeyId"];
            _accessKeySecret = configuration["OSS:AccessKeySecret"];
            _endpoint = configuration["OSS:Endpoint"];
            _bucketName = configuration["OSS:BucketName"];
        }
        #endregion

        /// <summary>
        /// 头像上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadAvatarAsync(IFormFile file)
        {
            //if (file == null || file.Length == 0)
            //    throw new ArgumentException("文件不能为空");

            //// 使用阿里云OSS SDK上传文件
            //var client = new OssClient(_endpoint, _accessKeyId, _accessKeySecret);

            //using (var stream = file.OpenReadStream())
            //{
            //    var fileName = $"avatars/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            //    await client.PutObjectAsync(_bucketName, fileName, stream);

            //    // 返回可访问的URL
            //    return $"https://{_bucketName}.{_endpoint}/{fileName}";
            //}

            return $"https://11111";
        }
    }
}
