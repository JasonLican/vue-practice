import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        redirect: '/login',
    },
    {
        path: '/login',
        meta: {
            title: '登录',
            noAuth: true,
        },
        component: () => import('../views/SysPages/login.vue'),
    },
    {
        path: '/home',
        name: 'home',
        meta: {
            title: '首页',
        },
        redirect:"dashboard",
        component: () => import('../views/home.vue'),
        children: [
            {
                path: '/dashboard',
                name: 'dashboard',
                meta: {
                    title: '首页',
                },
                component: () => import( '../views/Main/dashboard.vue'),
            },
            {
                path: '/sys-user',
                name: 'sys-user',
                meta: {
                    title: '用户管理',
                    parentTitle:"系统设置",
                },
                component: () => import( '../views/SysSetting/user.vue'),
            },
            {
                path: '/ucenter',
                name: 'ucenter',
                meta: {
                    title: '个人中心',
                },
                component: () => import('../views/SysPages/ucenter.vue'),
            },
            {
                path: '/sys-role',
                name: 'sys-role',
                meta: {
                    title: '角色管理',
                    parentTitle:"系统设置",
                },
                component: () => import('../views/SysSetting/role.vue'),
            },
            {
                path: '/sys-menu',
                name: 'sys-menu',
                meta: {
                    title: '菜单管理',
                    parentTitle:"系统设置",
                    permiss: '13',
                },
                component: () => import( '../views/SysSetting/menu.vue'),
            },
        ],
    },
    { path: '/:path(.*)', redirect: '/404' },
];

const router = createRouter({
    history: createWebHashHistory(),
    routes,
});

export default router;
