namespace CTFPlatForm.Core.Dto.User
{
    /// <summary>
    /// 修改密码请求
    /// </summary>
    public class ChangePwdReq
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
