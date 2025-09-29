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
            <TeamStatItem :item="{ label: $t('team.establishmentTime'), value: teamInfo.establishmentTime }" />
            <TeamStatItem :item="{ label: $t('team.member'), value: teamInfo.memberCount.toString() }" />
            <TeamStatItem :item="{ label: $t('team.points'), value: teamInfo.teamPoints.toString() }" />

            <TeamStatItem :item="{ label: $t('team.country'), value: teamInfo.country }" />
            <TeamStatItem :item="{ label: $t('team.city'), value: teamInfo.city }" />
            <TeamStatItem :item="{ label: $t('team.organization'), value: teamInfo.university }" />

            <TeamStatItem
                :item="{ label: $t('team.viceCaptain'), value: teamInfo.teamLeader1, avatar: teamInfo.userImage1 }" />
            <TeamStatItem
                :item="{ label: $t('team.captain'), value: teamInfo.teamLeader, avatar: teamInfo.userImage }" />
            <TeamStatItem
                :item="{ label: $t('team.viceCaptain'), value: teamInfo.teamLeader2, avatar: teamInfo.userImage2 }" />
        </div>

        <div class="center-box">
            <el-button type="warning" @click="copyToClipboard(teamInfo.teamWebsite, $t('team.website'))">{{
                $t('team.viewWebsite') }}</el-button>
            <el-button type="success" @click="copyToClipboard(teamInfo.teamEmail, $t('team.email'))">{{
                $t('team.viewEmail') }}</el-button>
            <el-button type="primary">{{ $t('team.editInfo') }}</el-button>
        </div>
    </div>
</template>

<script setup lang='ts'>
//官方引入
import { onMounted, reactive } from 'vue';

//插件引入
import { ElMessage } from 'element-plus';
import { useI18n } from 'vue-i18n';

//自定义引入
import SingleImageUpload from '@/components/base/singleImageUpload.vue';
import TeamStatItem from '@/components/team/teamStatItem.vue';
import apiClient from '@/api-services/apis';

//资源引入

//样式引入
import '@/assets/styles/element-custom/el-button.css';


//数据
interface Props {
    teamId?: string
}

// 定义组件属性
const props = withDefaults(defineProps<Props>(), {
    teamId: 'cbefe7e0-b167-4e13-be16-98f6ef08a8c3',
})


const teamInfo = reactive({
    teamName: "",
    teamIcon: "",
    declaration: "",
    teamIntroduction: "",
    establishmentTime: "",
    teamPoints: 0,
    teamEmail: "",
    teamWebsite: "",
    country: "",
    city: "",
    university: "",
    memberCount: 4,
    teamLeader: "",
    userImage: "",
    teamLeader1: "",
    userImage1: "",
    teamLeader2: "",
    userImage2: "",
})

//方法
const { t: $t } = useI18n()
// 定义默认团队信息键值（使用国际化键而不是翻译后的文本）
const defaultTeamInfoKeys = {
    teamName: 'team.noData',
    teamIcon: "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png",
    declaration: 'team.noDeclaration',
    teamIntroduction: 'team.noIntroduction',
    establishmentTime: 'team.notFilled',
    teamPoints: 0,
    teamEmail: "",
    teamWebsite: "",
    country: 'team.notFilled',
    city: 'team.notFilled',
    university: 'team.notFilled',
    memberCount: 0,
    teamLeader: 'team.pending',
    userImage: "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png",
    teamLeader1: 'team.pending',
    userImage1: "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png",
    teamLeader2: 'team.pending',
    userImage2: "https://cube.elemecdn.com/3/7c/3ea6beec64369c2642b92c6726f1epng.png"
};
/**
 * 辅助函数获取值，如果值为 null, undefined 或空字符串，则返回默认值
 * @param value 值
 * @param defaultValue 默认值
 * @returns 值或者默认值
 */
const getValueOrDefault = (value: any, defaultValueKey: any): any => {
    // 检查值是否为 null, undefined 或空字符串(根据需要可以调整)
    if (value === null || value === undefined || value === '') {
        // 如果默认值是字符串且存在于语言包中，则进行翻译
        if (typeof defaultValueKey === 'string' && defaultValueKey.startsWith('team.')) {
            return $t(defaultValueKey);
        }
        return defaultValueKey;
    }
    // 对于数字类型，额外检查是否为有效数字
    if (typeof defaultValueKey === 'number' && typeof value === 'string') {
        const numValue = Number(value);
        return isNaN(numValue) ? defaultValueKey : numValue;
    }
    return value;
};

/**
 * 复制文本到剪切板
 * @param text 要复制的文本
 * @param label 文本标签（用于提示）
 */
const copyToClipboard = async (text: string, label: string) => {
    try {
        // 检查文本是否存在
        if (!text || text.trim() === '') {
            ElMessage.warning($t('team.notConfigured', { label }));
            return;
        }

        // 使用 Clipboard API 复制文本
        await navigator.clipboard.writeText(text);
        ElMessage.success($t('team.copySuccess', { label, text }));
    } catch (error) {
        ElMessage.error($t('team.copyFailed'));
    }
};


/**
 * 获取团队信息
 */
const fetchTeamInfo = async () => {
    try {
        // 发送请求
        const response = await apiClient.get('/Team/GetTeamInfo', { params: { teamId: props.teamId } });

        // 处理成功响应
        if (response.data.isSuccess) {
            const result = response.data.result || {};

            // 使用辅助函数处理默认值
            teamInfo.teamName = getValueOrDefault(result.teamName, defaultTeamInfoKeys.teamName);
            teamInfo.teamIcon = getValueOrDefault(result.teamIcon, defaultTeamInfoKeys.teamIcon);
            teamInfo.declaration = getValueOrDefault(result.declaration, defaultTeamInfoKeys.declaration);
            teamInfo.teamIntroduction = getValueOrDefault(result.teamIntroduction, defaultTeamInfoKeys.teamIntroduction);
            teamInfo.establishmentTime = getValueOrDefault(result.establishmentTime, defaultTeamInfoKeys.establishmentTime);
            teamInfo.teamPoints = getValueOrDefault(result.teamPoints, defaultTeamInfoKeys.teamPoints);
            teamInfo.teamEmail = getValueOrDefault(result.teamEmail, defaultTeamInfoKeys.teamEmail);
            teamInfo.teamWebsite = getValueOrDefault(result.teamWebsite, defaultTeamInfoKeys.teamWebsite);
            teamInfo.country = getValueOrDefault(result.country, defaultTeamInfoKeys.country);
            teamInfo.city = getValueOrDefault(result.city, defaultTeamInfoKeys.city);
            teamInfo.university = getValueOrDefault(result.university, defaultTeamInfoKeys.university);
            teamInfo.memberCount = getValueOrDefault(result.memberCount, defaultTeamInfoKeys.memberCount);
            teamInfo.teamLeader = getValueOrDefault(result.teamLeader, defaultTeamInfoKeys.teamLeader);
            teamInfo.userImage = getValueOrDefault(result.userImage, defaultTeamInfoKeys.userImage);
            teamInfo.teamLeader1 = getValueOrDefault(result.teamLeader1, defaultTeamInfoKeys.teamLeader1);
            teamInfo.userImage1 = getValueOrDefault(result.userImage1, defaultTeamInfoKeys.userImage1);
            teamInfo.teamLeader2 = getValueOrDefault(result.teamLeader2, defaultTeamInfoKeys.teamLeader2);
            teamInfo.userImage2 = getValueOrDefault(result.userImage2, defaultTeamInfoKeys.userImage2);
        } else {
            ElMessage.error($t('team.getTeamInfoError'));
            // 请求失败时使用默认值
            resetToDefaultValues();
        }
    }
    catch (error: any) {
        if (error.response?.status === 401) {
            ElMessage.error($t('user.tokenExpired'));
        } else {
            ElMessage.error($t('team.getTeamInfoError'));
        }
        // 出错时使用默认值
        resetToDefaultValues();
    }
}

/**
 * 设置为默认值
 */
const resetToDefaultValues = () => {
    teamInfo.teamName = $t(defaultTeamInfoKeys.teamName);
    teamInfo.teamIcon = defaultTeamInfoKeys.teamIcon;
    teamInfo.declaration = $t(defaultTeamInfoKeys.declaration);
    teamInfo.teamIntroduction = $t(defaultTeamInfoKeys.teamIntroduction);
    teamInfo.establishmentTime = $t(defaultTeamInfoKeys.establishmentTime);
    teamInfo.teamPoints = defaultTeamInfoKeys.teamPoints;
    teamInfo.teamEmail = defaultTeamInfoKeys.teamEmail;
    teamInfo.teamWebsite = defaultTeamInfoKeys.teamWebsite;
    teamInfo.country = $t(defaultTeamInfoKeys.country);
    teamInfo.city = $t(defaultTeamInfoKeys.city);
    teamInfo.university = $t(defaultTeamInfoKeys.university);
    teamInfo.memberCount = defaultTeamInfoKeys.memberCount;
    teamInfo.teamLeader = $t(defaultTeamInfoKeys.teamLeader);
    teamInfo.userImage = defaultTeamInfoKeys.userImage;
    teamInfo.teamLeader1 = $t(defaultTeamInfoKeys.teamLeader1);
    teamInfo.userImage1 = defaultTeamInfoKeys.userImage1;
    teamInfo.teamLeader2 = $t(defaultTeamInfoKeys.teamLeader2);
    teamInfo.userImage2 = defaultTeamInfoKeys.userImage2;
}

//监听
onMounted(() => {
    //获取团队信息
    fetchTeamInfo()
})

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