<template>
  <view class="rex-head-container" :style="'margin-top: ' + headMarginTop + 'px'">
    <block v-for="(item, index) in rexShopData" :key="index">
      <!-- 搜索 -->
      <rexshop-search
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'search'"
      />

      <!-- 公告 -->
      <rexshop-notice
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'notice'"
      />

      <!-- 图片轮播(幻灯片) -->
      <rexshop-img-slide
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'imgSlide'"
      />

      <!-- 优惠劵 -->
      <rexshop-coupon
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'coupon'"
      />

      <!-- 辅助空白 -->
      <rexshop-blank
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        :rexShopData="item"
        v-if="item.widgetCode == 'blank'"
      />

      <!-- 多行文本输入区 -->
      <rexshop-textarea
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        :rexShopData="item"
        v-if="item.widgetCode == 'textarea'"
      />

      <!-- 视频 -->
      <rexshop-video
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'video'"
      />

      <!-- 图片 -->
      <rexshop-img-single
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'imgSingle'"
      />

      <!-- 图片分组 -->
      <rexshop-img-window
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'imgWindow'"
      />

      <!-- 商品 -->
      <rexshop-goods
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'goods'"
      />

      <!-- 商品选项卡 -->
      <rexshop-good-tab-bar
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'goodTabBar'"
      />

      <!-- 文章 -->
      <rexshop-article
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'article'"
      />

      <!-- 文章分类 -->
      <rexshop-article-classify
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'articleClassify'"
      />

      <!-- 导航组 -->
      <rexshop-nav-bar
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'navBar'"
      />

      <!-- 团购秒杀 -->
      <rexshop-group-purchase
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'groupPurchase'"
      />

      <!-- 浏览记录 -->
      <view :style="`width: ${winWidth};`" v-if="item.widgetCode == 'record'"></view>

      <!-- 拼团 -->
      <rexshop-pin-tuan
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'pinTuan'"
      />

      <!-- 商品服务 -->
      <rexshop-service
        :style="`width: ${winWidth};`"
        class="rex-component-item"
        radius="10rpx"
        :rexShopData="item"
        v-if="item.widgetCode == 'service'"
      />
    </block>
  </view>
</template>

<script setup lang="ts">
import { defineProps, onMounted } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => [],
  },
});
const headMarginTop = ref(44);
const winWidth = ref("100%");

onMounted(() => {
  // #ifdef MP-WEIXIN
  let winInfo = uni.getWindowInfo();
  headMarginTop.value += winInfo.statusBarHeight + 8;
  winWidth.value = `calc(${winInfo.windowWidth}px - 30rpx);`;
  // #endif
});
</script>

<style scoped lang="scss">
.rex-head-container {
  width: 100%;
  // margin-top: 90rpx;

  .rex-component-item {
    margin-bottom: 15rpx;
    display: block;
  }
}
</style>
