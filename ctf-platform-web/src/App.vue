<template>
    <router-view />
</template>

<script setup lang="ts">

</script>

<style>
/* 全局样式 */
@import '@/assets/styles/variables.css';

body {
    background-color: var(--el-bg-color-page);
    color: var(--el-text-color-primary);
    transition: background-color 0.3s, color 0.3s;
    margin: 0;
    font-family: "Helvetica Neue", Helvetica, "PingFang SC", "Hiragino Sans GB", "Microsoft YaHei", "微软雅黑", Arial, sans-serif;
}

#app {
    background-color: var(--el-bg-color-page);
    min-height: 100vh;
    /* 确保 #app 的背景色正确 */
}
</style>
