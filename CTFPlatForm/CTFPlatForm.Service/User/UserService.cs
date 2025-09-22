using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Entitys;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Core.Other;
using CTFPlatForm.Repository.User;
using Microsoft.Extensions.Logging;

namespace CTFPlatForm.Service.Register
{
    /// <summary>
    /// 用户服务类
    /// </summary>
    public class UserService : IUserService
    {

        #region 构造函数
        private UserRepository _userRepository;
        private ILogger<UserService> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// 账号密码注册用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResult> RegisterUser(RegisterReq req)
        {
            try
            {
                bool isExisting = await _userRepository.ExistingUser(req.UserAccount);
                //检查账号是否已存在
                if (isExisting)
                    return new ApiResult
                    {
                        IsSuccess = true,
                        Result = false,
                        Msg = "注册失败，账号已存在!"
                    };
                else
                {
                    Users newUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        NickName = req.UserAccount,
                        UserAccount = req.UserAccount,
                        Password = BCrypt.Net.BCrypt.HashPassword(req.PassWord), //BCrypt加密
                        UserType = 1,
                        IsEnable = true,
                        Image = "https://img2.baidu.com/it/u=3233382939,3485050990&fm=253&fmt=auto&app=138&f=JPEG?w=500&h=500",
                        CreateDate = DateTime.Now,
                        CreateUserId = "system",
                        ModifyDate = null
                    };
                    bool result = await _userRepository.AddUser(newUser);
                    return new ApiResult
                    {
                        IsSuccess = result,
                        Result = result,
                        Msg = result ? "注册成功!" : "注册失败，请重新尝试，若多次尝试失败，请联系客服!"
                    };

                }

            }
            catch (Exception ex)
            {
                // 记录日志
                _logger?.LogError(ex, "用户注册失败: {UserAccount}", req.UserAccount);

                return new ApiResult
                {
                    IsSuccess = false,
                    Result = false,
                    Msg = "系统异常，请稍后重试!"
                };
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUserById(string userId)
        {
            UserRes user = await _userRepository.GetUserById(userId);
            if (user != null)
            {
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = user,
                    Msg = "Success!"
                };
            }
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = null,
                    Msg = "未查询到指定用户!"
                };
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userImageUrl"></param>
        /// <returns></returns>
        public async Task<ApiResult> ChangeUserImage(string userId, string userImageUrl)
        {
            bool isSuccess = await _userRepository.ChangeUserImage(userId, userImageUrl);
            if (isSuccess)
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = true
                };
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = false
                };
        }

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newNickName"></param>
        /// <returns></returns>
        public async Task<ApiResult> ChangeUserNickname(string userId, string newNickName)
        {
            bool isSuccess = await _userRepository.ChangeUserNickname(userId, newNickName);
            if (isSuccess)
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = true
                };
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = false
                };
        }

        /// <summary>
        /// 通过旧密码修改用户密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<ApiResult> ChangePwdByOldPwd(string userId, ChangePwdReq req)
        {
            //验证旧密码
            UserRes user = await _userRepository.GetUserById(userId);
            bool isSuccess = true;

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(req.OldPassword, user.Password))

                    if (BCrypt.Net.BCrypt.Verify(req.NewPassword, user.Password))
                        return new ApiResult
                        {
                            IsSuccess = true,
                            Result = false,
                            Msg = "same password!"
                        };
                    else
                        //修改旧密码
                        isSuccess = await _userRepository.ChangePwdByOldPwd(userId, BCrypt.Net.BCrypt.HashPassword(req.NewPassword));
                else
                    return new ApiResult
                    {
                        IsSuccess = true,
                        Result = false,
                        Msg = "oldPassword Error!"
                    };
            }
            else
                isSuccess = false;
            //返回结果
            if (isSuccess)
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = true
                };
            else
                return new ApiResult
                {
                    IsSuccess = true,
                    Result = false
                };
        }
    }
}
