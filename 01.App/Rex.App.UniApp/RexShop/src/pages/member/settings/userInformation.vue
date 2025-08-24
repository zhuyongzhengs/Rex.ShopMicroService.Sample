<template>
  <view class="information-body">
    <view class="up-padding-30">
      <view class="user-avatar-box">
        <up-avatar
          size="80"
          shape="circle"
          :src="userInformationModel.avatar"
        ></up-avatar>
      </view>
      <up-form
        ref="userInformationFormRef"
        labelPosition="left"
        labelAlign="center"
        labelWidth="100rpx"
        :model="userInformationModel"
        :rules="userInformationRules"
      >
        <up-form-item label="昵称" prop="name">
          <up-input
            type="text"
            v-model="userInformationModel.name"
            :maxlength="100"
            placeholder="请输入用户昵称"
            clearable
          ></up-input>
        </up-form-item>
        <up-form-item label="性别" prop="gender">
          <up-radio-group v-model="selGender" placement="row">
            <up-radio name="male" label="男" />&nbsp;&nbsp;
            <up-radio name="female" label="女" />
          </up-radio-group>
        </up-form-item>
        <up-form-item label="生日" prop="birthStamp">
          <up-datetime-picker
            hasInput
            :show="showdate"
            :minDate="0"
            :maxDate="Date.now()"
            v-model="userInformationModel.birthStamp"
            mode="date"
            placeholder="请选择生日"
            clearable
          ></up-datetime-picker>
        </up-form-item>
      </up-form>
    </view>
    <view class="information-btn-box">
      <up-button type="error" size="default" @click="onValidateForm">保存</up-button>
    </view>

    <u-toast ref="uToastRef" />
  </view>
</template>
<script setup lang="ts">
import { ref, watch } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 定义变量
const userInformationFormRef = ref();
const uToastRef = ref();
const selGender = ref("");
const showdate = ref(false);
const userInformationModel = ref({
  avatar: "",
  name: "",
  gender: 0,
  birthDate: "",
  birthStamp: 0,
});

watch(selGender, (newVal, oldVal) => {
  switch (newVal) {
    case "male":
      userInformationModel.value.gender = 1;
      break;
    case "female":
      userInformationModel.value.gender = 2;
      break;
    default:
      userInformationModel.value.gender = 0;
      break;
  }
});

// 校验规则
const userInformationRules = {
  name: [
    {
      required: true,
      message: "请输入用户昵称。",
      trigger: ["change", "blur"],
    },
  ],
};

// 表单验证
const onValidateForm = () => {
  userInformationFormRef.value
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
  userInformationModel.value.birthDate = formatTimestampToDate(
    userInformationModel.value.birthStamp
  );
  http<boolean>({
    method: "PUT",
    url: "/api/base/common/sys-user-information",
    data: userInformationModel.value,
  })
    .then((result) => {
      if (!result) return;
      useUserLoginStore().refreshCurrentSysUser();
      uToastRef.value.show({
        type: "success",
        duration: 2000,
        position: "top",
        message: "个人信息修改成功。",
        complete() {
          uni.navigateBack();
        },
      });
    })
    .catch((err: any) => {
      console.error("用户资料修改出错：", err);
    });
};

// 获取用户资料信息
const getUserInformation = () => {
  let currUser = useUserLoginStore().currentSysUser;
  if (!currUser) return;
  userInformationModel.value.avatar = currUser.avatar;
  userInformationModel.value.name = currUser.name;
  switch (currUser.gender) {
    case 1:
      selGender.value = "male";
      break;
    case 2:
      selGender.value = "female";
      break;
    default:
      break;
  }
  if (currUser.birthDate)
    userInformationModel.value.birthStamp = Object(new Date(currUser.birthDate));
};

// 将时间戳转为为日期
const formatTimestampToDate = (timestamp: number) => {
  const date = new Date(timestamp);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};

onLoad(() => {
  getUserInformation();
});
</script>
<style lang="scss" scoped>
.information-body {
  background-color: white;
  .user-avatar-box {
    text-align: center;
    display: flex;
    justify-content: center;
  }
  .information-btn-box {
    position: fixed;
    bottom: 30rpx;
    width: 92%;
    margin: 0px 30rpx;
  }
}
</style>
