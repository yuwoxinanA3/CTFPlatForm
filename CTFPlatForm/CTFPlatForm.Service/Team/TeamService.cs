using CTFPlatForm.Core.Dto.Team;
using CTFPlatForm.Core.Entitys;
using CTFPlatForm.Core.Interface.Team;
using CTFPlatForm.Repository.Team;

namespace CTFPlatForm.Service.Team
{
    /// <summary>
    /// 团队服务类
    /// </summary>
    public class TeamService : BaseService, ITeamService
    {
        #region 构造函数
        private readonly TeamRepository _teamRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        #endregion

        /// <summary>
        /// 团队名是否可用
        /// </summary>
        /// <param name="TeamName"></param>
        /// <returns></returns>
        public async Task<bool> IsAvailableTeamName(string TeamName)
        {
            return await _teamRepository.IsAvailableTeamName(TeamName);
        }

        /// <summary>
        /// 创建团队
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="createTeamReq"></param>
        /// <returns></returns>
        public async Task<bool> CreateCTFTeam(string UserId, CreateTeamReq createTeamReq)
        {
            Teams newTeam = new()
            {
                Id = Guid.NewGuid().ToString(),
                TeamName = createTeamReq.TeamName,
                TeamIcon = createTeamReq.TeamIcon,
                Declaration = createTeamReq.Declaration,
                TeamIntroduction = createTeamReq.TeamIntroduction,
                EstablishmentTime = createTeamReq.EstablishmentTime,
                TeamPoints = 0,
                TeamLeader = UserId,
                TeamEmail = createTeamReq.TeamEmail,
                TeamWebsite = createTeamReq.TeamWebsite,
                Country = createTeamReq.Country,
                City = createTeamReq.City,
                University = string.Empty,
                MemberCount = 1,
                IsPublic = createTeamReq.IsPublic,
                //标准字段
                CreateUserId = UserId,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };
            return await _teamRepository.AddTeam(newTeam);
        }

    }
}
