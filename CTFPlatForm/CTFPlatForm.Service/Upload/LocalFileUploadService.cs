using CTFPlatForm.Core.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

/// <summary>
/// 本地服务上传类
/// </summary>
public class LocalFileUploadService : IFileUploadService
{
    #region 构造函数
    private readonly IWebHostEnvironment _environment;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="environment"></param>
    public LocalFileUploadService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    #endregion

    /// <summary>
    /// 头像上传
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<string> UploadAvatarAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("文件不能为空");

        try
        {
            // 获取web根路径
            string webRootPath = _environment.WebRootPath;

            // 如果WebRootPath为null或空，使用当前目录下的wwwroot
            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            // 确保web根目录存在
            if (!Directory.Exists(webRootPath))
            {
                Directory.CreateDirectory(webRootPath);
            }

            // 创建上传目录
            var uploadPath = Path.Combine(webRootPath, "uploads", "avatars");

            // 确保上传目录存在
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // 生成唯一文件名
            var fileExtension = Path.GetExtension(file.FileName);
            // 确保文件扩展名安全
            if (string.IsNullOrEmpty(fileExtension))
            {
                fileExtension = ".png"; // 默认扩展名
            }

            var fileName = $"{Guid.NewGuid()}{fileExtension.ToLowerInvariant()}";
            var filePath = Path.Combine(uploadPath, fileName);

            // 保存文件
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 返回可访问的URL
            var url = $"/uploads/avatars/{fileName}";
            return url;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"文件上传失败: {ex.Message}", ex);
        }
    }
}