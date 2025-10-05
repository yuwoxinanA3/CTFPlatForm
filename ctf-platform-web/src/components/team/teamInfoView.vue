<template>
    <div class="team-info-view">
        <teamInfo :team-id="teamId" />
    </div>
</template>

<script setup lang="ts">
// 官方引入
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

// 自定义引入
import teamInfo from '@/components/team/teamInfo.vue'

// 数据
const route = useRoute()
const teamId = ref('')

// 方法
onMounted(() => {
    // 从路由参数中获取teamId，如果没有则使用默认值
    teamId.value = route.params.teamId as string || 'cbefe7e0-b167-4e13-be16-98f6ef08a8c3'

    // 如果需要从查询参数获取，可以使用:
    // teamId.value = route.query.teamId as string || '默认ID'
})
</script>

<style scoped>
.team-info-view {
    padding: 20px;
}
</style>