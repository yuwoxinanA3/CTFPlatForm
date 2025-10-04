<template>
    <div class="showPanel">
        <el-container>
            <el-header style="padding:0 0;">
                <div style="display: flex;justify-content: space-between;">
                    <el-input v-model="input" style="width:15%;min-width: 120px;" size="large" placeholder="战队名称"
                        :suffix-icon="Search" />
                    <el-button type="primary">创建战队</el-button>
                </div>
            </el-header>

            <el-main style="padding:0 0;">
                <div class="team-container">
                    <teamInfo v-for="teamId in teamIds" :key="teamId" :teamId="teamId" />
                </div>
            </el-main>

            <el-footer style="margin-top: 40px;padding:0 0;">
                <div class="demo-pagination-block">
                    <el-pagination v-model:current-page="currentPage4" v-model:page-size="pageSize4"
                        :page-sizes="[100, 200, 300, 400]" :size="size" :disabled="disabled" :background="background"
                        layout="total, sizes, prev, pager, next, jumper" :total="400" @size-change="handleSizeChange"
                        @current-change="handleCurrentChange" />
                </div>
            </el-footer>
        </el-container>
    </div>
</template>

<script setup lang='ts'>
//官方引入
import { onMounted, ref } from 'vue'

//插件引入
import { Search } from '@element-plus/icons-vue'

//自定义引入
import teamInfo from '@/components/team/teamInfo.vue'
//样式引入
import '@/assets/styles/element-custom/el-input.css';
import '@/assets/styles/element-custom/el-button.css';
import '@/assets/styles/element-custom/el-pagination.css';
//资源引入


//数据
const input = ref('')
const teamIds = ref<string[]>([])
import type { ComponentSize } from 'element-plus'

const currentPage4 = ref(4)
const pageSize4 = ref(100)
const size = ref<ComponentSize>('default')
const background = ref(false)
const disabled = ref(false)

const handleSizeChange = (val: number) => {
    console.log(`${val} items per page`)
}
const handleCurrentChange = (val: number) => {
    console.log(`current page: ${val}`)
}


//方法
const fetchTeamIds = async () => {
    try {
        // 这里是你获取战队 ID 列表的 API 调用
        // 例如:
        // const response = await apiClient.get('/Team/GetTeamIds');
        // teamIds.value = response.data.result;

        // 临时示例数据
        teamIds.value = [
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c3',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c4',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c5',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c3',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c4',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c5',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c3',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c4',
            'cbefe7e0-b167-4e13-be16-98f6ef08a8c5'
        ];
    } catch (error) {
        console.error('获取战队 ID 列表失败:', error);
    }
}
// 监听
onMounted(() => {
    fetchTeamIds();
})

</script>

<style scoped>
.team-container {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(380px, 1fr));
    gap: 40px;
    max-width: 100%;
    overflow-x: hidden;
    padding: 0 0px;
    margin: 0 auto;
}

/* 响应式断点 */
@media (max-width: 1200px) {
    .team-container {
        grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
        gap: 30px;
    }
}

@media (max-width: 768px) {
    .team-container {
        grid-template-columns: 1fr;
        gap: 20px;
    }
}
</style>