<template>
  <view class="pwd-body">
    <view class="up-padding-30">
      <up-form
        ref="userPwdFormRef"
        labelPosition="left"
        labelAlign="center"
        labelWidth="150rpx"
        :model="userPwdModel"
        :rules="userPwdRules"
      >
        <up-form-item label="　新密码" prop="newPwd">
          <up-input
            type="password"
            v-model="userPwdModel.newPwd"
            :password="true"
            :maxlength="50"
            placeholder="请输入新密码"
            clearable
          ></up-input>
        </up-form-item>
        <up-form-item label="确认密码" prop="rePwd">
          <up-input
            type="password"
            v-model="userPwdModel.rePwd"
            :password="true"
            :maxlength="50"
            placeholder="请输入确认密码"
            clearable
          ></up-input>
        </up-form-item>
      </up-form>
    </view>
    <view class="pwd-btn-box">
      <up-button type="error" size="default" @click="onValidateForm">保存</up-button>
    </view>

    <u-toast ref="uToastRef" />
  </view>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { http } from "@/utils/http";
import { validatePassword } from "@/utils/toolsValidate";

// 定义变量
const userPwdFormRef = ref();
const uToastRef = ref();
const userPwdModel = ref({
  newPwd: "",
  rePwd: "",
});

// 校验规则
const userPwdRules = {
  newPwd: [
    {
      required: true,
      message: "请输入新密码。",
      trigger: ["change", "blur"],
    },
    {
      validator: (rule: any, value: string, callback: Function) => {
        let verifyResult = validatePassword(value);
        if (!verifyResult) {
          callback(
            new Error(
              "密码必须包含：字母(大小写)、数字[0~9]和特殊符号[!@#$%^&*]，长度在6~16之间。"
            )
          );
          return;
        }
        callback();
      },
      trigger: ["change", "blur"],
    },
  ],
  rePwd: [
    {
      required: true,
      message: "请输入确认密码。",
      trigger: ["change", "blur"],
    },
    {
      validator: (rule: any, value: string, callback: Function) => {
        if (value !== userPwdModel.value.newPwd) {
          callback(new Error("新密码与确认密码不一致。"));
          return;
        }
        callback();
      },
      trigger: ["change", "blur"],
    },
  ],
};

// 表单验证
const onValidateForm = () => {
  userPwdFormRef.value
    .validate()
    .then((valid: any) => {
      if (valid) {
        onSubmit();
        return;
      }
      console.warn("检验失败.");
    })
    .catch((err: any) => {
      console.error("校验出错", err);
    });
};

// 提交
const onSubmit = () => {
  http<boolean>({
    method: "PUT",
    url: "/api/base/common/sys-user-pwd?password=" + userPwdModel.value.rePwd,
  })
    .then((result) => {
      if (!result) return;
      uToastRef.value.show({
        type: "success",
        duration: 2000,
        position: "top",
        message: "密码修改成功。",
        complete() {
          uni.navigateBack();
        },
      });
    })
    .catch((err: any) => {
      console.error("密码修改出错：", err);
    });
};
</script>
<style lang="scss" scoped>
.pwd-body {
  background-color: white;
  .pwd-btn-box {
    position: fixed;
    bottom: 30rpx;
    width: 92%;
    margin: 0px 30rpx;
  }
}
</style>
