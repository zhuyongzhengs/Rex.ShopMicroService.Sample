<template>
  <view class="recharge-play-body">
    <view class="recharge-payment-detail">
      <u-cell-group>
        <u-cell title="充值金额" :isLink="false" :titleStyle="cellItemTitleStyle">
          <template #value>
            <view class="recharge-cell-item">
              <view class="product-price">{{ recharge }}</view>
            </view>
          </template>
        </u-cell>
        <!-- #ifdef MP -->
        <u-cell title="付款方式" :isLink="false" :titleStyle="cellItemTitleStyle">
          <template #value>
            <view class="recharge-cell-item">
              <!-- #ifdef MP-WEIXIN -->
              微信支付
              <!-- #endif -->
              <!-- #ifdef MP-ALIPAY -->
              支付宝支付
              <!-- #endif -->
            </view>
          </template>
        </u-cell>
        <!-- #endif -->
      </u-cell-group>
    </view>
  </view>
</template>

<script setup lang="ts" name="rechargePayList">
import { ref, onMounted, defineProps, nextTick } from "vue";

// 定义父组件传过来的值
const props = defineProps({
  recharge: {
    type: String,
    default: () => "",
  },
});

// 定义子组件向父组件传值/事件
const emit = defineEmits(["completed"]);

// 定义变量
const cellItemTitleStyle = ref({
  fontSize: "28rpx",
  color: "#8799a3",
});

onMounted(() => {
  nextTick(() => {
    emit("completed", Number(props.recharge));
  });
});
</script>
<style lang="scss" scoped>
.recharge-play-body {
  .recharge-payment-detail {
    background-color: white;
    .recharge-cell-item {
      font-size: 28rpx;
      color: #8799a3;
    }
  }
}
</style>
