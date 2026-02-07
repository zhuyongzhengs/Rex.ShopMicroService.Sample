<template>
  <view class="login-mp-body">
    <u-popup
      customStyle="width: 600rpx; border-radius: 8rpx;"
      :show="showLogin"
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
                  />
                </view>
              </up-col>
              <up-col span="9.5">
                <view class="shop-name">
                  <text>模拟授权</text>
                  <text class="u-margin-left-10">申请</text>
                </view>
              </up-col>
            </up-row>
            <view class="modal-desc" style="margin: 20rpx auto;">
				注：仅[调试模式]下有效。
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
				  @click="onLogin"
                >
                  确定授权
                </button>
              </up-col>
            </up-row>
          </view>
        </view>
      </view>
    </u-popup>
  </view>
</template>

<script setup lang="ts" name="rexshopSimulatedLogin">
import { ref, reactive } from "vue";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { http } from "@/utils/http";

// 定义变量
const showLogin = ref(false);

// 登录表单
const wechatLoginForm = reactive<WechatLoginFormType>({
  client_id: import.meta.env.VITE_AUTH_WECHAT_CODE_CLIENT_ID,
  grant_type: "wechat_code",
  scope: import.meta.env.VITE_AUTH_SCOPE,
  openid: "",
  session_Key: "",
  unionid: "",
  invitecode: "",
  code: "SIMULATED_LOGIN"
});

// 显示[小程序]登录框
const openAuth = () => {
	showLogin.value = true;
}

// 关闭[小程序]登录框
const closeAuth = () => {
  showLogin.value = false;
};

// 登录
const onLogin = () => {
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
      closeAuth();
    })
    .catch((err: any) => {
      console.error("登录失败：", err);
    });
};

// 暴露变量
defineExpose({
  openAuth,
  closeAuth
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
