using CTFPlatForm.Core.Dto.Team;
using CTFPlatForm.Core.Entitys;
using CTFPlatForm.Core.Interface.Team;
using CTFPlatForm.Infrastructure.CustomException;
using CTFPlatForm.Repository.Team;
using CTFPlatForm.Repository.User;

namespace CTFPlatForm.Service.Team
{
    /// <summary>
    /// 团队服务类
    /// </summary>
    public class TeamService : BaseService, ITeamService
    {
        #region 构造函数
        private readonly TeamRepository _teamRepository;
        private readonly UserRepository _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TeamService(TeamRepository teamRepository, UserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
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

        /// <summary>
        /// 获取团队信息
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public async Task<TeamInfoRes> GetTeamInfo(string TeamId)
        {
            //获取团队基础信息
            var teaminfo = await _teamRepository.GetTeamInfo(TeamId);

            // 查找不到对应战队，抛出异常
            if (teaminfo == null)
            {
                throw new NotFoundException($"未找到ID为 {TeamId} 的战队");
            }

            // 获取队长、副队长1和副队长2的昵称和头像
            string teamLeaderName = string.Empty;
            string teamLeaderAvatar = string.Empty;
            string teamLeader1Name = string.Empty;
            string teamLeader1Avatar = string.Empty;
            string teamLeader2Name = string.Empty;
            string teamLeader2Avatar = string.Empty;

            // 获取队长信息
            if (!string.IsNullOrEmpty(teaminfo.TeamLeader))
            {
                try
                {
                    var leader = await _userRepository.GetUserById(teaminfo.TeamLeader);
                    if (leader != null)
                    {
                        teamLeaderName = leader.NickName ?? string.Empty;
                        teamLeaderAvatar = leader.Image ?? string.Empty;
                    }
                }
                catch (Exception)
                {
                    // 如果获取用户信息失败，保持默认空值
                }
            }

            // 获取副队长1信息
            if (!string.IsNullOrEmpty(teaminfo.TeamLeader1))
            {
                try
                {
                    var leader1 = await _userRepository.GetUserById(teaminfo.TeamLeader1);
                    if (leader1 != null)
                    {
                        teamLeader1Name = leader1.NickName ?? string.Empty;
                        teamLeader1Avatar = leader1.Image ?? string.Empty;
                    }
                }
                catch (Exception)
                {
                    // 如果获取用户信息失败，保持默认空值
                }
            }

            // 获取副队长2信息
            if (!string.IsNullOrEmpty(teaminfo.TeamLeader2))
            {
                try
                {
                    var leader2 = await _userRepository.GetUserById(teaminfo.TeamLeader2);
                    if (leader2 != null)
                    {
                        teamLeader2Name = leader2.NickName ?? string.Empty;
                        teamLeader2Avatar = leader2.Image ?? string.Empty;
                    }
                }
                catch (Exception)
                {
                    // 如果获取用户信息失败，保持默认空值
                }
            }

            //封装数据
            return new TeamInfoRes
            {
                TeamName = teaminfo.TeamName,
                TeamIcon = teaminfo.TeamIcon,
                Declaration = teaminfo.Declaration,
                TeamIntroduction = teaminfo.TeamIntroduction,
                EstablishmentTime = teaminfo.EstablishmentTime.ToString(),
                TeamPoints = teaminfo.TeamPoints,
                TeamEmail = teaminfo.TeamEmail,
                TeamWebsite = teaminfo.TeamWebsite,
                Country = teaminfo.Country,
                City = teaminfo.City,
                University = teaminfo.University,
                MemberCount = (int)teaminfo.MemberCount,
                TeamLeader = teamLeaderName,
                UserImage = teamLeaderAvatar,
                TeamLeader1 = teamLeader1Name,
                UserImage1 = teamLeader1Avatar,
                TeamLeader2 = teamLeader2Name,
                UserImage2 = teamLeader2Avatar
            };
        }


    }
}
