using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface
{
    /// <summary>
    /// 升级服务接口类
    /// </summary>
    public interface IUpgradeService
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <returns></returns>
        public Task<bool> InitDataBase();
        
        /// <summary>
        /// 数据库升级
        /// </summary>
        /// <returns></returns>
        public Task<bool> UpgradeDataBase();

    }
}
