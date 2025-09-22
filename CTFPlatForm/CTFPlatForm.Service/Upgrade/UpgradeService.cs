using CTFPlatForm.Core.Interface;
using CTFPlatForm.Repository;

namespace CTFPlatForm.Service.Upgrade
{
    /// <summary>
    /// 升级服务类
    /// </summary>
    public class UpgradeService:BaseService, IUpgradeService
    {
        #region 构造函数
        private UpgradeRepository _upgradeRepository;

        public UpgradeService(UpgradeRepository upgradeRepository)
        {
            _upgradeRepository = upgradeRepository;
        }
        #endregion

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitDataBase()
        {
            return await _upgradeRepository.InitDataBase();
        }

        /// <summary>
        /// 升级数据库
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpgradeDataBase()
        {
            return await _upgradeRepository.UpgradeDataBase();
        }
    }
}
