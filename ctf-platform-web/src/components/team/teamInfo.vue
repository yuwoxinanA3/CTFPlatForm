<template>
    <div class="card-box">
        <div class="top-glow"></div>
        <div class="bottom-glow"></div>

        <SingleImageUpload v-model:value="teamInfo.teamIcon" :disabled="true" />

        <div class="center-box">
            <div id="teamName">
                <span class="text-content">{{ teamInfo.teamName }}</span>
            </div>
        </div>

        <div class="center-box">
            <div id="teamDeclaration">
                <span class="text-content">{{ teamInfo.declaration }}</span>
            </div>
        </div>

        <div class="center-box">
            <div id="teamIntroduction">
                <p class="intro-text">
                    {{ teamInfo.teamIntroduction }}
                </p>
            </div>
        </div>
        <div class="stats-container">
            <TeamStatItem :item="{ label: '成立', value: teamInfo.establishmentTime }" />
            <TeamStatItem :item="{ label: '成员', value: teamInfo.memberCount.toString() }" />
            <TeamStatItem :item="{ label: '积分', value: teamInfo.teamPoints.toString() }" />

            <TeamStatItem :item="{ label: '国家/地区', value: teamInfo.country }" />
            <TeamStatItem :item="{ label: '城市', value: teamInfo.city }" />
            <TeamStatItem :item="{ label: '组织/机构', value: teamInfo.university }" />

            <TeamStatItem :item="{ label: '副队长', value: teamInfo.teamLeader1, avatar: teamInfo.userImage1 }" />
            <TeamStatItem :item="{ label: '队长', value: teamInfo.teamLeader, avatar: teamInfo.userImage }" />
            <TeamStatItem :item="{ label: '副队长', value: teamInfo.teamLeader2, avatar: teamInfo.userImage2 }" />
        </div>

        <div class="center-box">
            <el-button type="warning">查看官网</el-button>
            <el-button type="success">查看邮箱</el-button>
            <el-button type="primary">编辑信息</el-button>
        </div>
    </div>
</template>
<script setup lang='ts'>
//官方引入
import { reactive } from 'vue';

//插件引入

//自定义引入
import SingleImageUpload from '@/components/base/singleImageUpload.vue';
import TeamStatItem from '@/components/team/teamStatItem.vue';
//资源引入

//样式引入
import '@/assets/styles/element-custom/el-button.css';



//数据
const teamInfo = reactive({
    teamName: "Rotten",
    teamIcon: "http://localhost:5193/uploads/avatars/1b1e306c-d7df-43de-b9cd-44dd9c903500.png",
    declaration: "没有网络安全就没有国家安全",
    teamIntroduction: "一支由网络安全爱好者组成的CTF战队，致力于在网络安全领域不断探索和突破。战队成员来自不同的技术背景，包括Web安全、逆向工程、密码学、二进制漏洞挖掘等多个方向的专业人才。",
    establishmentTime: "2022-07-01",
    teamPoints: 9841,
    teamEmail: "1958522865@qq.com",
    teamWebsite: "http://60.204.248.120/",
    country: "中国",
    city: "杭州",
    university: "浙江树人学院",
    memberCount: 4,

    teamLeader: "小R酱",
    userImage: "http://127.0.0.1:5193/uploads/avatars/cd4c8079-4e4d-40e4-95de-5d5d5b798709.png",
    teamLeader1: "Q",
    userImage1: "https://img1.baidu.com/it/u=2423265256,3468479084&fm=253&fmt=auto&app=138&f=JPEG?w=807&h=800",
    teamLeader2: "Kicky_Mu",
    userImage2: "http://127.0.0.1:5193/uploads/avatars/027ab4ed-d3a9-4586-8ffa-205d22090e4b.png",
})


//方法


//监听

</script>
<style scoped>
#teamName {
    display: flex;
    align-items: center;
    gap: 4px;
    font-size: 32px;
    font-weight: bold;
    margin-top: 10px;
}

.text-content {
    color: var(el-text-color-primary);
    line-height: 30px;
}

#teamDeclaration {
    color: var(--el-text-color-secondary);
    margin-top: 5px;
}

#teamIntroduction {
    font-size: 20px;
    margin-top: 20px;
}

.intro-text {
    font-size: 18px;
    line-height: 1.8;
    color: var(--el-text-color-primary);
    margin-top: 20px;
    padding: 0 40px;
    text-align: justify;
    word-break: break-word;
    font-family: 'PingFang SC', 'Microsoft YaHei', sans-serif;
    background: linear-gradient(135deg, transparent, rgba(255, 255, 255, 0.1));
    padding: 16px;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
}


.stats-container {
    display: flex;
    flex-wrap: wrap;
    gap: 16px;
    margin-top: 20px;
}
</style>
