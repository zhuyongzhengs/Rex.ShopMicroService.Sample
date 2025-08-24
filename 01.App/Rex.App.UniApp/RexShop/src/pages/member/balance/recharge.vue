<template>
  <view class="recharge-body" v-if="currentUser">
    <view class="recharge-form-box">
      <up-form
        labelPosition="left"
        :model="rechargeForm"
        :rules="rechargeRules"
        ref="rechargeFormRef"
        labelWidth="150rpx"
      >
        <up-form-item label="当前金额" prop="money">
          <up-text type="error" mode="price" :text="rechargeForm.money"></up-text>
        </up-form-item>
        <up-form-item label="充值金额" prop="recharge" @click="openKeyboard">
          <up-input
            type="text"
            prefixIcon="￥"
            placeholder="请输入充值金额"
            :readonly="true"
            v-model="rechargeForm.recharge"
          />
        </up-form-item>
      </up-form>
    </view>
    <view class="recharge-footer">
      <up-button type="error" @click="onValidateForm">去支付</up-button>
    </view>
    <up-keyboard
      ref="uKeyboard"
      mode="number"
      @change="valChange"
      @backspace="backspace"
      @confirm="closeKeyboard"
      @cancel="closeKeyboard"
      :show="showKeyboard"
      :overlay="false"
    ></up-keyboard>
    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { verifyNumberIntegerAndFloat } from "@/utils/toolsValidate";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 定义变量
const uToastRef = ref();
const rechargeFormRef = ref();
const showKeyboard = ref(false);
const currentUser = ref<CurrentSysUserType>();
const rechargeForm = ref({
  money: 0,
  recharge: "",
});

const openKeyboard = () => {
  showKeyboard.value = true;
};

const closeKeyboard = () => {
  showKeyboard.value = false;
};

const valChange = (val: number) => {
  let inputVal = rechargeForm.value.recharge + val;
  rechargeForm.value.recharge = verifyNumberIntegerAndFloat(inputVal);
};

const backspace = () => {
  let inputVal = rechargeForm.value.recharge;
  if (!inputVal) return;
  rechargeForm.value.recharge = inputVal.slice(0, inputVal.length - 1);
};

// 校验规则
const rechargeRules = {
  recharge: [
    {
      required: true,
      message: "请输入充值金额",
      trigger: "change",
    },
    {
      validator: (rule: any, value: string, callback: Function) => {
        let rechargeVal = Number(value);
        if (rechargeVal <= 0) {
          callback(new Error("充值金额不能小于或等于0。"));
          return;
        }
        callback();
      },
      trigger: "change",
    },
  ],
};

// 表单验证
const onValidateForm = () => {
  rechargeFormRef.value
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
  uni.navigateTo({
    url: "/pages/payment/index?playType=2&recharge=" + rechargeForm.value.recharge,
  });
};

onLoad(() => {
  if (!useUserLoginStore().hasLogin) {
    uni.navigateBack();
    return;
  }

  currentUser.value = useUserLoginStore().currentSysUser;
  if (currentUser.value) {
    rechargeForm.value.money = currentUser.value.balance;
  }
});
</script>

<style lang="scss" scoped>
.recharge-body {
  .recharge-form-box {
    margin: 30rpx;
    padding: 10rpx 30rpx;
    background-color: white;
    border-radius: 20rpx;
  }

  .recharge-footer {
    width: 92%;
    position: fixed;
    bottom: 30rpx;
    padding: 0rpx 30rpx;
  }
}
</style>
