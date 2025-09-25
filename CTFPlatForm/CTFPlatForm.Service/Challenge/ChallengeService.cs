using CTFPlatForm.Core.Interface;
using CTFPlatForm.Infrastructure.Tools.Docker;
using CTFPlatForm.Repository;
using CTFPlatForm.Repository.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Service.Challenge
{
    public class ChallengeService : BaseService, IChallengeService
    {
        #region 构造函数
        private readonly ChallengeRepository _challengeRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChallengeService(ChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }
        #endregion

        public async Task<(string url, string errorMessage)> DockerTest()
        {
            return await DockerHelper.DockerTest();
        }


    }
}
