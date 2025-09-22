using CTFPlatForm.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    /// <summary>
    /// 数据库 升级接口
    /// </summary>
    public class UpgradeController : BaseController
    {

        #region 构造函数
        private IUpgradeService _upgradeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UpgradeController(IConfiguration configuration, ILogger<LoginController> logger, IUpgradeService upgradeService) : base(configuration, logger)
        {
            _upgradeService = upgradeService;
        }
        #endregion

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> InitDataBase()
        {
            return await _upgradeService.InitDataBase();
        }

        /// <summary>
        /// 升级数据库
        /// </summary>
        /// <remarks>
        /// 此接口用于升级数据库表结构，包括：
        /// - 创建新增的表
        /// - 添加新增的字段
        /// - 不会影响已有数据
        /// </remarks>
        /// <returns>返回升级操作是否成功</returns>
        [HttpPost]
        public async Task<bool> UpgradeDataBase()
        {
            return await _upgradeService.UpgradeDataBase();
        }

    }

}