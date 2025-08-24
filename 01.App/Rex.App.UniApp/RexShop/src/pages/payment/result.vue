<template>
  <view class="payment-result-body">
    <view class="payment-result-image">
      <view class="pay-img-item">
        <up-image
          width="240rpx"
          height="240rpx"
          bgColor="white"
          :show-loading="true"
          :src="payResult.url"
        ></up-image>
      </view>
      <view class="payment-result-desc">{{
        payResult.description || "查询中，请稍等！"
      }}</view>
    </view>
    <view class="order-detail-box" v-show="payResult.showDetail">
      <template v-if="payResult.playType == 1">
        <view class="order-detail-content">
          <view class="order-title">订单详情</view>
          <view class="order-detail-item">
            订单编号：{{ paymentOrderDetail.orderNo }}
          </view>
          <view class="order-detail-item">
            下单时间：{{ paymentOrderDetail.creationTime }}
          </view>
          <view class="order-detail-item">
            支付金额：<text style="color: #f56c6c"
              >￥{{ paymentOrderDetail.money }}</text
            ></view
          >
          <view class="order-detail-item">
            支付方式：{{ formatPaymentType(paymentOrderDetail.paymentCode) }}</view
          >
        </view>
      </template>
      <template v-if="payResult.playType == 2">
        <view class="order-detail-content">
          <view class="order-title">支付详情</view>
          <view class="order-detail-item">
            支付单号：{{ paymentOrderDetail.paymentNo }}
          </view>
          <view class="order-detail-item">
            支付时间：{{ paymentOrderDetail.creationTime }}
          </view>
          <view class="order-detail-item">
            支付金额：<text style="color: #f56c6c"
              >￥{{ paymentOrderDetail.money }}</text
            ></view
          >
          <view class="order-detail-item">
            支付方式：{{ formatPaymentType(paymentOrderDetail.paymentCode) }}</view
          >
        </view>
      </template>
    </view>
    <view class="order-group-btns" v-show="payResult.showBtn">
      <up-row>
        <up-col span="6">
          <view class="order-btn">
            <up-button
              v-if="payResult.playType == 1"
              type="error"
              text="查看订单"
              @click="goOrderDetail"
            ></up-button>
            <up-button
              v-else-if="payResult.playType == 2"
              type="error"
              text="查看余额"
              @click="goBalanceDetail"
            ></up-button>
          </view>
        </up-col>
        <up-col span="6">
          <view class="order-btn">
            <up-button
              type="error"
              :plain="true"
              text="返回"
              @click="payResultBack"
            ></up-button>
          </view>
        </up-col>
      </up-row>
    </view>
  </view>
</template>

<script setup lang="ts">
import { reactive } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { formatPaymentType } from "@/utils/other";
import paySuccess from "@/static/images/payment/pay-success.png";
import payFail from "@/static/images/payment/pay-fail.png";

// 定义变量
const payResult = reactive({
  playType: 1,
  showDetail: false,
  showBtn: false,
  url: "/static/images/payment/wait-pay.png",
  description: "",
});
const paymentOrderDetail = reactive<PaymentOrderDetailType>({} as PaymentOrderDetailType);

// 查询支付单详情
const loadPaymentDetail = (no: string) => {
  http<PaymentOrderDetailType>({
    url: "/api/front/aggregation/payment/detail/" + no,
    method: "GET",
  })
    .then((result) => {
      if (result) {
        if (result.paymentType == 2 || result.paymentCode == "BalancePay") {
          refreshUserInfo(20 * 1000);
        }
        Object.assign(paymentOrderDetail, result);
        payResult.description = "您已支付成功！";
        payResult.url = paySuccess;
        payResult.showDetail = true;
      } else {
        payResult.description = "支付失败，请稍后重试！";
        payResult.url = payFail;
        payResult.showDetail = false;
      }
      payResult.showBtn = true;
    })
    .catch((err) => {
      console.error(err);
    });
};

// 查看余额
const goBalanceDetail = () => {
  uni.navigateBack({
    delta: 2,
  });
};

// 查看订单详情
const goOrderDetail = () => {
  uni.redirectTo({
    url: "/pages/member/order/detail?orderId=" + paymentOrderDetail.orderId,
  });
};

// 支付返回
const payResultBack = () => {
  uni.navigateBack();
};

// 刷新用户
const refreshUserInfo = (delay: number) => {
  useUserLoginStore().refreshCurrentSysUser(delay);
};

onLoad((event: any) => {
  if (!event.BillPaymentNo) {
    payResultBack();
    return;
  }
  payResult.playType = event.playType; // 1：订单、2：充值
  loadPaymentDetail(event.BillPaymentNo);
});
</script>

<style lang="scss">
body,
page {
  background-color: white !important;
}
</style>
<style lang="scss" scoped>
.payment-result-body {
  .payment-result-image {
    padding-top: 50rpx;
    padding-bottom: 20rpx;
    background-color: white;
    text-align: center;
    .pay-img-item {
      display: inline-block;
    }
    .payment-result-desc {
      background-color: white;
      font-size: 34rpx;
      color: #2d2d2d;
      text-align: center;
    }
  }
  .order-detail-box {
    background-color: white;
    padding: 20rpx 100rpx;
    .order-detail-content {
      box-shadow: 0px 0px 10px 0 rgba(0, 0, 0, 0.3);
      border-radius: 10rpx;
      color: #777777;
      .order-title {
        height: 50rpx;
        line-height: 50rpx;
        font-size: 30rpx;
        padding: 15rpx;
        border-bottom: 1px #777777 dashed;
      }
      .order-detail-item {
        height: 45rpx;
        line-height: 45rpx;
        font-size: 28rpx;
        padding: 0rpx 15rpx;
        margin: 10rpx auto;
      }
    }
  }
  .order-group-btns {
    background-color: white;
    .order-btn {
      margin-top: 50rpx;
      padding: 0rpx 30rpx;
    }
  }
}
</style>
