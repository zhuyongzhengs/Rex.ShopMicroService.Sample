<template>
  <view class="goods-body">
    <view class="goods-box">
      <!-- Start：列表平铺两列三列 -->
      <view
        class="two-three-column-container"
        v-if="
          (rexShopData.parameters.column == '2' &&
            rexShopData.parameters.display == 'list') ||
          (rexShopData.parameters.column == '3' &&
            rexShopData.parameters.display == 'list')
        "
        v-bind:class="'column' + rexShopData.parameters.column"
      >
        <view class="goods-item bg-white" :style="'border-radius:' + radius">
          <up-row>
            <up-col span="8">
              <view class="item-title">{{ rexShopData.parameters.title }}</view>
            </up-col>
            <up-col span="4" v-if="rexShopData.parameters.lookMore">
              <view
                class="item-icon"
                @click="
                  goGoodsList({
                    classifyId: rexShopData.parameters.classifyId,
                    brandId: rexShopData.parameters.brandId,
                  })
                "
              >
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

        <view class="goods-content" v-if="isParamList">
          <u-grid :col="rexShopData.parameters.column" :border="false" align="left">
            <u-grid-item
              bg-color="transparent"
              :custom-style="{ padding: '0rpx' }"
              v-for="(item, index) in rexShopData.parameters.list"
              :key="index"
              @click="routeHelper.goGoodsDetail(item.id)"
            >
              <view class="good-item-box">
                <view
                  class="good-content"
                  :style="'border-radius:' + radius + ';' + item.customStyle"
                >
                  <up-image
                    class="good-img"
                    width="100%"
                    :height="rexShopData.parameters.column == 3 ? '200rpx' : '340rpx'"
                    mode="scaleToFill"
                    :radius="5"
                    :src="item.image"
                    :lazy-load="true"
                  />
                  <view class="good-title u-line-2">
                    {{ item.name }}
                  </view>
                  <up-row>
                    <up-col span="6">
                      <view class="product-price u-margin-top-5 align-l">{{
                        item.price
                      }}</view>
                    </up-col>
                    <up-col span="6" v-show="item.price != item.mktPrice">
                      <view class="product-mkt-price u-margin-top-5 align-r">{{
                        item.mktPrice
                      }}</view>
                    </up-col>
                  </up-row>
                  <view class="good-tag-recommend" v-if="item.isRecommend"> 推荐 </view>
                  <view class="good-tag-hot" v-if="item.isHot"> 热门 </view>
                </view>
              </view>
            </u-grid-item>
          </u-grid>
        </view>
      </view>
      <!-- End：列表平铺两列三列 -->

      <!-- Start：列表平铺单列 -->
      <view
        class="one-column-container"
        v-if="
          rexShopData.parameters.column == '1' && rexShopData.parameters.display == 'list'
        "
      >
        <view class="goods-item bg-white" :style="'border-radius:' + radius">
          <up-row>
            <up-col span="8">
              <view class="item-title">{{ rexShopData.parameters.title }}</view>
            </up-col>
            <up-col span="4" v-if="rexShopData.parameters.lookMore">
              <view
                class="item-icon"
                @click="
                  goGoodsList({
                    classifyId: rexShopData.parameters.classifyId,
                    brandId: rexShopData.parameters.brandId,
                  })
                "
              >
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
        <view class="goods-content" v-if="isParamList">
          <u-grid :col="1" :border="false" align="center">
            <u-grid-item
              bg-color="transparent"
              :custom-style="{ padding: '0rpx' }"
              v-for="(item, index) in rexShopData.parameters.list"
              :key="index"
              @click="routeHelper.goGoodsDetail(item.id)"
            >
              <view class="good-item-box">
                <view class="good-content" :style="'border-radius:' + radius">
                  <u-row gutter="5" justify="space-between">
                    <u-col span="4">
                      <up-image
                        class="good-img"
                        width="100%"
                        height="220rpx"
                        mode="scaleToFill"
                        :radius="5"
                        :src="item.image"
                        :lazy-load="true"
                      />
                      <view class="good-tag-recommend" v-if="item.isRecommend">
                        推荐
                      </view>
                      <view class="good-tag-hot" v-if="item.isHot"> 热门 </view>
                    </u-col>
                    <u-col span="8">
                      <view class="good-title u-line-4 u-padding-10">
                        {{ item.name }}
                      </view>
                      <up-row>
                        <up-col span="6">
                          <view class="product-price u-padding-10 align-l">{{
                            item.price
                          }}</view>
                        </up-col>
                        <up-col span="6" v-show="item.price != item.mktPrice">
                          <view class="product-mkt-price u-padding-10 align-r">{{
                            item.mktPrice
                          }}</view>
                        </up-col>
                      </up-row>
                    </u-col>
                  </u-row>
                </view>
              </view>
            </u-grid-item>
          </u-grid>
        </view>
        <view class="order-none" v-else>
          <image class="order-none-img" src="/static/images/order.png"></image>
        </view>
      </view>
      <!-- End：列表平铺单列 -->

      <!-- Start：横向滚动 -->
      <view
        class="horizontal-scrolling-container"
        v-if="
          (rexShopData.parameters.column == '2' &&
            rexShopData.parameters.display == 'slide') ||
          (rexShopData.parameters.column == '3' &&
            rexShopData.parameters.display == 'slide')
        "
        v-bind:class="'slide' + rexShopData.parameters.column"
      >
        <view class="goods-item bg-white" :style="'border-radius:' + radius">
          <up-row>
            <up-col span="8">
              <view class="item-title">{{ rexShopData.parameters.title }}</view>
            </up-col>
            <up-col span="4" v-if="rexShopData.parameters.lookMore">
              <view
                class="item-icon"
                @click="
                  goGoodsList({
                    classifyId: rexShopData.parameters.classifyId,
                    brandId: rexShopData.parameters.brandId,
                  })
                "
              >
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
        <view class="goods-content" v-if="isParamList">
          <swiper
            indicator-active-color="#dd6161"
            :style="
              'width: 100%; height: ' +
              (rexShopData.parameters.column == 2 ? '350rpx' : '420rpx') +
              ';'
            "
            :indicator-dots="true"
            @change="onCurrentChange"
          >
            <template v-for="(goods, index) in horizontalGoods" :key="index">
              <swiper-item>
                <u-grid :col="rexShopData.parameters.column" :border="false" align="left">
                  <template v-for="item in goods" :key="item.id">
                    <u-grid-item
                      bg-color="transparent"
                      :custom-style="{ padding: '0rpx' }"
                      @click="routeHelper.goGoodsDetail(item.id)"
                    >
                      <view class="good-item-box">
                        <view
                          class="good-content"
                          :style="'border-radius:' + radius + ';' + item.customStyle"
                        >
                          <image
                            :style="
                              'width: 100%;border-radius: 10rpx;height:' +
                              (rexShopData.parameters.column == 2 ? '260rpx' : '200rpx')
                            "
                            :mode="item.mode"
                            :src="item.image"
                            :lazy-load="true"
                          />
                          <template v-if="rexShopData.parameters.column == 3">
                            <view class="good-title u-line-2">
                              {{ item.name }}
                            </view>
                            <up-row>
                              <up-col span="6">
                                <view class="product-price align-l">{{
                                  item.price
                                }}</view>
                              </up-col>
                              <up-col span="6" v-show="item.price != item.mktPrice">
                                <view class="product-mkt-price align-r">{{
                                  item.mktPrice
                                }}</view>
                              </up-col>
                            </up-row>
                          </template>
                          <view class="good-tag-recommend" v-if="item.isRecommend">
                            推荐
                          </view>
                          <view class="good-tag-hot" v-if="item.isHot"> 热门 </view>
                        </view>
                      </view>
                    </u-grid-item>
                  </template>
                </u-grid>
              </swiper-item>
            </template>
          </swiper>
        </view>
      </view>
      <!-- End：横向滚动 -->
    </view>
  </view>
</template>

<script setup lang="ts" name="rexShopGoods">
import { ref, computed, onMounted, defineProps } from "vue";
import _ from "lodash";
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

// 是否包含商品数据
const isParamList = computed(() => {
  return props.rexShopData.parameters.list.length > 0;
});

// 轮播图切换
const current = ref(0);
const onCurrentChange = (e: any) => {
  current.value = e.detail.current;
};

// 跳转商品列表页
const goGoodsList = (obj: any) => {
  let url = `/pages/category/list?goodCategoryId=${obj.classifyId}&brandId=${obj.brandId}`;
  if (props.rexShopData.parameters.type == "choose") {
    url = "/pages/category/list";
  }
  uni.navigateTo({
    url,
  });
};

// 加载横向商品数据
const horizontalGoods = ref([] as any);
const loadHorizontalGoods = () => {
  let pSize = props.rexShopData.parameters.column;
  let pList = props.rexShopData.parameters.list;
  for (let i = 0; i < pSize; i++) {
    let goods = pageList(pList, i + 1, pSize);
    if (!goods || goods.length < 1) continue;
    horizontalGoods.value.push(goods);
  }
};

// 获取分页数据
const pageList = (array: any, pageNumber: number, pageSize: number) => {
  const startIndex = (pageNumber - 1) * pageSize;
  const endIndex = startIndex + pageSize;
  const pagedArray = array.slice(startIndex, endIndex);
  return pagedArray;
};

// 加载自定义样式
const loadCustomStyle = () => {
  let column = props.rexShopData.parameters.column;
  let flag = 0;
  for (let i = 0; i < props.rexShopData.parameters.list.length; i++) {
    flag++;
    if (flag == 1) {
      props.rexShopData.parameters.list[i].customStyle = "margin-left: 0rpx !important;";
    } else if (flag == column) {
      props.rexShopData.parameters.list[i].customStyle = "margin-right: 0rpx !important;";
    }
    if (flag >= column) {
      flag = 0;
    }
  }
};

onMounted(() => {
  loadHorizontalGoods();
  loadCustomStyle();
});
</script>

<style scoped lang="scss">
.goods-box {
  border-radius: 16rpx;
  color: #333333 !important;
}

/* 两三列部分 */
.two-three-column-container {
  .goods-item {
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

  .goods-content {
    .good-item-box {
      width: 100%;

      .good-content {
        background-color: white;
        padding: 10rpx;
        margin: 0rpx 6rpx;
        margin-top: 10rpx;

        .good-title {
          font-size: 26rpx;
          margin-top: 5px;
          height: 68rpx;
          color: $u-main-color;
        }
        .good-price {
          font-size: 30rpx;
          color: $u-error;
          margin-top: 5px;
        }
        .good-tag-hot {
          display: flex;
          margin-top: 5px;
          position: absolute;
          top: 15rpx;
          left: 25rpx;
          background-color: $u-error;
          color: #ffffff;
          display: flex;
          align-items: center;
          padding: 4rpx 14rpx;
          border-radius: 50rpx;
          font-size: 20rpx;
          line-height: 1;
        }
        .good-tag-recommend {
          display: flex;
          margin-top: 5px;
          position: absolute;
          top: 15rpx;
          right: 25rpx;
          background-color: $u-primary;
          color: #ffffff;
          margin-left: 10px;
          border-radius: 50rpx;
          line-height: 1;
          padding: 4rpx 14rpx;
          display: flex;
          align-items: center;
          border-radius: 50rpx;
          font-size: 20rpx;
        }
      }
    }
  }
}

/*
.two-three-column-container.column2 {
  .u-grid-item:nth-child(odd) .good-item-box .good-content {
    margin-left: 0rpx !important;
  }

  .u-grid-item:nth-child(even) .good-item-box .good-content {
    margin-right: 0rpx !important;
  }
}

.two-three-column-container.column3 {
  .u-grid-item:first-child .good-item-box .good-content {
    margin-left: 0rpx !important;
  }

  .u-grid-item:last-child .good-item-box .good-content {
    margin-right: 0rpx !important;
  }
}
*/

/* 单列 */
.one-column-container {
  .goods-item {
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

  .goods-content {
    .good-item-box {
      width: 100%;

      .good-content {
        background-color: white;
        margin-top: 8rpx;
        padding: 15rpx;
        .good-title {
          font-size: 26rpx;
          margin-top: 5px;
          height: 124rpx;
          color: $u-main-color;
        }
        .good-price {
          font-size: 30rpx;
          color: $u-error;
          margin-top: 5px;
        }
        .good-tag-hot {
          display: flex;
          margin-top: 5px;
          position: absolute;
          bottom: 25rpx;
          left: 25rpx;
          background-color: $u-error;
          color: #ffffff;
          display: flex;
          align-items: center;
          padding: 4rpx 14rpx;
          border-radius: 50rpx;
          font-size: 20rpx;
          line-height: 1;
        }
        .good-tag-recommend {
          display: flex;
          margin-top: 5px;
          position: absolute;
          top: 25rpx;
          left: 25rpx;
          background-color: $u-primary;
          color: #ffffff;
          border-radius: 50rpx;
          line-height: 1;
          padding: 4rpx 14rpx;
          display: flex;
          align-items: center;
          border-radius: 50rpx;
          font-size: 20rpx;
        }
      }
    }
  }
}

/* 横向滚动 */
.horizontal-scrolling-container {
  .goods-item {
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

  .goods-content {
    .good-item-box {
      width: 100%;
      margin-top: 10rpx;

      .good-content {
        background-color: white;
        padding: 10rpx;
        margin: 0rpx 6rpx;
        .good-title {
          font-size: 26rpx;
          margin-top: 5px;
          height: 72rpx;
          color: $u-main-color;
        }
        .good-price {
          font-size: 30rpx;
          color: $u-error;
          margin-top: 5px;
        }
        .good-tag-hot {
          display: flex;
          margin-top: 5px;
          position: absolute;
          top: 25rpx;
          left: 25rpx;
          background-color: $u-error;
          color: #ffffff;
          display: flex;
          align-items: center;
          padding: 4rpx 14rpx;
          border-radius: 50rpx;
          font-size: 20rpx;
          line-height: 1;
        }
        .good-tag-recommend {
          display: flex;
          margin-top: 5px;
          position: absolute;
          top: 25rpx;
          right: 25rpx;
          background-color: $u-primary;
          color: #ffffff;
          border-radius: 50rpx;
          line-height: 1;
          padding: 4rpx 14rpx;
          display: flex;
          align-items: center;
          border-radius: 50rpx;
          font-size: 20rpx;
        }
      }
    }
  }
}

/*
.horizontal-scrolling-container.slide2,
.horizontal-scrolling-container.slide3 {
  .u-grid-item:first-child .good-item-box .good-content {
    margin-left: 0rpx !important;
  }

  .u-grid-item:last-child .good-item-box .good-content {
    margin-right: 0rpx !important;
  }
}
*/
</style>
