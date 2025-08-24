<template>
  <view class="balance-list-body">
    <view class="balance-list-tabs-box">
      <up-tabs
        lineColor="#f56c6c"
        :list="userBalanceStatusList"
        :current="currUserBalanceType.index"
        :activeStyle="{ color: '#f56c6c', fontSize: '26rpx' }"
        :inactiveStyle="{ fontSize: '26rpx' }"
        :itemStyle="{ height: '75rpx', width: '25%' }"
        @click="userBalanceStatusHandle"
      ></up-tabs>
    </view>
    <template v-for="(userBalance, key) in userBalanceData.data" :key="key">
      <view class="balance-list-box">
        <up-row>
          <up-col span="6" customStyle="margin-bottom: 20rpx">
            <up-text size="30rpx" :text="userBalance.typeDisplay"></up-text>
          </up-col>
          <up-col span="6" customStyle="margin-bottom: 20rpx">
            <up-text
              size="30rpx"
              type="error"
              mode="price"
              align="right"
              :text="userBalance.money"
            ></up-text>
          </up-col>
        </up-row>
        <up-row>
          <up-col span="6">
            <up-text
              type="info"
              size="24rpx"
              :text="`余额：￥${userBalance.balance}`"
            ></up-text>
          </up-col>
          <up-col span="6">
            <up-text
              type="info"
              size="24rpx"
              align="right"
              :text="userBalance.creationTime"
            ></up-text>
          </up-col>
        </up-row>
      </view>
    </template>

    <view
      class="balance-list-empty-data"
      v-show="!userBalanceData.data || userBalanceData.data.length < 1"
    >
      <u-empty mode="list" :text="`无${currUserBalanceType.name}信息`" />
    </view>
    <view class="u-margin-top-10" v-show="userBalanceData.data.length > 0">
      <u-loadmore nomore-text="" :status="userBalanceData.loading" />
    </view>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import { http } from "@/utils/http";

// 余额信息
const userBalanceData = reactive({
  data: [] as UserBalanceType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    type: 0,
    sorting: "CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return userBalanceData.data.length < userBalanceData.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) userBalanceData.loading = "loadmore";
  else userBalanceData.loading = "nomore";
  if (userBalanceData.data.length < 1) {
    userBalanceData.showEmptyData = Object(true);
  } else {
    userBalanceData.showEmptyData = null;
  }
};

// 审核状态标签
const userBalanceStatusList = reactive([
  { name: "全部" },
  { name: "我的消费" },
  { name: "我的退款" },
  { name: "充值" },
]);

// 选中审核状态
const currUserBalanceType = reactive({
  index: 0,
  name: "全部",
});

// 触发审核状态
const userBalanceStatusHandle = (item: any) => {
  userBalanceData.showEmptyData = null;
  userBalanceData.param.type = 0;
  switch (item.name) {
    case "我的消费":
      userBalanceData.param.type = 1;
      break;
    case "我的退款":
      userBalanceData.param.type = 2;
      break;
    case "充值":
      userBalanceData.param.type = 3;
      break;

    default:
      break;
  }
  currUserBalanceType.name = item.name;
  userBalanceData.currentPage = 1;
  userBalanceData.data = [];
  userBalanceData.total = 0;
  loadUserBalances();
};

// 加载余额信息
const loadUserBalances = () => {
  userBalanceData.param.skipCount =
    (userBalanceData.currentPage - 1) * userBalanceData.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<UserBalanceType[]>>({
    url: "/api/base/user-balance",
    method: "GET",
    data: userBalanceData.param,
  }).then((result) => {
    userBalanceData.total = result.totalCount;
    userBalanceData.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

onLoad((evt: any) => {
  let typeName = evt.type ?? "全部";
  for (let i = 0; i < userBalanceStatusList.length; i++) {
    const aType = userBalanceStatusList[i];
    if (aType.name == typeName) {
      currUserBalanceType.index = i;
      currUserBalanceType.name = aType.name;
      break;
    }
  }
  userBalanceStatusHandle(currUserBalanceType);
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    userBalanceData.currentPage++;
    loadUserBalances();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.balance-list-body {
  .balance-list-tabs-box {
    background-color: white;
  }
  .balance-list-box {
    margin: 20rpx;
    background-color: white;
    border-radius: 10rpx;
    padding: 30rpx 20rpx;
  }
  .balance-list-empty-data {
    margin-top: 100rpx;
    text-align: center;
  }
}
</style>
