<template>
  <view class="content">
    <view class="pinia-item">
      <text>【Pinia 测试】{{ counter.count }}</text>
      <u-button type="primary" @click="onCount">数量+1</u-button>
    </view>
    <view class="pinia-item">
      <text>【Pinia-Persist 测试】{{ counterPersist.countVal }}</text>
      <u-button type="primary" @click="onCountPersist">数量+1</u-button>
    </view>
    <view class="http-item">
      <u-button type="primary" @click="onTestHttp">请求数据</u-button>
      <text>{{ httpData }}</text>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onPullDownRefresh, onReachBottom } from "@dcloudio/uni-app";

import { http } from "@/utils/http";
import { useCounterStore } from "@/stores/test/counter";
import { useCounterPersistStore } from "@/stores/test/counterPersist";
const counter = useCounterStore();
const counterPersist = useCounterPersistStore();

// 数量+1
const onCount = () => {
  /*
  counter.count++;
  // 自动补全！
  counter.$patch({ count: counter.count + 1 });
  */
  // 使用 action 方式
  counter.increment();
};

// 数量+1
const onCountPersist = () => {
  counterPersist.countVal++;
};

// 定义变量 Data
const httpData = ref("");
// 请求数据
const onTestHttp = async () => {
  const data = await http<string>({
    method: "GET",
    url: "/api/account/test",
    data: {
      isError: false,
    },
  });
  httpData.value = data;
};

// 监听下拉刷新
onPullDownRefresh(async () => {
  await onTestHttp();
  uni.stopPullDownRefresh();
  console.log("下拉刷新");
});

// 监听上拉加载
onReachBottom(() => {
  console.log("下拉刷新");
});
</script>

<style lang="scss" scoped>
.content {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  .pinia-item {
    width: 100%;
    background-color: lightskyblue;
    margin-bottom: 40rpx;

    text {
      width: 100%;
      text-align: center;
      padding: 20rpx;
      display: inline-block;
    }
  }
}
</style>
