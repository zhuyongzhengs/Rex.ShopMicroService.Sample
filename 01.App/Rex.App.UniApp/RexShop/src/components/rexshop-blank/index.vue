<template>
  <view class="bg-white">
    <u-gap
      :height="rexShopData.parameters.height"
      :bg-color="rexShopData.parameters.backgroundColor"
    />
  </view>
</template>

<script setup lang="ts" name="rexShopBlank">
import { onMounted, defineProps } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => {},
  },
});

// 页面加载完时
onMounted(() => {
  // console.log(props.rexShopData);
});
</script>

<style scoped lang="scss"></style>
