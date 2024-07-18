<template>
  <div class="sidebar">
    <el-menu
      class="sidebar-el-menu"
      :default-active="onRoutes"
      :collapse="store.collapse"
      background-color="#545c64"
      text-color="#fff"
      active-text-color="#ffd04b"
      router
    >
      <template v-for="item in store.menuData">
        <template v-if="item.children.length > 0">
          <el-sub-menu
            :index="item.url || item.id"
            :key="item.url || item.id"
          >
            <template #title>
              <el-icon>
                <component :is="item.icon"></component>
              </el-icon>
              <span>{{ item.label }}</span>
            </template>
            <template v-for="subItem in item.children">
              <el-sub-menu
                v-if="subItem.children.length > 0"
                :index="subItem.url || subItem.id"
                :key="subItem.url || subItem.id"
              >
                <template #title>{{ subItem.label }}</template>
                <el-menu-item
                  v-for="(threeItem, i) in subItem.children"
                  :key="i"
                  :index="threeItem.url || threeItem.id"
                >
                  {{ threeItem.label }}
                </el-menu-item>
              </el-sub-menu>
              <el-menu-item v-else :index="subItem.url || subItem.id">
                {{ subItem.label }}
              </el-menu-item>
            </template>
          </el-sub-menu>
        </template>
        <template v-else>
          <el-menu-item
            :index="item.url || item.id"
            :key="item.url || item.id"
          >
            <el-icon>
              <component :is="item.icon"></component>
            </el-icon>
            <template #title>{{ item.label }}</template>
          </el-menu-item>
        </template>
      </template>
    </el-menu>
  </div>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";
import { mainStore } from "../store/index";

const store = mainStore();
const route = useRoute();
const onRoutes = computed(() => {
  return route.path;
});
</script>

<style scoped>
.sidebar {
  display: block;
  position: absolute;
  left: 0;
  top: 69px;
  bottom: 0;
  overflow-y: scroll;
}

.sidebar::-webkit-scrollbar {
  width: 0;
}

.sidebar-el-menu:not(.el-menu--collapse) {
  width: 250px;
}

.sidebar-el-menu {
  min-height: 100%;
}
</style>
