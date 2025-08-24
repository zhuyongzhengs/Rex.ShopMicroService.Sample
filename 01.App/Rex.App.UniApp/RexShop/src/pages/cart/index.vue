<template>
  <view class="cart-body">
    <!--无网络组件-->
    <u-no-network></u-no-network>

    <!--头部组件-->
    <u-navbar
      :leftIcon="false"
      :title="navItem.homeTitle"
      :bgColor="navItem.background.backgroundColor"
      :title-color="navItem.titleColor"
      :titleStyle="{ color: navItem.titleColor }"
    />

    <!-- 没有数据 -->
    <view class="cart-empty-data" v-if="!shoppingCarts || shoppingCarts.length < 1">
      <u-empty mode="car" icon="/static/images/good/car.png" text="您的购物车空空如也" />
      <view class="cart-category-btn">
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

    <!-- 有数据 -->
    <view class="cart-list-box" v-else>
      <template v-for="shoppingCart in shoppingCarts" :key="shoppingCart.productId">
        <checkbox-group @change="shoppingCartCheckedChange(shoppingCart.id)">
          <view class="cart-module-item">
            <view
              class="good-name"
              @tap="routeHelper.goGoodsDetail(shoppingCart.goodId)"
              >{{ shoppingCart.goodName }}</view
            >
            <view class="cart-product-item">
              <up-row customStyle="margin: 0px">
                <up-col span="1">
                  <view class="cart-checkbox-item">
                    <checkbox
                      style="transform: scale(0.8)"
                      :value="shoppingCart.productId"
                      :checked="shoppingCart.isChecked"
                    />
                  </view>
                </up-col>
                <up-col span="11">
                  <up-row customStyle="margin: 0px">
                    <up-col span="2.5">
                      <view class="product-images">
                        <view style="display: inline-block">
                          <up-image
                            width="100rpx"
                            height="100rpx"
                            radius="5"
                            :show-loading="true"
                            :src="shoppingCart.images"
                          />
                        </view>
                      </view>
                    </up-col>
                    <up-col span="9.5">
                      <view class="product-spes">
                        {{ shoppingCart.spesDesc }}
                      </view>
                      <view class="product-number">
                        <view class="product-price float-l">{{
                          shoppingCart?.price
                        }}</view>
                        <view class="product-nums float-r">
                          <!--
                          <u-number-box
                            buttonSize="25"
                            v-model="shoppingCart.nums"
                            :max="shoppingCart.stock"
                            :min="1"
                            :step="1"
                            :integer="true"
                            @change="cartNumsChange($event, shoppingCart)"
                          ></u-number-box>
                          -->
                          <up-text type="info" :text="`× ${shoppingCart.nums}`"></up-text>
                        </view>
                        <view class="clear-both"></view>
                      </view>
                    </up-col>
                  </up-row>
                </up-col>
              </up-row>
            </view>
            <view class="product-del">
              <view class="del-icon-btn">
                <u-icon
                  name="trash"
                  color="#aaaaaa"
                  size="30rpx"
                  @click="deleteCart(shoppingCart.id)"
                ></u-icon>
              </view>
            </view>
          </view>
        </checkbox-group>
      </template>
    </view>

    <!-- 购物车结算 -->
    <view class="cart-settlement-footer">
      <up-row customStyle="margin: 0px">
        <up-col span="3">
          <view class="cart-checkbox-all">
            <checkbox-group @change="checkboxAllChange">
              <label>
                <checkbox value="all" :checked="isCheckboxAll" />
                <text>全选</text>
              </label>
            </checkbox-group>
          </view>
        </up-col>
        <up-col span="6">
          <view class="cart-calc-price"
            >合计：<text class="calc-price">￥{{ calcPrice }}</text></view
          >
        </up-col>
        <up-col span="3">
          <view
            class="cart-calc-btn"
            :class="orderBtnDisabled ? 'cart-order-btn-disabled' : ''"
            @tap="orderSettlement"
            >立即结算</view
          >
        </up-col>
      </up-row>
    </view>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref, computed } from "vue";
import { onShow } from "@dcloudio/uni-app";
import { mainNabBar } from "@/utils/theme";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import routeHelper from "@/utils/routeHelper";

// 定义变量
const uToastRef = ref();
const navItem = ref({
  homeTitle: "购物车",
  background: mainNabBar.background,
  titleColor: mainNabBar.titleColor,
});
const shoppingCarts = ref<ShoppingCartType[] | undefined>();
const isCheckboxAll = ref(false); // 全选

// 合计
const calcPrice = computed(() => {
  if (!shoppingCarts.value) return 0;
  let priceSum = shoppingCarts.value.reduce(function (initVal, cart) {
    return initVal + cart.nums * cart.price;
  }, 0);
  return Number(priceSum.toFixed(2));
});

// 结算按钮禁用
const orderBtnDisabled = computed(() => {
  if (!shoppingCarts.value || shoppingCarts.value.filter((x) => x.isChecked).length < 1) {
    return true;
  }
  return false;
});

// 全选切换
const checkboxAllChange = (event: any) => {
  isCheckboxAll.value = event.detail.value.length > 0;
  if (shoppingCarts.value) {
    for (let i = 0; i < shoppingCarts.value.length; i++) {
      shoppingCarts.value[i].isChecked = isCheckboxAll.value;
    }
  }
};

// 购物车选中切换
const shoppingCartCheckedChange = (cartId: string) => {
  let shoppingCart = shoppingCarts.value?.find((x) => x.id == cartId);
  if (shoppingCart) shoppingCart.isChecked = !shoppingCart.isChecked;
};

// 商品数量切换
const cartNumsChange = (item: any, shoppingCart: ShoppingCartType) => {
  http({
    method: "PUT",
    url: `/api/front/aggregation/cart/${shoppingCart.id}/nums/${item.value}`,
    data: {},
  })
    .then((result) => {
      if (!result) return;
      console.log("修改[商品数量]成功！");
    })
    .catch((err: any) => {
      console.error("修改[商品数量]出错：", err);
    });
};

// 订单结算
const orderSettlement = () => {
  // 得到选择的订单
  const cartIds = shoppingCarts.value
    ?.filter((cart) => cart.isChecked)
    .map((cart) => cart.id);
  if (!cartIds || cartIds.length < 1) return;

  // 购物车参数
  let strCartIds = JSON.stringify(cartIds);
  isCheckboxAll.value = false;
  uni.navigateTo({
    url: "/pages/placeOrder/index?cartIds=" + strCartIds,
  });
};

// 查询用户购物车
const loadUserCartInfo = () => {
  http<ShoppingCartType[]>({
    method: "GET",
    url: "/api/front/aggregation/cart?type=1",
    data: {},
  })
    .then((result) => {
      if (!result) return;
      shoppingCarts.value = result;
      if (isCheckboxAll.value && shoppingCarts.value) {
        for (let i = 0; i < shoppingCarts.value.length; i++) {
          shoppingCarts.value[i].isChecked = true;
        }
      }
    })
    .catch((err: any) => {
      console.error("查询[用户购物车]出错：", err);
    });
};

// 删除购物车
const deleteCart = (id: string) => {
  uni.showModal({
    title: "提示",
    content: "您确定要将该商品从购物车中移除吗？",
    success: function (res) {
      if (res.confirm) {
        http<boolean>({
          method: "DELETE",
          url: "/api/order/cart/many?ids=" + id,
        })
          .then((result) => {
            if (!result) return;
            shoppingCarts.value = shoppingCarts.value?.filter((cart) => cart.id !== id);
            uToastRef.value.success("已移除！");
          })
          .catch((err: any) => {
            console.error("删除[用户购物车]出错：", err);
          });
      }
    },
  });
};

// 跳转到分类
const goCategory = () => {
  uni.switchTab({
    url: "/pages/category/index",
  });
};

onShow(() => {
  if (useUserLoginStore().hasLogin) {
    // 获取用户购物车信息
    loadUserCartInfo();
  }
});
</script>

<style lang="scss" scoped>
.cart-body {
  padding-top: 130rpx;
}
.cart-empty-data {
  margin-top: 20%;
  text-align: center;
  .cart-category-btn {
    display: inline-block;
    margin: 30rpx auto;
  }
}
.cart-list-box {
  padding: 20rpx;
  .cart-module-item {
    margin-bottom: 20rpx;
    background-color: white;
    border-radius: 10rpx;
    padding: 15rpx;
    .good-name {
      font-size: 26rpx;
      font-weight: bold;
    }

    .cart-product-item {
      margin: 20rpx 0rpx;
      .cart-checkbox-item {
        text-align: center;
        padding-left: 5rpx;
      }

      .product-images {
        text-align: center;
      }

      .product-spes {
        font-size: 24rpx;
        text-align: left;
        color: #aaaaaa;
        margin-bottom: 15rpx;
      }
    }

    .product-del {
      text-align: right;
      .del-icon-btn {
        display: inline-block;
      }
    }
  }
}

.cart-settlement-footer {
  width: 100%;
  background-color: white;
  position: fixed;
  bottom: 0;
  height: 90rpx;
  line-height: 90rpx;
  font-size: 26rpx;
  .cart-checkbox-all {
    text-align: center;
  }
  .cart-calc-price {
    text-align: center;
    .calc-price {
      color: #f56c6c;
    }
  }
  .cart-calc-btn {
    text-align: center;
    background-color: #e45656;
    color: white;
  }
  .cart-order-btn-disabled {
    background-color: #f7b2b2 !important;
  }
}
</style>
