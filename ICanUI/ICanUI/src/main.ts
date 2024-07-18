import { createApp } from 'vue'
import router from './router'
import ElementPlus from "element-plus"
import * as ElementPlusIconsVue from '@element-plus/icons-vue';
import 'element-plus/dist/index.css';
import App from './App.vue'
import "./assets/css/base.css"
import { createPinia } from 'pinia';

const pinia=createPinia();
const app = createApp(App);
// 注册elementplus图标
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component);
}

app.use(pinia);

app.use(ElementPlus).use(router);
app.mount('#app');
