<template>
  <view class="receiving-body">
    <view class="receiving-form-box">
      <u-form ref="userShipFormRef" labelPosition="left" :model="userShip">
        <u-form-item label="收 货 人" labelWidth="140rpx" prop="name" borderBottom>
          <u-input
            clearable
            maxlength="20"
            border="none"
            placeholder="请输入收货人"
            v-model="userShip.name"
          ></u-input>
        </u-form-item>
        <u-form-item label="手 机 号" labelWidth="140rpx" prop="mobile" borderBottom>
          <u-input
            clearable
            maxlength="20"
            border="none"
            placeholder="请输入手机号"
            v-model="userShip.mobile"
          ></u-input>
        </u-form-item>

        <u-form-item
          label="省 市 区"
          labelWidth="140rpx"
          prop="displayArea"
          borderBottom
          @click="showCity = true"
        >
          <u-input
            disabled
            disabledColor="#ffffff"
            border="none"
            placeholder="请选择省市区"
            v-model="userShip.displayArea"
            @click="showCity = true"
          ></u-input>
          <template #right>
            <u-icon name="arrow-right"></u-icon>
          </template>
        </u-form-item>

        <u-form-item label="详细地址" labelWidth="140rpx" prop="address" borderBottom>
          <u-textarea
            maxlength="200"
            height="50"
            v-model="userShip.address"
            placeholder="请输入详细地址"
          ></u-textarea>
        </u-form-item>

        <u-form-item label="设为默认" labelWidth="140rpx" prop="isDefault" borderBottom>
          <u-switch v-model="userShip.isDefault" />
        </u-form-item>
      </u-form>
    </view>

    <view class="receiving-footer">
      <view
        class="address-delete"
        v-if="userShip.id && userShip.id != guidEmpty"
        @tap="deleteUserShip(userShip.id)"
        >删除</view
      >
      <view
        class="address-save"
        :style="!userShip.id || userShip.id == guidEmpty ? 'width: 100%' : ''"
        @tap="saveValidate"
        >保存</view
      >
    </view>

    <u-toast ref="uToastRef" />
    <rexshop-city-select v-model="showCity" @city-change="cityChange" />
  </view>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";
import { onLoad, onReady } from "@dcloudio/uni-app";
import { useUserLoginStore } from "@/stores/userLogin/index";
import { getGuidEmpty, isMobile } from "@/utils/other";
import { http } from "@/utils/http";

// 定义变量
const userShipFormRef = ref();
const uToastRef = ref();
const showCity = ref(false);
const guidEmpty = getGuidEmpty();
const userShip = reactive<UserShipType>({
  id: guidEmpty,
  tenantId: null,
  userId: useUserLoginStore().currentSysUser?.id,
  areaId: 0,
  displayArea: "",
  address: "",
  name: "",
  mobile: "",
  isDefault: false,
  concurrencyStamp: "",
});
const userShipRules = reactive({
  name: [
    {
      required: true,
      message: "请输入收货人",
      trigger: ["blur", "change"],
    },
  ],
  mobile: [
    {
      required: true,
      message: "请输入手机号",
      trigger: ["blur", "change"],
    },
    {
      validator: (rule: any, value: any, callback: Function) => {
        let verifyResult = isMobile(value);
        if (!verifyResult) {
          callback(new Error("请输入正确的手机号码。"));
          return;
        }
        callback();
      },
      trigger: ["blur", "change"],
    },
  ],
  displayArea: [
    {
      required: true,
      message: "请选择省市区",
      trigger: ["blur", "change"],
    },
  ],
  address: [
    {
      required: true,
      message: "请输入详细地址",
      trigger: ["blur", "change"],
    },
  ],
});

// 区域选择
const cityChange = (item: any) => {
  userShip.displayArea = `${item.province.name} - ${item.city.name} - ${item.area.name}`;
  userShip.areaId = item.area.id;
};

// 保存验证
const saveValidate = () => {
  userShipFormRef.value
    .validate()
    .then((valid: any) => {
      if (valid) {
        if (!userShip.id || userShip.id == guidEmpty) addUserShip();
        else updateUserShip(userShip.id);
        return;
      }
      console.warn("校验失败！");
    })
    .catch((err: any) => {
      console.error("校验失败", err);
    });
};

// 添加收货地址
const addUserShip = () => {
  http<boolean>({
    method: "POST",
    url: "/api/base/user-ship",
    data: userShip,
  })
    .then((result) => {
      uni.$emit("refreshUserShip", result);
      if (!result) return;
      uToastRef.value.success("添加成功！");
      setTimeout(() => {
        uni.navigateBack();
      }, 2 * 1000);
    })
    .catch((err: any) => {
      console.error("添加收货地址出错：", err);
    });
};

// 修改收货地址
const updateUserShip = (id: string) => {
  http<boolean>({
    method: "PUT",
    url: "/api/base/user-ship/" + id,
    data: userShip,
  })
    .then((result) => {
      uni.$emit("refreshUserShip", result);
      if (!result) return;
      uToastRef.value.success("修改成功！");
      setTimeout(() => {
        uni.navigateBack();
      }, 2 * 1000);
    })
    .catch((err: any) => {
      console.error("修改收货地址出错：", err);
    });
};

// 删除收货地址
const deleteUserShip = (id: string) => {
  uni.showModal({
    title: "提示",
    content: "您确定要删除该收货地址吗？",
    success: function (res) {
      if (res.confirm) {
        http<boolean>({
          method: "DELETE",
          url: "/api/base/user-ship/" + id,
        })
          .then((result) => {
            uni.$emit("refreshUserShip", result);
            if (!result) return;
            uToastRef.value.error("删除成功！");
            setTimeout(() => {
              uni.navigateBack();
            }, 2 * 1000);
          })
          .catch((err: any) => {
            console.error("删除收货地址出错：", err);
          });
      }
    },
  });
};

// 获取收货信息
const loadUserShip = (id: string) => {
  http<UserShipType>({
    method: "GET",
    url: "/api/base/user-ship/" + id,
    data: userShip,
  })
    .then((result) => {
      if (!result) return;
      Object.assign(userShip, result);
    })
    .catch((err: any) => {
      console.error("获取收货信息出错：", err);
    });
};

onLoad((evt: any) => {
  if (evt.id) {
    loadUserShip(evt.id);
  }
});

onReady(() => {
  userShipFormRef.value.setRules(userShipRules);
});
</script>

<style lang="scss" scoped>
.receiving-body {
  display: block;
  .receiving-form-box {
    background-color: white;
    padding: 0rpx 30rpx;
  }

  .receiving-footer {
    position: fixed;
    bottom: 0;
    width: 100%;
    .address-delete {
      width: 50%;
      display: inline-block;
      text-align: center;
      background-color: #e45656;
      color: white;
      height: 88rpx;
      line-height: 88rpx;
    }
    .address-save {
      width: 50%;
      display: inline-block;
      text-align: center;
      background-color: #19be6b;
      color: white;
      height: 88rpx;
      line-height: 88rpx;
    }
  }
}
</style>
