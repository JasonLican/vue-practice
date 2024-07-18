import { fa } from "element-plus/es/locales.mjs";
import { defineStore } from "pinia";
import { Router } from 'vue-router';

export const mainStore = defineStore("mainStore", {
    state: () => {
        return {
            collapse: false,
            menuData: [],
            permissionData: []
        }
    },
    getters: {},
    actions: {
        handelCollapse() {
            this.collapse = !this.collapse;
        },
        initMenu(router: Router) {
            if (!localStorage.getItem("menus")) return;
            const menu = JSON.parse(localStorage.getItem("menus"));
            this.menuData = menu;
            menu.forEach((item: any) => {
                if (item.children) {
                    item.children.forEach((subItem: any) => {
                        let _component = () => import(subItem.component);
                        router.addRoute("home", {
                            path: subItem.url,
                            name: subItem.name,
                            meta: subItem.meta,
                            component: _component,
                        })
                    })
                } else {

                }
            });
        },
        initPermissionData(permissions: any) {
            this.permissionData = permissions;
        }
    }

})