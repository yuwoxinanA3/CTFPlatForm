using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Interface
{
    /// <summary>
    /// CTF题目服务接口类
    /// </summary>
    public interface IChallengeService : IBaseService
    {
        /// <summary>
        /// Docker服务测试用接口
        /// </summary>
        /// <returns></returns>
        public Task<(string url, string errorMessage)> DockerTest();
    }
}
