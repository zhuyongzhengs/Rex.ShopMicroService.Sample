<template>
  <view class="place-order-body">
    <up-loading-page
      :loading="
        !cartOrder || !cartOrder.shoppingCarts || cartOrder.shoppingCarts.length < 1
      "
    ></up-loading-page>

    <view class="place-order-box" v-if="cartOrder && cartOrder.shoppingCarts">
      <!-- 收货地址 -->
      <view class="address-btn-item">
        <view class="no-receiving-address" v-if="!userShip || !userShip?.id">
          <up-button type="error" size="mini" text="添加收货地址" @click="addUserShip" />
        </view>
        <view class="receiving-address-detail" v-else>
          <up-row customStyle="margin: 0px" @click="selectUserShip">
            <up-col span="1">
              <view class="address-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-dizhiguanli"
                />
              </view>
            </up-col>
            <up-col span="10">
              <view class="receiving-user-info">
                <text class="user-name">收货人：{{ userShip.name }}</text>
                <text class="user-phone-number">{{ userShip.mobile }}</text>
              </view>
              <view class="full-address"
                >{{ userShip.displayArea }} {{ userShip.address }}</view
              >
            </up-col>
            <up-col span="1">
              <view class="address-icon">
                <u-icon name="arrow-right" color="#888888" size="38rpx"></u-icon>
              </view>
            </up-col>
          </up-row>
        </view>
      </view>

      <!-- 商品信息 -->
      <template v-for="shoppingCart in cartOrder.shoppingCarts" :key="shoppingCart.id">
        <view class="good-order-item u-margin-top-20">
          <up-row customStyle="margin: 0rpx">
            <up-col span="3">
              <view class="good-image">
                <up-image
                  width="164rpx"
                  height="164rpx"
                  radius="6rpx"
                  :show-loading="true"
                  :src="shoppingCart.images"
                />
              </view>
            </up-col>
            <up-col span="9">
              <view class="good-detail">
                <view class="good-title">{{ shoppingCart.goodName }}</view>
                <view class="good-spec">{{ shoppingCart.spesDesc }}</view>
                <view class="good-money">
                  <view class="product-price float-l">{{ shoppingCart.price }}</view>
                  <view class="product-num float-r">× {{ shoppingCart.nums }}</view>
                  <view class="clear-both"></view>
                </view>
              </view>
            </up-col>
          </up-row>
          <up-row customStyle="margin: 0rpx" v-if="!shoppingCart.isStockSufficient">
            <up-col span="12">
              <view class="product-stock-sufficient"
                >该商品库存[{{ shoppingCart.stock }}]不足，请重新选择！</view
              >
            </up-col>
          </up-row>
        </view>
      </template>

      <!-- 优惠卷抵扣 -->
      <view
        class="coupon-money-item u-margin-top-20"
        v-if="cartOrder.couponExchanges && cartOrder.couponExchanges.length > 0"
      >
        <!-- <view class="coupon-title">优惠卷</view> -->
        <view class="coupon-scroll-box">
          <up-scroll-list :indicator="false">
            <view class="scroll-list" style="flex-direction: row">
              <view
                class="scroll-list__coupon-item"
                v-for="(coupon, index) in cartOrder.couponExchanges"
                :key="index"
                :class="[index === 9 && 'scroll-list__coupon-item--no-margin-right']"
              >
                <view class="coupon-detail-item" @tap="onIsUseCoupon(index)">
                  <up-row customStyle="margin: 0px">
                    <up-col span="7">
                      <view class="coupon-name">{{ coupon.couponName }}</view>
                      <view class="coupon-endTime"
                        >有效期至{{ coupon.endTime.split(" ")[0] }}</view
                      >
                    </up-col>
                    <up-col span="3.5">
                      <view class="coupon-discount-content">
                        <view class="coupon-price">{{ coupon.money }}</view>
                        <view class="coupon-desc">{{
                          getResultMsg(coupon.code, coupon.parameters)
                        }}</view>
                      </view>
                    </up-col>
                    <up-col span="1.5">
                      <view class="coupon-use">
                        <up-icon
                          v-if="coupon.isUseCoupon"
                          name="checkmark-circle-fill"
                          color="#5ac725"
                          size="50rpx"
                        ></up-icon>

                        <up-icon
                          v-else
                          name="checkmark-circle"
                          color="#EBEBEC"
                          size="50rpx"
                        ></up-icon>
                      </view>
                    </up-col>
                  </up-row>
                </view>
              </view>
            </view>
          </up-scroll-list>
        </view>
      </view>

      <!-- 积分抵扣 -->
      <view
        class="point-exchange-money u-margin-top-20"
        v-if="cartOrder.pointExchangeItem && cartOrder.pointExchangeItem.money >= 0"
      >
        <up-row customStyle="margin: 0rpx">
          <up-col span="10">
            <view class="point-exchange-name">积分抵扣</view>
            <view class="point-exchange-desc">
              <up-text
                type="info"
                size="12"
                :text="`可用${cartOrder.pointExchangeItem.canUsePoint}积分，可抵扣${cartOrder.pointExchangeItem.money}元，共有${cartOrder.pointExchangeItem.point}积分。`"
              >
              </up-text>
            </view>
          </up-col>
          <up-col span="2">
            <up-switch
              activeColor="#5ac725"
              v-model="cartOrder.pointExchangeItem.isUsePoint"
            ></up-switch>
          </up-col>
        </up-row>
      </view>

      <!-- 商品价格计算 -->
      <view class="good-calc-price-item u-margin-top-20">
        <view class="good-discount-content u-margin-bottom-8">
          <view class="discount-label">商品总额</view>
          <view class="discount-value good-money">{{ cartOrder.goodAmount || "0" }}</view>
          <view class="clear-both"></view>
        </view>
        <view
          class="good-discount-content u-margin-bottom-8"
          v-show="cartOrder.goodGradeMoney > 0"
        >
          <view class="discount-label">会员优惠</view>
          <view class="discount-value"> - {{ cartOrder.goodGradeMoney || "0" }}</view>
          <view class="clear-both"></view>
        </view>
        <view
          class="good-discount-content u-margin-bottom-8"
          v-show="cartOrder.goodPromotionMoney > 0"
        >
          <view class="discount-label">商品优惠</view>
          <view class="discount-value"> - {{ cartOrder.goodPromotionMoney || "0" }}</view>
          <view class="clear-both"></view>
        </view>
        <view
          class="good-discount-content u-margin-bottom-8"
          v-show="cartOrder.goodSeckillMoney > 0"
        >
          <view class="discount-label">秒杀优惠</view>
          <view class="discount-value"> - {{ cartOrder.goodSeckillMoney || "0" }}</view>
          <view class="clear-both"></view>
        </view>
        <view
          class="good-discount-content u-margin-bottom-8"
          v-show="cartOrder.orderPromotionMoney > 0"
        >
          <view class="discount-label">订单优惠</view>
          <view class="discount-value">
            - {{ cartOrder.orderPromotionMoney || "0" }}</view
          >
          <view class="clear-both"></view>
        </view>
        <view
          class="good-discount-content u-margin-bottom-8"
          v-show="cartOrder.couponPromotionMoney > 0"
        >
          <view class="discount-label">优惠劵抵扣</view>
          <view class="discount-value">
            - {{ cartOrder.couponPromotionMoney || "0" }}</view
          >
          <view class="clear-both"></view>
        </view>
        <view
          class="good-discount-content u-margin-bottom-8"
          v-show="cartOrder.pointExchangeMoney > 0"
        >
          <view class="discount-label">积分抵扣</view>
          <view class="discount-value"> - {{ cartOrder.pointExchangeMoney || "0" }}</view>
          <view class="clear-both"></view>
        </view>
        <view class="good-discount-content">
          <view class="discount-label">运费</view>
          <view class="discount-value"> + {{ cartOrder.costFreight || "0" }}</view>
          <view class="clear-both"></view>
        </view>
      </view>

      <!-- 买家留言 -->
      <view class="buyer-msg-item u-margin-top-20">
        <view class="msg-title">买家留言</view>
        <view class="buyer-textarea">
          <u-textarea
            placeholder="50字以内(选填)"
            :maxlength="50"
            :height="50"
            :count="true"
            v-model="cartOrder.memo"
          ></u-textarea>
        </view>
      </view>
    </view>

    <!-- 底部操作 -->
    <view
      class="place-order-footer"
      v-show="cartOrder.shoppingCarts && cartOrder.shoppingCarts.length > 0"
    >
      <up-row customStyle="margin: 0px">
        <up-col span="8">
          <view class="good-total-detail">
            <text class="gd-text">共</text>
            <text class="gd-count">{{ cartOrder.productNums }}</text>
            <text class="gd-text">件商品</text>
            <text class="gd-total">合计</text>
            <text class="gd-text product-price">{{ Number(totalOrderMoney) }}</text>
          </view>
        </up-col>
        <up-col span="4">
          <view class="good-order-btn">
            <view class="order-btn-ok">
              <up-button type="error" size="normal" text="确定下单" @click="goPayment" />
            </view>
          </view>
        </up-col>
      </up-row>
    </view>

    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, computed } from "vue";
import { onLoad, onShow } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { getResultMsg } from "@/utils/other";

// 定义变量
const uToastRef = ref();
const cartOrder = ref<CartOrderType>({} as CartOrderType);
const userShip = ref<UserShipType | undefined>();
const orderConfirm = reactive<OrderConfirmType>({
  cartIds: [],
  orderType: 1, // [订单类型]1：普通、2：拼团、3：团购、4：秒杀、5：积分兑换、6：砍价、7：赠品、8：接龙
  receiptType: 1, // [收货方式]1：物流快递、2：同城配送、3：门店自提
  paymentCode: "1", // [支付方式编码]1：微信支付、2：支付宝支付、3：线下支付
  shipAreaId: 0,
  couponIds: null,
  isUsePoint: false,
  userShipId: "",
  objectId: null,
  orderNo: null,
  memo: null,
});

// 选择优惠劵
const onIsUseCoupon = (index: number) => {
  if (!cartOrder.value.couponExchanges || cartOrder.value.couponExchanges.length < 1)
    return;
  let isUseCoupon = cartOrder.value.couponExchanges[index].isUseCoupon;
  cartOrder.value.couponExchanges[index].isUseCoupon = !isUseCoupon;
};

// 订单总金额
const totalOrderMoney = computed(() => {
  let orderAmount = cartOrder.value.orderAmount;
  let discountMoney = 0;

  // 积分优惠
  if (
    !cartOrder.value.pointExchangeItem ||
    !cartOrder.value.pointExchangeItem.isUsePoint ||
    cartOrder.value.pointExchangeItem.money <= 0
  ) {
    cartOrder.value.pointExchangeMoney = 0;
  } else {
    cartOrder.value.pointExchangeMoney = cartOrder.value.pointExchangeItem.money;
    discountMoney += cartOrder.value.pointExchangeMoney;
  }

  // 优惠卷优惠
  if (
    !cartOrder.value.couponExchanges ||
    cartOrder.value.couponExchanges.length < 1 ||
    cartOrder.value.couponExchanges.filter((x) => x.isUseCoupon).length < 1
  ) {
    cartOrder.value.couponPromotionMoney = 0;
  } else {
    cartOrder.value.couponPromotionMoney = cartOrder.value.couponExchanges.reduce(
      (accumulator, currentValue) => {
        let val = currentValue.isUseCoupon ? currentValue.money : 0;
        return accumulator + val;
      },
      0
    );
    discountMoney += cartOrder.value.couponPromotionMoney;
  }

  return (orderAmount - discountMoney).toFixed(2);
});

// 确认下单[支付]
const goPayment = () => {
  if (!userShip.value || !userShip.value?.id) {
    uToastRef.value.warning("请选择收货地址！");
    return;
  }
  if (cartOrder.value.shoppingCarts.filter((x) => !x.isStockSufficient).length > 0) {
    uToastRef.value.warning("提交的订单存在库存不足，请重新选择！");
    return;
  }

  orderConfirm.orderNo = cartOrder.value?.orderNo;
  orderConfirm.userShipId = userShip.value?.id;
  orderConfirm.shipAreaId = userShip.value?.areaId;
  orderConfirm.memo = cartOrder.value.memo;

  // 优惠劵
  if (
    cartOrder.value.couponExchanges != null &&
    cartOrder.value.couponExchanges.filter((x) => x.isUseCoupon).length > 0
  ) {
    orderConfirm.couponIds = cartOrder.value.couponExchanges
      .filter((x) => x.isUseCoupon)
      .map((m) => m.id);
  }

  // 积分
  orderConfirm.isUsePoint =
    cartOrder.value.pointExchangeItem != null &&
    cartOrder.value.pointExchangeItem.isUsePoint;

  uni.showLoading({
    title: "下单中，请稍等！",
  });
  http<OrderType>({
    method: "POST",
    url: "/api/front/aggregation/cart/orderConfirm",
    data: orderConfirm,
  })
    .then((result) => {
      if (!result) {
        uni.hideLoading();
        return;
      }
      uni.redirectTo({
        url: "/pages/payment/index?playType=1&paymentType=1&orderNo=" + result,
        complete: function () {
          uni.hideLoading();
        },
      });
    })
    .catch((err: any) => {
      console.error("创建订单出错", err);
      const errData = err.data;
      let errMsg = errData?.error?.message;
      if (errMsg) {
        // 刷新订单数据
        loadUserCartGoods(orderConfirm.cartIds);
      }
      setTimeout(() => {
        uni.hideLoading();
        uToastRef.value.warning(errMsg);
      }, 2000);
    });
};

// 查询默认的收货地址
const loadUseShipDefault = () => {
  http<UserShipType>({
    method: "GET",
    url: "/api/base/user-ship/default",
  })
    .then((result) => {
      if (!result) return;
      userShip.value = result;
    })
    .catch((err: any) => {
      console.error(err);
    });
};

// 添加收货地址
const addUserShip = () => {
  uni.navigateTo({
    url: "/pages/member/address/receiving",
  });
};

// 选择收货地址
const selectUserShip = () => {
  uni.$once("selectUserShip", (uShip: UserShipType) => {
    if (uShip) {
      userShip.value = uShip;
      loadUserCartGoods(orderConfirm.cartIds);
    }
  });
  uni.navigateTo({
    url: "/pages/member/address/index?type=option",
  });
};

// 查询购物车商品
const loadUserCartGoods = (ids: string[]) => {
  let shipAreaId = 0;
  if (userShip.value && userShip.value?.areaId) {
    shipAreaId = userShip.value.areaId;
  }
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<CartOrderType>({
    method: "POST",
    url: `/api/front/aggregation/cart/ids?receiptType=${orderConfirm.receiptType}&shipAreaId=${shipAreaId}`,
    data: ids,
  })
    .then((result) => {
      if (!result) return;
      result.memo = result.memo ?? "";
      cartOrder.value = result;
      if (orderConfirm.memo) {
        cartOrder.value.memo = orderConfirm.memo;
      }
      uni.hideLoading();
    })
    .catch((err: any) => {
      console.error("查询[用户购物车商品]出错：", err);
    });
};

onLoad((event: any) => {
  loadUseShipDefault();
  if (event.objectId) {
    orderConfirm.objectId = event.objectId;
    orderConfirm.orderType = 4;
  }
  if (event.cartIds) {
    // 加载购物车商品
    orderConfirm.cartIds = JSON.parse(event.cartIds);
    loadUserCartGoods(orderConfirm.cartIds);
  }
});

onShow(() => {
  if (!userShip.value || !userShip.value?.id) {
    loadUseShipDefault(); // 加载默认收货地址
  }
});
</script>

<style lang="scss">
.u-scroll-list {
  padding-bottom: 0px !important;
}
</style>

<style lang="scss" scoped>
.place-order-body {
  padding-bottom: 120rpx;
  .place-order-box {
    padding: 20rpx;
    .address-btn-item {
      text-align: center;
      background-color: white;
      border-radius: 6rpx;
      padding: 15rpx 0rpx;
      .no-receiving-address {
        display: inline-block;
      }
      .receiving-address-detail {
        .address-icon {
          margin-left: 10rpx;
          display: inline-block;
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
          -webkit-line-clamp: 1;
        }
      }
    }
  }

  .good-order-item {
    background-color: white;
    padding: 20rpx 15rpx;
    border-radius: 6rpx;
    .good-detail {
      padding-left: 10rpx;
      .good-title {
        font-size: 26rpx;
        color: #2c313b;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
      }
      .good-spec {
        font-size: 24rpx;
        color: #aaaaaa;
        margin: 8rpx 0rpx;
      }
      .good-money {
        .product-num {
          font-size: 30rpx;
          color: #2c313b;
        }
      }
    }
    .product-stock-sufficient {
      margin-top: 4rpx;
      border-radius: 4rpx;
      padding: 5rpx 10rpx;
      background-color: #f7b2b2;
      font-size: 26rpx;
      color: white;
    }
  }

  .coupon-money-item {
    background-color: white;
    padding: 15rpx;
    border-radius: 6rpx;
    .coupon-title {
      font-size: 26rpx;
    }
    .coupon-scroll-box {
      text-align: center;
      // padding-top: 15rpx;

      .scroll-list {
        @include flex(column);

        &__coupon-item {
          margin-right: 15rpx;
          .coupon-detail-item {
            width: 450rpx;
            border: 1px red dotted;
            margin: 1px auto;
            border-radius: 8rpx;
            background-color: #fffafa;
            padding: 10rpx 5rpx;
            padding-left: 10rpx;
            .coupon-name {
              font-size: 24rpx;
              font-weight: bold;
              color: #2c313b;
              display: -webkit-box;
              -webkit-box-orient: vertical;
              -webkit-line-clamp: 2;
              overflow: hidden;
              text-overflow: ellipsis;
              max-height: 70rpx;
            }
            .coupon-endTime {
              font-size: 20rpx;
              color: #646464;
            }
            .coupon-discount-content {
              text-align: center;
              .coupon-price {
                color: #f56c6c;
                font-size: 30rpx;
              }
              .coupon-price::before {
                font-size: 20rpx;
                content: "￥";
              }
              .coupon-desc {
                font-size: 20rpx;
                color: #f56c6c;
              }
            }
            .coupon-use {
              width: 100%;
              text-align: center;
            }
          }
        }
      }
    }
  }

  .point-exchange-money {
    background-color: white;
    padding: 15rpx;
    border-radius: 6rpx;
    .point-exchange-name {
      font-size: 26rpx;
    }
  }

  .good-calc-price-item {
    background-color: white;
    padding: 15rpx;
    border-radius: 6rpx;
    .good-discount-content {
      .discount-label {
        float: left;
        font-size: 26rpx;
        color: #2c313b;
      }

      .discount-value {
        float: right;
        font-size: 26rpx;
        color: #aaaaaa;
      }

      .good-money {
        color: #f56c6c;
      }

      .good-money::before {
        font-size: 20rpx;
        content: "￥";
      }
    }
  }

  .buyer-msg-item {
    background-color: white;
    padding: 15rpx;
    border-radius: 6rpx;
    .msg-title {
      font-size: 26rpx;
    }
    .buyer-textarea {
      margin: 10rpx 0rpx;
    }
  }

  .place-order-footer {
    background-color: white;
    padding: 20rpx 15rpx;
    width: 100%;
    height: 80rpx;
    position: fixed;
    bottom: 0rpx;
    border-top: 1px #f2f3f7 solid;
    .good-total-detail {
      font-size: 26rpx;
      font-weight: bold;
      .gd-text {
        display: inline-block;
        margin: 0rpx 10rpx;
      }
    }
    .good-order-btn {
      z-index: 9999;
      .order-btn-ok {
        width: 85%;
        display: inline-block;
      }
    }
  }
}
</style>
