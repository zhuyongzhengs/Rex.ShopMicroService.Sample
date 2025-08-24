<template>
  <view class="good-tabBar-body">
    <view class="tabs-nav bg-white">
      <u-tabs
        lineColor="#f56c6c"
        :style="'border-radius:' + radius"
        :list="goodTabBarItem.tabNames"
        :current="goodTabBarItem.current"
        :itemStyle="{
          height: '70rpx',
          fontSize: '30rpx',
        }"
        @change="onTabsChange"
      />
    </view>

    <template v-if="goodTabBarItem.isGoodData">
      <view
        v-for="(child, indexP) in goodTabBarItem.goodData.parameters.list"
        :key="indexP"
      >
        <view class="goods-box" v-show="child.isShow == true">
          <!-- 列表平铺两列三列 -->
          <view
            class="two-three-column-container"
            v-if="child.column == '2' || child.column == '3'"
            v-bind:class="'column' + child.column"
          >
            <view class="goods-content" v-if="child.list">
              <u-grid :col="child.column" :border="false" align="left">
                <u-grid-item
                  bg-color="transparent"
                  :custom-style="{ padding: '0rpx' }"
                  v-for="(item, index) in child.list"
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
                        :height="child.column == 3 ? '200rpx' : '340rpx'"
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
                      <view class="good-tag-recommend" v-if="item.isRecommend">
                        推荐
                      </view>
                      <view class="good-tag-hot" v-if="item.isHot"> 热门 </view>
                    </view>
                  </view>
                </u-grid-item>
              </u-grid>
            </view>
          </view>

          <!-- 列表平铺单列 -->
          <view class="one-column-container" v-if="child.column == '1'">
            <view class="goods-content" v-if="child.list">
              <u-grid :col="1" :border="false" align="center">
                <u-grid-item
                  bg-color="transparent"
                  :custom-style="{ padding: '0rpx' }"
                  v-for="item in child.list"
                  :key="item.id"
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
        </view>
      </view>
    </template>
  </view>
</template>

<script setup lang="ts" name="rexShopGoodTabBar">
import { ref, onMounted, defineProps } from "vue";
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

// 商品选项卡项
const goodTabBarItem = ref({
  current: 0,
  tabNames: [] as any,
  isGoodData: false,
  goodData: {} as any,
});

// 选项卡切换
const onTabsChange = (evt: any) => {
  goodTabBarItem.value.current = evt.index;
  for (var i = 0; i < goodTabBarItem.value.goodData.parameters.list.length; i++) {
    goodTabBarItem.value.goodData.parameters.list[i].isShow = Boolean(
      goodTabBarItem.value.current == i
    );
  }
};

// 加载自定义样式
const loadCustomStyle = (column: number, list: any) => {
  let flag = 0;
  for (let i = 0; i < list.length; i++) {
    flag++;
    if (flag == 1) {
      list[i].customStyle = "margin-left: 0rpx !important;";
    } else if (flag == column) {
      list[i].customStyle = "margin-right: 0rpx !important;";
    }
    if (flag >= column) {
      flag = 0;
    }
  }
};

onMounted(() => {
  goodTabBarItem.value.goodData = props.rexShopData;
  for (var i = 0; i < goodTabBarItem.value.goodData.parameters.list.length; i++) {
    var paramItem = goodTabBarItem.value.goodData.parameters.list[i];
    loadCustomStyle(paramItem.column, paramItem.list);
    paramItem.isShow = i == 0;
    let item = {
      name: paramItem.title,
    };
    goodTabBarItem.value.tabNames.push(item);
  }
  goodTabBarItem.value.isGoodData = true;
});
</script>

<style scoped lang="scss">
.good-tabBar-body {
  .goods-box {
    border-radius: 16rpx;
    color: #333333 !important;
  }

  .tabs-nav {
    font-size: 30rpx;
  }
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
        margin: 0rpx 6rpx;
        padding: 10rpx;
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
        margin-top: 10rpx;
        padding: 10rpx;
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
</style>
