<template>
  <view class="login-mp-body">
    <u-popup
      customStyle="width: 600rpx; border-radius: 8rpx;"
      :show="showMpLogin"
      mode="center"
    >
      <view class="login-mp-modal" @tap.stop>
        <view class="modal-box">
          <view class="modal-content">
            <up-row>
              <up-col span="1.5">
                <view class="shop-logo">
                  <u-image
                    width="50rpx"
                    radius="2"
                    height="50rpx"
                    :src="shopStore.platformLogo"
                  />
                </view>
              </up-col>
              <up-col span="9.5">
                <view class="shop-name">
                  <text>{{ shopStore.shopName || "登录授权" }}</text>
                  <text class="u-margin-left-10">申请</text>
                </view>
              </up-col>
            </up-row>
            <view class="modal-title">获取以下权限为您提供服务</view>
            <view class="modal-desc">
              1、获取你的手机号提供更好的账户安全，物流，订单状态提醒等服务（在接下来微信授权手机号的弹窗中选择“允许”）<br />
              2、使用我们的相关服务，需要将您的手机号授权给我们。
            </view>
            <!--服务协议-->
            <view class="modal-service-agreement">
              <up-row>
                <up-col span="1">
                  <u-radio-group v-model="isAgreeAgreement" placement="column">
                    <u-radio shape="square" size="15" :name="true" />
                  </u-radio-group>
                </up-col>
                <up-col span="11">
                  <view class="agreement-desc">
                    <text class="ok-text">同意</text>
                    <text class="service-agreement">《服务协议》</text>
                    <text class="or-text">与</text>
                    <text class="privacy-agreement">《隐私协议》</text>
                  </view>
                </up-col>
              </up-row>
            </view>
          </view>
          <view class="modal-btn-item">
            <up-row>
              <up-col span="6">
                <button class="auth-no-btn" @click="closeAuth">暂不授权</button>
              </up-col>
              <up-col span="6">
                <button
                  class="auth-ok-btn"
                  open-type="getPhoneNumber"
                  :disabled="authOkDisabled"
                  :class="authOkDisabled ? 'auth-ok-btn-disabled' : ''"
                  @getphonenumber="getPhoneNumber"
                >
                  {{ authOkDisabled ? "授权中…" : "确定授权" }}
                </button>
              </up-col>
            </up-row>
          </view>
        </view>
      </view>
    </u-popup>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts" name="rexShopLoginMp">
import { ref, reactive, computed, onMounted } from "vue";
import _ from "lodash";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { http } from "@/utils/http";
import { getApplicationConfigurationAsync } from "@/utils/other";

// 定义变量
const uToastRef = ref();
const authOkDisabled = ref(false);
const isAgreeAgreement = ref(true);
// 商城店铺信息
const shopStore = reactive({
  platformLogo: null,
  shopName: null,
  shopMobile: null,
  shareTitle: null,
});

// 登录表单
const wechatLoginForm = reactive<WechatLoginFormType>({
  client_id: import.meta.env.VITE_AUTH_WECHAT_CODE_CLIENT_ID,
  grant_type: "wechat_code",
  scope: import.meta.env.VITE_AUTH_SCOPE,
  openid: "",
  session_Key: "",
  unionid: "",
  invitecode: "",
  code: "",
});

// 显示[小程序]登录框
const showMpLogin = computed({
  get() {
    return useUserLoginStore().showMpLoginTip;
  },
  set(value) {
    useUserLoginStore().showMpLoginTip = value;
  },
});

// 暂不授权
const closeAuth = () => {
  showMpLogin.value = false;
};

// 获取手机号码
const getPhoneNumber = (e: any) => {
  if (e.detail.errMsg == "getPhoneNumber:ok") {
    wechatLoginForm.code = e.detail.code;
    onLogin();
    return;
  }
  uni.showToast({
    icon: "none",
    title: "授权已取消！",
  });
  closeAuth();
};

// 登录
const onLogin = () => {
  authOkDisabled.value = true;
  http<LoginResultType>({
    method: "POST",
    url: "/connect/token",
    header: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    data: wechatLoginForm,
  })
    .then((result) => {
      if (!result) return;
      useUserLoginStore().userAuthToken = result;
      useUserLoginStore().hasLogin = true;
      // 获取用户信息
      useUserLoginStore().getCurrentSysUser();
      authOkDisabled.value = false;
      closeAuth();
    })
    .catch((err: any) => {
      authOkDisabled.value = false;
      console.error("登录失败：", err);
    });
};

// 加载服务配置信息
const loadBaseServiceConfigAsync = async () => {
  getApplicationConfigurationAsync("setting").then((appConfig) => {
    if (!appConfig) return;
    let settingValues = appConfig.values;
    shopStore.platformLogo = settingValues["BaseService.PlatformSetting.PlatformLogo"];
    shopStore.shopName = settingValues["BaseService.PlatformSetting.ShopName"];
    shopStore.shareTitle = settingValues["BaseService.ShareSetting.ShareTitle"];
    shopStore.shopMobile = settingValues["BaseService.UsersSetting.ShopMobile"];
  });
};

// 获取Code
const getCode = (callBack: Function) => {
  uni.login({
    // #ifdef MP-ALIPAY
    scopes: "auth_user",
    // #endif
    success: function (res) {
      if (res.code) {
        return callBack(res.code);
      }
      uToastRef.value.warning("未取得code，请重试！");
    },
    fail: function (res) {
      console.error(res);
      uToastRef.value.error("用户授权失败wx.login，请重试！");
    },
  });
};

// 获取登录凭证
const getLoginCredentials = () => {
  authOkDisabled.value = true;
  getCode((jsCode: string) => {
    http<Code2SessionResultType>({
      method: "GET",
      url: "/api/authorization/wechat/jscode2session",
      data: {
        jsCode,
      },
    })
      .then((result) => {
        if (!result || result.errorCode != 0) {
          console.error("获取登录凭证失败！", result);
          return;
        }
        wechatLoginForm.openid = result.openid;
        wechatLoginForm.session_Key = result.session_key;
        if (result.unionid) wechatLoginForm.unionid = result.unionid;
        authOkDisabled.value = false;
      })
      .catch((err: any) => {
        console.error("登录失败：", err);
      });
  });
};

// 加载数据
onMounted(() => {
  // 获取服务配置信息
  loadBaseServiceConfigAsync();

  // 获取登录凭证信息
  getLoginCredentials();
});
</script>

<style lang="scss" scoped>
.login-mp-body {
  .login-mp-modal {
    background: none;
    overflow: visible;
    .modal-box {
      padding: 30rpx;
      .modal-content {
        text-align: center;
        font-size: 30rpx;
        .shop-logo {
          display: inline-block;
          text-align: center;
        }
        .shop-name {
          display: inline-block;
          color: #787878;
        }
      }
      .modal-title {
        color: #46351b;
        font-size: 30rpx;
        font-weight: bold;
        text-align: left;
        line-height: 35rpx;
        padding: 30rpx 0 30rpx 30rpx;
      }
      .modal-desc {
        font-size: 24rpx;
        line-height: 40rpx;
        color: #333;
        background: #f7f8fa;
        text-align: left;
        padding: 20rpx;
      }
      .modal-service-agreement {
        margin: 20rpx 15rpx;
        .agreement-desc {
          font-size: 24rpx;
          color: #333;
          .service-agreement,
          .privacy-agreement {
            color: #409eff;
          }
        }
      }
      .modal-btn-item {
        .auth-no-btn {
          width: 200rpx;
          font-size: 30rpx;
        }
        .auth-ok-btn {
          width: 200rpx;
          background-color: #19be6b;
          color: white;
          font-size: 30rpx;
        }
        .auth-ok-btn-disabled {
          background-color: #a9e08f !important;
        }
      }
    }
  }
}
</style>
