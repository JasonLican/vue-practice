<template>
  <div class="wrapper">
    <common-header />
    <common-aside />
  </div>
  <div class="content-box" :class="{ 'content-collapse': store.collapse }">
    <tabs></tabs>
    <div class="content">
      <router-view />
    </div>
  </div>
</template>
<script lang="ts" setup>
import CommonHeader from "@/components/CommonHeader.vue";
import CommonAside from "@/components/CommonAside.vue";
import Tabs from "@/components/Tabs.vue";
import { mainStore } from "@/store/index";
import { useRouter } from "vue-router";
import { onMounted, ref } from "vue";
import { GetMenuList } from "@/api/Menu";

const router = useRouter();
const store = mainStore();

onMounted(async () => {
  let res = await GetMenuList();
  if (res.code == 200) {
    localStorage.setItem("menus", JSON.stringify(res.data));
    store.initMenu(router);
  }
});
</script>
<style lang="less" scoped>
.wrapper {
  height: 100vh;
  overflow: hidden;
}
.content-box {
  position: absolute;
  left: 250px;
  right: 0;
  top: 70px;
  bottom: 0;
  padding-bottom: 30px;
  -webkit-transition: left 0.3s ease-in-out;
  transition: left 0.3s ease-in-out;
  background: #eef0fc;
  overflow: hidden;
}

.content {
  width: auto;
  height: 100%;
  padding: 20px;
  overflow-y: scroll;
  box-sizing: border-box;
}

.content::-webkit-scrollbar {
  width: 0;
}

.content-collapse {
  left: 65px;
}
</style>
