<template>
  <view class="textarea-body bg-white">
    <view class="textarea u-margin-bottom-20">
      <rexshop-content
        :content="rexShopData.parameters.value"
        v-if="rexShopData.parameters.value"
      />
    </view>
  </view>
</template>

<script setup lang="ts" name="rexShopTextarea">
import { defineProps } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => {},
  },
});
</script>

<style lang="scss" scoped>
.textarea-body {
  .textarea {
    background-color: #fff;
    padding: 10upx 26upx;
    p {
      img {
        width: 100% !important;
        background-color: #000;
      }
    }
    div {
      background-color: #000;
    }
  }
}
</style>
