using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;

namespace CTFPlatForm.Core.Interface.Login
{
    /// <summary>
    /// 登录服务接口类
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Task<UserRes> GetUser(LoginReq req);

    }
}
