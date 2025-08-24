<template>
  <view :class="isFixedTop ? 'status-bar-box' : ''">
    <u-gap :height="viewHeight" :bg-color="bgColor" />
  </view>
</template>

<script setup lang="ts" name="rexShopBlank">
import { mainNabBar } from "@/utils/theme";

// 定义父组件传过来的值
const props = defineProps({
  bgColor: {
    type: String,
    default: () => mainNabBar.background.backgroundColor,
  },
  isFixedTop: {
    type: Boolean,
    default: () => true,
  },
});

const viewHeight = ref(uni.getWindowInfo().statusBarHeight);
</script>

<style scoped lang="scss">
.status-bar-box {
  position: fixed;
  width: 100%;
  top: 0;
  z-index: 999999;
}
</style>
