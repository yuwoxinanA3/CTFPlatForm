using CTFPlatForm.Core.Dto.Base;
using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Core.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CTFPlatForm.Api.Controllers.User
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    public class UserController : BaseController
    {
        #region 构造函数
        private IUserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        public UserController(IConfiguration configuration, ILogger<UserController> logger, IUserService userService) : base(configuration, logger)
        {
            _userService = userService;
        }

        #endregion

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="registerReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RegisterUser([FromBody] RegisterReq registerReq)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.RegisterUser(registerReq);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult> GetUserById()
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.GetUserById(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult> ChangeUserImage([FromBody] TextReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.ChangeUserImage(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value,req.TextContent);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult> ChangeUserNickname([FromBody] TextReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                return await _userService.ChangeUserNickname(User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value,req.TextContent);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }

        /// <summary>
        /// 修改密码（需要旧密码）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult> ChangePwdByOldPwd([FromBody] ChangePwdReq req)
        {
            //模型验证
            if (ModelState.IsValid)
            {
                // 从JWT中解析用户ID
                var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                return await _userService.ChangePwdByOldPwd(userId, req);
            }
            return new ApiResult()
            {
                IsSuccess = true,
                Result = false
            };
        }
    }
}
