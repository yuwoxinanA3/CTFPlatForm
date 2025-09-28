using CTFPlatForm.Core.Entitys;
using SqlSugar;

namespace CTFPlatForm.Repository.Team
{
    /// <summary>
    /// 战队仓储类
    /// </summary>
    public class TeamRepository : BaseRepository
    {
        #region 构造函数
        public TeamRepository(ISqlSugarClient db) : base(db)
        {


        }
        #endregion

        /// <summary>
        /// 判断战队名是否可用
        /// </summary>
        /// <param name="TeamName"></param>
        /// <returns></returns>
        public async Task<bool> IsAvailableTeamName(string TeamName)
        {
            var exists = await _db.Queryable<Teams>()
                .AnyAsync(p => p.TeamName == TeamName);
            return !exists;
        }

        /// <summary>
        /// 创建战队
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<bool> AddTeam(Teams team)
        {
            return await _db.Insertable(team).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 获取指定战队
        /// </summary>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        public async Task<Teams> GetTeamInfo(string TeamId)
        {
            Teams team = await _db.Queryable<Teams>()
                .FirstAsync(p => p.Id == TeamId);

            return team;
        }
    }
}
