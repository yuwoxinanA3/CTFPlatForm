using CTFPlatForm.Core.Dto.Login;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.Login;
using CTFPlatForm.Infrastructure.Tools.JWT;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    /// <summary>
    /// 登录系统相关接口
    /// </summary>
    public class LoginController : BaseController
    {
        #region 构造函数
        private ILoginService _loginService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="loginService"></param>
        public LoginController(IConfiguration configuration, ILogger<LoginController> logger, ILoginService loginService) : base(configuration, logger)
        {
            _loginService = loginService;
        }
        #endregion

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="loginReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginReq loginReq)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                UserRes user = await _loginService.GetUser(loginReq);
                if (user == null)
                {
                    return Unauthorized();
                }
                JWTHelper jWTHelper = new JWTHelper(_configuration);
                var token = jWTHelper.GenerateJwtToken(user.Id,loginReq.UserAccount);

                _logger.LogInformation("用户登录,用户编号:"+user.Id +"用户昵称:" +user.NickName);
                return Ok(new { token });
            }

            return Unauthorized();
        }

    }
}
