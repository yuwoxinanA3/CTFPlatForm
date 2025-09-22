namespace CTFPlatForm.Core.Dto.User
{
    /// <summary>
    /// 用户信息响应
    /// </summary>
    public class UserRes
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary> 
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary> 
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary> 
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary> 
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary> 
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary> 
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Image { get; set; }
    }
}
