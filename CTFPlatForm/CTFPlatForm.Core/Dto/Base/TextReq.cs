using System.ComponentModel.DataAnnotations;

namespace CTFPlatForm.Core.Dto.Base
{
    /// <summary>
    /// 文本类型请求
    /// </summary>
    public class TextReq
    {
        [Required(ErrorMessage = "内容缺失")]
        public string TextContent { get; set; }

        /// <summary>
        /// 可选文本2
        /// </summary>
        public string? TextContent2 { get; set; }

        /// <summary>
        /// 可选文本3
        /// </summary>
        public string? TextContent3 { get; set; }
    }
}
