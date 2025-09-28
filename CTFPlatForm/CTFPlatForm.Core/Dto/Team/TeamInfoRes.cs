using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Dto.Team
{
    /// <summary>
    /// 战队信息返回类
    /// </summary>
    public class TeamInfoRes
    {
        /// <summary>
        /// 战队名称
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 战队图标
        /// </summary>
        public string TeamIcon { get; set; }

        /// <summary>
        /// 战队宣言
        /// </summary>
        public string Declaration { get; set; }

        /// <summary>
        /// 战队介绍
        /// </summary>
        public string TeamIntroduction { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        public string EstablishmentTime { get; set; }

        /// <summary>
        /// 战队积分
        /// </summary>
        public int TeamPoints { get; set; }

        /// <summary>
        /// 战队邮箱
        /// </summary>
        public string TeamEmail { get; set; }

        /// <summary>
        /// 战队网站
        /// </summary>
        public string TeamWebsite { get; set; }

        /// <summary>
        /// 国家/地区
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 大学/组织机构
        /// </summary>
        public string University { get; set; }

        /// <summary>
        /// 成员数量
        /// </summary>
        public int MemberCount { get; set; }

        /// <summary>
        /// 队长名称
        /// </summary>
        public string TeamLeader { get; set; }

        /// <summary>
        /// 队长头像
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// 副队长1名称
        /// </summary>
        public string TeamLeader1 { get; set; }

        /// <summary>
        /// 副队长1头像
        /// </summary>
        public string UserImage1 { get; set; }

        /// <summary>
        /// 副队长2名称
        /// </summary>
        public string TeamLeader2 { get; set; }

        /// <summary>
        /// 副队长2头像
        /// </summary>
        public string UserImage2 { get; set; }
    }
}
