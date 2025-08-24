<template>
  <view class="point-body">
    <view class="point-point-box">
      <view class="point-title">
        <up-text
          color="white"
          mode="text"
          align="left"
          size="24rpx"
          text="可用积分"
        ></up-text>
      </view>
      <template v-if="currentUser">
        <up-text
          color="white"
          align="center"
          size="50rpx"
          :text="currentUser.point"
        ></up-text>
      </template>
    </view>
    <template v-for="(userPointLog, key) in userPointLogData.data" :key="key">
      <view class="point-box">
        <up-row>
          <up-col span="7" customStyle="margin-bottom: 20rpx">
            <up-text size="30rpx" :text="userPointLog.typeDisplay"></up-text>
          </up-col>
          <up-col span="5" customStyle="margin-bottom: 20rpx">
            <up-text
              size="30rpx"
              type="error"
              align="right"
              :text="userPointLog.num > 0 ? '+' + userPointLog.num : userPointLog.num"
            ></up-text>
          </up-col>
        </up-row>
        <up-row>
          <up-col span="7">
            <up-text type="info" size="24rpx" :text="userPointLog.remark"></up-text>
          </up-col>
          <up-col span="5">
            <up-text
              type="info"
              size="24rpx"
              align="right"
              :text="userPointLog.creationTime"
            ></up-text>
          </up-col>
        </up-row>
      </view>
    </template>

    <view
      class="point-empty-data"
      v-show="!userPointLogData.data || userPointLogData.data.length < 1"
    >
      <u-empty mode="list" text="无积分信息" />
    </view>
    <view class="u-margin-top-10" v-show="userPointLogData.data.length > 0">
      <u-loadmore nomore-text="" :status="userPointLogData.loading" />
    </view>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";

// 定义变量
const currentUser = ref<CurrentSysUserType>();
const userPointLogData = reactive({
  data: [] as UserPointLogType[],
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
  return userPointLogData.data.length < userPointLogData.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) userPointLogData.loading = "loadmore";
  else userPointLogData.loading = "nomore";
  if (userPointLogData.data.length < 1) {
    userPointLogData.showEmptyData = Object(true);
  } else {
    userPointLogData.showEmptyData = null;
  }
};

// 加载积分信息
const loadUserPointLogs = () => {
  userPointLogData.param.skipCount =
    (userPointLogData.currentPage - 1) * userPointLogData.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<UserPointLogType[]>>({
    url: "/api/base/common/user-point-log-page-list",
    method: "GET",
    data: userPointLogData.param,
  }).then((result) => {
    userPointLogData.total = result.totalCount;
    userPointLogData.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

onLoad(() => {
  if (!useUserLoginStore().hasLogin) {
    uni.navigateBack();
    return;
  }
  useUserLoginStore().refreshCurrentSysUser();
  currentUser.value = useUserLoginStore().currentSysUser;

  loadUserPointLogs();
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    userPointLogData.currentPage++;
    loadUserPointLogs();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.point-body {
  .point-point-box {
    background-color: #d25a49;
    padding: 50rpx 20rpx;
    .point-title {
      position: relative;
      top: -30rpx;
    }
  }
  .point-box {
    margin: 20rpx;
    background-color: white;
    border-radius: 10rpx;
    padding: 30rpx 20rpx;
  }
  .point-empty-data {
    margin-top: 100rpx;
    text-align: center;
  }
}
</style>
