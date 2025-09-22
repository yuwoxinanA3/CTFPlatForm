using CTFPlatForm.Core.Common;
using SqlSugar;

namespace CTFPlatForm.Core.Entitys
{
    /// <summary>
    /// 用户表结构
    /// </summary>
    public class Users : IEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string UserAccount { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public string Password { get; set; }
        /// <summary>
        /// 用户类型（0=超级管理员，1=普通用户）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int UserType { get; set; }
        /// <summary>
        /// 是否启用（0=未启用，1=启用）
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public bool IsEnable { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Image { get; set; }
        /// <summary>
        /// 性别（0=女，1=男）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Sex { get; set; }
    }
}
