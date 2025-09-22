<template>
    <div class="card-box">
        <el-form ref="ruleFormRef" style="max-width: 600px" :model="ruleForm" status-icon :rules="rules"
            label-width="auto" class="demo-ruleForm">
            <el-form-item :label="$t('user.oldPwd')" prop="oldPwd">
                <el-input v-model="ruleForm.oldPwd" type="password" autocomplete="off" />
            </el-form-item>
            <el-form-item :label="$t('user.newPwd')" prop="newPwd">
                <el-input v-model="ruleForm.newPwd" type="password" autocomplete="off" />
            </el-form-item>
            <el-form-item :label="$t('user.newPwdAgain')" prop="newPwdAgain">
                <el-input v-model="ruleForm.newPwdAgain" type="password" autocomplete="off" />
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitForm(ruleFormRef)">
                    {{ $t('user.submitForm') }}
                </el-button>
                <el-button @click="resetForm(ruleFormRef)">{{ $t('user.restForm') }}</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script setup lang='ts'>
//官方引入
import { reactive, ref } from 'vue';

//插件引入
import type { FormInstance, FormRules } from 'element-plus';
//自定义引入
import { ElMessage } from 'element-plus';
import apiClient from '@/api-services/apis';
import type { changePwd_req } from '@/api-services/models/user/changePwdReq';
import { useI18n } from 'vue-i18n';

//资源引入

//数据
const ruleFormRef = ref<FormInstance>()
const ruleForm = reactive({
    oldPwd: '',
    newPwd: '',
    newPwdAgain: '',
})
const { t } = useI18n()

// 验证旧密码
const validateOldPass = (_rule: any, value: any, callback: any) => {
    if (value === '') {
        callback(new Error(t('user.oldPwd') + t('login.inputPassword')))
    } else {
        callback()
    }
}

// 验证新密码
const validateNewPass = (_rule: any, value: any, callback: any) => {
    if (value === '') {
        callback(new Error(t('user.newPwd') + t('login.inputPassword')))
    } else {
        // 可以添加密码强度验证
        if (ruleForm.newPwdAgain !== '') {
            if (!ruleFormRef.value) return
            ruleFormRef.value.validateField('newPwdAgain')
        }
        callback()
    }
}

// 验证确认密码
const validateNewPassAgain = (_rule: any, value: any, callback: any) => {
    if (value === '') {
        callback(new Error(t('user.newPwdAgain') + t('login.inputPasswordAgain')))
    } else if (value !== ruleForm.newPwd) {
        callback(new Error(t('user.newPwdAgain') + t('login.inputPasswordAgain')))
    } else {
        callback()
    }
}

const rules = reactive<FormRules<typeof ruleForm>>({
    oldPwd: [{ validator: validateOldPass, trigger: 'blur' }],
    newPwd: [{ validator: validateNewPass, trigger: 'blur' }],
    newPwdAgain: [{ validator: validateNewPassAgain, trigger: 'blur' }]
})

//方法
const submitForm = (formEl: FormInstance | undefined) => {
    if (!formEl) return
    formEl.validate(async (valid) => {
        if (valid) {

            // 构造符合后端  格式的请求体
            const reqData: changePwd_req = {
                newPassWord: ruleForm.newPwd,
                oldPassWord: ruleForm.oldPwd
            };
            // 发送请求
            const response = await apiClient.post('/User/ChangePwdByOldPwd', reqData);
            if (response.data.result) {
                ElMessage.success(t('user.changePwdSuccess'))
            }
            else if (response.data.msg && response.data.msg == "oldPassword Error!") {
                ElMessage.error(t('user.oldPasswordError'));
            }
            else if (response.data.msg && response.data.msg == "same password!") {
                ElMessage.error(t('user.samePasswordError'));
            }
            else
                ElMessage.error(t('user.changePwdFailed'));

        } else {
            ElMessage.error(t('user.formValidationError'))
        }
    })
}

const resetForm = (formEl: FormInstance | undefined) => {
    if (!formEl) return
    formEl.resetFields()
}

//监听

</script>

<style scoped>
.card-box {
    padding: 20px;
    border-radius: 4px;
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
}
</style>