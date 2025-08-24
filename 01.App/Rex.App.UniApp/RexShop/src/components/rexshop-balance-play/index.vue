<template>
  <view class="balance-play-body">
    <up-modal
      width="300rpx"
      height="200rpx"
      :show="isPaymenting"
      :showConfirmButton="false"
      :showCancelButton="false"
    >
      <view class="play-wait-item">
        <up-loading-icon
          color="green"
          text="支付中，请稍等！"
          textSize="24rpx"
          :vertical="true"
        ></up-loading-icon>
      </view>
    </up-modal>
  </view>
</template>

<script setup lang="ts" name="rexShopBalancePlay">
import { ref, defineExpose } from "vue";
import { http } from "@/utils/http";

// 是否正在支付
const isPaymenting = ref(false);

// 获取余额支付信息
const callPayment = (uPay: UserPayType) => {
  let errMsg = "调用余额支付异常，请稍后重试！";
  http<BillPaymentDetailType>({
    url: "/api/front/aggregation/payment/balance",
    method: "POST",
    data: uPay,
  })
    .then((result) => {
      if (!result) {
        closeBalancempPlay();
        console.error(errMsg);
        return;
      }
      goPaymentResult(result.no, result.type);
    })
    .catch((err) => {
      closeBalancempPlay();
      console.error(errMsg, err);
    });
};

// 跳转到支付结果页面
const goPaymentResult = (no: string, type: number) => {
  closeBalancempPlay();
  uni.redirectTo({
    url: `/pages/payment/result?BillPaymentNo=${no}&playType=${type}`,
  });
};

// 显示错误的信息
const showErrorToast = (errMsg: string, isClose: boolean = true) => {
  uni.showToast({
    icon: "none",
    title: errMsg,
    duration: 4 * 1000,
  });
  if (isClose) closeBalancempPlay();
};

// 显示余额支付
const showBalancePlay = (uPay: UserPayType) => {
  uni.showModal({
    title: "余额支付",
    content: "您确定要支付吗？",
    success: function (res) {
      if (res.confirm) {
        isPaymenting.value = true;
        callPayment(uPay);
      }
    },
  });
};

// 关闭余额支付
const closeBalancempPlay = () => {
  isPaymenting.value = false;
};

// 暴露变量
defineExpose({
  showBalancePlay,
  closeBalancempPlay,
});
</script>
