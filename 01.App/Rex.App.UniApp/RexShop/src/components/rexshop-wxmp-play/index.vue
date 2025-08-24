<template>
  <view class="wx-play-body">
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

<script setup lang="ts" name="rexShopWxPlay">
import { ref, defineExpose } from "vue";
import { http } from "@/utils/http";

// 是否正在支付
const isPaymenting = ref(false);

// 获取微信[小程序]支付信息
const callPayment = (uPay: UserPayType) => {
  let errMsg = "调用微信支付异常，请稍后重试！";
  http<WeChatPayParameterResultType>({
    url: "/api/front/aggregation/payment/wxmp",
    method: "POST",
    data: uPay,
  })
    .then((result) => {
      if (!result) {
        showErrorToast(errMsg);
        return;
      }
      if (result.detail.money <= 0) {
        goPaymentResult(result.detail.no, result.detail.type);
        return;
      }
      requestPayment(result);
    })
    .catch((err) => {
      showErrorToast(errMsg);
      console.error(err);
    });
};

// 调用微信支付请求
const requestPayment = (payParameterResult: WeChatPayParameterResultType) => {
  let errMsg = "调用微信支付异常，请稍后重试！";
  const payParam = payParameterResult.payParameter;
  uni.requestPayment({
    provider: "wxpay",
    timeStamp: String(payParam.timeStamp),
    nonceStr: payParam.nonceStr,
    package: payParam.package,
    signType: payParam.signType,
    paySign: payParam.paySign,
    orderInfo: {},
    success(res) {
      if (res.errMsg === "requestPayment:ok") {
        goPaymentResult(payParameterResult.detail.no, payParameterResult.detail.type);
        return;
      }
      showErrorToast(errMsg);
    },
    fail(err) {
      if (err.errMsg === "requestPayment:fail cancel") {
        errMsg = "您已取消支付。";
      }
      showErrorToast(errMsg);
      console.error(err);
    },
  });
};

// 跳转到支付结果页面
const goPaymentResult = (no: string, type: number) => {
  closeWxmpPlay();
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
  if (isClose) closeWxmpPlay();
};

// 显示微信[小程序]支付
const showWxmpPlay = (uPay: UserPayType) => {
  isPaymenting.value = true;
  callPayment(uPay);
};

// 关闭微信[小程序]支付
const closeWxmpPlay = () => {
  isPaymenting.value = false;
};

// 暴露变量
defineExpose({
  showWxmpPlay,
  closeWxmpPlay,
});
</script>
