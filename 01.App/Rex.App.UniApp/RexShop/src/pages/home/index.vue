<template>
  <view class="home-body">
    <!--无网络组件-->
    <u-no-network></u-no-network>

    <!--头部组件-->
    <u-navbar
      :leftIcon="false"
      :title="navItem.homeTitle"
      :bgColor="navItem.background.backgroundColor"
      :title-color="navItem.titleColor"
      :titleStyle="{ color: navItem.titleColor }"
    />

    <!--首页模块化组合组件-->
    <rexshop-home v-if="isPageData" :rexShopData="pageLayout.pageData" />
    <up-back-top :scroll-top="scrollTop" :duration="500"></up-back-top>

    <!--提示框组件-->
    <u-toast ref="uToast" />
  </view>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import { onLoad, onPullDownRefresh, onPageScroll } from "@dcloudio/uni-app";
import { mainNabBar } from "@/utils/theme";
import { http } from "@/utils/http";

const scrollTop = ref(0);
// 头部
const navItem = ref({
  homeTitle: "Rex商城平台",
  background: mainNabBar.background,
  titleColor: mainNabBar.titleColor,
});

// 页面布局
const pageLayout = ref({
  layout: 1, // 布局样式：1、手机端  2、PC端
  pageCode: "", // 布局编码：空[表示获取默认布局]
  pageData: [], // 布局数据
});

// 是否包含页面布局数据
const isPageData = computed(() => {
  return pageLayout.value.pageData.length > 0;
});

// 获取页面布局
const getPageLayoutAsync = async () => {
  uni.showLoading({
    title: "页面加载中",
  });
  const layoutData = await http<any>({
    method: "GET",
    url: `/api/front/aggregation/common/pageLayout/${pageLayout.value.layout}`,
    data: {
      pageCode: pageLayout.value.pageCode,
    },
  });
  pageLayout.value.pageData = layoutData.items;
};

onPageScroll((e) => {
  scrollTop.value = e.scrollTop;
});

// 监听下拉刷新
onPullDownRefresh(async () => {
  await getPageLayoutAsync();
  uni.stopPullDownRefresh();
  console.log("【首页】下拉刷新，重新加载数据！");
});

// 数据加载
onLoad(async () => {
  // 获取页面布局信息
  await getPageLayoutAsync();
});
</script>

<style>
.home-body {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.logo {
  height: 200rpx;
  width: 200rpx;
  margin-top: 200rpx;
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 50rpx;
}

.text-area {
  display: flex;
  justify-content: center;
}

.title {
  font-size: 36rpx;
  color: #8f8f94;
}
</style>
