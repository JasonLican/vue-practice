<template>
 <div class="comment">
    <ul>
      <li v-for="(_item, index) in 4" :key="index">
        <div class="item">
          <img src="@/assets/images/logo.svg" />
          <div class="item-content">
            <span class="title">某某某</span>
            <span class="time">2021-12-31</span>
            <span class="text">
              我个人认为，这个意大利面就应该拌42号混凝土，因为这个螺丝钉的长度，它很容易会直接影响到挖掘机的扭矩你知道吧，你往里砸的时候，一瞬间它就会产生大量的高能蛋白，俗称ufo，会严重影响经济的发展，甚至对整个太平洋以及充电器都会造成一定的核污染，你知道啊？再者说，根据这个勾股定理，你可以很容易地推断出人工饲养的东条英机它是可以捕获野生的三角函数的，所以说这个秦始皇的切面是否具有放射性啊，特朗普的N次方是否含有沉淀物，都不影响这个沃尔玛跟维尔康在南极会合。
            </span>
          </div>
        </div>
        <el-divider v-if="index != 3" border-style="double" />
      </li>
    </ul>
  </div>
</template>
<style lang="less" scoped>
 .comment {
    width: 100%;
    height: 410px;
    overflow-y: auto;

    .item {
      display: flex;

      img {
        width: 50px;
        height: 50px;
        border-radius: 50%;
      }

      .item-content {
        display: flex;
        flex-direction: column;
        margin-left: 16px;

        .title {
          font-size: var(--font-size-medium);
        }

        .time {
          margin: 5px 0;
          font-size: var(--font-size-extra-small);
        }

        .text {
          display: -webkit-box;
          overflow: hidden;
          text-overflow: ellipsis;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
        }
      }
    }
  }
</style>