<template>
  <view class="settings-body">
    <!--无网络组件-->
    <up-no-network></up-no-network>

    <view class="setting-cell-box">
      <up-cell-group>
        <up-cell
          icon="setting"
          title="个人资料"
          isLink
          url="/pages/member/settings/userInformation"
        ></up-cell>
        <!-- #ifdef MP-WEIXIN  -->
        <up-cell
          @click="syncWeChatInfo"
          icon="reload"
          title="同步微信昵称头像"
          isLink
        ></up-cell>
        <!-- #endif  -->
        <up-cell
          icon="lock"
          title="修改密码"
          isLink
          url="/pages/member/settings/password"
        ></up-cell>
        <up-cell icon="tags" title="关于我们" isLink :url="aboutUrl"></up-cell>
        <up-cell icon="order" title="用户协议" isLink :url="userAgreementUrl"></up-cell>
        <up-cell icon="eye" title="隐私政策" isLink :url="privacyPolicyUrl"></up-cell>
      </up-cell-group>
    </view>

    <view class="setting-btn-box">
      <up-button type="error" size="default" @click="logOut">退出登录</up-button>
    </view>
    <u-toast ref="uToastRef" />
  </view>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { onShow } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { syncWeChatInfo, getApplicationConfigurationAsync } from "@/utils/other";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 定义变量
const uToastRef = ref();
const aboutUrl = ref("");
const userAgreementUrl = ref("");
const privacyPolicyUrl = ref("");

// 加载服务配置信息
const loadBaseServiceConfig = () => {
  getApplicationConfigurationAsync("setting").then((appConfig) => {
    if (!appConfig) return;
    let settingValues = appConfig.values;
    // 关于我们
    let aboutArticleItem = settingValues["BaseService.PlatformSetting.AboutArticle"];
    if (aboutArticleItem) {
      let aboutItem = JSON.parse(aboutArticleItem) as SelectArticleType;
      aboutUrl.value = `/pages/member/settings/agreement?id=${aboutItem.id}&title=${aboutItem.title}`;
    }

    // 用户协议
    let userAgreementItem = settingValues["BaseService.PlatformSetting.UserAgreement"];
    if (userAgreementItem) {
      let uAgreementItem = JSON.parse(userAgreementItem) as SelectArticleType;
      userAgreementUrl.value = `/pages/member/settings/agreement?id=${uAgreementItem.id}&title=${uAgreementItem.title}`;
    }

    // 隐私政策
    let privacyPolicyItem = settingValues["BaseService.PlatformSetting.PrivacyPolicy"];
    if (privacyPolicyItem) {
      let pPolicyItem = JSON.parse(privacyPolicyItem) as SelectArticleType;
      privacyPolicyUrl.value = `/pages/member/settings/agreement?id=${pPolicyItem.id}&title=${pPolicyItem.title}`;
    }
  });
};

// 退出登录
const logOut = () => {
  uni.showModal({
    title: "提示",
    content: "您确定要退出吗？",
    success: function (res) {
      if (res.confirm) {
        useUserLoginStore().logout();
        uToastRef.value.show({
          type: "success",
          duration: 2500,
          position: "top",
          message: "已退出！",
          complete() {
            uni.reLaunch({
              url: "/pages/member/index",
            });
          },
        });
      }
    },
  });
};

onShow(() => {
  loadBaseServiceConfig();
});
</script>
<style lang="scss" scoped>
.settings-body {
  .setting-cell-box {
    background-color: white;
  }
  .setting-btn-box {
    position: fixed;
    bottom: 30rpx;
    width: 92%;
    margin: 0px 30rpx;
  }
}
</style>
