using CTFPlatForm.Api.Config;
using CTFPlatForm.Core.Interface;
using CTFPlatForm.Core.Interface.User;
using CTFPlatForm.Core.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    public class ChallengeController : BaseController
    {
        #region 构造函数
        private IChallengeService _challengeService;

        public ChallengeController(IConfiguration configuration, ILogger<ChallengeController> logger, IChallengeService challengeService) : base(configuration, logger)
        {
            _challengeService = challengeService;
        }
        #endregion

        /// <summary>
        /// Docker服务测试用接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> DockerTest()
        {
            var (dockerUrl, errorMessage) = await _challengeService.DockerTest();

            if (!string.IsNullOrEmpty(dockerUrl))
                return ResultHelper.Success(dockerUrl);
            else
                return ResultHelper.Error($"Docker服务存在问题：{errorMessage}");
        }


    }
}
