using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Repository
{
    /// <summary>
    /// CTF题目仓储类
    /// </summary>
    public class ChallengeRepository : BaseRepository
    {
        #region 构造函数
        public ChallengeRepository(ISqlSugarClient db) : base(db)
        {

        }
        #endregion


    }
}
