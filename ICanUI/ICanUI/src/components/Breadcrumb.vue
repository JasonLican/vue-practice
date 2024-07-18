<template>
  <el-breadcrumb separator="/">
    <el-breadcrumb-item >首页</el-breadcrumb-item>
    <el-breadcrumb-item style="color: #fff;" v-if="route.path!=='/dashboard'" v-for="(item,index) in breadList" :key="index">{{ item }}</el-breadcrumb-item>
  </el-breadcrumb>
</template>
  
<script setup lang="ts">
import { useRoute } from "vue-router";
import { Watch, ref, watch } from "vue";

const breadList = ref([]);
const route = useRoute();

watch(
  route,
  () => {
    if (route.path !== "/login") {
      breadList.value = [];
      for (let i = 1; i < route.matched.length; i++) {
        let matchedName = route.matched[i].meta.title;
        let parentName = route.matched[i].meta.parentTitle;
        breadList.value.push(parentName);
        breadList.value.push(matchedName);
      }
    }
  },
  { immediate: true, deep: true }
);
</script>
  
<style lang="less" scoped>
.el-breadcrumb__inner{
  color: #fff;
}
// 覆盖 element-plus 的样式
.el-breadcrumb__inner,
.el-breadcrumb__inner a {
  font-weight: 400 !important;
}
</style>
  