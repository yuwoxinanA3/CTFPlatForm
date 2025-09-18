<template>
  <div class="home-wrapper">
    <div id="nav-div">
      <el-menu :default-active="activeIndex" class="el-menu-demo nav-box" mode="horizontal" :ellipsis="false">
        <el-menu-item index="0">
          <img style="width: 50px" src="@/assets/images/主页贴图.png" alt="Rotten logo" />
          <span style="font-size: x-large;">{{ $t('platform_name') }}</span>
        </el-menu-item>
        <el-menu-item index="1" class="nav-font">{{ $t('home.home') }}</el-menu-item>

        <el-menu-item index="2" class="nav-font">{{ $t('home.competition') }}</el-menu-item>

        <el-menu-item index="3" class="nav-font">{{ $t('home.training') }}</el-menu-item>
        <el-menu-item index="4" class="nav-font" @click="showTeamPanel">{{ $t('home.team') }}</el-menu-item>


        <el-sub-menu index="5">
          <template #title>
            <div class="nav-font nav-font-submenu">{{ $t('home.resources') }}</div>
          </template>
          <el-menu-item index="5-1">{{ $t('home.blog') }}</el-menu-item>
          <el-menu-item index="5-2">{{ $t('home.video') }}</el-menu-item>
        </el-sub-menu>

        <el-sub-menu index="6">
          <template #title>
            <div class="nav-font nav-font-submenu">{{ $t('home.tools') }}</div>
          </template>
          <el-menu-item index="6-1">{{ $t('home.onlineTools') }}</el-menu-item>
          <el-menu-item index="6-2">{{ $t('home.toolDownload') }}</el-menu-item>
          <el-menu-item index="6-3">{{ $t('home.toolGuide') }}</el-menu-item>
        </el-sub-menu>


        <div class="theme-toggle" @click="showLanguageSelector">
          {{ $t('home.language') }}
        </div>

        <div class="theme-toggle" @click="toggleTheme">
          <el-icon size="20px">
            <component :is="isDarkTheme ? 'Moon' : 'Sunny'" />
          </el-icon>
        </div>

        <el-menu-item index="7" class="nav-font" @click="showUserPanel">
          <el-avatar :size="40"
            :src="'http://localhost:5193/uploads/avatars/7d4e03c9-ba2d-49f6-9428-a27a8a4dca97.png'" />
          <span class="username" style="margin-left: 10px;">{{ formatUsername('予我心安A3') }}</span>
        </el-menu-item>


        <div class="theme-toggle" @click="handleLogout">
          <el-icon size="20px">
            <SwitchButton />
          </el-icon>
        </div>

      </el-menu>
    </div>


    <!-- 语言选择弹窗 -->
    <el-dialog v-model="languageDialogVisible" width="300px" center>
      <language-change />
    </el-dialog>

    <!-- 退出登录确认弹窗 -->
    <el-dialog v-model="logoutDialogVisible" width="300px" center>
      <span>{{ $t('home.logoutMessage') }}</span>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="logoutDialogVisible = false">{{ $t('home.cancel') }}</el-button>
          <el-button type="primary" @click="confirmLogout">
            {{ $t('home.confirm') }}
          </el-button>
        </span>
      </template>
    </el-dialog>

    <!-- 嵌套路由视图，用于显示子组件 -->
    <div class="user-panel-container">
      <router-view />
    </div>

    <!-- ICP备案备案信息 ICP filing information -->
    <FilingInfo :filing="{
      number: '京ICP备12345678号',
      link: 'https://beian.miit.gov.cn'
    }" :security-filing="{
      number: '京公网安备12345678901234号',
      link: 'http://www.beian.gov.cn/portal/registerSystemInfo'
    }" icon-url="" />
  </div>
</template>

<script lang="ts" setup>
//官方引入
import router from '@/router'
import { onMounted, ref } from 'vue'

//插件引入
import LanguageChange from '@/components/base/languageChange.vue'
import FilingInfo from '@/components/base/icpInfo.vue'
import { useAuthStore } from '@/store/authStore'

//自定义引入

//资源引入


//数据
const activeIndex = ref('1')
const isDarkTheme = ref(false)
const languageDialogVisible = ref(false) // 控制语言选择弹窗显示
const logoutDialogVisible = ref(false) // 控制退出登录弹窗显示
//方法
// 显示用户面板
const showUserPanel = () => {
  router.push('/home/user/userInfo')
}

const showTeamPanel = () => {
  router.push('/home/team')
}


// 切换主题的方法接口
const toggleTheme = () => {
  isDarkTheme.value = !isDarkTheme.value
  onThemeChange(isDarkTheme.value ? 'dark' : 'light')
}

// 主题切换实现
const onThemeChange = (theme: 'light' | 'dark') => {
  // 使用更可靠的方式获取根元素
  const app = document.getElementById('app') || document.body

  if (theme === 'dark') {
    app.classList.add('dark-theme')
    document.body.classList.add('dark-theme')  // 同时给body添加主题类
  } else {
    app.classList.remove('dark-theme')
    document.body.classList.remove('dark-theme')  // 同时移除body的主题类
  }

  // 保存用户主题偏好到 localStorage
  localStorage.setItem('theme', theme)

  // 同时更新 HTML 元素的 data-theme 属性（如果需要）
  document.documentElement.setAttribute('data-theme', theme)
}

// 格式化用户名，超过8个字符显示省略号
const formatUsername = (username: string) => {
  if (username.length > 8) {
    return username.substring(0, 8) + '...'
  }
  return username
}

// 显示语言选择弹窗
const showLanguageSelector = () => {
  languageDialogVisible.value = true
}

// 处理退出登录点击事件
const handleLogout = () => {
  logoutDialogVisible.value = true
}

// 确认退出登录
const confirmLogout = () => {
  // 清除认证信息
  const authStore = useAuthStore();
  authStore.clearToken();
  logoutDialogVisible.value = false
  // 跳转到登录页
  router.push('/login')
}

//监听
// 在组件挂载时初始化主题
onMounted(() => {
  // 从 localStorage 获取保存的主题，如果没有则根据系统偏好设置
  const savedTheme = localStorage.getItem('theme')
  if (savedTheme) {
    isDarkTheme.value = savedTheme === 'dark'
    onThemeChange(savedTheme as 'light' | 'dark')
  } else {
    // 根据系统主题设置初始主题
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches
    isDarkTheme.value = prefersDark
    onThemeChange(prefersDark ? 'dark' : 'light')
  }
})
</script>

<style scoped>
.home-wrapper {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

#nav-div {
  padding-top: 10px;
  flex-shrink: 0;
}

.nav-box {
  background-color: var(--el-menu-bg-color);
  /* 使用 CSS 变量 */
  margin: 0 20px;
  padding: 0px 10px;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  border: 1px solid var(--el-border-color);
}

.nav-font {
  font-size: larger;
  color: var(--el-menu-text-color);
  /* 使用 CSS 变量 */
}

.nav-font-submenu {
  font-size: 21px;
  font-weight: 350;
  color: var(--el-menu-text-color);
  /* 使用 CSS 变量 */
}

/* 暗色主题下的特殊样式 */
.dark-theme .nav-box {
  box-shadow: 0 0 10px rgba(255, 255, 255, 0.1);
}

.dark-theme .nav-font {
  color: var(--el-menu-text-color);
}

.dark-theme .nav-font-submenu {
  color: var(--el-menu-text-color);
}

.el-menu--horizontal {
  --el-menu-horizontal-height: 70px;
}

/* 从语言选择开始居右 */
.el-menu--horizontal> :nth-child(7) {
  margin-right: auto;
}

/* 为独立的图标添加样式 */
.theme-toggle {
  height: var(--el-menu-horizontal-height);
  display: flex;
  align-items: center;
  padding: 0 20px;
  cursor: pointer;
  transition: background-color 0.3s;
  color: var(--el-menu-text-color);
  /* 使用 CSS 变量 */
  font-size: larger;
}

.theme-toggle:hover {
  background-color: var(--el-menu-hover-bg-color);
  color: var(--el-menu-hover-text-color);
}

/* 用户面板容器样式 */
.user-panel-container {
  background-color: var(--el-bg-color-page);
  flex: 1;
  border-radius: 10px;
  margin-bottom: 0;
}


/* 退出弹窗按钮样式 */
.dialog-footer button:first-child {
  margin-right: 10px;
}
</style>