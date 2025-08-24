<template>
  <view class="coupon-body" v-if="couponList">
    <template v-for="(coupon, key) in couponList" :key="key">
      <view class="coupon-box">
        <view class="coupon-item">
          <up-row>
            <up-col span="4">
              <up-image
                width="100px"
                height="75px"
                src="/static/images/coupon/coupon-element.png"
              />
            </up-col>
            <up-col span="8">
              <view class="coupon-content">
                <view class="coupon-name">{{ coupon.name }}</view>
                <view class="coupon-result">
                  优惠方式：<text
                    v-for="(couponResult, index) in coupon.promotionResults"
                    :key="index"
                    >{{ getResultMsg(couponResult.code, couponResult.parameters) }}</text
                  >
                </view>
                <view class="coupon-time"
                  >领取时间：{{ formatDate(new Date(coupon.startTime), "YYYY-mm-dd") }} 至
                  {{ formatDate(new Date(coupon.endTime), "YYYY-mm-dd") }}
                </view>
              </view>
              <view class="coupon-btn">
                <u-button
                  type="success"
                  shape="circle"
                  size="mini"
                  @click="receiveCoupon(coupon.id, key)"
                  >立即领取</u-button
                >
              </view>
            </up-col>
          </up-row>
        </view>
        <view class="coupon-conditions" v-if="coupon.promotionConditions">
          <template
            v-for="(couponCondition, index) in coupon.promotionConditions"
            :key="index"
          >
            <up-tag
              type="warning"
              plain
              plainFill
              size="mini"
              :text="getConditionMsg(couponCondition.code, couponCondition.parameters)"
            />&nbsp;
          </template>
        </view>
      </view>
      <view
        class="bar-line"
        v-if="couponList.length > 1 && key !== couponList.length - 1"
      ></view>
    </template>
    <view class="coupon-empty-data" v-show="!couponList || couponList.length < 1">
      <u-empty mode="list" text="无优惠劵信息." />
    </view>
    <!-- #ifdef MP-WEIXIN -->
    <rexshop-login-mp v-if="useUserLoginStore().showMpLoginTip" />
    <!-- #endif -->
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { formatDate } from "@/utils/formatTime";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { getResultMsg, getConditionMsg } from "@/utils/other";

// 优惠劵项
const uToastRef = ref();
const couponList = ref<CouponType[]>();

onLoad(() => {
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<CouponType[]>({
    url: "/api/promotion/common/receive-coupon",
    method: "GET",
    data: {
      limit: 50,
    },
  }).then((result) => {
    couponList.value = result;
    uni.hideLoading();
  });
});

// 触发了优惠卷
const receiveCoupon = (couponId: string, index: number) => {
  if (!useUserLoginStore().hasLogin) {
    useUserLoginStore().showAuthLogin();
    return;
  }
  http<boolean>({
    method: "POST",
    url: "/api/promotion/promotion-global/user-receive-coupon/" + couponId,
  })
    .then((result) => {
      if (!result) return;
      uToastRef.value.success("优惠卷领取成功！");
    })
    .catch((err: any) => {
      console.error("领取优惠卷出错", err);
    });
};
</script>
<style lang="scss" scoped>
.coupon-body {
  overflow-y: auto;
  padding-top: 5rpx;
  .coupon-box {
    margin-bottom: 20rpx;
    background-color: white;
    border: 1px #e54d42 dotted;
    .coupon-item {
      background-color: #fff5f5;
      padding: 20rpx;
      border-top-left-radius: 5rpx;
      border-top-right-radius: 5rpx;
      .coupon-content {
        line-height: 45rpx;
        .coupon-name {
          width: 480rpx;
          color: #e54d42;
          overflow: hidden;
          white-space: nowrap;
          text-overflow: ellipsis;
          font-size: 30rpx;
        }
        .coupon-result,
        .coupon-time {
          color: #999898;
          font-size: 24rpx;
        }
      }
    }

    .bar-line {
      height: 10rpx;
    }

    .coupon-conditions {
      display: flex;
      background-color: #ffeced;
      padding: 8rpx;
      border-top-left-radius: 5rpx;
      border-top-right-radius: 5rpx;
    }
  }
  .coupon-empty-data {
    margin-top: 100rpx;
    text-align: center;
  }
}
</style>
