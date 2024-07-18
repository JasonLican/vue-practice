<template>
  <div ref="chartRef" class="chart-ref"></div>
</template>

<script lang="ts" setup>
import { onMounted, ref } from "vue";
import * as echarts from "echarts";
const chartRef = ref<HTMLDivElement | null>(null);
onMounted(() => {
  const mychart = echarts.init(chartRef.value);
  mychart.setOption({
    title: {
      subtext: "Fake Data",
      left: "center",
    },
    tooltip: {
      trigger: "item",
    },
    legend: {
      orient: "vertical",
      left: "right",
    },
    series: [
      {
        name: "Access From",
        type: "pie",
        radius: "50%",
        data: [
          { value: 1048, name: "Search Engine" },
          { value: 735, name: "Direct" },
          { value: 580, name: "Email" },
          { value: 484, name: "Union Ads" },
          { value: 300, name: "Video Ads" },
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: "rgba(0, 0, 0, 0.5)",
          },
        },
      },
    ],
  });
});
</script>
<style lang="less">
.chart-ref {
  width: 100%;
  height: 362px;
}
</style>