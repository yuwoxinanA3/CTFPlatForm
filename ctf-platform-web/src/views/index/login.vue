<template>
    <div style="display: flex;justify-content: center;">
        <!-- 左侧巨石阵显示加盟战队 -->
        <div id="abiltily_wrapper_box" style="width: 75%;">
            <div class="abiltily_wrapper">
                <div class="abiltily_inner" style="--quantity: 10;">
                    <div class="abiltily_card" style="--index: 0;--color-card: 142, 249, 252;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png' /></div>
                    </div>
                    <div class="abiltily_card" style="--index: 1;--color-card: 142, 252, 204;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 2;--color-card: 142, 252, 157;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 3;--color-card: 215, 252, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 4;--color-card: 252, 252, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 5;--color-card: 252, 208, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 6;--color-card: 252, 142, 142;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 7;--color-card: 252, 142, 239;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 8;--color-card: 204, 142, 252;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                    <div class="abiltily_card" style="--index: 9;--color-card: 142, 202, 252;">
                        <div class="abiltily_img"><img src='@/assets/images/主页贴图.png'></div>
                    </div>
                </div>
            </div>
        </div>


        <!-- 登录组件框 -->
        <div style="width: 25%; background-color: white;">

            <div id="login_box">
                <!-- 登录面板logo区 -->
                <div id="login_box_title">
                    <div id="login_box_title_logo_box">

                        <div id="login_box_title_logo_box_padding">
                            <div style="">
                                <el-image style="width: 100px; height: 100px;" :src="logo" />
                            </div>

                            <div style="padding-left: 30px;">
                                <div style="line-height:100px;font-size: 90px;font-weight: 600;">Rotten</div>
                            </div>
                        </div>
                    </div>
                    <div id="login_box_title_text">
                        {{ $t('platform_name') }}
                    </div>
                </div>
                <div v-if="showLoginForm">
                    <loginForm @switch-to-register="showLoginForm = false" />
                </div>
                <div v-else>
                    <registerForm @switch-to-login="showLoginForm = true" />
                </div>

            </div>


            <languageChange style="margin-top: 300px;"></languageChange>
        </div>


    </div>

</template>


<script setup lang="ts">
//官方引入
import { onMounted, onUnmounted, ref } from 'vue';
//插件引入

//自定义引入
import loginForm from '@/components/login/loginForm.vue'
import languageChange from '@/components/base/languageChange.vue';
import registerForm from '@/components/login/registerForm.vue';
//资源引入
import logo from '@/assets/icons/logo.png'

// 控制显示登录或注册表单
const showLoginForm = ref(true)
// 组件挂载时
onMounted(() => {
    // 强制设置为亮色主题并指定黑色背景
    const app = document.getElementById('app')
    if (app) {
        app.classList.remove('dark-theme')
        app.style.backgroundColor = '#1d1c1c'
    }

    // 设置 html 和 body 元素背景色确保一致性
    document.documentElement.style.backgroundColor = '#1d1c1c'
    document.body.style.backgroundColor = '#1d1c1c'
    // 设置 html 和 body 元素背景色确保一致性
    document.documentElement.style.backgroundColor = '#1d1c1c'
    document.body.style.backgroundColor = '#1d1c1c'
})

// 组件卸载时恢复背景色
onUnmounted(() => {
    // 移除固定的背景色设置，让主题系统接管
    const app = document.getElementById('app')
    if (app) {
        app.style.backgroundColor = ''
    }

    // 清除 html 和 body 的固定背景色
    document.documentElement.style.backgroundColor = ''
    document.body.style.backgroundColor = ''
})
</script>


<style scoped>
/* 功能巨石阵样式 */
#abiltily_wrapper_box {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
    overflow: hidden;
}

.abiltily_wrapper {
    width: 100%;
    height: 130%;
    position: relative;
    text-align: center;
    display: flex;
    align-items: center;
    justify-content: center;
    overflow: hidden;
}

.abiltily_inner {
    --w: 200px;
    --h: 300px;
    --translateZ: calc((var(--w) + var(--h)) + 0px);
    --rotateX: -15deg;
    /*仰角*/
    --perspective: 1000px;
    position: absolute;
    width: var(--w);
    height: var(--h);
    top: 25%;
    left: calc(50% - (var(--w) / 2) - 2.5px);
    z-index: 2;
    transform-style: preserve-3d;
    transform: perspective(var(--perspective));
    animation: abiltily_rotating 20s linear infinite;
}

@keyframes abiltily_rotating {
    from {
        transform: perspective(var(--perspective)) rotateX(var(--rotateX))rotateY(0);
    }

    to {
        transform: perspective(var(--perspective)) rotateX(var(--rotateX))rotateY(1turn);
    }
}

.abiltily_card {
    position: absolute;
    -webkit-box-reflect: below 10px linear-gradient(transparent, rgba(0, 0, 0, 0.5));
    border-radius: 12px;
    overflow: hidden;
    inset: 0;
    transform: rotateY(calc((360deg / var(--quantity)) * var(--index)))translateZ(var(--translateZ));
}

.abiltily_img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transform: rotateY(-1);
}

img {
    width: 100%;
}



/* logo区 */

#login_box {
    text-align: center;
}

#login_box_title_logo_box {
    display: flex;
    justify-content: center;
}

#login_box_title_logo_box_padding {
    width: 80%;
    display: flex;
    justify-content: center;
}

#login_box_title_text {
    margin-top: 20px;
    font-size: xx-large;
    font-weight: 400;
    color: rgb(120, 120, 117);
}
</style>