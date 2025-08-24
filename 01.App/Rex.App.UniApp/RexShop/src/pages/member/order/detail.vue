<template>
  <view class="order-detail-body">
    <!--
    <view class="order-steps-box">
      <up-steps activeColor="#fa3534" direction="row" :current="orderSteps.current">
        <up-steps-item
          v-for="(item, index) in orderSteps.items"
          :key="index"
          :title="item.title"
          :desc="item.desc"
        ></up-steps-item>
      </up-steps>
    </view>
    -->
    <view class="order-logistics-box" v-if="userOrder?.shipAreaId">
      <view class="order-logistics-title">物流信息</view>
      <view class="logistics-detail-item">
        <up-row customStyle="margin: 0rpx">
          <up-col span="1">
            <view class="logistics-dtl-icon dtl-text-align-l">
              <rexshop-al-icon
                color="#666666"
                size="38rpx"
                icon="custom-icon-dizhiguanli"
              />
            </view>
          </up-col>
          <up-col span="10">
            <view class="receiving-user-info">
              <text class="user-name">收货人：{{ userOrder?.shipName }}</text>
              <text class="user-phone-number">{{ userOrder?.shipMobile }}</text>
            </view>
            <view class="full-address">
              {{ userOrder?.displayArea }} {{ userOrder?.shipAddress }}</view
            >
          </up-col>
          <up-col span="1">
            <view class="logistics-dtl-icon dtl-text-align-r">
              <up-copy
                notice="收货地址复制成功！"
                :content="
                  userOrder?.shipName +
                  ' ' +
                  userOrder?.shipMobile +
                  '\n' +
                  userOrder?.displayArea +
                  userOrder?.shipAddress
                "
              >
                <rexshop-al-icon color="#19be6b" size="38rpx" icon="custom-icon-copy" />
              </up-copy>
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <view class="order-product-box">
      <view class="order-product-title">商品信息</view>
      <template v-for="orderItem in userOrder?.orderItems" :key="orderItem.Id">
        <view
          class="product-list-item"
          @click="routeHelper.goGoodsDetail(orderItem.goodId)"
        >
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
                  <view class="product-price f24 float-l">{{ orderItem.amount }}</view>
                  <view class="product-num float-r">× {{ orderItem.nums }}</view>
                  <view class="clear-both"></view>
                </view>
              </view>
            </up-col>
          </up-row>
        </view>
      </template>
    </view>

    <view class="order-detail-box">
      <view class="order-detail-title">订单信息</view>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="order-detail-name">订单编号</view>
        </up-col>
        <up-col span="6">
          <view class="order-detail-value">
            <up-copy notice="订单编号复制成功！" :content="userOrder?.no">
              <text>{{ userOrder?.no }}</text>
            </up-copy>
          </view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="order-detail-name">订单类型</view>
        </up-col>
        <up-col span="6">
          <view class="order-detail-value">{{
            formatOrderType(userOrder?.orderType)
          }}</view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="order-detail-name">下单时间</view>
        </up-col>
        <up-col span="6">
          <view class="order-detail-value">{{ userOrder?.creationTime }}</view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 10rpx 15rpx">
        <up-col span="6">
          <view class="order-detail-name">订单状态</view>
        </up-col>
        <up-col span="6">
          <view class="order-detail-value">{{ formatOrderStatus(userOrder) }}</view>
        </up-col>
      </up-row>
    </view>

    <view class="order-money-box" v-if="userOrder">
      <view class="order-money-title">费用信息</view>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx">
        <up-col span="6">
          <view class="order-money-name">商品总额</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">￥{{ userOrder.goodAmount }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-show="userOrder.gradeDiscountAmount > 0"
      >
        <up-col span="6">
          <view class="order-money-name">会员优惠</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">- {{ userOrder.gradeDiscountAmount }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-show="userOrder.goodsDiscountAmount > 0"
      >
        <up-col span="6">
          <view class="order-money-name">商品优惠</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">- {{ userOrder.goodsDiscountAmount }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-show="userOrder.orderDiscountAmount > 0"
      >
        <up-col span="6">
          <view class="order-money-name">订单优惠</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">- {{ userOrder.orderDiscountAmount }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-show="userOrder.pointMoney > 0"
      >
        <up-col span="6">
          <view class="order-money-name">积分抵扣</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">- {{ userOrder.pointMoney }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-show="userOrder.couponDiscountAmount > 0"
      >
        <up-col span="6">
          <view class="order-money-name">优惠劵抵扣</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">- {{ userOrder?.couponDiscountAmount }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-show="userOrder.costFreight > 0"
      >
        <up-col span="6">
          <view class="order-money-name">运费</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">+ {{ userOrder.costFreight }}</view>
        </up-col>
      </up-row>
      <up-row
        customStyle="padding: 10rpx 15rpx 0rpx 15rpx"
        v-if="formatPaymentType(userOrder?.paymentCode)"
      >
        <up-col span="6">
          <view class="order-money-name">支付方式</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">{{
            formatPaymentType(userOrder?.paymentCode)
          }}</view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 0rpx 15rpx" v-if="userOrder?.paymentTime">
        <up-col span="6">
          <view class="order-money-name">支付时间</view>
        </up-col>
        <up-col span="6">
          <view class="order-money-value">{{ userOrder?.paymentTime }}</view>
        </up-col>
      </up-row>
      <up-row customStyle="padding: 10rpx 15rpx 10rpx 15rpx">
        <up-col span="12">
          <view class="order-money-value amount-value"
            >应付款：￥{{ userOrder?.orderAmount }}</view
          >
        </up-col>
      </up-row>
    </view>

    <view
      class="order-footer-btn"
      v-if="userOrder?.status == 1 || userOrder?.status == 2"
    >
      <view class="order-btn order-cancel" v-if="showCancelOrderBtn(userOrder)">
        <up-button
          type="info"
          size="small"
          text="取消订单"
          :plain="true"
          @click="cancelOrder(userOrder)"
        ></up-button>
      </view>
      <view class="order-btn order-play" v-if="showPayBtn(userOrder)">
        <up-button
          type="error"
          size="small"
          text="立即支付"
          :plain="true"
          @click="goPlay(userOrder?.no)"
        ></up-button>
      </view>
      <view class="order-btn order-confirm" v-if="showConfirmReceiptBtn(userOrder)">
        <up-button
          type="success"
          size="small"
          text="确认收货"
          :plain="true"
          @click="onConfirmOrder(userOrder)"
        ></up-button>
      </view>
      <view class="order-btn order-comment" v-if="showCommentBtn(userOrder)">
        <up-button
          type="primary"
          size="small"
          text="立即评价"
          :plain="true"
          @click="onComment"
        ></up-button>
      </view>
      <view
        class="order-btn order-apply-after-sales"
        v-if="showApplyAftersalesBtn(userOrder)"
      >
        <up-button
          type="primary"
          size="small"
          text="申请售后"
          :plain="true"
          @click="onAfterSale"
        ></up-button>
      </view>
      <view
        class="order-btn order-viewer-after-sales"
        v-if="showViewerAftersalesBtn(userOrder)"
      >
        <up-button
          type="info"
          size="small"
          text="查看售后"
          :plain="true"
          @click="goAftersalesDetail"
        ></up-button>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";
import { onLoad, onShow } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { formatOrderType, formatOrderStatus, formatPaymentType } from "@/utils/other";
import { useUserLoginStore } from "@/stores/userLogin/index";
import routeHelper from "@/utils/routeHelper";

// 订单状态
const orderSteps = reactive({
  items: [
    { title: "下单", desc: "" },
    { title: "付款", desc: "" },
    { title: "发货", desc: "" },
    { title: "收货", desc: "" },
    { title: "评价", desc: "" },
  ],
  current: 0,
});
// 用户订单
const orderId = ref("");
const userOrder = ref<UserOrderType>();

// 查询用户订单
const loadUserOrder = (orderId: string) => {
  http<UserOrderType>({
    url: "/api/order/orders/" + orderId + "/user-order",
    method: "GET",
  })
    .then((result) => {
      if (!result) return;
      userOrder.value = result;
      currentOrderStatus(userOrder.value);
    })
    .catch((err) => {
      console.error(err);
    });
};

// 触发确认收货
const onConfirmOrder = (uOrder: UserOrderType) => {
  const userId = useUserLoginStore().currentSysUser?.id;
  uni.showModal({
    title: "提示",
    content: "您确定要收货吗？",
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
              loadUserOrder(orderId.value);
              uni.$emit("refreshOrderTypeHandle", true);
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

// 查看售后单详情
const goAftersalesDetail = () => {
  if (!userOrder.value?.aftersales || userOrder.value?.aftersales.length < 1) return;
  let url = `/pages/member/afterSales/detail?aftersalesId=${userOrder.value.aftersales[0].id}`;
  if (userOrder.value.aftersales.length > 1) {
    url = "/pages/member/afterSales/list";
  }
  uni.navigateTo({
    url,
    success: (result) => {
      console.log("已跳转到售后单详情！", result);
    },
  });
};

// 取消订单
const cancelOrder = (uOrder: UserOrderType) => {
  const userId = useUserLoginStore().currentSysUser?.id;
  uni.showModal({
    title: "提示",
    content: "您确定要将该订单取消吗？",
    success: function (res) {
      if (res.confirm) {
        http<boolean>({
          url: `/api/order/orders/user-cancel/${userId}`,
          method: "PUT",
          data: [uOrder.id],
        })
          .then((result) => {
            let cancelMsg = "该订单已取消！";
            if (result) {
              loadUserOrder(orderId.value);
              uni.$emit("refreshOrderTypeHandle", true);
            } else {
              cancelMsg = "订单取消失败！";
            }
            uni.showToast({
              icon: "none",
              title: cancelMsg,
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

// 当前订单状态
const currentOrderStatus = (uOrder: UserOrderType) => {
  if (uOrder.status == 1) {
    // 订单正常
    if (uOrder.payStatus == 1) {
      orderSteps.current = 0;
      orderSteps.items[orderSteps.current].desc =
        "您已拍下，1分钟后未支付该订单则自动取消！";
    } else if (uOrder.payStatus >= 2 && uOrder.shipStatus == 1) {
      orderSteps.current = 1;
      orderSteps.items[orderSteps.current].desc = "支付成功，待卖家发货！";
    } else if (uOrder.payStatus >= 2 && uOrder.shipStatus == 2) {
      orderSteps.current = 2;
      orderSteps.items[orderSteps.current].desc = "已发货，快递正在路上！";
    } else if (
      uOrder.payStatus >= 2 &&
      uOrder.shipStatus >= 3 &&
      uOrder.confirmStatus == 1
    ) {
      orderSteps.current = 2;
      orderSteps.items[orderSteps.current].desc =
        "已发货，快递正在路上，务必在收到商品后再确认收货(发货20天后将自动确认收货)！";
    } else if (
      uOrder.payStatus >= 2 &&
      uOrder.shipStatus >= 3 &&
      uOrder.confirmStatus >= 2 &&
      uOrder.isComment == false
    ) {
      orderSteps.current = 3;
      orderSteps.items[orderSteps.current].desc =
        "已收货，请您对此次购物体检进行评价(收货30天后将自动评价)！";
    } else if (
      uOrder.payStatus >= 2 &&
      uOrder.shipStatus >= 3 &&
      uOrder.confirmStatus >= 2 &&
      uOrder.isComment == true
    ) {
      orderSteps.current = 4;
      orderSteps.items[orderSteps.current].desc = "交易成功，感谢您的评价！";
    }
  } else if (uOrder.status == 2) {
    // 订单完成
    orderSteps.current = 4;
    orderSteps.items[orderSteps.current].desc = "交易成功.期待下次服务。";
  } else if (uOrder.status == 3) {
    // 订单取消
    orderSteps.current = 0;
    orderSteps.items[orderSteps.current].desc = "该订单，您已取消！";
  }
};

// 显示[取消订单]按钮
const showCancelOrderBtn = (uOrder: UserOrderType) => {
  return uOrder.status == 1 && uOrder.payStatus == 1 && uOrder.shipStatus == 1;
};

// 显示[立即支付]按钮
const showPayBtn = (uOrder: UserOrderType) => {
  return uOrder.status == 1 && uOrder.payStatus == 1;
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

// 显示[申请售后]按钮
const showApplyAftersalesBtn = (uOrder: UserOrderType) => {
  return uOrder.isAftersalesStatus;
};

// 显示[查看售后]按钮
const showViewerAftersalesBtn = (uOrder: UserOrderType) => {
  return uOrder.aftersales != null && uOrder.aftersales.length > 0;
};

// 跳转到支付页
const goPlay = (orderNo: string | undefined) => {
  if (!orderNo) return;
  uni.navigateTo({
    url: "/pages/payment/index?playType=1&orderNo=" + orderNo,
  });
};

// 跳转到售后申请
const onAfterSale = () => {
  uni.navigateTo({
    url: "/pages/member/afterSales/index?orderId=" + userOrder.value?.id,
  });
};

// 评价
const onComment = () => {
  uni.navigateTo({
    url: "/pages/member/order/comment?orderId=" + userOrder.value?.id,
  });
};

onLoad((evt: any) => {
  if (!evt.orderId) {
    uni.navigateBack();
    return;
  }
  orderId.value = evt.orderId;
});

onShow(() => {
  loadUserOrder(orderId.value);
});
</script>

<style lang="scss">
.order-detail-body {
  .u-steps-item__content__title {
    .u-text {
      position: relative;
      top: -12rpx;
    }
  }
}
</style>
<style lang="scss" scoped>
.order-detail-body {
  padding: 20rpx 0rpx;
  padding-bottom: 100rpx;
  .order-steps-box {
    margin: 0rpx 20rpx;
    background-color: white;
    padding: 20rpx 15rpx;
    border-radius: 10rpx;
    .order-item-tips {
      margin-top: 20rpx;
      text-align: left;
      .order-item-desc {
        padding: 0px 50rpx;
      }
    }
  }

  .order-logistics-box {
    margin: 0rpx 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .order-logistics-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .logistics-detail-item {
      padding: 10rpx 18rpx;
      .logistics-dtl-icon {
        width: 100%;
        display: inline-block;
      }
      .dtl-text-align-l {
        text-align: left;
      }
      .dtl-text-align-r {
        text-align: right;
      }
      .receiving-user-info {
        font-size: 26rpx;
        color: #2c313b;
        .user-phone-number {
          margin-left: 15rpx;
        }
      }
      .full-address {
        font-size: 24rpx;
        color: #999999;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 3;
      }
    }
  }

  .order-product-box {
    margin: 20rpx 20rpx 0rpx 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .order-product-title {
      font-size: 28rpx;
      margin-bottom: 10rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .product-list-item {
      padding: 10rpx 18rpx;
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
    .product-list-item:last-child {
      padding-bottom: 20rpx;
    }
  }
  .order-money-box {
    margin: 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .order-money-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .order-money-name {
      text-align: left;
      font-size: 26rpx;
    }
    .order-money-value {
      text-align: right;
      font-size: 26rpx;
      color: #909399;
    }
    .amount-value {
      font-weight: bold;
      color: #2c313b;
    }
  }
  .order-detail-box {
    margin: 20rpx;
    border-radius: 10rpx;
    background-color: white;
    .order-detail-title {
      font-size: 28rpx;
      padding: 10rpx 15rpx;
      border-bottom: 1px #f2f3f7 solid;
    }
    .order-detail-name {
      text-align: left;
      font-size: 26rpx;
    }
    .order-detail-value {
      text-align: right;
      font-size: 26rpx;
      color: #909399;
    }
  }
  .order-footer-btn {
    border-top: 1px #f2f3f7 solid;
    background-color: white;
    position: fixed;
    bottom: 0rpx;
    width: 100%;
    text-align: center;
    height: 100rpx;
    line-height: 96rpx;
    .order-btn {
      display: inline-block;
      margin: 0rpx 20rpx;
    }
  }
}
</style>
