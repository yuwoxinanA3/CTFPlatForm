<template>
    <!-- 语言选择器 -->
    <div id="language-box">
        <el-radio-group v-model="selectedLocale" @change="changeLanguage">
            <el-radio label="zh">中文</el-radio>
            <el-radio label="en">English</el-radio>
        </el-radio-group>
    </div>

</template>


<script setup lang="ts">
//官方引入
import { ref, watch } from 'vue';

//插件引入

// 图标引入

//自定义引入
import i18n from '@/utils/i18n';// 引入 i18n 实例


//数据

// 语言选择相关逻辑
const availableLocales = ['zh', 'en'];
const selectedLocale = ref(i18n.global.locale.value); // 初始值为当前 locale

//方法
/**
 * 语言切换方法
 */
function changeLanguage(locale: string) {
    if (availableLocales.includes(locale)) {
        i18n.global.locale.value = locale as 'zh' | 'en';
        // 将语言选择保存到 localStorage
        localStorage.setItem('locale', locale);
    }
}

//监听
// 监听 i18n locale 变化，同步到组件
watch(() => i18n.global.locale.value, (newLocale) => {
    selectedLocale.value = newLocale;
});
</script>


<style scoped>
#language-box {
    width: 100%;
    display: flex;
    justify-content: center;
}
</style>