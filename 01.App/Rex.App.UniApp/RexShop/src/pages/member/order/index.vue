<template>
  <view class="order-body">
    <view class="order-tabs-box">
      <up-tabs
        lineColor="#f56c6c"
        :list="orderTypeList"
        :current="currOrderType.index"
        :activeStyle="{ color: '#f56c6c', fontSize: '26rpx' }"
        :inactiveStyle="{ fontSize: '26rpx' }"
        :itemStyle="{ height: '75rpx', width: '20%' }"
        @click="orderTypeHandle"
      ></up-tabs>
    </view>
    <template v-for="uOrder in userOrder.data" :key="uOrder.id">
      <view class="order-list-box">
        <view @tap="goOrderDetail(uOrder.id)">
          <view class="order-no-item float-l">
            <text>订单：{{ uOrder.no }}</text>
            <view style="display: inline-block">
              <up-icon name="arrow-right" color="#606266" size="14"></up-icon>
            </view>
          </view>
          <view class="order-state-item float-r" :class="orderStateColor(uOrder)">
            <text>{{ formatOrderStatus(uOrder) }}</text>
          </view>
          <view class="clear-both u-margin-bottom-10"></view>
          <up-line margin="10rpx 0rpx 10px 0px"></up-line>
          <template v-for="orderItem in uOrder.orderItems" :key="orderItem.Id">
            <view class="product-list-box u-margin-bottom-15">
              <up-row customStyle="margin: 0rpx">
                <up-col span="3">
                  <view class="product-image">
                    <up-image
                      width="164rpx"
                      height="164rpx"
                      radius="6rpx"
                      :show-loading="true"
                      :src="orderItem.imageUrl"
                    />
                  </view>
                </up-col>
                <up-col span="9">
                  <view class="product-detail">
                    <view class="product-title">{{ orderItem.name }}</view>
                    <view class="product-spec">{{ orderItem.addon }}</view>
                    <view class="product-money">
                      <view class="product-price f24 float-l">{{
                        orderItem.amount
                      }}</view>
                      <view class="product-num float-r">× {{ orderItem.nums }}</view>
                      <view class="clear-both"></view>
                    </view>
                  </view>
                </up-col>
              </up-row>
            </view>
          </template>
          <up-line margin="20rpx 0rpx 10rpx 0rpx"></up-line>
          <view class="good-order-box">
            <up-row customStyle="margin: 0px">
              <up-col span="7">
                <view class="order-time">下单时间：{{ uOrder.creationTime }}</view>
              </up-col>
              <up-col span="5">
                <view class="order-total">
                  共<text>{{ uOrder.productNums }}</text
                  >件商品&nbsp;&nbsp;合计:<text class="product-price">{{
                    uOrder.orderAmount
                  }}</text>
                </view>
              </up-col>
            </up-row>
          </view>
        </view>
        <view class="order-play-box u-margin-top-10">
          <view class="order-type-item">
            <view class="order-type">
              <up-tag
                size="mini"
                plain
                plainFill
                :text="formatOrderType(uOrder.orderType)"
              ></up-tag>
            </view>
          </view>
          <view class="play-btn-item" v-if="showPayBtn(uOrder)">
            <view class="order-btn">
              <up-button
                type="error"
                size="small"
                text="立即支付"
                :plain="true"
                @click="goPlay(uOrder.no)"
              ></up-button>
            </view>
          </view>
          <view class="confirm-btn-item" v-if="showConfirmReceiptBtn(uOrder)">
            <view class="order-btn">
              <up-button
                type="success"
                size="small"
                text="确认收货"
                :plain="true"
                @click="onConfirmOrder(uOrder)"
              ></up-button>
            </view>
          </view>
          <view class="comment-btn-item" v-if="showCommentBtn(uOrder)">
            <view class="order-btn">
              <up-button
                type="primary"
                size="small"
                text="立即评价"
                :plain="true"
                @click="onComment(uOrder.id)"
              ></up-button>
            </view>
          </view>
          <view class="detail-btn-item" v-if="showDetailBtn(uOrder)">
            <view class="order-btn">
              <up-button
                type="info"
                size="small"
                text="查看详情"
                :plain="true"
                @click="goOrderDetail(uOrder.id)"
              ></up-button>
            </view>
          </view>
          <view class="clear-both"></view>
        </view>
      </view>
    </template>
    <view class="order-empty-data" v-show="userOrder.showEmptyData">
      <u-empty mode="car" icon="/static/images/good/car.png" text="您的订单空空如也" />
      <view class="order-category-btn">
        <up-button
          type="warning"
          size="default"
          shape="circle"
          text="随便逛逛"
          customStyle="margin-top: 20rpx;padding: 0rpx 45rpx;"
          @click="goCategory"
        />
      </view>
    </view>
    <view class="u-margin-top-10" v-show="userOrder.data.length > 0">
      <u-loadmore nomore-text="" :status="userOrder.loading" />
    </view>
    <up-back-top :scroll-top="scrollTop" :duration="500"></up-back-top>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom, onPageScroll } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { formatOrderType, formatOrderStatus, removeNullObject } from "@/utils/other";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 定义变量
const scrollTop = ref(0);
const userOrder = reactive({
  data: [] as UserOrderType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    no: "",
    payStatus: null,
    shipStatus: null,
    status: null,
    orderType: null,
    receiptType: null,
    confirmStatus: null,
    isComment: null,
    sorting: "Status ASC,CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return userOrder.data.length < userOrder.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) userOrder.loading = "loadmore";
  else userOrder.loading = "nomore";
  if (userOrder.data.length < 1) {
    userOrder.showEmptyData = Object(true);
  } else {
    userOrder.showEmptyData = null;
  }
};

// 订单类型标签
const orderTypeList = reactive([
  { name: "全部" },
  { name: "待付款" },
  { name: "待发货" },
  { name: "待收货" },
  { name: "待评价" },
]);

// 选中订单类型
const currOrderType = reactive({
  index: 0,
  name: "全部",
});

// 订单状态字体颜色
const orderStateColor = (uOrder: UserOrderType) => {
  let cssColor = "order-state-error";
  if (uOrder.status == 2) {
    cssColor = "order-state-success";
  } else if (uOrder.status == 3) {
    cssColor = "order-state-warning";
  }
  return cssColor;
};

// 显示[立即支付]按钮
const showPayBtn = (uOrder: UserOrderType) => {
  return uOrder.status === 1 && uOrder.payStatus == 1;
};

// 显示[确认收货]按钮
const showConfirmReceiptBtn = (uOrder: UserOrderType) => {
  return (
    uOrder.status == 1 &&
    uOrder.payStatus >= 2 &&
    uOrder.shipStatus >= 3 &&
    uOrder.confirmStatus == 1
  );
};

// 显示[立即评价]按钮
const showCommentBtn = (uOrder: UserOrderType) => {
  return (
    uOrder.status == 1 &&
    uOrder.payStatus >= 2 &&
    uOrder.shipStatus >= 3 &&
    uOrder.confirmStatus >= 2 &&
    uOrder.isComment == false
  );
};

// 显示[查看详情]按钮
const showDetailBtn = (uOrder: UserOrderType) => {
  if (showPayBtn(uOrder)) return false;
  if (showConfirmReceiptBtn(uOrder)) return false;
  if (showCommentBtn(uOrder)) return false;
  return true;
};

// 触发确认收货
const onConfirmOrder = (uOrder: UserOrderType) => {
  const userId = useUserLoginStore().currentSysUser?.id;
  uni.showModal({
    title: "提示",
    content: "您确定要执行收货吗？",
    success: function (res) {
      if (res.confirm) {
        http<boolean>({
          url: `/api/order/orders/confirm`,
          method: "PUT",
          data: {
            orderId: uOrder.id,
            userId: userId,
          },
        })
          .then((result) => {
            let confirmMsg = "收货成功！";
            if (result) {
              uOrder.confirmStatus = 2;
            } else {
              confirmMsg = "收货失败！";
            }
            uni.showToast({
              icon: "none",
              title: confirmMsg,
              duration: 4 * 1000,
            });
          })
          .catch((err) => {
            console.error(err);
          });
      }
    },
  });
};

// 触发订单类型
const orderTypeHandle = (item: any) => {
  userOrder.showEmptyData = null;
  userOrder.param.payStatus = null;
  userOrder.param.shipStatus = null;
  userOrder.param.status = null;
  userOrder.param.orderType = null;
  userOrder.param.receiptType = null;
  userOrder.param.confirmStatus = null;
  userOrder.param.isComment = null;
  switch (item.name) {
    case "待付款":
      userOrder.param.payStatus = Object(1);
      userOrder.param.status = Object(1);
      break;
    case "待发货":
      userOrder.param.payStatus = Object(2);
      userOrder.param.shipStatus = Object(1);
      break;
    case "待收货":
      userOrder.param.status = Object(1); // 订单正常
      userOrder.param.payStatus = Object(2); // 已付款
      userOrder.param.shipStatus = Object(3); // 已发货
      userOrder.param.confirmStatus = Object(1); // 未确定收货
      break;
    case "待评价":
      userOrder.param.status = Object(1); // 订单正常
      userOrder.param.payStatus = Object(2); // 已付款
      userOrder.param.shipStatus = Object(3); // 已发货
      userOrder.param.confirmStatus = Object(2); // 已确定收货
      userOrder.param.isComment = Object(false);
      break;

    default:
      break;
  }
  userOrder.currentPage = 1;
  userOrder.data = [];
  userOrder.total = 0;
  loadUserOrders();
};

// 加载用户订单
const loadUserOrders = () => {
  userOrder.param.skipCount =
    (userOrder.currentPage - 1) * userOrder.param.maxResultCount;
  let rParam = removeNullObject(userOrder.param);
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<UserOrderType[]>>({
    url: "/api/front/aggregation/order/user",
    method: "GET",
    data: rParam,
  }).then((result) => {
    userOrder.total = result.totalCount;
    userOrder.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

// 跳转到分类
const goCategory = () => {
  uni.switchTab({
    url: "/pages/category/index",
  });
};

// 跳转到支付页
const goPlay = (orderNo: string) => {
  uni.navigateTo({
    url: "/pages/payment/index?playType=1&orderNo=" + orderNo,
  });
};

// 查看订单详情
const goOrderDetail = (orderId: string) => {
  uni.navigateTo({
    url: "/pages/member/order/detail?orderId=" + orderId,
    success: (result) => {
      console.log("已跳转到订单详情！", result);
      uni.$once("refreshOrderTypeHandle", function (data) {
        orderTypeHandle(currOrderType);
        console.log("触发了[refreshOrderTypeHandle]事件！");
      });
    },
  });
};

// 评价
const onComment = (orderId: string) => {
  uni.$once("userGoodComment", (isComment: boolean) => {
    let uOrder = userOrder.data.find((x) => x.id == orderId);
    if (uOrder) uOrder.isComment = isComment;
  });
  uni.navigateTo({
    url: "/pages/member/order/comment?orderId=" + orderId,
  });
};

onLoad((evt: any) => {
  var oTypeName = evt.orderTypeName ?? "全部";
  for (let i = 0; i < orderTypeList.length; i++) {
    const oType = orderTypeList[i];
    if (oType.name == oTypeName) {
      currOrderType.index = i;
      currOrderType.name = oType.name;
      break;
    }
  }
  orderTypeHandle(currOrderType);
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    userOrder.currentPage++;
    loadUserOrders();
    console.log("触发了[加载更多]！");
  }
});

onPageScroll((e) => {
  scrollTop.value = e.scrollTop;
});
</script>

<style lang="scss" scoped>
.order-body {
  .order-tabs-box {
    background-color: white;
  }
  .order-list-box {
    background-color: white;
    margin: 20rpx;
    padding: 15rpx;
    border-radius: 15rpx;

    .order-no-item {
      font-size: 26rpx;
      color: #606266;
    }

    .order-state-item {
      font-size: 26rpx;
    }

    .order-state-error {
      color: #dd6161;
    }

    .order-state-warning {
      color: #ff9900;
    }

    .order-state-success {
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
        -webkit-line-clamp: 2;
      }
      .product-spec {
        font-size: 24rpx;
        color: #aaaaaa;
        margin: 8rpx 0rpx;
      }
      .product-money {
        .product-num {
          font-size: 26rpx;
          color: #2c313b;
        }
      }
    }
  }
  .good-order-box {
    font-size: 24rpx;
    color: #606266;
    .order-time {
      padding-top: 6rpx;
      text-align: left;
    }
    .order-total {
      text-align: right;
    }
  }

  .order-play-box {
    .order-type-item {
      float: left;
      .order-type {
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
      .order-btn {
        display: inline-block;
      }
    }
  }

  .f24 {
    font-size: 24rpx !important;
  }
}

.order-empty-data {
  margin-top: 20%;
  text-align: center;
  .order-category-btn {
    display: inline-block;
    margin: 30rpx auto;
  }
}
</style>
