<template>
    <div class="showPanel">
        <div class="sidebar" style="height: 200px;">
            <el-menu default-active="1" class="el-menu-vertical-demo card-box" style="padding: 20px 10px 50px 10px;">
                <el-menu-item index="1" class="sub-nav-font" @click="navigateTo('/home/team/createTeam')">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <CirclePlus />
                    </el-icon>
                    <span>{{ $t('team.createTeam') }}</span>
                </el-menu-item>
                <el-menu-item index="1" class="sub-nav-font" @click="navigateTo('/home/teamCenter')">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <Tickets />
                    </el-icon>
                    <span>战队大厅</span>
                </el-menu-item>
                <el-menu-item index="1" class="sub-nav-font" @click="navigateTo('/home/team/teamInfo')">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <Tickets />
                    </el-icon>
                    <span>{{ $t('team.teamInfo') }}</span>
                </el-menu-item>
            </el-menu>
        </div>

        <div class="main-content">
            <router-view style="margin-left: 20px;" />
        </div>
    </div>
</template>
<script setup lang='ts'>
//插件引入

//自定义引入
import router from '@/router';
//资源引入

//数据

//方法
/**
 * 跳转
 * @param path 路径
 */
const navigateTo = (path: string) => {
    router.push(path)
}



//监听

</script>
<style scoped></style>