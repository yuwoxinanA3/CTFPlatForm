<template>
    <div id="register_form">
        <div style="width: 80%;">
            <el-tabs v-model="activeName" class="demo-tabs" :stretch="true" style="height: 340px;">
                <el-tab-pane :label="$t('login.register')" name="first">
                    <el-form ref="registerFormRef" :model="registerForm" label-width="auto" style="margin-top: 30px;">
                        <!-- 用户名 -->
                        <el-form-item :label="$t('login.account')" prop="userAccount">
                            <el-input v-model="registerForm.userAccount"
                                :placeholder="$t('login.inputAccount')"></el-input>
                        </el-form-item>

                        <!-- 密码 -->
                        <el-form-item :label="$t('login.password')" prop="password">
                            <el-input type="password" v-model="registerForm.password"
                                :placeholder="$t('login.inputPassword')"></el-input>
                        </el-form-item>

                        <!-- 确认密码 -->
                        <el-form-item :label="$t('login.surePassword')" prop="confirmPassword">
                            <el-input type="password" v-model="registerForm.confirmPassword"
                                :placeholder="$t('login.inputPasswordAgain')"></el-input>
                        </el-form-item>

                        <!-- 注册按钮 -->
                        <el-form-item>
                            <div style="width: 100%;display: flex;justify-content: space-between;padding: 0 30px;">
                                <el-button type="primary" @click="submitRegister">{{ $t('login.register') }}</el-button>
                                <el-button @click="resetRegister">{{ $t('login.reset') }}</el-button>
                                <el-button @click="backToLogin">{{ $t('login.backLogin') }}</el-button>
                            </div>
                        </el-form-item>
                    </el-form>
                </el-tab-pane>
            </el-tabs>
        </div>

    </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { ElMessage } from 'element-plus';
import type { ElForm } from 'element-plus/lib';
import type { register_req } from '@/api-services/models/user/register_req';
import axios from 'axios';

const activeName = ref('first')
const emit = defineEmits(['switch-to-login'])

const registerForm = ref({
    userAccount: '',
    password: '',
    confirmPassword: ''
});
const registerFormRef = ref<InstanceType<typeof ElForm> | null>(null);

const submitRegister = async () => {
    if (registerForm.value.password !== registerForm.value.confirmPassword) {
        ElMessage.error('两次输入的密码不一致');
        return;
    }

    if (!registerFormRef.value) return;

    try {
        const valid = await registerFormRef.value.validate();
        if (valid) {
            // 构造符合后端 LoginReq 格式的请求体
            const registerData: register_req = {
                UserAccount: registerForm.value.userAccount,
                PassWord: registerForm.value.password
            };
            // 发送登录请求
            const response = await axios.post('/api/User/RegisterUser', registerData);

            if (response.data.result == true) {
                ElMessage.success('注册成功');
                //切换回登录
                emit('switch-to-login')
            }
            else
                ElMessage.error(response.data.msg);
        }
    } catch (error) {
        ElMessage.error('注册失败！');
    }

};
const backToLogin = () => {
    emit('switch-to-login')
}

const resetRegister = () => {
    registerForm.value.userAccount = '';
    registerForm.value.password = '';
    registerForm.value.confirmPassword = '';
};
</script>

<style scoped>
.demo-tabs>.el-tabs__content {
    padding: 32px;
    color: #6b778c;
    font-size: 32px;
    font-weight: 600;
}

#register_form {
    display: flex;
    justify-content: center;
    margin-top: 50px;
}
</style>