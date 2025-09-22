<template>
    <div class="showPanel">
        <div class="sidebar" style="height: 200px;">
            <el-menu default-active="1" class="el-menu-vertical-demo card-box" style="padding: 20px 10px 50px 10px;">
                <el-menu-item index="1" class="sub-nav-font" @click="showUserPanel">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <User />
                    </el-icon>
                    <span>{{ $t('user.pseronPage') }}</span>
                </el-menu-item>

                <el-menu-item index="2" class="sub-nav-font" @click="showChangePwdPanel">
                    <el-icon :size="18" style="margin-right: 8px;">
                        <Key />
                    </el-icon>
                    <span>{{ $t('user.changePwdPage') }}</span>
                </el-menu-item>
            </el-menu>
        </div>

        <div class="main-content">
            <router-view style="margin-left: 20px;" />
        </div>
    </div>
</template>





<script setup lang="ts">
import router from '@/router';


// 显示用户面板
const showUserPanel = () => {
    router.push('/home/user/userInfo')
}

const showChangePwdPanel = () => {
    router.push('/home/user/changePwd')
}




</script>

<style scoped>
.showPanel {
    display: flex;
    padding: 20px 0;
    margin: 10px 20px;
    background: none;
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