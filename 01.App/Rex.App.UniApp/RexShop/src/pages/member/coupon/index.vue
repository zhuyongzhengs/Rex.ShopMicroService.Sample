<template>
  <view class="coupon-body">
    <view class="coupon-tabs-box">
      <up-tabs
        lineColor="#f56c6c"
        :list="couponStatusList"
        :current="currCouponType.index"
        :activeStyle="{ color: '#f56c6c', fontSize: '26rpx' }"
        :inactiveStyle="{ fontSize: '26rpx' }"
        :itemStyle="{ height: '75rpx', width: '33%' }"
        @click="couponStatusHandle"
      ></up-tabs>
    </view>
    <template v-for="(coupon, key) in couponData.data" :key="key">
      <view class="coupon-list-box" v-if="coupon.promotion">
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
                <view class="coupon-name">{{ coupon.promotion.name }}</view>
                <view class="coupon-result">
                  优惠方式：<text
                    v-for="(couponResult, index) in coupon.promotion.promotionResults"
                    :key="index"
                    >{{ getResultMsg(couponResult.code, couponResult.parameters) }}</text
                  >
                </view>
                <view class="coupon-time"
                  >有效时间：{{ formatDate(new Date(coupon.startTime), "YYYY-mm-dd") }} 至
                  {{ formatDate(new Date(coupon.endTime), "YYYY-mm-dd") }}
                </view>
                <view class="coupon-code">优惠券码：{{ coupon.couponCode }} </view>
              </view>
            </up-col>
          </up-row>
        </view>
        <view class="coupon-conditions" v-if="coupon.promotion.promotionConditions">
          <template
            v-for="(couponCondition, index) in coupon.promotion.promotionConditions"
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
        v-if="couponData.data.length > 1 && key !== couponData.data.length - 1"
      ></view>
    </template>
    <view
      class="coupon-empty-data"
      v-show="!couponData.data || couponData.data.length < 1"
    >
      <u-empty mode="list" :text="`无 ${currCouponType.name} 优惠劵`" />
    </view>
    <view class="u-margin-top-10" v-show="couponData.data.length > 0">
      <u-loadmore nomore-text="" :status="couponData.loading" />
    </view>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { formatDate } from "@/utils/formatTime";
import { getResultMsg, getConditionMsg } from "@/utils/other";

// 优惠劵
const couponData = reactive({
  data: [] as UserCouponType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    couponCode: "",
    status: 1,
    sorting: "CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return couponData.data.length < couponData.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) couponData.loading = "loadmore";
  else couponData.loading = "nomore";
  if (couponData.data.length < 1) {
    couponData.showEmptyData = Object(true);
  } else {
    couponData.showEmptyData = null;
  }
};

// 审核状态标签
const couponStatusList = reactive([
  { name: "未使用" },
  { name: "已使用" },
  { name: "已失效" },
]);

// 选中审核状态
const currCouponType = reactive({
  index: 0,
  name: "未使用",
});

// 触发审核状态
const couponStatusHandle = (item: any) => {
  couponData.showEmptyData = null;
  couponData.param.couponCode = "";
  couponData.param.status = 1;
  switch (item.name) {
    case "未使用":
      couponData.param.status = 1;
      break;
    case "已使用":
      couponData.param.status = 2;
      break;
    case "已失效":
      couponData.param.status = 3;
      break;

    default:
      break;
  }
  currCouponType.name = item.name;
  couponData.currentPage = 1;
  couponData.data = [];
  couponData.total = 0;
  loadUserCoupons();
};

// 加载优惠劵
const loadUserCoupons = () => {
  couponData.param.skipCount =
    (couponData.currentPage - 1) * couponData.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<UserCouponType[]>>({
    url: "/api/promotion/coupon/user-coupon-list",
    method: "GET",
    data: couponData.param,
  }).then((result) => {
    couponData.total = result.totalCount;
    couponData.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

onLoad((evt: any) => {
  var statusName = evt.status ?? "全部";
  for (let i = 0; i < couponStatusList.length; i++) {
    const aType = couponStatusList[i];
    if (aType.name == statusName) {
      currCouponType.index = i;
      currCouponType.name = aType.name;
      break;
    }
  }
  couponStatusHandle(currCouponType);
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    couponData.currentPage++;
    loadUserCoupons();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.coupon-body {
  .coupon-tabs-box {
    background-color: white;
  }
  .coupon-list-box {
    margin: 20rpx;
    background-color: white;
    border: 1px #e54d42 dotted;
    border-radius: 10rpx;
    .coupon-item {
      background-color: #fff5f5;
      padding: 15rpx;
      .coupon-content {
        line-height: 40rpx;
        .coupon-name {
          width: 480rpx;
          color: #e54d42;
          overflow: hidden;
          white-space: nowrap;
          text-overflow: ellipsis;
          font-size: 30rpx;
        }
        .coupon-result,
        .coupon-time,
        .coupon-code {
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
