using System.ComponentModel.DataAnnotations;

namespace CTFPlatForm.Core.Dto.Login
{
    /// <summary>
    /// 登录请求
    /// </summary>
    public class LoginReq
    {
        [Required(ErrorMessage = "账号缺失")]
        public string UserAccount { get; set; }
        [Required(ErrorMessage = "密码缺失")]
        public string PassWord { get; set; }
    }
}
