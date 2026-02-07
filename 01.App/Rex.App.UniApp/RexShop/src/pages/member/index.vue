<template>
  <view class="member-body">
    <!--无网络组件-->
    <u-no-network></u-no-network>
    <!-- #ifdef MP-WEIXIN -->
    <rexshop-status-bar />
    <!-- #endif -->
    <view class="head-box">
      <!-- 用户信息 -->
      <view class="user-info-box">
        <!--未登陆-->
        <view class="login-user-view" v-if="!useUserLoginStore().hasLogin">
          <view class="login-avatar-item">
            <view class="user-avatar">
              <up-avatar
                size="50"
                shape="circle"
                :src="useUserLoginStore().currentSysUser?.avatar"
              />
            </view>
          </view>
          <view class="user-login-item">
            <view class="login-btn">
              <u-button type="info" size="mini" @click="goLogin">立即登录</u-button>
            </view>
			<view class="login-btn" style="margin-left: 20rpx;">
				<!-- 模拟调试 -->
			  <u-button type="info" size="mini" @click="goSimulatedLogin">模拟登录</u-button>
			</view>
          </view>
        </view>
        <!-- 已登录 -->
        <view class="user-info-view" v-else>
          <up-row customStyle="margin: 0rpx">
            <up-col span="2">
              <view class="user-avatar-item">
                <up-avatar
                  size="50"
                  shape="circle"
                  :src="useUserLoginStore().currentSysUser?.avatar"
                />
                <view class="user-grade">{{
                  useUserLoginStore().currentSysUser?.grade?.title
                }}</view>
              </view>
            </up-col>
            <up-col span="7">
              <view class="user-detail-item">
                <view class="user-name">{{
                  useUserLoginStore().currentSysUser?.nickName
                }}</view>
                <view class="user-money">
                  <text class="user-point"
                    >积分&nbsp;{{ useUserLoginStore().currentSysUser?.point || 0 }}</text
                  >
                  &nbsp;|&nbsp;
                  <text class="user-balance"
                    >余额&nbsp;{{
                      useUserLoginStore().currentSysUser?.balance || 0
                    }}</text
                  >
                </view>
              </view>
            </up-col>
            <up-col span="3">
              <view class="user-link-item">
                <view class="user-icon-reload" @tap="onRefreshUserInfo(true)">
                  <u-icon name="reload" color="white" size="22"></u-icon>
                </view>
                <view
                  class="user-icon-arrow-right"
                  @tap="goNavigatePage('/pages/member/settings/userInformation', true)"
                >
                  <u-icon name="arrow-right" color="white" size="20"></u-icon>
                </view>
                <view class="clear-both"></view>
              </view>
            </up-col>
          </up-row>
        </view>
      </view>
      <!-- 用户数据 -->
      <view class="user-info-number-box">
        <up-row justify="space-between">
          <up-col
            span="3"
            @click="goNavigatePage('/pages/member/goodBrowsing/index', true)"
          >
            <view class="uinb-item">
              <view class="unib-value">{{ userNoticeNumber.totalGoodBrowsing }}</view>
              <view class="unib-name">足迹</view>
            </view>
          </up-col>
          <up-col span="3" @click="goNavigatePage('/pages/member/coupon/index', true)">
            <view class="uinb-item">
              <view class="unib-value">{{ userNoticeNumber.totalCoupon }}</view>
              <view class="unib-name">优惠劵</view>
            </view>
          </up-col>
          <up-col
            span="3"
            @click="goNavigatePage('/pages/member/goodCollection/index', true)"
          >
            <view class="uinb-item">
              <view class="unib-value">{{ userNoticeNumber.totalGoodCollection }}</view>
              <view class="unib-name">收藏</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="uinb-item"
              @tap="goNavigatePage('/pages/member/afterSales/list', true)"
            >
              <view class="unib-value">{{ userNoticeNumber.totalBillAftersale }}</view>
              <view class="unib-name">售后</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <!-- 订单 -->
    <view class="user-trade-box">
      <view class="trade-title">我的订单</view>
      <view class="user-trade-item">
        <up-row customStyle="margin: 20rpx 0rpx">
          <up-col span="2.4">
            <view class="trade-order-content" @tap="goMyOrder('全部')">
              <view class="trade-order-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="34rpx"
                  icon="custom-icon-quanbudingdan"
                />
              </view>
              <view class="trade-order-label">全部</view>
            </view>
          </up-col>
          <up-col span="2.4">
            <view class="trade-order-content" @tap="goMyOrder('待付款')">
              <view class="trade-order-icon">
                <view v-if="userNoticeNumber.totalPendingPayment" class="order-num"
                  ><up-badge
                    :max="99"
                    :value="userNoticeNumber.totalPendingPayment"
                  ></up-badge
                ></view>
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-daifukuan"
                />
              </view>
              <view class="trade-order-label">待付款</view>
            </view>
          </up-col>
          <up-col span="2.4">
            <view class="trade-order-content" @tap="goMyOrder('待发货')">
              <view class="trade-order-icon">
                <view v-if="userNoticeNumber.totalPendingShipment" class="order-num"
                  ><up-badge
                    :max="99"
                    :value="userNoticeNumber.totalPendingShipment"
                  ></up-badge
                ></view>
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-daifahuo"
                />
              </view>
              <view class="trade-order-label">待发货</view>
            </view>
          </up-col>
          <up-col span="2.4">
            <view class="trade-order-content" @tap="goMyOrder('待收货')">
              <view class="trade-order-icon">
                <view v-if="userNoticeNumber.totalPendingDelivery" class="order-num"
                  ><up-badge
                    :max="99"
                    :value="userNoticeNumber.totalPendingDelivery"
                  ></up-badge
                ></view>
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-daishouhuo"
                />
              </view>
              <view class="trade-order-label">待收货</view>
            </view>
          </up-col>
          <up-col span="2.4">
            <view class="trade-order-content" @tap="goMyOrder('待评价')">
              <view class="trade-order-icon">
                <view v-if="userNoticeNumber.totalPendingEvaluate" class="order-num"
                  ><up-badge
                    :max="99"
                    :value="userNoticeNumber.totalPendingEvaluate"
                  ></up-badge
                ></view>
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-daipingjia"
                />
              </view>
              <view class="trade-order-label">待评价</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <!-- 服务 -->
    <view class="user-service-box">
      <view class="service-title">我的服务</view>
      <view class="user-service-item">
        <up-row customStyle="margin-bottom: 40rpx">
          <up-col span="3">
            <view
              class="service-module-content"
              @tap="goNavigatePage('/pages/member/coupon/index', true)"
            >
              <view class="service-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-wodeyouhuijuan"
                />
              </view>
              <view class="service-module-label">我的优惠劵</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="service-module-content"
              @tap="goNavigatePage('/pages/member/balance/index', true)"
            >
              <view class="service-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-wodeyue"
                />
              </view>
              <view class="service-module-label">我的余额</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="service-module-content"
              @tap="goNavigatePage('/pages/member/pointLog/index', true)"
            >
              <view class="service-module-icon">
                <rexshop-al-icon color="#666666" size="38rpx" icon="custom-icon-jifen" />
              </view>
              <view class="service-module-label">我的积分</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="service-module-content"
              @tap="goNavigatePage('/pages/member/address/index', true)"
            >
              <view class="service-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-dizhiguanli"
                />
              </view>
              <view class="service-module-label">地址管理</view>
            </view>
          </up-col>
        </up-row>
        <up-row customStyle="margin-bottom: 20rpx">
          <up-col span="3">
            <view
              class="service-module-content"
              @tap="goNavigatePage('/pages/member/goodCollection/index', true)"
            >
              <view class="service-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="34rpx"
                  icon="custom-icon-shoucang"
                />
              </view>
              <view class="service-module-label">我的收藏</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="service-module-content"
              @tap="goNavigatePage('/pages/member/goodBrowsing/index', true)"
            >
              <view class="service-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-liulanjilu"
                />
              </view>
              <view class="service-module-label">我的足迹</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <!-- 增值业务 -->
    <view class="increment-business-box">
      <view class="business-title">增值业务</view>
      <view class="increment-business-item">
        <up-row customStyle="margin-bottom: 40rpx">
          <up-col span="3" v-if="showStore === true">
            <view
              class="business-module-content"
              @tap="goNavigatePage('/pages/member/store/index', false)"
            >
              <view class="business-module-icon">
                <up-icon name="home" color="#666666" size="38rpx"></up-icon>
              </view>
              <view class="business-module-label">商家门店</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="business-module-content"
              @tap="goNavigatePage('/pages/promotion/seckill/index', false)"
            >
              <view class="business-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-miaosha"
                />
              </view>
              <view class="business-module-label">秒杀</view>
            </view>
          </up-col>
          <up-col span="3">
            <view
              class="business-module-content"
              @tap="goNavigatePage('/pages/promotion/coupon/index', false)"
            >
              <view class="business-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-youhuijuan"
                />
              </view>
              <view class="business-module-label">优惠卷</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <!-- 其他 -->
    <view class="user-other-box">
      <view class="other-title">其他</view>
      <view class="user-other-item">
        <up-row customStyle="margin-bottom: 20rpx">
          <!--
          <up-col span="3">
            <view class="other-module-content">
              <view class="other-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="36rpx"
                  icon="custom-icon-shangpinjiansuo"
                />
              </view>
              <view class="other-module-label">商品检索</view>
            </view>
          </up-col>
          <up-col span="3">
            <view class="other-module-content">
              <view class="other-module-icon">
                <rexshop-al-icon
                  color="#666666"
                  size="38rpx"
                  icon="custom-icon-bangzhuzhongxin"
                />
              </view>
              <view class="other-module-label">帮助中心</view>
            </view>
          </up-col>
          -->
          <up-col span="3">
            <view
              class="other-module-content"
              @tap="goNavigatePage('/pages/member/settings/index', true)"
            >
              <view class="other-module-icon">
                <rexshop-al-icon color="#666666" size="38rpx" icon="custom-icon-shezhi" />
              </view>
              <view class="other-module-label">系统设置</view>
            </view>
          </up-col>
          <up-col span="3">
            <view class="other-module-content">
              <view class="other-module-icon">
                <up-icon name="more-dot-fill" color="#666666" size="38rpx"></up-icon>
              </view>
              <!-- <view class="other-module-label">开发中</view> -->
            </view>
          </up-col>
        </up-row>
      </view>
    </view>

    <u-toast ref="uToastRef" />
    <!-- #ifdef MP-WEIXIN -->
    <rexshop-login-mp v-if="useUserLoginStore().showMpLoginTip" />
    <!-- #endif -->
	<rexshop-simulated-login ref="rexshopSimulatedLoginRef" />
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onShow } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { getApplicationConfigurationAsync } from "@/utils/other";

// 定义变量
const uToastRef = ref();
const showStore = ref(false);
const rexshopSimulatedLoginRef = ref();
const userNoticeNumber = ref<UserNoticeNumberType>({
  totalGoodBrowsing: 0,
  totalCoupon: 0,
  totalGoodCollection: 0,
  totalBillAftersale: 0,
  totalPendingPayment: 0,
  totalPendingShipment: 0,
  totalPendingDelivery: 0,
  totalPendingEvaluate: 0,
});

// 页面跳转
const goNavigatePage = (url: string, isLogin: boolean) => {
  if (isLogin && !useUserLoginStore().hasLogin) {
    goLogin();
    return;
  }
  uni.navigateTo({
    url,
  });
};

// 我的订单
const goMyOrder = (orderTypeName: string) => {
  if (!useUserLoginStore().hasLogin) {
    goLogin();
    return;
  }
  uni.navigateTo({
    url: "/pages/member/order/index?orderTypeName=" + orderTypeName,
  });
};

// 打开登录框
const goLogin = () => {
  useUserLoginStore().showAuthLogin();
};

// 打开模拟登录框
const goSimulatedLogin = () => {
	rexshopSimulatedLoginRef.value.openAuth();
};

// 刷新用户信息
const onRefreshUserInfo = (isShowLoading: boolean = true) => {
  if (isShowLoading) {
    uni.showLoading({
      title: "刷新中~",
    });
  }
  useUserLoginStore().refreshCurrentSysUser(50);
  getUserNoticeNumber();
};

// 查询用户通知数量
const getUserNoticeNumber = () => {
  http<UserNoticeNumberType>({
    method: "GET",
    url: "/api/front/aggregation/common/userNoticeNumbers",
  })
    .then((result) => {
      if (!result) return;
      userNoticeNumber.value = result;
    })
    .catch((err: any) => {
      console.error("查询用户通知数量出错：", err);
    });
};

// 加载服务配置信息
const loadBaseServiceConfig = () => {
  getApplicationConfigurationAsync("setting").then((appConfig) => {
    if (!appConfig) return;
    let settingValues = appConfig.values;
    showStore.value =
      settingValues["BaseService.SpecialSwitch.ShowStore"] &&
      settingValues["BaseService.SpecialSwitch.ShowStore"].toLocaleLowerCase() == "true";
  });
};

onShow(() => {
  if (useUserLoginStore().hasLogin) {
    onRefreshUserInfo(false);
  }
  loadBaseServiceConfig();
});
</script>

<style lang="scss" scoped>
.member-body {
  padding-bottom: 5rpx;
}

.head-box {
  /* #ifdef MP-WEIXIN */
  margin-top: 30rpx;
  /* #endif */
  padding-bottom: 72rpx;
  background-image: url("/static/images/common/bg.png");
  background-size: cover;
  background-position: center;
  background-color: #e54d42;
  height: 300rpx;

  .user-info-box {
    .login-user-view {
      height: 215rpx;
      .login-avatar-item {
        text-align: center;
        padding: 40rpx 0rpx 10rpx 0rpx;
        .user-avatar {
          display: inline-block;
          margin: 0rpx auto;
        }
      }
      .user-login-item {
        text-align: center;
        .login-btn {
          display: inline-block;
          margin: 0rpx auto;
        }
      }
    }
    .user-info-view {
      height: 150rpx;
      text-align: center;
      padding: 50rpx 30rpx 0rpx 30rpx;
      .user-avatar-item {
        text-align: center;
        display: inline-block;
        margin: 0rpx auto;
        .user-grade {
          margin-top: 5rpx;
          font-size: 22rpx;
          color: white;
        }
      }
      .user-detail-item {
        .user-name {
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
          font-size: 30rpx;
          color: white;
        }
        .user-money {
          margin-top: 20rpx;
          font-size: 22rpx;
          color: white;
        }
      }
      .user-link-item {
        padding-left: 20rpx;
        margin-top: 50rpx;
        .user-icon-reload {
          float: left;
        }
        .user-icon-arrow-right {
          float: right;
        }
      }
    }
  }

  .user-info-number-box {
    .uinb-item {
      width: 100%;
      display: block;
      text-align: center;
      color: white;
      .unib-value {
        font-size: 36rpx;
      }
      .unib-name {
        font-size: 22rpx;
      }
    }
  }
}

.user-trade-box {
  margin-top: -60rpx !important;
  background-color: white;
  padding: 20rpx;
  border-radius: 20rpx;
  width: 88%;
  margin: 0rpx auto;
  .trade-title {
    font-size: 26rpx;
    font-weight: bold;
  }
  .user-trade-item {
    margin-top: 38rpx;
    .trade-order-content {
      text-align: center;
      .trade-order-icon {
        display: inline-block;
        text-align: center;
        margin-bottom: 10rpx;
        position: relative;
        .order-num {
          position: absolute;
          left: 40rpx;
          bottom: 20rpx;
        }
      }
      .trade-order-label {
        font-size: 24rpx;
        color: #666666;
      }
    }
  }
}

.user-service-box {
  margin-top: 30rpx !important;
  background-color: white;
  padding: 20rpx;
  border-radius: 20rpx;
  width: 88%;
  margin: 0rpx auto;
  .service-title {
    font-size: 26rpx;
    font-weight: bold;
  }
  .user-service-item {
    margin-top: 38rpx;
    .service-module-content {
      text-align: center;
      .service-module-icon {
        display: inline-block;
        text-align: center;
        margin-bottom: 10rpx;
      }
      .service-module-label {
        font-size: 24rpx;
        color: #666666;
      }
    }
  }
}

.increment-business-box {
  margin-top: 30rpx !important;
  background-color: white;
  padding: 20rpx;
  border-radius: 20rpx;
  width: 88%;
  margin: 0rpx auto;
  .business-title {
    font-size: 26rpx;
    font-weight: bold;
  }
  .increment-business-item {
    margin-top: 38rpx;
    .business-module-content {
      text-align: center;
      .business-module-icon {
        display: inline-block;
        text-align: center;
        margin-bottom: 10rpx;
      }
      .business-module-label {
        font-size: 24rpx;
        color: #666666;
      }
    }
  }
}

.user-other-box {
  background-color: white;
  padding: 20rpx;
  border-radius: 20rpx;
  width: 88%;
  margin: 30rpx auto;
  .other-title {
    font-size: 26rpx;
    font-weight: bold;
  }
  .user-other-item {
    margin-top: 38rpx;
    .other-module-content {
      text-align: center;
      .other-module-icon {
        display: inline-block;
        text-align: center;
        margin-bottom: 10rpx;
      }
      .other-module-label {
        font-size: 24rpx;
        color: #666666;
      }
    }
  }
}
</style>
