using CTFPlatForm.Core.Other;
using Microsoft.AspNetCore.Http;

namespace CTFPlatForm.Api.Config
{
    /// <summary>
    /// 返回结果类
    /// </summary>
    public class ResultHelper
    {
        /// <summary>
        /// 请求成功结果
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResult Success(object data)
        {
            return new ApiResult()
            {
                IsSuccess = true,
                Result = data,
                Msg = string.Empty
            };
        }

        /// <summary>
        /// 请求失败结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ApiResult Error(string msg)
        {
            return new ApiResult()
            {
                IsSuccess = false,
                Result = null,
                Msg = msg
            };
        }
    }
}
