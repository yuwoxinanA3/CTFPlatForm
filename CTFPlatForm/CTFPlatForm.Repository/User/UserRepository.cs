using CTFPlatForm.Core.Dto.User;
using CTFPlatForm.Core.Entitys;
using SqlSugar;

namespace CTFPlatForm.Repository.User
{
    /// <summary>
    /// 用户仓储类
    /// </summary>
    public class UserRepository : BaseRepository
    {
        #region 构造函数

        /// <summary>
        /// 用户仓储类构造函数
        /// </summary>
        /// <param name="db"></param>
        public UserRepository(ISqlSugarClient db) : base(db)
        {

        }

        #endregion

        #region 查询
        /// <summary>
        /// 使用账号判断用户是否存在
        /// </summary>
        /// <param name="UserAccount">账号</param>
        /// <returns></returns>
        public async Task<bool> ExistingUser(string UserAccount)
        {
            var existingUser = await _db.Queryable<Users>()
            .Where(p => p.UserAccount == UserAccount)
            .Select(p => new UserRes() { }, true).FirstAsync();
            return existingUser != null;
        }

        public async Task<UserRes> GetUserById(string id)
        {
            var user = await _db.Queryable<Users>().Where(p => p.Id == id).Select(p => new UserRes() { }, true).FirstAsync();
            if (user == null)
            {
                throw new Exception($"User with id {id} not found");
            }
            return user;
        }

        #endregion

        #region 增加
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(Users user)
        {
            return await _db.Insertable(user).ExecuteCommandAsync() > 0;
        }
        #endregion

        #region 修改
        public async Task<bool> ChangeUserImage(string id, string UserImageUrl)
        {
            if (UserImageUrl == null)
                UserImageUrl = string.Empty;
            // 根据用户ID更新用户头像URL
            var result = await _db.Updateable<Users>()
                .SetColumns(it => new Users() { Image = UserImageUrl })
                .Where(it => it.Id == id)
                .ExecuteCommandAsync();

            // 返回是否更新成功（影响行数大于0表示成功）
            return result > 0;
        }

        public async Task<bool> ChangeUserNickname(string id, string newNickName)
        {
            if (newNickName == null)
                newNickName = string.Empty;
            // 根据用户ID更新用户头像URL
            var result = await _db.Updateable<Users>()
                .SetColumns(it => new Users() { NickName = newNickName })
                .Where(it => it.Id == id)
                .ExecuteCommandAsync();

            // 返回是否更新成功（影响行数大于0表示成功）
            return result > 0;
        }
        /// <summary>
        /// 修改用户密码(旧密码)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public async Task<bool> ChangePwdByOldPwd(string id, string passWord)
        {
            // 根据用户ID更新密码
            var result = await _db.Updateable<Users>()
                .SetColumns(it => new Users() { Password = passWord })
                .Where(it => it.Id == id)
                .ExecuteCommandAsync();

            // 返回是否更新成功（影响行数大于0表示成功）
            return result > 0;
        }

        #endregion
    }
}
