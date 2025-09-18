<!-- ICP备案备案信息 ICP filing information -->
<template>
    <div class="footer-filing-wrapper">
        <div class="filing-container">
            <div class="filing-content">
                <span class="filing-text">
                    <span class="filing-label">备案号：</span>
                    <a :href="filing.link || 'https://beian.miit.gov.cn'" target="_blank" rel="noopener noreferrer"
                        class="filing-link">
                        {{ filing.number || '京ICP备00000000号' }}
                    </a>
                </span>

                <span class="separator">|</span>

                <span class="filing-text">
                    <span class="filing-label">公网安备：</span>
                    <a :href="securityFiling.link || 'http://www.beian.gov.cn/portal/registerSystemInfo'"
                        target="_blank" rel="noopener noreferrer" class="filing-link">
                        {{ securityFiling.number || '京公网安备00000000000000号' }}
                    </a>
                </span>
            </div>

            <div class="filing-icon">
                <img :src="computedIconUrl" alt="备案图标" class="icon-img" @error="handleIconError" />
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import defaultIcpIcon from '@/assets/icons/icp.png'
import { computed } from 'vue'

interface FilingInfo {
    number?: string
    link?: string
}

interface Props {
    filing?: FilingInfo
    securityFiling?: FilingInfo
    iconUrl?: string
}

const props = withDefaults(defineProps<Props>(), {
    filing: () => ({}),
    securityFiling: () => ({}),
    iconUrl: undefined
})

// 计算属性，确保在没有传入有效值时使用默认图标
const computedIconUrl = computed(() => {
    return props.iconUrl && props.iconUrl.trim() !== '' ? props.iconUrl : defaultIcpIcon
})

const handleIconError = (event: Event) => {
    const img = event.target as HTMLImageElement
    // 当图标加载失败时，使用默认的base64图标
    img.src = 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAyNCAyNCI+PHBhdGggZmlsbD0iIzAwN2FjYyIgZD0iTTEyIDJDNi40OCAyIDIgNi40OCAyIDEyczQuNDggMTAgMTAgMTAgMTAtNC40OCAxMC0xMFMxNy41MiAyIDEyIDJ6bTAgMThjLTQuNDEgMC04LTMuNTktOC04czMuNTktOCA4LTggOCAzLjU5IDggOC0zLjU5IDgtOCA4eiIvPjxwYXRoIGZpbGw9IiMwMDdhY2MiIGQ9Ik0xMiA2Yy0zLjMxIDAtNiAyLjY5LTYgNnMyLjY5IDYgNiA2IDYtMi42OSA2LTYtMi42OS02LTYtNnptMCAxMGMtMi4yMSAwLTQtMS43OS00LTRzMS43OS00IDQtNCA0IDEuNzkgNCA0LTEuNzkgNC00IDR6Ii8+PC9zdmc+'
}
</script>

<style scoped lang="scss">
.footer-filing-wrapper {
    width: 100%;
    padding: 20px 0;
    background-color: var(--el-bg-color-page);
}

.filing-container {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;
    gap: 15px;
    max-width: 1200px;
    margin: 0 auto;
    padding: 0 20px;
}

.filing-content {
    display: flex;
    align-items: center;
    flex-wrap: wrap;
    justify-content: center;
    gap: 10px;
}

.filing-text {
    font-size: 14px;

    display: flex;
    align-items: center;
    flex-wrap: wrap;
    gap: 4px;
}

.filing-label {
    font-weight: 500;
    color: var(--el-text-color-primary);
}

.filing-link {
    color: var(--el-color-primary);
    text-decoration: none;
    transition: color 0.3s ease;

    &:hover {
        color: var(--el-menu-hover-text-color);
        text-decoration: underline;
    }
}

.separator {
    color: var(--el-border-color);
    font-weight: bold;
}

.filing-icon {
    display: flex;
    align-items: center;
}

.icon-img {
    height: 24px;
    width: auto;
    display: block;
}

// 响应式设计
@media (max-width: 768px) {
    .filing-container {
        flex-direction: column;
        gap: 10px;
    }

    .filing-content {
        gap: 8px;
    }

    .filing-text {
        font-size: 13px;
    }

    .separator {
        display: none;
    }
}
</style>