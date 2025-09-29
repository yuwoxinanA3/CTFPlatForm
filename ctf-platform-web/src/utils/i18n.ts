import en from '@/locale-language/en.json';
import zh from '@/locale-language/zh.json';
import { createI18n } from 'vue-i18n';

const messages = {
    en,
    zh
};

// 从 localStorage 获取保存的语言设置，默认为 'zh'
const savedLocale = localStorage.getItem('locale') || 'zh';


const i18n = createI18n({
    legacy: false, // 使用 Composition API 模式
    locale: savedLocale, // 设置默认语言
    fallbackLocale: 'en', // 当前语言没有对应翻译时使用的备用语言
    messages // 注入语言包
});

export default i18n;