using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Repository.Login;

namespace CTFPlatForm.Service.Login
{
    /// <summary>
    /// 登录服务类
    /// </summary>
    public class LoginService : BaseService, ILoginService
    {
        #region 构造函数
        private readonly LoginRepository _loginRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginService(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        #endregion

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<UserRes> GetUser(LoginReq req)
        {
            return await _loginRepository.GetUser(req);
        }
    }
}
