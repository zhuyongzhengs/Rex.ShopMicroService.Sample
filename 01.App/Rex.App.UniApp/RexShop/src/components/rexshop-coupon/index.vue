<template>
  <view class="coupon-body" v-if="couponItem.list.length > 0">
    <template v-for="(coupon, key) in couponItem.list" :key="key">
      <view class="coupon-box">
        <view
          class="coupon-item"
          :style="
            'border-top-left-radius:' + radius + ';border-top-right-radius:' + radius
          "
        >
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
        <!--
		  <view
			class="coupon-conditions"
			:style="
			  'border-bottom-left-radius:' + radius + ';border-bottom-right-radius:' + radius
			"
			v-if="coupon.promotionConditions"
		  >
			<u-tag
			  type="warning"
			  plain
			  plainFill
			  size="mini"
			  class="u-margin-right-15"
			  v-for="(couponCondition, index) in coupon.promotionConditions" :key="index"
			  :text="getConditionMsg(couponCondition.code, couponCondition.parameters)"
			/>
		  </view>
		  -->
      </view>
      <view
        class="bar-line"
        v-if="couponItem.list.length > 1 && key !== couponItem.list.length - 1"
      ></view>
    </template>
    <!-- #ifdef MP-WEIXIN -->
    <rexshop-login-mp v-if="useUserLoginStore().showMpLoginTip" />
    <!-- #endif -->
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts" name="rexShopCoupon">
import { ref, onMounted, defineProps } from "vue";
import { formatDate } from "@/utils/formatTime";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { getResultMsg, getConditionMsg } from "@/utils/other";

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => {},
  },
  radius: {
    type: String,
    default: () => "0rpx",
  },
});

// 优惠劵项
const uToastRef = ref();
const couponItem = ref({
  list: [] as any,
});

// 加载优惠劵项
const loadCouponItem = () => {
  couponItem.value.list = props.rexShopData.parameters.list;
};

// 页面加载完时
onMounted(() => {
  loadCouponItem();
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
      // couponItem.value.list.splice(index, 1);
    })
    .catch((err: any) => {
      console.error("领取优惠卷出错", err);
    });
};
</script>
<style lang="scss" scoped>
.coupon-item {
  background-color: #fff5f5;
  padding: 20rpx;
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
  background-color: #ffeced;
  padding: 8rpx;
}
</style>
