<template>
    <div class="stat-item">
        <div class="label">{{ item.label }}</div>
        <template v-if="item.avatar">
            <div class="avatar">
                <img :src="item.avatar" alt="avatar" />
            </div>
            <div class="value" style="margin-top: 15px;">{{ item.value }}</div>
        </template>
        <template v-else>
            <div class="value">{{ item.value }}</div>
        </template>
    </div>
</template>

<script setup lang="ts">
// 接收外部传入的数据项
defineProps<{
    item: {
        label: string;
        value: string;
        avatar?: string;
    };
}>();
</script>

<style scoped>
.stat-item {
    flex: 1 1 calc(33.333% - 16px);
    min-width: calc(33.333% - 16px);
    border-radius: 8px;
    padding: 12px;
    text-align: center;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.stat-item .label {
    font-size: 20px;
    color: var(--el-text-color-secondary);
    margin-bottom: 16px;
}

.stat-item .value {
    font-size: 24px;
    font-weight: bold;
    color: var(--el-text-color-primary);
    margin-bottom: 8px;
}

.stat-item .avatar {
    width: 100px;
    height: 100px;
    border-radius: 50%;
    overflow: hidden;
}

.stat-item .avatar img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}
</style>