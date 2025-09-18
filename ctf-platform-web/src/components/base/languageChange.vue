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
import { ref } from 'vue';
// 引入 i18n 实例
import i18n from '@/utils/i18n';


//数据


// 数据部分新增语言选择相关逻辑
const availableLocales = ['zh', 'en'];
const selectedLocale = ref(i18n.global.locale.value); // 初始值为当前 locale


//方法

/**
 * 语言切换方法
 */
function changeLanguage(locale: string) {
    if (availableLocales.includes(locale)) {
        i18n.global.locale.value = locale as 'zh' | 'en';
    }
}

</script>


<style scoped>
#language-box{
    width: 100%;
    display: flex;
    justify-content: center;
}
</style>