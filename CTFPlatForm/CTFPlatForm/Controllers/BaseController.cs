using Microsoft.AspNetCore.Mvc;

namespace CTFPlatForm.Api.Controllers
{
    /// <summary>
    /// 基础Controller
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        #region 构造函数
        protected readonly IConfiguration _configuration;

        protected readonly ILogger<BaseController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public BaseController(IConfiguration configuration, ILogger<BaseController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #endregion
    }
}
