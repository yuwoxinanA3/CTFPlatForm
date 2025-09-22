using SqlSugar;

namespace CTFPlatForm.Repository
{
    /// <summary>
    /// 仓储类基类
    /// </summary>
    public class BaseRepository
    {
        #region 构造函数
        /// <summary>
        /// SqlSugarClient 实例，db
        /// </summary>
        protected readonly ISqlSugarClient _db;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        public BaseRepository(ISqlSugarClient db)
        {
            _db = db;
        }
        #endregion

    }
}
