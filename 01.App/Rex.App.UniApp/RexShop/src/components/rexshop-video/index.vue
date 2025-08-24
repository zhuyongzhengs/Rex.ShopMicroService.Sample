<template>
  <view class="video-body video" :style="'border-radius: ' + radius">
    <video
      class="video-item"
      :src="rexShopData.parameters.list[0].url"
      :autoplay="rexShopData.parameters.autoplay"
      :poster="rexShopData.parameters.list[0].image"
    />
  </view>
</template>

<script setup lang="ts" name="rexShopVideo">
import { defineProps } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => {},
  },
  radius: {
    type: String,
    default: () => "0rpx",
  },
});
</script>

<style lang="scss" scoped>
.video {
  .video-item {
    width: 100%;
    min-height: 200upx;
  }
}
</style>
