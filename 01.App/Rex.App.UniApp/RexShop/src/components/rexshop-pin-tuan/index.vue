<template>
  <view class="pin-tuan-body" v-if="isParamList">
    <view class="pin-tuan-item bg-white" :style="'border-radius:' + radius">
      <up-row>
        <up-col span="8">
          <view class="item-title" @click="routeHelper.goPinTuanList()">{{
            rexShopData.parameters.title
          }}</view>
        </up-col>
        <up-col span="4">
          <view v-if="isMore" class="item-icon" @click="routeHelper.goPinTuanList()">
            <u-icon
              size="25rpx"
              label="更多"
              labelSize="25rpx"
              labelPos="left"
              name="arrow-right"
            />
          </view>
        </up-col>
      </up-row>
    </view>
    <view
      class="pin-tuan-content bg-white"
      :style="'border-radius:' + radius"
      v-if="goodItems.items && isParamList"
    >
      <view class="pin-tuan-swiper-box">
        <swiper
          class="pin-tuan-swiper"
          circular
          :indicator-dots="false"
          @change="onSwiperChange"
          :autoplay="true"
          :duration="3000"
        >
          <swiper-item v-for="(goods, index) in goodItems.items" :key="index">
            <view class="pin-tuan-goods-box">
              <view
                class="goods-item"
                v-for="(good, gdIndex) in goods"
                :key="good.id"
                :style="gdIndex < goods.length - 1 ? 'margin-right: 15rpx' : ''"
                @tap="routeHelper.goPinTuanDetail(good.goodsId, good.id)"
              >
                <view class="goods-item-block">
                  <view class="img-box">
                    <image
                      class="good-img"
                      mode="scaleToFill"
                      :src="good.goodsImage"
                      :lazy-load="true"
                    />
                    <view class="img-tag">{{ good.peopleNumber }}人团</view>
                  </view>
                  <view class="price-box">
                    <view class="good-price">
                      <text class="seckill-current">{{ good.pinTuanPrice }}</text>
                      <text class="original">{{
                        good.pinTuanPrice + good.discountAmount
                      }}</text>
                    </view>
                  </view>
                </view>
              </view>
              <view class="clear-both"></view>
            </view>
          </swiper-item>
        </swiper>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts" name="rexShopPinTuan">
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
  return props.rexShopData.parameters.list.length > 0;
});

// 是否显示更多
const isMore = computed(() => {
  return props.rexShopData.parameters.list.length > 4;
});

// 商品数据项
const goodItems = ref({
  items: [] as any,
  current: 0,
});

// 轮播切换
const onSwiperChange = (evt: any) => {
  goodItems.value.current = evt.detail.current;
};

// 数据分层
const sortData = (oArr: [], length: number) => {
  let arr = new Array();
  let minArr = new Array();
  oArr.forEach((c) => {
    if (minArr.length === length) {
      minArr = [];
    }
    if (minArr.length === 0) {
      arr.push(minArr);
    }
    minArr.push(c);
  });
  return arr;
};

onMounted(() => {
  if (props.rexShopData.parameters.list.length > 0) {
    let itemList = sortData(props.rexShopData.parameters.list, 4);
    goodItems.value.items = itemList;
  }
});
</script>

<style lang="scss" scoped>
.pin-tuan-body {
  .pin-tuan-item {
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
  .pin-tuan-content {
    margin-top: 10rpx;

    .pin-tuan-swiper-box {
      padding: 10rpx;
      .pin-tuan-swiper {
        height: 210rpx;
      }
      .goods-item {
        float: left;
        width: 162rpx;
        height: 162rpx;
        .goods-item-block {
          .img-box {
            .img-tag {
              position: relative;
              left: 0rpx;
              bottom: 42rpx;
              z-index: 2;
              line-height: 35rpx;
              background: linear-gradient(132deg, #f3dfb1, #f3dfb1, #ecbe60);
              border-radius: 0px 18rpx 18rpx 0px;
              padding: 0 10rpx;
              font-size: 24rpx;
              font-family: PingFang SC;
              font-weight: bold;
              color: #784f06;
              float: left;
            }
            .good-img {
              width: 168rpx;
              height: 168rpx;
              border-radius: 5rpx;
            }
          }
          .price-box {
            position: relative;
            left: 0rpx;
            bottom: 42rpx;
            .good-price {
              .seckill-current {
                font-size: 30rpx;
                font-weight: 500;
                color: #e1212b;
              }
              .seckill-current::before {
                content: "￥";
                font-size: 22rpx;
              }
              .original {
                font-size: 20rpx;
                font-weight: 400;
                text-decoration: line-through;
                color: #aaaaaa;
                margin-left: 14rpx;
              }
              .original::before {
                content: "￥";
              }
            }
          }
        }
      }
    }
  }
}
</style>
