import { useAuthStore } from "@/store/authStore";
import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            name: 'index',
            component: () => import('@/views/index/login.vue')
        },
        {
            path: '/login',
            name: 'login',
            component: () => import('@/views/index/login.vue')
        },
        {
            path: '/home',
            name: 'home',
            component: () => import('@/views/index/home.vue'),
            children: [
                {
                    path: 'user',
                    name: 'user',
                    component: () => import('@/views/admin/user.vue'),
                    children: [{
                        path: 'userInfo',
                        name: 'userInfo',
                        component: () => import('@/components/user/userInfo.vue'),
                    },
                    {
                        path: 'changePwd',
                        name: 'changePwd',
                        component: () => import('@/components/user/changeUserPwd.vue'),
                    }]
                },
                {
                    path: 'team',
                    name: 'team',
                    component: () => import('@/views/admin/team.vue'),
                    children: [{
                        path: 'createTeam',
                        name: 'createTeam',
                        component: () => import('@/components/team/createTeam.vue'),
                    },
                    {
                        path: 'teamInfo',
                        name: 'teamInfo',
                        component: () => import('@/components/team/teamInfo.vue'),
                    }
                ]
                }
            ]
        },
        {
            path: '/:pathMatch(.*)',
            name: 'notfund',
            component: () => import('@/views/index/notFund.vue')
        }
    ]
})

// 添加全局前置守卫
router.beforeEach((to, _from, next) => {
    // 使用 Pinia store 获取 token
    const authStore = useAuthStore();

    // 如果要去登录页，直接放行
    if (to.name === 'login' || to.name === 'index') {
        next();
        return;
    }

    // 检查 token 是否存在且未过期
    if (authStore.token) {
        try {
            // 检查 token 是否过期
            const token = authStore.token;
            const payload = JSON.parse(atob(token.split('.')[1]));
            const currentTime = Date.now() / 1000;

            if (payload.exp < currentTime) {
                // token 过期，清除 token 并跳转到登录页
                authStore.clearToken();
                next({ name: 'login' });
                return;
            }
            next();
        } catch (e) {
            // token 解析失败，清除 token 并跳转到登录页
            authStore.clearToken();
            next({ name: 'login' });
            return;
        }
    } else {
        // 没有 token，跳转到登录页
        next({ name: 'login' });
    }
});

export default router; 