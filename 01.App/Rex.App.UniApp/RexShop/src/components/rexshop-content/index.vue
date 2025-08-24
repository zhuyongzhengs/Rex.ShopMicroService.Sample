<template>
  <view class="u-content">
    <u-parse :content="content" :selectable="true" />
  </view>
</template>
<script setup lang="ts" name="rexShopContent">
import { defineProps } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  content: {
    type: String,
    default: () => null,
  },
});
</script>
<style lang="scss" scoped>
.u-content {
  color: $u-content-color;
  font-size: 26rpx;
  line-height: 1.8;
}
.u-content p {
  color: $u-tips-color;
}
</style>
