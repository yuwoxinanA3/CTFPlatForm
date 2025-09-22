<template>
  <div 
    class="background-container" 
    :class="{ 
      'image-background': showImageBackground && backgroundImage,
      'with-overlay': isDarkMode && showImageBackground && backgroundImage
    }"
    :style="backgroundStyle"
  >
    <div 
      v-if="isDarkMode && showImageBackground && backgroundImage"
      class="dark-overlay"
      :style="{ opacity: darkOverlayOpacity }"
    ></div>
    
    <div class="content-wrapper">
      <slot></slot>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'

// 背景相关状态
const showImageBackground = ref(true)
const backgroundImage = ref('')
const isDarkMode = ref(false)
const darkOverlayOpacity = ref(0.7)

// 背景样式计算
const backgroundStyle = computed(() => {
  if (showImageBackground.value && backgroundImage.value) {
    return {
      backgroundImage: `url(${backgroundImage.value})`
    }
  }
  return {
    backgroundImage: 'none'
  }
})

// 切换背景类型
const toggleBackground = () => {
  showImageBackground.value = !showImageBackground.value
}

// 设置背景图片
const setBackgroundImage = (url: string) => {
  backgroundImage.value = url
}

// 设置暗色模式
const setDarkMode = (dark: boolean) => {
  isDarkMode.value = dark
}

// 设置暗色遮罩透明度
const setDarkOverlayOpacity = (opacity: number) => {
  darkOverlayOpacity.value = opacity
}

// 暴露方法给父组件使用
defineExpose({
  toggleBackground,
  setBackgroundImage,
  setDarkMode,
  setDarkOverlayOpacity
})
</script>

<style scoped>
.background-container {
  min-height: 100vh;
  background-color: var(--el-bg-color-page);
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  position: relative;
  transition: background-color 0.3s;
}

.content-wrapper {
  position: relative;
  z-index: 2;
}

.image-background {
  background-color: transparent;
}

.dark-overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #000000;
  z-index: 1;
  transition: opacity 0.3s;
}
</style>