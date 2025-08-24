<template>
  <view class="address-body">
    <view class="address-list">
      <template v-for="userShip in userShipTypeState.tableData.data" :key="userShip.id">
        <up-row customStyle="background-color: white; padding: 22rpx;margin-bottom: 1px;">
          <up-col span="11" @click="selectUserShip(userShip)">
            <view class="address-item">
              <text class="user-name">{{ userShip.name }}</text>
              <text class="user-phone">{{ userShip.mobile }}</text>
              <text class="default-tag" v-if="userShip.isDefault">默认</text>
            </view>
            <view class="full-address">{{
              userShip.displayArea + " " + userShip.address
            }}</view>
          </up-col>
          <up-col span="1">
            <view class="address-edit-icon" @tap="goReceiving(userShip.id)">
              <u-icon name="edit-pen" size="50rpx"></u-icon>
            </view>
          </up-col>
        </up-row>
      </template>
      <!-- <u-loadmore :status="UserShipState.tableData.loading" /> -->
    </view>
    <view class="address-footer">
      <view class="add-address-btn" @tap="goReceiving('')">新增收货地址</view>
    </view>

    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";

// 定义变量
const isOption = ref(false);

// 收货地址查询
const userShipTypeState = reactive<UserShipTypeState>({
  tableData: {
    data: [],
    total: 0,
    loading: "loadmore",
    currentPage: 1,
    param: {
      sorting: "IsDefault DESC",
      skipCount: 0,
      maxResultCount: 20,
    },
  },
});

// 重置
const reset = () => {
  userShipTypeState.tableData.data = [];
  userShipTypeState.tableData.total = 0;
  userShipTypeState.tableData.currentPage = 1;
};

// 获取用户(收货)地址信息
const loadUserShipData = () => {
  userShipTypeState.tableData.loading = "loading";
  userShipTypeState.tableData.param.skipCount =
    (userShipTypeState.tableData.currentPage - 1) *
    userShipTypeState.tableData.param.maxResultCount;

  http<TableResultType>({
    method: "GET",
    url: "/api/base/user-ship",
    data: userShipTypeState.tableData.param,
  })
    .then((result) => {
      userShipTypeState.tableData.total = result.totalCount;
      userShipTypeState.tableData.data.push(...result.items);
    })
    .catch((err: any) => {
      console.error("查询用户(收货)地址信息出错：", err);
    });
};

// 收货地址
const goReceiving = (id: string) => {
  uni.$once("refreshUserShip", (result: boolean) => {
    if (result) {
      reset();
      loadUserShipData();
    }
  });
  uni.navigateTo({
    url: "/pages/member/address/receiving?id=" + id,
  });
};

// 选择收货地址
const selectUserShip = (userShip: UserShipType) => {
  if (isOption.value) {
    uni.$emit("selectUserShip", userShip);
    uni.navigateBack();
  }
};

onLoad((evt: any) => {
  isOption.value = evt.type === "option";
  loadUserShipData();
});
</script>

<style lang="scss" scoped>
.address-body {
  padding-bottom: 88rpx;
}

.address-list {
  .address-item {
    font-size: 26rpx;
    margin-bottom: 15rpx;
    .user-name {
      font-weight: bold;
    }
    .user-phone {
      display: inline-block;
      margin: 0rpx 15rpx;
      font-weight: bold;
    }
    .default-tag {
      background-color: #f56c6c;
      font-size: 20rpx;
      padding: 4rpx 8rpx;
      border-radius: 5rpx;
      color: white;
    }
  }
  .full-address {
    font-size: 24rpx;
    color: #999999;
  }
}

.address-footer {
  position: fixed;
  bottom: 0;
  width: 100%;
  text-align: center;
  border-top: 1px #f8f8f8 solid;
  .add-address-btn {
    font-size: 30rpx;
    height: 88rpx;
    line-height: 88rpx;
    background-color: white;
  }
}
</style>
