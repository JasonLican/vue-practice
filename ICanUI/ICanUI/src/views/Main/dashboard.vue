<template>
  <div>
    <el-card>
      <div>this is a information,也不知道说点啥，佛主保佑，世界和平</div>
    </el-card>
    <el-row :gutter="20" class="mgb20">
      <el-col :span="6">
        <el-card
          shadow="hover"
          body-class="card-body"
          :body-style="{ display: 'flex', padding: 0 }"
        >
          <el-icon class="card-icon bg1"><User /></el-icon>
          <div class="card-content">
            <span class="num">123</span>
            <span class="txt">用户数量</span>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card
          shadow="hover"
          body-class="card-body"
          :body-style="{ display: 'flex', padding: 0 }"
        >
          <el-icon class="card-icon bg2"><ChatDotRound /></el-icon>
          <div class="card-content">
            <span class="num">1</span>
            <span class="txt">系统消息</span>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card
          shadow="hover"
          body-class="card-body"
          :body-style="{ display: 'flex', padding: 0 }"
        >
          <el-icon class="card-icon bg3"><Goods /></el-icon>
          <div class="card-content">
            <span class="num">200</span>
            <span class="txt">商品数量</span>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card
          shadow="hover"
          body-class="card-body"
          :body-style="{ display: 'flex', padding: 0 }"
        >
          <el-icon class="card-icon bg4"><ShoppingCartFull /></el-icon>
          <div class="card-content">
            <span class="num">1000</span>
            <span class="txt">今日订单量</span>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>

  <div style="margin-top: 20px;">
    <el-row :gutter="20" class="enter-y">
      <el-col
        v-for="(item, index) in speedList"
        :key="index"
        :xs="24"
        :sm="24"
        :md="6"
        :lg="6"
        :xl="6"
      >
        <el-card class="box-card">
          <template #header>
            <div class="card-header cursor">
              <span>{{ item.title }}</span>
              <el-icon><ArrowRight /></el-icon>
            </div>
          </template>
          <div class="card-content">
            <div class="numerical-value">
              <span class="number">{{ item.online }}/{{ item.total }}</span>
              <span>Online/Total</span>
            </div>
            <el-progress
              :text-inside="true"
              :stroke-width="26"
              :percentage="value(item.online, item.total)"
            />
          </div>
        </el-card>
      </el-col>
    </el-row>
    <el-row :gutter="20" class="enter-y">
      <el-col :xs="24" :sm="24" :md="18" :lg="18" :xl="18">
        <el-card class="box-card">
          <template #header>
            <div class="card-header cursor">
              <span>任务数据</span>
            </div>
          </template>
          <AnalysisChart />
        </el-card>
      </el-col>
      <el-col :xs="24" :sm="24" :md="6" :lg="6" :xl="6">
        <el-card class="box-card">
          <template #header>
            <div class="card-header cursor">
              <span>任务数据</span>
            </div>
          </template>
          <PieChart />
        </el-card>
      </el-col>
    </el-row>
    <el-row :gutter="20" class="enter-y">
      <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
        <el-card class="box-card">
          <template #header>
            <div class="card-header cursor">
              <span>评论列表</span>
            </div>
          </template>
          <Comment />
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts" setup>
import PieChart from '../../components/dashboard/PieChart.vue';
import AnalysisChart from '../../components/dashboard/AnalysisChart.vue';
import Comment from '../../components/dashboard/Comment.vue';
defineOptions({
    name: 'RtWelcome',
  });

  const speedList = [
    {
      title: '待办事项',
      online: 24,
      total: 70,
    },
    {
      title: '待办任务',
      online: 39,
      total: 100,
    },
    {
      title: '目标计划',

      online: 5,
      total: 10,
    },
    {
      title: '评论回复',
      online: 10,
      total: 40,
    },
  ];

  const value = (online: number, total: number) => {
    return Math.round((online / total) * 100);
  };
</script>

<style lang="less" scoped>
.mgb20{
  margin-top:10px ;
}

.card-body{
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  .el-card{
    width: 32%;
    margin-bottom: 20px;
  }
  .el-icon{
    width: 80px;
    height: 80px;
    font-size: 30px;
    text-align: center;
    line-height: 80px;
    color: #fff;
  }
  .card-content{
    margin-left: 15px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    .num{
      margin-bottom: 10px;
    }
    .txt{
      font-size: 11px;
      text-align: center;
      color: #999;
    }
  }

  .bg1{
    background: #2d8cf0;
  }
  .bg2{
    background: #64d572;
  }
  .bg3{
    background: #f25e43;
  }
  .bg4{
    background: #e9a745;
  }
}

.box-card {
    margin-bottom: 20px;

    :deep(.el-card__header) {
      padding-bottom: 0;
      border: none;
    }

    .card-header {
      display: flex;
      align-items: center;
      justify-content: space-between;
      font-weight: 600;
    }

    .card-content {
      :deep(.el-progress-bar__outer) {
        height: 17px !important;
      }

      .numerical-value {
        display: flex;
        align-items: flex-end;
        justify-content: space-between;
        margin-bottom: 10px;

        .number {
          color: var(--text-color-primary);
          font-size: var(--font-size-extra-large);
          font-weight: 600;
        }
      }
    }
  }
</style>