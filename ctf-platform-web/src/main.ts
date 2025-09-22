import { createApp } from 'vue'
import App from './App.vue'
//引入全局样式
import '@/assets/styles/global.scss'
//引入elementUI plus
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
//引入路由
import router from './router'
//引入pinia
import { createPinia } from 'pinia'
// 引入 i18n 配置
import i18n from '@/utils/i18n';

const app = createApp(App)
//使用饿了么UI
app.use(ElementPlus)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}
//挂载路由
app.use(router)
// 挂载 Pinia 
const pinia = createPinia()
app.use(pinia)
//国际化
app.use(i18n)
app.mount('#app')