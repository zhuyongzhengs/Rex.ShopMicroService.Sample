<template>
  <view class="seckill-body">
    <view class="seckill-tabs-box">
      <up-tabs
        :list="tabList"
        lineColor="#ff7900"
        :itemStyle="{ width: '33%', height: '80rpx' }"
        :activeStyle="{ color: '#ff7900' }"
        @click="seckillStatusHandle"
      ></up-tabs>
    </view>
    <template v-for="sGood in seckillGoods.data">
      <view class="seckill-list-box">
        <view class="product-container">
          <view class="product-list-box u-margin-bottom-15">
            <up-row customStyle="margin: 0rpx">
              <up-col span="3">
                <up-image
                  width="150rpx"
                  height="150rpx"
                  radius="6rpx"
                  :show-loading="true"
                />
              </up-col>
              <up-col span="9">
                <view class="product-detail">
                  <view class="product-title">{{ sGood.name }}</view>
                  <up-row>
                    <up-col span="10">
                      <view class="product-spec">{{ sGood.brief }}</view>
                    </up-col>
                    <up-col span="2">
                      <text class="seckill-mktPrice">{{ sGood.mktPrice }}</text>
                    </up-col>
                  </up-row>
                  <up-row>
                    <up-col span="2">
                      <up-text type="info" size="20rpx" text="已抢购"></up-text>
                    </up-col>
                    <up-col span="10">
                      <up-line-progress
                        :percentage="goodBuyRate(sGood)"
                        activeColor="#ff7900"
                      ></up-line-progress>
                    </up-col>
                  </up-row>
                </view>
              </up-col>
            </up-row>
          </view>
        </view>
        <up-line margin="0rpx"></up-line>
        <view class="seckill-price-box u-margin-top-10">
          <up-row customStyle="margin: 0px">
            <up-col span="5">
              <text class="seckill-price">{{ sGood.goodSeckillMoney }}</text>
            </up-col>
            <up-col span="7">
              <up-button
                type="error"
                size="small"
                text="立即抢购"
                v-if="sGood.startStatus == 1"
                @click="routeHelper.goSeckillDetail(sGood.id, sGood.promotionId)"
              ></up-button>
              <up-text
                type="info"
                size="small"
                v-else-if="sGood.startStatus == 2"
                :text="'开始时间：' + sGood.promotionStartTime"
              ></up-text>
              <up-button
                type="info"
                size="small"
                text="谢谢参与"
                :disabled="true"
                v-else
              ></up-button>
            </up-col>
          </up-row>
        </view>
      </view>
    </template>
  </view>
  <view class="seckillGoods-empty-data" v-show="seckillGoods.showEmptyData">
    <u-empty mode="list" text="无秒杀商品信息." />
  </view>
  <view class="u-margin-top-10" v-show="seckillGoods.data.length > 0">
    <u-loadmore nomore-text="" :status="seckillGoods.loading" />
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, computed } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import routeHelper from "@/utils/routeHelper";
import { http } from "@/utils/http";

const tabList = reactive([{ name: "进行中" }, { name: "即将开始" }, { name: "已结束" }]);
// 选中审核状态
const activeTabIndex = reactive({
  index: 0,
  name: "进行中",
});

// 秒杀商品
const seckillGoods = reactive({
  data: [] as SeckillGoodType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    status: 1,
    name: "",
    sorting: "CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 抢购率
const goodBuyRate = (sGood: SeckillGoodType) => {
  return Number((sGood.totalFreezeStock / sGood.totalStock) * 100).toFixed(2);
};

// 加载秒杀商品
const loadSeckillGoods = () => {
  seckillGoods.param.skipCount =
    (seckillGoods.currentPage - 1) * seckillGoods.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<SeckillGoodType[]>>({
    url: "/api/front/aggregation/common/promotionSeckills",
    method: "GET",
    data: seckillGoods.param,
  }).then((result) => {
    seckillGoods.total = result.totalCount;
    seckillGoods.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

// 是否还有更多数据
const isMoreData = computed(() => {
  return seckillGoods.data.length < seckillGoods.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) seckillGoods.loading = "loadmore";
  else seckillGoods.loading = "nomore";
  if (seckillGoods.data.length < 1) {
    seckillGoods.showEmptyData = Object(true);
  } else {
    seckillGoods.showEmptyData = null;
  }
};

// 触发审核状态
const seckillStatusHandle = (item: any) => {
  seckillGoods.showEmptyData = null;
  seckillGoods.param.name = "";
  switch (item.name) {
    case "进行中":
      seckillGoods.param.status = 1;
      break;
    case "即将开始":
      seckillGoods.param.status = 2;
      break;
    case "已结束":
      seckillGoods.param.status = 3;
      break;

    default:
      break;
  }
  seckillGoods.currentPage = 1;
  seckillGoods.data = [];
  seckillGoods.total = 0;
  loadSeckillGoods();
};

onLoad((evt: any) => {
  var statusName = evt.status ?? "全部";
  for (let i = 0; i < tabList.length; i++) {
    const aType = tabList[i];
    if (aType.name == statusName) {
      activeTabIndex.index = i;
      activeTabIndex.name = aType.name;
      break;
    }
  }
  seckillStatusHandle(activeTabIndex);
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    seckillGoods.currentPage++;
    loadSeckillGoods();
    console.log("触发了[加载更多]！");
  }
});
</script>
<style lang="scss" scoped>
.seckill-body {
  .seckill-tabs-box {
    width: 100%;
    background-color: white;
  }
  .seckill-list-box {
    background-color: white;
    margin: 20rpx;
    padding: 15rpx;
    border-radius: 15rpx;
    .product-container {
      margin-top: 10rpx;
    }
    .product-detail {
      padding-left: 10rpx;
      .product-title {
        font-size: 26rpx;
        color: #2c313b;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
      }
      .product-spec {
        font-size: 24rpx;
        margin: 8rpx 0rpx;
        color: #a8700d;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
      }
      .seckill-mktPrice {
        color: #909399;
        text-decoration: line-through;
        font-size: 20rpx;
        display: inline-block;
      }
      .seckill-mktPrice::before {
        content: "￥";
      }
    }

    .good-seckill-box {
      font-size: 24rpx;
      color: #606266;
      .seckill-time {
        padding-top: 6rpx;
        text-align: left;
      }
      .seckill-amount {
        text-align: right;
      }
    }

    .seckill-price-box {
      .seckill-price {
        color: #f56c6c;
        font-size: 30rpx;
        display: inline-block;
        margin-right: 10rpx;
      }
      .seckill-price::before {
        content: "￥";
      }
    }
  }
}

.seckillGoods-empty-data {
  margin-top: 100rpx;
  text-align: center;
}
</style>
