<template>
    <!-- 登录面板区 -->
    <div id="login_box_content">
        <div id="login_box_content_box">
            <div style="width: 75%;">
                <el-tabs v-model="activeName" class="demo-tabs" :stretch="true" style="height: 340px;">
                    <!-- 账号密码登录 -->
                    <el-tab-pane :label="$t('login.account_login')" name="first">
                        <el-form ref="formRef" :model="form" label-width="auto">

                            <el-form-item :label="$t('login.account')" prop="username" label-position="right"
                                class="el_form_item_login">
                                <el-input v-model="form.username" :placeholder="$t('login.inputAccount')"></el-input>
                            </el-form-item>

                            <el-form-item :label="$t('login.password')" prop="password" label-position="right"
                                class="el_form_item_login">
                                <el-input type="password" v-model="form.password"
                                    :placeholder="$t('login.inputPassword')"></el-input>
                            </el-form-item>

                            <el-form-item class="el_form_item_login">
                                <div style="display: flex;justify-content: space-between;width: 100%;">
                                    <div>
                                        <el-checkbox v-model:checked="form.isSavePassword" @change="changeSaveModel">{{
                                            $t('login.saveModel') }}</el-checkbox>
                                    </div>
                                    <div>
                                        <span id="register" @click="handleRegisterClick">{{ $t('login.register')
                                            }}</span>

                                        <span id="forgetPassword" @click="forgetPassword">{{
                                            $t('login.forgetPassword') }}</span>

                                    </div>
                                </div>

                            </el-form-item>

                        </el-form>
                        <!-- 登录按钮功能区 -->
                        <el-form-item class="el_form_item_login">
                            <div style="line-height: 22px;margin:0 0 8px 0;color: #9b9b9b;">
                                <span style="vertical-align:middle">{{ $t('login.thridWayLogin') }}</span>
                                <img :src="qqIcon" width="22" height="22" style="vertical-align:middle;margin-left: 8px"
                                    @click="qqLogin" title="QQ">
                                <img :src="WeChatIcon" width="22" height="22"
                                    style="vertical-align:middle;margin-left: 8px" @click="WeChatLogin" title="WeChat">
                                <img :src="zhifubaoIcon" width="22" height="22"
                                    style="vertical-align:middle;margin-left: 8px" @click="payLogin" title="支付宝">
                                <img :src="giteeIcon" width="22" height="22"
                                    style="vertical-align:middle;margin-left: 8px" @click="giteeLogin" title="Gitee">
                                <img :src="githubIcon" width="22" height="22"
                                    style="vertical-align:middle;margin-left: 8px" @click="githubLogin" title="GitHub">
                            </div>

                            <div id="login_button_box">
                                <el-button id="login_button" type="primary" @click="submitForm">{{
                                    $t('login.submit')
                                    }}</el-button>
                                <el-button id="reset_button" type="info" @click="resetForm" plain>{{
                                    $t('login.reset') }}</el-button>
                            </div>
                        </el-form-item>

                    </el-tab-pane>

                    <!-- 手机号登录 -->
                    <el-tab-pane :label="$t('login.phoneNumberLogin')" name="second">


                        <el-form ref="phoneFormRef" :model="phoneForm" label-width="auto">

                            <!-- 手机号码 -->
                            <el-form-item :label="$t('login.phoneNumber')" prop="phoneNumber" label-position="right"
                                class="el_form_item_login">
                                <el-input v-model="phoneForm.phoneNumber"
                                    :placeholder="$t('login.inputPhoneNumber')"></el-input>
                            </el-form-item>

                            <!-- 验证码 -->
                            <el-form-item :label="$t('login.code')" prop="verificationCode" label-position="right"
                                class="el_form_item_login">
                                <div style="display: flex; align-items: center;">
                                    <el-input v-model="phoneForm.verificationCode"
                                        :placeholder="$t('login.inputCode')"></el-input>
                                    <el-button type="primary" @click="getVerificationCode" style="margin-left: 10px;">
                                        {{ codeButtonText }}
                                    </el-button>
                                </div>
                            </el-form-item>

                            <!-- 操作按钮 -->
                            <el-form-item class="el_form_item_login">
                                <div id="login_button_box">
                                    <el-button id="login_button" type="primary" @click="submitPhoneForm">{{
                                        $t('login.submit') }}</el-button>
                                    <el-button id="reset_button" type="info" plain>{{ $t('login.reset')
                                        }}</el-button>
                                </div>
                            </el-form-item>

                        </el-form>

                    </el-tab-pane>

                </el-tabs>

            </div>

        </div>

    </div>
</template>

<script setup lang="ts">
//官方引入
import { ref, nextTick, onMounted } from 'vue'
//插件引入
import axios from 'axios';
import { ElMessage, type ElForm } from 'element-plus';
import router from '@/router';
import { useI18n } from 'vue-i18n'
//自定义引入
import { type login_req } from '@/api-services/models/user/login_req';

//资源引入
import qqIcon from '@/assets/icons/qq.png'
import WeChatIcon from '@/assets/icons/WeChat.png'
import zhifubaoIcon from '@/assets/icons/alipay.png'
import giteeIcon from '@/assets/icons/gitee.png'
import githubIcon from '@/assets/icons/github.png'
import { useAuthStore } from '@/store/authStore';
import { deleteCookie, getCookie, setCookie } from '@/utils/cookieUtils';
import { decrypt, encrypt } from '@/utils/cryptoUtils';
import emitter from '@/utils/eventBus';
import apiClient from '@/api-services/apis';

//数据
const activeName = ref('first')
//获取翻译文本
const { t } = useI18n()

//尝试从cookie中读取保存的账号密码
const savedEncryptedUser = getCookie('savedUsername');
const savedEncryptedPass = getCookie('savedPassword');
const savedUsername = savedEncryptedUser ? decrypt(savedEncryptedUser) : null;
const savedPassword = savedEncryptedPass ? decrypt(savedEncryptedPass) : null;


// form 
const form = ref({
    username: savedUsername || '',
    password: savedPassword || '',
    isSavePassword: !!(savedUsername && savedPassword)
});

const formRef = ref<InstanceType<typeof ElForm> | null>(null);


const phoneFormRef = ref<InstanceType<typeof ElForm> | null>(null);
const phoneForm = ref({
    phoneNumber: '',
    verificationCode: ''
});
const codeButtonText = ref(t('login.getCode'));

//方法

/**
 * 切换保存密码模式
 */
function changeSaveModel() {
    form.value.isSavePassword = !form.value.isSavePassword;
}

/**
 * 重置表单
 */
const resetForm = () => {
    formRef.value?.resetFields();
    form.value.password = '';
    form.value.isSavePassword = false;
    //清除缓存
    deleteCookie('savedUsername');
    deleteCookie('savedPassword');
};

/**
 * 账号密码登录
 */
const submitForm = async () => {
    if (!formRef.value) return;

    try {
        const valid = await formRef.value.validate();
        if (valid) {
            // 构造符合后端 LoginReq 格式的请求体
            const loginData: login_req = {
                UserAccount: form.value.username,
                PassWord: form.value.password
            };
            // 发送登录请求
            const response = await apiClient.post('/Login/Login', loginData);
            const token = response.data.token;
            // 存储 token 到 pinia
            const authStore = useAuthStore();
            authStore.setToken(token);

            //本地是否保存账号密码，加密后存放在cookie
            if (form.value.isSavePassword) {
                const encryptedUser = encrypt(form.value.username);
                const encryptedPass = encrypt(form.value.password);

                setCookie('savedUsername', encryptedUser, 7); // 保存7天
                setCookie('savedPassword', encryptedPass, 7);
            } else {
                deleteCookie('savedUsername');
                deleteCookie('savedPassword');
            }
          // 登录成功后将背景色改为白色
            document.body.style.backgroundColor = '#ebeef5';
            // 登录成功后跳转到主页
            router.push('/home')
        }
    } catch (error) {
        if (axios.isAxiosError(error)) {
            // 请求发出但收到错误响应
            if (error.response) {
                switch (error.response.status) {
                    case 401:
                        // 401 Unauthorized - 用户名或密码错误
                        ElMessage.error(t('login.invalidCredentials'));
                        break;
                    case 500:
                        // 500 Internal Server Error - 服务器内部错误
                        ElMessage.error(t('error.serverError'));
                        break;
                    case 400:
                        // 400 Bad Request - 请求参数错误
                        ElMessage.error(t('error.badRequest'));
                        break;
                    default:
                        ElMessage.error(`${t('error.unknownError')}: ${error.response.status}`);
                }
            } 
            // 请求发出但没有收到响应（网络问题）
            else if (error.request) {
                ElMessage.error(t('error.networkError'));
            } 
            // 其他错误
            else {
                ElMessage.error(t('error.unknownError'));
            }
        } else {
            // 非 Axios 错误
            ElMessage.error(t('error.unknownError'));
        }
    }
};


/**
 * 忘记密码
 */
function forgetPassword() {

}

const emit = defineEmits(['switch-to-register'])
/**
 * 注册
 */
const handleRegisterClick = () => {
    // 触发事件通知父组件切换到注册页面
    emit('switch-to-register')
};


function getVerificationCode() {
    // 模拟发送验证码
    codeButtonText.value = t("login.hasSendCode");
    setTimeout(() => {
        codeButtonText.value = t("login.getCode");
    }, 5000);
}

async function submitPhoneForm() {

    ElMessage('手机登录研发中！');
    // if (!phoneFormRef.value) return;

    // try {
    //     const valid = await phoneFormRef.value.validate();
    //     if (valid) {
    //         router.push('/home');
    //         console.log('手机号登录成功', phoneForm.value);
    //         ElMessage.success('登录成功');
    //     }
    // } catch (error) {
    //     ElMessage.error('登录失败，请检查信息');
    // }



}


/**
 * QQ登录
 */
function qqLogin() {
    ElMessage('QQ登录功能待开发')
}
/**
 * 微信登录
 */
function WeChatLogin() {
    ElMessage('微信登录功能待开发')
}

/**
 * 支付宝登录
 */
function payLogin() {
    ElMessage('支付宝登录功能待开发')
}

/**
 * Gitee登录
 */
function giteeLogin() {
    ElMessage('Gitee登录功能待开发')
}
/**
 * GitHub登录
 */
function githubLogin() {
    ElMessage('GitHub登录功能待开发')
}

//监听
onMounted(() => {
    // 延迟执行 emit，确保 login.vue 已完成监听注册
    nextTick(() => {
        emitter.emit('switch-to-register');
    });
});

</script>


<style scoped>
#login_box_content {
    /* background-color: red; */
    margin-top: 50px;
}

#login_box_content_box {
    /* background-color: rgb(13, 255, 0); */
    display: flex;
    justify-content: center;
}

.demo-tabs>.el-tabs__content {
    padding: 32px;
    color: #6b778c;
    font-size: 32px;
    font-weight: 600;
}


.el_form_item_login {
    margin-top: 30px;
}


#register {
    color: #409EFF;
    /* 使用 Element Plus 主色调 */
    cursor: pointer;
    text-decoration: none;
}

#register:hover {
    color: #337ecc;
    /* 悬停时变深色 */
    text-decoration: underline;
    /* 添加下划线 */
}

#forgetPassword {
    cursor: pointer;
}


#third_party_login .el-button {
    margin: 0 10px;
}

#login_button_box {
    width: 100%;
    display: flex;
    justify-content: space-between;
    padding: 0 30px;
}

#login_button {
    width: 30%;
}

#reset_button {
    width: 30%;
}
</style>