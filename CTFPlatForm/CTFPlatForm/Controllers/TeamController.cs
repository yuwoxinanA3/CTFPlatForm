using CTFPlatForm.Core.Dto.Base;
using CTFPlatForm.Core.Dto.Team;
using CTFPlatForm.Core.Interface.Team;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CTFPlatForm.Api.Controllers
{
    /// <summary>
    /// 团队相关接口
    /// </summary>
    public class TeamController : BaseController
    {
        #region 构造函数
        private ITeamService _teamService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="teamService"></param>
        public TeamController(IConfiguration configuration, ILogger<TeamController> logger, ITeamService teamService) : base(configuration, logger)
        {
            _teamService = teamService;
        }
        #endregion

        /// <summary>
        /// 判断战队名是否可用
        /// </summary>
        /// <param name="textReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> IsAvailableTeamName([FromBody] TextReq textReq)
        {
            return await _teamService.IsAvailableTeamName(textReq.TextContent);
        }

        /// <summary>
        /// 创建战队
        /// </summary>
        /// <param name="createTeamReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> CreateCTFTeam([FromBody] CreateTeamReq createTeamReq)
        {
            // 从JWT中解析用户ID
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            return await _teamService.CreateCTFTeam(userId, createTeamReq);
        }
    }
}
