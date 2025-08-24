<template>
  <view class="order-payment-body">
    <view class="payment-loading-box" v-if="loading">
      <up-loading-icon
        text="正在生成支付单，请稍等..."
        size="30"
        :vertical="true"
      ></up-loading-icon>
    </view>
    <view class="payment-content-box" v-show="!loading">
      <rexshop-order-payment-list
        v-if="playType == 1 && orderNo"
        ref="rexShopOrderPaymentListRef"
        :orderNo="orderNo"
        @completed="orderCompleted"
      />
      <rexshop-recharge-payment-list
        v-else-if="playType == 2 && recharge > 0"
        ref="rexShopRechargePaymentListRef"
        :recharge="recharge"
        @completed="rechargeCompleted"
      />
      <!-- 支付 -->
      <view class="payment-amount-box" v-if="!loading">
        <up-button type="success" text="确认付款" @click="submitPlay" />
      </view>
    </view>

    <!-- 微信[小程序]支付 -->
    <rexshop-wxmp-play ref="rexShopWxmpPlayRef" />

    <!-- 余额支付 -->
    <rexshop-balance-play ref="rexShopBalancePlayRef" />

    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, computed } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 定义变量
const uToastRef = ref();
const rexShopWxmpPlayRef = ref();
const rexShopBalancePlayRef = ref();
const rexShopOrderPaymentListRef = ref();
const rexShopRechargePaymentListRef = ref();
const userOrder = ref<OrderType>();
const orderNo = ref("");
const playType = ref(0);
const recharge = ref(0);

const userPayment = reactive<UserPayType>({
  sourceIds: [],
  paymentCode: "WechatPay",
  paymentType: 1,
  params: {},
});

// 是否正在加载数据
const loading = computed(() => {
  let result = true;
  if (playType.value == 1 && userOrder && userOrder.value && userOrder.value.no) {
    result = false;
  } else if (playType.value == 2 && recharge.value > 0) {
    result = false;
  }
  return result;
});

// 充值加载完成
const rechargeCompleted = (recharge: number) => {
  userPayment.paymentType = 2;
  userPayment.params = {
    Money: recharge,
  };
  var currUser = useUserLoginStore().currentSysUser;
  if (!currUser) return;
  userPayment.sourceIds = [currUser.id];
};

// 订单加载完成
const orderCompleted = (uOrder: OrderType, paymentCode: string) => {
  userPayment.paymentType = 1;
  userPayment.paymentCode = paymentCode;
  userOrder.value = uOrder;
  userPayment.sourceIds = [userOrder.value.id];
};

// 确认支付
const submitPlay = () => {
  if (userPayment.paymentCode == "WechatPay") {
    rexShopWxmpPlayRef.value.showWxmpPlay(userPayment);
  } else if (userPayment.paymentCode == "BalancePay") {
    rexShopBalancePlayRef.value.showBalancePlay(userPayment);
  }
};

onLoad((event: any) => {
  if (!event.playType) {
    uni.navigateBack();
    return;
  }
  if (event.playType) playType.value = event.playType;
  if (event.recharge) recharge.value = Number(event.recharge);
  if (event.orderNo) orderNo.value = event.orderNo;
  if (event.paymentType) userPayment.paymentType = Number(event.paymentType);
});
</script>

<style lang="scss" scoped>
.order-payment-body {
  padding-bottom: 80rpx;
  .payment-loading-box {
    margin-top: 25%;
  }
  .payment-amount-box {
    position: fixed;
    bottom: 30rpx;
    width: 92%;
    margin-top: 40rpx;
    padding: 0rpx 30rpx;
  }
}
</style>
