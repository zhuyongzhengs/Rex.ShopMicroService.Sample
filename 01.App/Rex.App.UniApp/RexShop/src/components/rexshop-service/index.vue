<template>
  <view class="good-service-body" v-if="isParamList">
    <view class="good-service-item bg-white" :style="'border-radius:' + radius">
      <up-row>
        <up-col span="8">
          <view class="item-title" @click="routeHelper.goServicesList()">{{
            rexShopData.parameters.title
          }}</view>
        </up-col>
        <up-col span="4">
          <view class="item-icon" @click="routeHelper.goServicesList()">
            <u-icon
              size="25rpx"
              label="查看更多"
              labelSize="25rpx"
              labelPos="left"
              name="arrow-right"
            />
          </view>
        </up-col>
      </up-row>
    </view>
    <view class="good-service-content" v-if="isParamList">
      <view
        class="img-list-item bg-white"
        :style="'border-radius:' + radius"
        v-for="(item, index) in rexShopData.parameters.list"
        :key="index"
        @click="routeHelper.goServicesDetail(item.id)"
      >
        <view class="img-list-item-l">
          <image
            class="good-img"
            mode="scaleToFill"
            width="100%"
            height="100%"
            :src="item.thumbnail"
            :lazy-load="true"
          />
        </view>
        <view class="img-list-item-r">
          <view class="service-good-title u-line-1">{{ item.title }}</view>
          <view class="service-good-description u-line-2">{{ item.description }}</view>
          <view class="item-c">
            <view class="product-price">{{ item.money }}</view>
            <view class="time-item-box">
              <up-row
                justify="space-between"
                gutter="0"
                v-if="item.status == 0 && item.lastTime > 0"
              >
                <up-col span="10">
                  <u-count-down
                    :time="item.lastTime"
                    format="DD:HH:mm:ss"
                    autoStart
                    millisecond
                    @change="onCountDownChange"
                    @finish="onCountDownFinish(index)"
                  >
                    <view class="good-time">
                      剩余：
                      <text class="time-item">{{ countDownTime.days }}天&nbsp;</text>
                      <text class="time-item"
                        >{{
                          countDownTime.hours > 10
                            ? countDownTime.hours
                            : "0" + countDownTime.hours
                        }}时&nbsp;</text
                      >
                      <text class="time-item">{{ countDownTime.minutes }}分</text>
                      <text class="time-item">{{ countDownTime.seconds }}秒</text>
                    </view>
                  </u-count-down>
                </up-col>
                <up-col span="2" textAlign="right">
                  <u-icon
                    class="icon-cart"
                    name="shopping-cart"
                    color="#f9ae3d"
                    size="24"
                  ></u-icon>
                </up-col>
              </up-row>
              <view
                class="rexshop-text-through overdue-weight-bold"
                v-if="item.pinTuanStartStatus == 3"
                >已结束</view
              >
              <view
                class="rexshop-text-through overdue-weight-bold"
                v-if="item.status == 2"
                >售罄</view
              >
              <view class="clear-both"></view>
            </view>
          </view>
        </view>
        <view class="clear-both"></view>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts" name="rexShopService">
import { computed, onMounted, defineProps } from "vue";
import routeHelper from "@/utils/routeHelper";

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

// 是否包含数据
const isParamList = computed(() => {
  if (!props.rexShopData.parameters) {
    return false;
  }
  return props.rexShopData.parameters.list.length > 0;
});

// 倒计时切换
const countDownTime = ref({} as any);
const onCountDownChange = (evt: any) => {
  countDownTime.value = evt;
};

// 倒计时结束
const onCountDownFinish = (index: number) => {
  props.rexShopData.parameters.list[index].status = 1;
};

onMounted(() => {
  // console.log(props.rexShopData.parameters);
});
</script>

<style lang="scss" scoped>
.good-service-body {
  .good-service-item {
    height: 70rpx;
    line-height: 70rpx;
    padding: 0rpx 10rpx;
    .item-title {
      font-size: 30rpx;
      color: #2c313b;
    }
    .item-icon {
      width: 100%;
      text-align: center;
    }
  }

  .img-list-item {
    margin: 10rpx 0rpx 15rpx 0rpx;
    padding: 10rpx;
    position: relative;
    overflow: hidden;
    .img-list-item-l {
      width: 200rpx;
      height: 200rpx;
      float: left;
      .good-img {
        width: 100%;
        height: 100%;
        border-radius: 10rpx;
      }
    }
    .img-list-item-r {
      width: calc(100% - 220rpx);
      margin-left: 15rpx;
      float: left;
      position: relative;
      .service-good-title {
        font-size: 28rpx;
      }
      .service-good-description {
        font-size: 24rpx;
        height: 64rpx;
        color: #929292;
        margin: 10rpx 0rpx;
      }

      .item-c {
        width: 100%;
        margin-top: 0;
        font-size: 24rpx;
        .red-price {
          color: #ff7159 !important;
        }
        .time-item-box {
          .good-time {
            @include flex;
            align-items: center;
            color: #2c313b;
            .time-item {
              font-size: 12px;
              text-align: center;
            }
          }
          .icon-cart {
            position: relative;
            left: 24rpx;
          }
        }
      }
    }
  }
}

.overdue-weight-bold {
  font-weight: bold;
  color: #929292;
  float: right;
}
</style>
