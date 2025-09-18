// req.ts
export interface CreateTeamReq {
    /**
     * 战队名称（必填）
     */
    teamName: string;

    /**
     * 战队图标
     */
    teamIcon?: string;

    /**
     * 战队宣言
     */
    declaration?: string;

    /**
     * 战队介绍
     */
    teamIntroduction?: string;

    /**
     * 成立时间
     */
    establishmentTime: Date;

    /**
     * 战队邮箱
     */
    teamEmail?: string;

    /**
     * 战队网站
     */
    teamWebsite?: string;

    /**
     * 国家
     */
    country?: string;

    /**
   * 城市
   */
    city?: string;

    /**
     * 是否公开
     */
    isPublic: boolean;
}