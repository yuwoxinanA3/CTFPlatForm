using System.ComponentModel.DataAnnotations;

namespace CTFPlatForm.Core.Dto.Team
{
    /// <summary>
    /// 创建团队请求
    /// </summary>
    public class CreateTeamReq
    {
        [Required(ErrorMessage = "战队名称缺失")]
        public string TeamName { get; set; }

        public string TeamIcon { get; set; }

        public string? Declaration { get; set; }

        public string? TeamIntroduction { get; set; }

        public DateTime? EstablishmentTime { get; set; }

        public string? TeamEmail { get; set; }

        public string? TeamWebsite { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }
        
        public bool IsPublic { get; set; }
        
    }
}
