<template>
  <!-- 商品秒杀 -->
  <view class="good-seckill-body" v-if="isParamList">
    <view class="good-seckill-item bg-white" :style="'border-radius:' + radius">
      <up-row>
        <up-col span="8">
          <view class="item-title" @click="routeHelper.goSeckillList()">{{
            rexShopData.parameters.title
          }}</view>
        </up-col>
        <up-col span="4">
          <view class="item-icon" @click="routeHelper.goSeckillList()">
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

    <view class="good-seckill-content" v-if="isParamList">
      <view
        class="img-list-item bg-white"
        :style="'border-radius:' + radius"
        v-for="(item, index) in rexShopData.parameters.list"
        :key="index"
        @click="routeHelper.goSeckillDetail(item.id, item.promotionId)"
      >
        <view class="img-list-item-l">
          <image
            class="good-img"
            mode="scaleToFill"
            width="100%"
            height="100%"
            :src="item.image"
            :lazy-load="true"
          />
        </view>

        <view class="img-list-item-r">
          <view class="seckill-good-title u-line-1">{{ item.name }}</view>
          <view class="seckill-good-description u-line-2">{{ item.brief }}</view>
          <view class="item-c">
            <view class="product-price-box">
              <text class="product-price">{{ item.goodSeckillMoney }}</text>
              <text class="product-mkt-price float-r">{{ item.mktPrice }}</text>
            </view>
            <view class="time-item-box">
              <up-row>
                <up-col span="10">
                  <view
                    class="goods-salesvolume"
                    v-if="item.startStatus == 1 && item.promotionSeconds"
                  >
                    <text class="count-down-name">剩余:</text>
                    <up-count-down
                      :time="item.promotionSeconds * 1000"
                      format="DD:HH:mm:ss"
                      autoStart
                      millisecond
                      @change="onTimeChage"
                      @finish="onCountDownEnd(index)"
                    >
                      <view class="time">
                        <text class="time__item">{{ timeData.days }}天</text>
                        <text class="time__item"
                          >{{
                            timeData.hours > 10 ? timeData.hours : "0" + timeData.hours
                          }}时</text
                        >
                        <text class="time__item">{{ timeData.minutes }}分</text>
                        <text class="time__item">{{ timeData.seconds }}秒</text>
                      </view>
                    </up-count-down>
                  </view>
                </up-col>
                <up-col span="2">
                  <view class="shopping-cart-icon">
                    <up-icon name="shopping-cart" color="#ff7d08" size="50rpx"></up-icon>
                  </view>
                </up-col>
              </up-row>
            </view>
            <view class="clear-both"></view>
          </view>
        </view>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts" name="rexShopGroupPurchase">
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
  if (!props.rexShopData) {
    return false;
  }
  if (!props.rexShopData.parameters) {
    return false;
  }
  if (!props.rexShopData.parameters.list) {
    return false;
  }
  return props.rexShopData.parameters.list.length > 0;
});

// 计算监听
const timeData = ref<any>({});
const onTimeChage = (e: any) => {
  timeData.value = e;
};

// 倒计时结束
const onCountDownEnd = (index: number) => {
  props.rexShopData.parameters.list.splice(index, 1);
};

onMounted(() => {
  // console.log(props.rexShopData.parameters.list);
});
</script>

<style lang="scss" scoped>
.good-seckill-body {
  .good-seckill-item {
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
      .seckill-good-title {
        font-size: 28rpx;
      }
      .seckill-good-description {
        font-size: 24rpx;
        height: 64rpx;
        color: #929292;
        margin: 10rpx 0rpx;
      }

      .item-c {
        width: 100%;
        margin-top: 0;
        font-size: 24rpx;
        .time-item-box {
          .goods-salesvolume {
            @include flex;
            align-items: center;
            color: #2c313b;
            .count-down-name {
              color: #ff7d08;
              font-weight: bold;
            }
            .time {
              @include flex;
              align-items: center;

              &__item {
                color: #ff7d08;
                font-weight: bold;
                text-align: center;
                margin-left: 10rpx;
              }
            }
          }
          .shopping-cart-icon {
            float: right;
          }
        }
      }
    }
  }
}
</style>
