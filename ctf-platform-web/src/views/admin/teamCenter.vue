<!-- teamCenter.vue -->
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
                    <!-- 直接使用获取到的完整战队数据 -->
                    <teamInfo v-for="team in teams" :key="team.id" :team-data="team" />
                </div>
            </el-main>

            <el-footer style="margin-top: 40px;padding:0 0;">
                <div class="demo-pagination-block">
                    <el-pagination v-model:current-page="currentPage4" v-model:page-size="pageSize4"
                        :page-sizes="[10, 20, 30, 40]" :size="size" :disabled="disabled" :background="background"
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

// 数据接口定义
interface TeamData {
    id: string;
    teamName: string;
    teamIcon: string;
    declaration: string;
    teamIntroduction: string;
    establishmentTime: string;
    teamPoints: number;
    teamEmail: string;
    teamWebsite: string;
    country: string;
    city: string;
    university: string;
    memberCount: number;
    teamLeader: string;
    userImage: string;
    teamLeader1: string;
    userImage1: string;
    teamLeader2: string;
    userImage2: string;
}

//数据
const input = ref('')
const teams = ref<TeamData[]>([])
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
const fetchTeams = async () => {
    try {
        // 一次性获取所有战队信息
        // 假设后端提供了一个可以获取所有战队信息的接口
        //const response = await apiClient.get('/Team/GetAllTeams');

        // 处理响应数据
        // if (response.data.isSuccess) {
        //     teams.value = response.data.result.map((team: any) => ({
        //         id: team.id || '',
        //         teamName: team.teamName || '',
        //         teamIcon: team.teamIcon || '',
        //         declaration: team.declaration || '',
        //         teamIntroduction: team.teamIntroduction || '',
        //         establishmentTime: team.establishmentTime || '',
        //         teamPoints: team.teamPoints || 0,
        //         teamEmail: team.teamEmail || '',
        //         teamWebsite: team.teamWebsite || '',
        //         country: team.country || '',
        //         city: team.city || '',
        //         university: team.university || '',
        //         memberCount: team.memberCount || 0,
        //         teamLeader: team.teamLeader || '',
        //         userImage: team.userImage || '',
        //         teamLeader1: team.teamLeader1 || '',
        //         userImage1: team.userImage1 || '',
        //         teamLeader2: team.teamLeader2 || '',
        //         userImage2: team.userImage2 || ''
        //     }));
        // }

        //测试数据
        // 测试数据
        teams.value = [
            {
                id: '1',
                teamName: 'Team Alpha',
                teamIcon: 'https://img0.baidu.com/it/u=3606135952,974223653&fm=253&fmt=auto&app=138&f=JPEG?w=505&h=500',
                declaration: '追求卓越，勇攀高峰',
                teamIntroduction: '我们是一支专注于网络安全和渗透测试的专业团队，成立于2020年，团队成员均具有丰富的实战经验。',
                establishmentTime: '2020-03-15',
                teamPoints: 1250,
                teamEmail: 'alpha@example.com',
                teamWebsite: 'https://alpha-team.example.com',
                country: '中国',
                city: '北京',
                university: '清华大学',
                memberCount: 5,
                teamLeader: '张三',
                userImage: 'https://via.placeholder.com/50x50/409EFF/FFFFFF?text=ZS',
                teamLeader1: '李四',
                userImage1: 'https://via.placeholder.com/50x50/67C23A/FFFFFF?text=LS',
                teamLeader2: '王五',
                userImage2: 'https://via.placeholder.com/50x50/E6A23C/FFFFFF?text=WW'
            },
            {
                id: '2',
                teamName: 'Cyber Dragons',
                teamIcon: 'https://img0.baidu.com/it/u=361804728,543588260&fm=253&fmt=auto&app=120&f=JPEG?w=294&h=294',
                declaration: '守护网络，龙腾四海',
                teamIntroduction: 'Cyber Dragons是一支国际知名的安全研究团队，专注于漏洞挖掘和安全防护技术研究。',
                establishmentTime: '2019-07-22',
                teamPoints: 2100,
                teamEmail: 'dragons@cyber.com',
                teamWebsite: 'https://cyberdragons.org',
                country: '美国',
                city: 'San Francisco',
                university: 'Stanford University',
                memberCount: 8,
                teamLeader: 'John Smith',
                userImage: 'https://via.placeholder.com/50x50/67C23A/FFFFFF?text=JS',
                teamLeader1: 'Emily Johnson',
                userImage1: 'https://via.placeholder.com/50x50/F56C6C/FFFFFF?text=EJ',
                teamLeader2: 'Michael Brown',
                userImage2: 'https://via.placeholder.com/50x50/909399/FFFFFF?text=MB'
            },
            {
                id: '3',
                teamName: 'Binary Knights',
                teamIcon: 'https://img2.baidu.com/it/u=1363567715,1180734128&fm=253&fmt=auto&app=138&f=JPEG?w=800&h=800',
                declaration: '代码即武器，二进制中见真章',
                teamIntroduction: 'Binary Knights专注于二进制安全和逆向工程，在CTF竞赛中屡获佳绩，是业界顶尖的二进制安全团队之一。',
                establishmentTime: '2021-11-30',
                teamPoints: 980,
                teamEmail: 'knights@binary.org',
                teamWebsite: 'https://binaryknights.net',
                country: '德国',
                city: 'Berlin',
                university: 'Technical University of Munich',
                memberCount: 6,
                teamLeader: 'Hans Mueller',
                userImage: 'https://via.placeholder.com/50x50/E6A23C/FFFFFF?text=HM',
                teamLeader1: 'Anna Schmidt',
                userImage1: 'https://via.placeholder.com/50x50/409EFF/FFFFFF?text=AS',
                teamLeader2: 'Klaus Weber',
                userImage2: 'https://via.placeholder.com/50x50/67C23A/FFFFFF?text=KW'
            },
            {
                id: '4',
                teamName: 'Binary Knights',
                teamIcon: 'https://img2.baidu.com/it/u=1363567715,1180734128&fm=253&fmt=auto&app=138&f=JPEG?w=800&h=800',
                declaration: '代码即武器，二进制中见真章',
                teamIntroduction: 'Binary Knights专注于二进制安全和逆向工程，在CTF竞赛中屡获佳绩，是业界顶尖的二进制安全团队之一。',
                establishmentTime: '2021-11-30',
                teamPoints: 980,
                teamEmail: 'knights@binary.org',
                teamWebsite: 'https://binaryknights.net',
                country: '德国',
                city: 'Berlin',
                university: 'Technical University of Munich',
                memberCount: 6,
                teamLeader: 'Hans Mueller',
                userImage: 'https://via.placeholder.com/50x50/E6A23C/FFFFFF?text=HM',
                teamLeader1: 'Anna Schmidt',
                userImage1: 'https://via.placeholder.com/50x50/409EFF/FFFFFF?text=AS',
                teamLeader2: 'Klaus Weber',
                userImage2: 'https://via.placeholder.com/50x50/67C23A/FFFFFF?text=KW'
            }
        ];

    } catch (error) {
        console.error('获取战队信息失败:', error);
    }
}

// 监听
onMounted(() => {
    fetchTeams();
})
</script>

<style scoped>
.team-container {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
    gap: 24px;
    width: 100%;
    padding: 0;
    margin: 0;
}

/* 平板小屏：≥ 768px */
@media (min-width: 768px) {
    .team-container {
        grid-template-columns: repeat(auto-fill, minmax(320px, 1fr));
        gap: 28px;
    }
}

/* 平板大屏/小桌面：≥ 1024px */
@media (min-width: 1024px) {
    .team-container {
        grid-template-columns: repeat(auto-fill, minmax(340px, 1fr));
        gap: 32px;
    }
}

/* 大桌面：≥ 1440px */
@media (min-width: 1440px) {
    .team-container {
        grid-template-columns: repeat(auto-fill, minmax(360px, 1fr));
        gap: 36px;
    }
}

/* 超大屏：≥ 1800px */
@media (min-width: 1800px) {
    .team-container {
        grid-template-columns: repeat(auto-fill, minmax(380px, 1fr));
        gap: 40px;
    }
}
</style>