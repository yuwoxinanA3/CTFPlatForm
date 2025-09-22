<template>
    <div style="padding:0; " class="card-box">

        <el-container>
            <el-aside width="5%"></el-aside>
            <el-main>
                <el-form ref="ruleFormRef"
                    style="max-width: 100%;background-color: var(--el-bg-color);padding: 40px 17px;border-radius: 10px;"
                    :model="ruleForm" status-icon :rules="rules" label-width="auto" class="demo-ruleForm">
                    <h1 style="text-align: center;width: 100%;">{{ t('team.createTeam') }}</h1>
                    <!-- 战队队标上传 -->
                    <el-form-item>
                        <SingleImageUpload v-model:value="ruleForm.teamLogo" @success="handleAvatarSuccess" />
                    </el-form-item>

                    <!-- 战队名称验证 -->
                    <el-form-item :label="$t('team.teamName')" prop="teamName">
                        <el-input v-model="ruleForm.teamName" style="max-width:100%"
                            :placeholder="$t('team.inputTeamName')">
                            <template #append>
                                <el-button type="primary" @click="checkTeamNameAvailability(ruleForm.teamName)"
                                    :loading="isChecking"
                                    :disabled="!ruleForm.teamName || ruleForm.teamName.trim() === ''">
                                    {{ $t('team.check') }}
                                </el-button>
                            </template>
                        </el-input>
                        <!-- 添加状态提示 -->
                        <div v-if="isAvailable !== null && lastCheckedName === ruleForm.teamName"
                            style="margin-top: 5px;">
                            <el-text v-if="isAvailable" type="success">✓ {{ $t('team.okName') }}</el-text>
                            <el-text v-else type="danger">✗ {{ $t('team.falseName') }}</el-text>
                        </div>
                    </el-form-item>


                    <el-form-item :label="$t('team.country')" prop="country">
                        <el-input v-model="ruleForm.country" style="max-width:100%"
                            :placeholder="$t('team.inputCountry')">
                        </el-input>
                    </el-form-item>


                    <el-form-item :label="$t('team.city')" prop="city">
                        <el-input v-model="ruleForm.city" style="max-width:100%" :placeholder="$t('team.inputCity')">
                        </el-input>
                    </el-form-item>

                    <el-form-item :label="$t('team.teamWebsite')" prop="teamWebsite">
                        <el-input v-model="ruleForm.teamWebsite" style="max-width:100%">
                            <template #prepend>Http://</template>
                        </el-input>
                    </el-form-item>

                    <el-form-item :label="$t('team.teamEmail')" prop="email">
                        <el-input v-model="ruleForm.email" style="max-width:100%"
                            :placeholder="$t('team.inputTeamEmail')">
                        </el-input>
                    </el-form-item>

                    <el-form-item :label="$t('team.establishmentTime')" prop="establishmentTime">
                        <el-date-picker v-model="ruleForm.establishmentTime" type="date"
                            :placeholder="$t('team.prickDay')" style="width:100%" />
                    </el-form-item>


                    <el-form-item :label="$t('team.teamIntroduction')" prop="teamIntroduction">
                        <el-input v-model="ruleForm.teamIntroduction" style="max-width:100%" maxlength="300"
                            :placeholder="$t('team.inputTeamIntroduction')" show-word-limit type="textarea" />
                    </el-form-item>

                    <el-form-item :label="$t('team.declaration')" prop="declaration">
                        <el-input v-model="ruleForm.declaration" style="max-width:100%" maxlength="100"
                            :placeholder="$t('team.declaration')" show-word-limit type="textarea" />
                    </el-form-item>


                    <el-form-item :label="$t('team.isPublic')" prop="isPublic">
                        <el-switch v-model="ruleForm.isPublic" class="mt-2" inline-prompt :active-icon="Check"
                            :inactive-icon="Close" />
                    </el-form-item>


                    <el-form-item>
                        <div style="display: flex;justify-content: center;width: 100%;">
                            <el-button type="primary" @click="submitForm(ruleFormRef)" style="width: 70%;"
                                :loading="false"
                                :disabled="isAvailable === false || !ruleForm.teamName || ruleForm.teamName.trim() === ''">
                                {{ $t('team.createTeam') }}
                            </el-button>
                        </div>
                    </el-form-item>


                </el-form>
            </el-main>
            <el-aside width="40%"></el-aside>
        </el-container>
    </div>
</template>

<script lang="ts" setup>
//官方引入
import { reactive, ref, watch } from 'vue'
//插件引入
import axios from 'axios'
import { ElMessage, type FormInstance, type FormRules } from 'element-plus'
import { Check, Close } from '@element-plus/icons-vue'
import { useI18n } from 'vue-i18n'
//自定义引入
import type { text_req } from '@/api-services/models/base/text_req'
import SingleImageUpload from '@/components/base/singleImageUpload.vue'
import type { CreateTeamReq } from '@/api-services/models/team/createTeamReq'
import apiClient from '@/api-services/apis'
//资源引入

//样式引入
import '@/assets/styles/element-custom/el-date-picker.css';
import '@/assets/styles/element-custom/el-input.css';

//数据
const ruleFormRef = ref<FormInstance>()

const ruleForm = reactive({
    teamName: '',
    teamLogo: '',
    country: '',
    city: '',
    teamWebsite: '',
    email: '',
    establishmentTime: '',
    teamIntroduction: '',
    declaration: '',
    isPublic: false
})

//状态变量
const isChecking = ref(false) // 正在检查中
const isAvailable = ref<boolean | null>(null) // null: 未检查, true: 可用, false: 不可用
const lastCheckedName = ref('') // 上次检查的名称
const { t } = useI18n()
//表单规则
const rules = reactive<FormRules<typeof ruleForm>>({
    teamName: [
        { required: true, message: t('team.inputTeamName'), trigger: 'blur' },
        { min: 1, max: 20, message: t('team.lengthLimit'), trigger: 'blur' },
        {
            validator: (_rule, value, callback) => {
                if (value && isAvailable.value === false && lastCheckedName.value === value) {
                    callback(new Error(t('team.falseName')))
                } else {
                    callback()
                }
            },
            trigger: 'blur'
        }
    ]
})

//方法
// 检查战队名称是否可用的函数
const checkTeamNameAvailability = async (name: string) => {
    // 允许单字名称，只需非空检查
    if (!name || name.trim() === '') return

    isChecking.value = true
    lastCheckedName.value = name

    try {
        // 构造符合后端 TextReq 格式的请求体
        const teamName: text_req = {
            TextContent: name
        };
        // 发送验证请求
        const response = await axios.post('/api/Team/IsAvailableTeamName', teamName);

        if (response.data) {
            isAvailable.value = true
            ruleFormRef.value?.validateField('teamName')
        }
        else
            isAvailable.value = false

    } catch (error) {
        isAvailable.value = false
        ElMessage.error(t('team.checkNameError') + error)
    } finally {
        isChecking.value = false
    }
}

// 头像上传成功处理函数
const handleAvatarSuccess = (url: string) => {
    ruleForm.teamLogo = url;
}

const submitForm = async (formEl: FormInstance | undefined) => {
    if (!formEl) return

    // 先触发表单验证
    const valid = await formEl.validate().catch(() => false)

    if (valid) {
        // 检查是否已经验证过名称且名称可用
        if (isAvailable.value === true && lastCheckedName.value === ruleForm.teamName) {
            await createTeam();
        } else {
            // 如果还没有检查名称或名称已更改，先检查名称
            await checkTeamNameAvailability(ruleForm.teamName)

            // 再次检查验证结果
            if (isAvailable.value === true) {
                await createTeam();
            } else {
                ElMessage.error(t('team.fasleName'));
            }
        }
    } else {
        ElMessage.error(t('team.falseCommit'));
    }
}


/**
 * 创建战队
 */
const createTeam = async () => {
    try {
        // 构造符合后端  格式的请求体
        const teamData: CreateTeamReq = {
            teamName: ruleForm.teamName,
            teamIcon: ruleForm.teamLogo,
            declaration: ruleForm.declaration,
            teamIntroduction: ruleForm.teamIntroduction,
            establishmentTime: new Date(ruleForm.establishmentTime),
            teamEmail: ruleForm.email,
            teamWebsite: ruleForm.teamWebsite,
            country: ruleForm.country,
            city: ruleForm.city,
            isPublic: ruleForm.isPublic
        };
        // 发送登录请求
        const response = await apiClient.post('/Team/CreateCTFTeam', teamData);
        if (response.data) {
            ElMessage.success(t('team.createTeamSuccess'));
            // 登录成功后跳转或处理 token 等操作

        }
        else
            ElMessage.error(t('team.createTeamFail'));

    }
    catch (error) {
        ElMessage.error(t('team.createTeamError') + error);
    }
}




//监听

// 监听战队名称变化，重置验证状态
watch(() => ruleForm.teamName, (newName, _oldName) => {
    // 支持单字，只在名称真正改变时重置状态
    if (newName !== lastCheckedName.value) {
        isAvailable.value = null
    }
})


</script>




<style scoped></style>