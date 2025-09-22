// req.ts
export interface changePwd_req {
    /**
     * 新密码
     */
    newPassWord: string;

    /**
     * 旧密码
     */
    oldPassWord?: string;

}