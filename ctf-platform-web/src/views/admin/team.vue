<template>
    <div class="showPanel">
        <div class="sidebar" style="height: 200px;">
            <el-menu default-active="1" class="el-menu-vertical-demo card-box" style="padding: 20px 10px 50px 10px;">
                <el-menu-item index="1" class="sub-nav-font" @click="showCreateTeam">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <User />
                    </el-icon>
                    <!-- <span>{{ $t('user.pseronPage') }}</span> -->
                    <span>创建战队</span>
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
const showCreateTeam = () => {
    router.push('/home/team/createTeam')
}

//监听

</script>
<style scoped>
.showPanel {
    display: flex;
    padding: 20px 0;
    margin: 10px 20px;
    background-color: none;
}

::v-deep(.el-menu) {
    border-right: none !important;
    border-bottom: none !important;
    /* 修复白边 */
}

.sidebar {
    flex-shrink: 0;
    width: auto;
    min-width: 120px;
    max-width: 200px;
    border-radius: 10px;
    overflow: hidden;
}

.main-content {
    flex-grow: 1;
    border-radius: 10px;
}

.sub-nav-font {
    font-size: 16px;
    border-radius: 10px;
    justify-content: left;
}
</style>