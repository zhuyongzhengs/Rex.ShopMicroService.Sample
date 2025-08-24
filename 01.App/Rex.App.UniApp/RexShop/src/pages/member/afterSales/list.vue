<template>
  <view class="aftersales-body">
    <view class="aftersales-tabs-box">
      <up-tabs
        lineColor="#f56c6c"
        :list="aftersalesStatusList"
        :current="currAftersalesType.index"
        :activeStyle="{ color: '#f56c6c', fontSize: '26rpx' }"
        :inactiveStyle="{ fontSize: '26rpx' }"
        :itemStyle="{ height: '75rpx', width: '20%' }"
        @click="aftersalesStatusHandle"
      ></up-tabs>
    </view>
    <template v-for="bAftersales in billAftersales.data" :key="bAftersales.id">
      <view class="aftersales-list-box">
        <view @tap="goAftersalesDetail(bAftersales.id)">
          <view class="aftersales-no-item float-l">
            <text>售后单号：{{ bAftersales.no }}</text>
            <view style="display: inline-block">
              <up-icon name="arrow-right" color="#606266" size="14"></up-icon>
            </view>
          </view>
          <view
            class="aftersales-state-item float-r"
            :class="aftersalesStatusColor(bAftersales)"
          >
            <text>{{ bAftersales.statusDisplay }}</text>
          </view>
          <view class="clear-both u-margin-bottom-10"></view>
          <up-line margin="10rpx 0rpx 10px 0px"></up-line>
          <template
            v-for="aftersalesItem in bAftersales.billAftersalesItems"
            :key="aftersalesItem.Id"
          >
            <view class="product-list-box u-margin-bottom-15">
              <up-row customStyle="margin: 0rpx">
                <up-col span="3">
                  <view class="product-image">
                    <up-image
                      width="164rpx"
                      height="164rpx"
                      radius="6rpx"
                      :show-loading="true"
                      :src="aftersalesItem.imageUrl"
                    />
                  </view>
                </up-col>
                <up-col span="9">
                  <view class="product-detail">
                    <view class="product-title">{{ aftersalesItem.name }}</view>
                    <up-row>
                      <up-col span="10">
                        <view class="product-spec">{{ aftersalesItem.addon }}</view>
                      </up-col>
                      <up-col span="2">
                        <view class="product-num float-r"
                          >× {{ aftersalesItem.nums }}</view
                        >
                      </up-col>
                    </up-row>
                  </view>
                </up-col>
              </up-row>
            </view>
          </template>
          <up-line margin="20rpx 0rpx 10rpx 0rpx"></up-line>
          <view class="good-aftersales-box">
            <up-row customStyle="margin: 0px">
              <up-col span="7">
                <view class="aftersales-time">
                  申请时间：{{ bAftersales.creationTime }}
                </view>
              </up-col>
              <up-col span="5">
                <view class="aftersales-amount"
                  >退款金额：<text class="product-price">{{
                    bAftersales.refundAmount
                  }}</text>
                </view>
              </up-col>
            </up-row>
          </view>
        </view>
        <view class="aftersales-play-box u-margin-top-10">
          <view class="aftersales-type-item">
            <view class="aftersales-type">
              <up-tag
                size="mini"
                plain
                plainFill
                :text="bAftersales.typeDisplay"
              ></up-tag>
            </view>
          </view>
          <view class="detail-btn-item">
            <view class="aftersales-btn">
              <up-button
                type="info"
                size="small"
                text="查看详情"
                :plain="true"
                @click="goAftersalesDetail(bAftersales.id)"
              ></up-button>
            </view>
          </view>
          <view class="clear-both"></view>
        </view>
      </view>
    </template>
    <view class="aftersales-empty-data" v-show="billAftersales.showEmptyData">
      <u-empty mode="list" text="无售后信息." />
    </view>
    <view class="u-margin-top-10" v-show="billAftersales.data.length > 0">
      <u-loadmore nomore-text="" :status="billAftersales.loading" />
    </view>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { removeNullObject } from "@/utils/other";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 售后单
const billAftersales = reactive({
  data: [] as BillAftersalesType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    userId: useUserLoginStore().currentSysUser?.id,
    no: null,
    orderNo: null,
    type: null,
    status: 0,
    sorting: "Status ASC,CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return billAftersales.data.length < billAftersales.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) billAftersales.loading = "loadmore";
  else billAftersales.loading = "nomore";
  if (billAftersales.data.length < 1) {
    billAftersales.showEmptyData = Object(true);
  } else {
    billAftersales.showEmptyData = null;
  }
};

// 审核状态标签
const aftersalesStatusList = reactive([
  { name: "全部" },
  { name: "等待审核" },
  { name: "审核通过" },
  { name: "审核拒绝" },
]);

// 选中审核状态
const currAftersalesType = reactive({
  index: 0,
  name: "全部",
});

// 审核状态字体颜色
const aftersalesStatusColor = (bAftersales: BillAftersalesType) => {
  let cssColor = "aftersales-state-info";
  switch (bAftersales.status) {
    case 1:
      cssColor = "aftersales-state-warning";
      break;
    case 2:
      cssColor = "aftersales-state-success";
      break;
    case 3:
      cssColor = "aftersales-state-error";
      break;

    default:
      break;
  }
  return cssColor;
};

// 触发审核状态
const aftersalesStatusHandle = (item: any) => {
  billAftersales.showEmptyData = null;
  billAftersales.param.no = null;
  billAftersales.param.orderNo = null;
  billAftersales.param.type = null;
  billAftersales.param.status = 0;
  switch (item.name) {
    case "等待审核":
      billAftersales.param.status = 1;
      break;
    case "审核通过":
      billAftersales.param.status = 2;
      break;
    case "审核拒绝":
      billAftersales.param.status = 3;
      break;

    default:
      break;
  }
  billAftersales.currentPage = 1;
  billAftersales.data = [];
  billAftersales.total = 0;
  loadUserAftersaless();
};

// 加载售后单
const loadUserAftersaless = () => {
  billAftersales.param.skipCount =
    (billAftersales.currentPage - 1) * billAftersales.param.maxResultCount;
  let rParam = removeNullObject(billAftersales.param);
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<BillAftersalesType[]>>({
    url: "/api/order/bill-aftersales/user",
    method: "GET",
    data: rParam,
  }).then((result) => {
    billAftersales.total = result.totalCount;
    billAftersales.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

// 查看售后单详情
const goAftersalesDetail = (aftersalesId: string) => {
  uni.navigateTo({
    url: "/pages/member/afterSales/detail?aftersalesId=" + aftersalesId,
    success: (result) => {
      console.log("已跳转到售后单详情！", result);
      uni.$once("refreshAftersalesTypeHandle", function (data) {
        aftersalesStatusHandle(currAftersalesType);
        console.log("触发了[refreshAftersalesTypeHandle]事件！");
      });
    },
  });
};

onLoad((evt: any) => {
  var statusName = evt.status ?? "全部";
  for (let i = 0; i < aftersalesStatusList.length; i++) {
    const aType = aftersalesStatusList[i];
    if (aType.name == statusName) {
      currAftersalesType.index = i;
      currAftersalesType.name = aType.name;
      break;
    }
  }
  aftersalesStatusHandle(currAftersalesType);
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    billAftersales.currentPage++;
    loadUserAftersaless();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.aftersales-body {
  .aftersales-tabs-box {
    background-color: white;
  }
  .aftersales-list-box {
    background-color: white;
    margin: 20rpx;
    padding: 15rpx;
    border-radius: 15rpx;

    .aftersales-no-item {
      font-size: 26rpx;
      color: #606266;
    }

    .aftersales-state-item {
      font-size: 26rpx;
    }

    .aftersales-state-info {
      color: #c8c9cc;
    }

    .aftersales-state-error {
      color: #dd6161;
    }

    .aftersales-state-warning {
      color: #ff9900;
    }

    .aftersales-state-success {
      color: #19be6b;
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
        -webkit-line-clamp: 3;
      }
      .product-spec {
        font-size: 24rpx;
        color: #aaaaaa;
        margin: 8rpx 0rpx;
      }
    }
  }
  .good-aftersales-box {
    font-size: 24rpx;
    color: #606266;
    .aftersales-time {
      padding-top: 6rpx;
      text-align: left;
    }
    .aftersales-amount {
      text-align: right;
    }
  }

  .aftersales-play-box {
    .aftersales-type-item {
      float: left;
      .aftersales-type {
        margin-top: 10rpx;
        display: inline-block;
      }
    }
    .play-btn-item,
    .confirm-btn-item,
    .comment-btn-item,
    .detail-btn-item {
      text-align: right;
      float: right;
      .aftersales-btn {
        display: inline-block;
      }
    }
  }

  .f24 {
    font-size: 24rpx !important;
  }
}

.aftersales-empty-data {
  margin-top: 100rpx;
  text-align: center;
}
</style>
