<template>
  <view class="balance-body">
    <view class="balance-money-box">
      <view class="balance-title">
        <up-text
          color="white"
          mode="text"
          align="left"
          size="24rpx"
          text="账号余额（元）"
        ></up-text>
      </view>
      <template v-if="currentUser">
        <up-text
          color="white"
          mode="price"
          align="center"
          size="50rpx"
          :text="currentUser.balance"
        ></up-text>
      </template>
    </view>
    <view class="balance-item-box">
      <view class="balance-item-list">
        <template v-if="showCharge">
          <up-row
            customStyle="margin: 25rpx 0rpx"
            @click="goNavigateTo('/pages/member/balance/recharge')"
          >
            <up-col span="1">
              <view class="item-icon">
                <up-icon name="gift" size="30rpx"></up-icon>
              </view>
            </up-col>
            <up-col span="10">
              <up-text type="info" text="账号充值" size="28rpx"></up-text>
            </up-col>
            <up-col span="1">
              <view class="item-icon">
                <up-icon name="arrow-right" size="30rpx"></up-icon>
              </view>
            </up-col>
          </up-row>
          <up-line></up-line>
        </template>
        <up-row customStyle="margin: 25rpx 0rpx" @click="onWithdraw">
          <up-col span="1">
            <view class="item-icon">
              <up-icon name="rmb" color="#F1C247" size="30rpx"></up-icon>
            </view>
          </up-col>
          <up-col span="10">
            <up-text type="info" text="余额提现" size="28rpx"></up-text>
          </up-col>
          <up-col span="1">
            <view class="item-icon">
              <up-icon name="arrow-right" size="30rpx"></up-icon>
            </view>
          </up-col>
        </up-row>
        <up-line></up-line>
        <up-row customStyle="margin: 25rpx 0rpx" @click="onUserBalance">
          <up-col span="1">
            <view class="item-icon">
              <up-icon name="rmb-circle" color="#99C55C" size="30rpx"></up-icon>
            </view>
          </up-col>
          <up-col span="10">
            <up-text type="info" text="余额明细" size="28rpx"></up-text>
          </up-col>
          <up-col span="1">
            <view class="item-icon">
              <up-icon name="arrow-right" size="30rpx"></up-icon>
            </view>
          </up-col>
        </up-row>
        <up-line></up-line>
        <up-row customStyle="margin: 25rpx 0rpx" @click="onWithdraw">
          <up-col span="1">
            <view class="item-icon">
              <up-icon name="list-dot" color="#E5A06E" size="30rpx"></up-icon>
            </view>
          </up-col>
          <up-col span="10">
            <up-text type="info" text="提现记录" size="28rpx"></up-text>
          </up-col>
          <up-col span="1">
            <view class="item-icon">
              <up-icon name="arrow-right" size="30rpx"></up-icon>
            </view>
          </up-col>
        </up-row>
      </view>
    </view>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onLoad, onShow } from "@dcloudio/uni-app";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { getApplicationConfigurationAsync } from "@/utils/other";

// 定义变量
const currentUser = ref<CurrentSysUserType>();
const showCharge = ref(false);
const uToastRef = ref();

// 跳转页面
const goNavigateTo = (url: string) => {
  uni.navigateTo({
    url,
  });
};

// 余额提现
const onWithdraw = () => {
  uToastRef.value.default("开发中！");
};

// 余额明细
const onUserBalance = () => {
  uni.navigateTo({
    url: "/pages/member/balance/list?type=全部",
  });
};

onLoad(() => {
  if (!useUserLoginStore().hasLogin) {
    uni.navigateBack();
    return;
  }
  useUserLoginStore().refreshCurrentSysUser();
  currentUser.value = useUserLoginStore().currentSysUser;
});

onShow(() => {
  loadBaseServiceConfig();
});

// 加载服务配置信息
const loadBaseServiceConfig = () => {
  getApplicationConfigurationAsync("setting").then((appConfig) => {
    if (!appConfig) return;
    let settingValues = appConfig.values;
    showCharge.value =
      settingValues["BaseService.SpecialSwitch.ShowCharge"] &&
      settingValues["BaseService.SpecialSwitch.ShowCharge"].toLocaleLowerCase() == "true";
  });
};
</script>

<style lang="scss" scoped>
.balance-body {
  .balance-money-box {
    background-color: #d25a49;
    padding: 50rpx 20rpx;
    .balance-title {
      position: relative;
      top: -30rpx;
    }
  }
  .balance-item-box {
    margin: 30rpx;
    background-color: white;
    border-radius: 20rpx;
    .balance-item-list {
      padding: 5rpx 0rpx;
      .item-icon {
        display: flex;
        justify-content: center;
      }
    }
  }
}
</style>
