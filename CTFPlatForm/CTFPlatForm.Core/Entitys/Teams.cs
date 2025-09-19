using CTFPlatForm.Core.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTFPlatForm.Core.Entitys
{
    public class Teams : IEntity
    {

        /// <summary>
        /// 团队名称
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 200)]
        public string? TeamName { get; set; }

        /// <summary>
        /// 战队图标
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string? TeamIcon { get; set; }

        /// <summary>
        /// 战队宣言
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 400)]
        public string? Declaration { get; set; }

        /// <summary>
        /// 团队介绍
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 1000)]
        public string? TeamIntroduction { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? EstablishmentTime { get; set; }

        /// <summary>
        /// 团队积分
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int TeamPoints { get; set; }

        /// <summary>
        /// 队长编号
        /// </summary>
        [SugarColumn(IsNullable = false, Length = 200)]
        public string TeamLeader { get; set; }

        /// <summary>
        /// 副队长1
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string? TeamLeader1 { get; set; }

        /// <summary>
        /// 副队长2
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string? TeamLeader2 { get; set; }

        /// <summary>
        /// 战队邮箱
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string? TeamEmail { get; set; }

        /// <summary>
        /// 战队网站
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string? TeamWebsite { get; set; }

        /// <summary>
        /// 国家/地区
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string? Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 100)]
        public string? City { get; set; }

        /// <summary>
        /// 学校/机构
        /// </summary>
        [SugarColumn(IsNullable = true, Length = 200)]
        public string? University { get; set; }

        /// <summary>
        /// 成员数量
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int? MemberCount { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsPublic { get; set; }
    }
}
