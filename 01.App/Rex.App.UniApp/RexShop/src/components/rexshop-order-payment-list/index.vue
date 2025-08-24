<template>
  <view class="order-play-body" v-if="userOrder && userOrder.no">
    <view class="order-payment-detail">
      <u-cell-group>
        <u-cell title="订单类型" :isLink="false" :titleStyle="cellItemTitleStyle">
          <template #value>
            <view class="order-cell-item">{{
              formatOrderType(userOrder.orderType)
            }}</view>
          </template>
        </u-cell>
        <u-cell title="订单编号" :isLink="false" :titleStyle="cellItemTitleStyle">
          <template #value>
            <view class="order-cell-item">
              <up-copy notice="订单编号复制成功！" :content="userOrder.no">
                <text>{{ userOrder.no }}</text>
              </up-copy>
            </view>
          </template>
        </u-cell>
        <u-cell title="订单金额" :isLink="false" :titleStyle="cellItemTitleStyle">
          <template #value>
            <view class="order-cell-item">
              <view class="product-price">{{ userOrder.orderAmount }}</view>
            </view>
          </template>
        </u-cell>
        <u-cell
          title="付款方式"
          :isLink="true"
          :titleStyle="cellItemTitleStyle"
          @click="onShowPayment"
        >
          <template #value>
            <view class="order-cell-item">
              {{ selPaymentCode.name }}
            </view>
          </template>
        </u-cell>
      </u-cell-group>
    </view>

    <!--提示信息-->
    <view class="payment-text-gray u-padding-20 u-font-sm">
      注：如果您在支付中出现异常，您可以到[我的-我的订单-待付款]中进行尝试或联系客服。
    </view>

    <up-action-sheet
      title="选择支付方式"
      :show="showPaymentCode"
      :actions="paymentCodeList"
      :safeAreaInsetBottom="true"
      @select="onSelectPaymentCode"
      @close="onClosePayment"
    >
    </up-action-sheet>
  </view>
</template>

<script setup lang="ts" name="orderPayList">
import { ref, defineProps, onMounted } from "vue";
import { formatOrderType } from "@/utils/other";
import { http } from "@/utils/http";

// 定义父组件传过来的值
const props = defineProps({
  orderNo: {
    type: String,
    default: () => "",
  },
});

// 定义子组件向父组件传值/事件
const emit = defineEmits(["completed"]);

// 定义变量
const showPaymentCode = ref(false);
const selPaymentCode = ref({
  name: "微信支付",
  value: "WechatPay",
});
const paymentCodeList = ref([
  {
    name: "微信支付",
    value: "WechatPay",
  },
  {
    name: "余额支付",
    value: "BalancePay",
  },
]);
const userOrder = ref<OrderType>({} as OrderType);
const cellItemTitleStyle = ref({
  fontSize: "28rpx",
  color: "#8799a3",
});

// [显示]选择支付方式
const onShowPayment = () => {
  showPaymentCode.value = true;
};

// [关闭]选择支付方式
const onClosePayment = () => {
  showPaymentCode.value = false;
};

// 选择中支付方式
const onSelectPaymentCode = (value: any) => {
  selPaymentCode.value = value;
  emit("completed", userOrder.value, selPaymentCode.value.value);
};

// 查询用户订单
let loadNum = 0;
const loadUserOrder = () => {
  http<OrderType>({
    url: "/api/order/orders/user-order-no",
    method: "GET",
    data: {
      no: props.orderNo,
    },
  })
    .then((result) => {
      loadNum++;
      if (result && result.no) {
        userOrder.value = result;
        emit("completed", userOrder.value, selPaymentCode.value.value);
        return;
      }
      if (loadNum > 8) {
        uni.showToast({
          title: "系统繁忙，请稍后重试！",
          icon: "none",
        });
        setTimeout(function () {
          uni.switchTab({
            url: "/pages/cart/index",
          });
        }, 3000);
        return;
      }
      setTimeout(() => {
        loadUserOrder();
      }, 1000);
    })
    .catch((err) => {
      console.error(err);
    });
};

// 获取用户订单
const getUserOrder = () => {
  return userOrder.value;
};

onMounted(() => {
  loadUserOrder();
});

// 暴露变量
defineExpose({
  getUserOrder,
});
</script>
<style lang="scss" scoped>
.order-play-body {
  .order-payment-detail {
    background-color: white;
    .order-cell-item {
      font-size: 28rpx;
      color: #8799a3;
    }
  }
  .payment-text-gray {
    font-size: 24rpx;
    color: #aaaaaa;
  }
}
</style>
