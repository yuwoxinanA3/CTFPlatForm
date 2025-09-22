import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import { visualizer } from 'rollup-plugin-visualizer'
// https://vite.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    //打包时显示包大小分析面板
    visualizer({
      filename: 'dist/stats.html',
      open: true
    })
  ],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),//使用@代表根路径
      '@components': path.resolve(__dirname, './src/components')
    }
  },
  server: {
    //启动项目后自动打开浏览器
    open: true,
    //设置主机
    host: '127.0.0.1',
    //端口
    port: 5001,
    //代理
    proxy: {
      '/api': {
        target: 'http://localhost:5193',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, '')
      },
      '/uploads': {
        target: 'http://localhost:5193',
        changeOrigin: true,
        ws: true
      }
    }
  },
  build: {
    rollupOptions: {
      output: {
        manualChunks: {
          // Vue 核心库
          'vue': ['vue', 'vue-router'],
          // 状态管理
          'pinia': ['pinia'],
          // UI 组件库
           'element-plus': ['element-plus', '@element-plus/icons-vue'],
          // 网络请求库
          'axios': ['axios'],
          // 国际化
          'i18n': ['vue-i18n'],
          // 工具库
          'utils': ['crypto-js', 'jwt-decode', 'mitt'],
          // 图片裁剪组件
          'cropper': ['vue-cropper']
        }
      }
    },
    chunkSizeWarningLimit: 1000 // 将警告阈值调整到 1000KB
  }
})
