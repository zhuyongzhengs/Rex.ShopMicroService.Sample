<template>
  <view class="good-browsing-body">
    <view
      class="good-browsing-empty-data"
      v-if="!goodBrowsingData.data || goodBrowsingData.data.length < 1"
    >
      <u-empty mode="list" text="无足迹信息" />
    </view>
    <template v-else>
      <view
        class="good-browsing-box"
        v-for="gBrowsing in goodBrowsingData.data"
        :key="gBrowsing.id"
      >
        <up-row @click="routeHelper.goGoodsDetail(gBrowsing.goodId)">
          <up-col span="3">
            <up-image
              width="150rpx"
              height="150rpx"
              radius="6rpx"
              :show-loading="true"
              :src="gBrowsing.image"
          /></up-col>
          <up-col span="9">
            <view class="product-detail">
              <view class="product-title">{{ gBrowsing.goodName }}</view>
              <view class="product-date">{{ gBrowsing.creationTime }}</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </template>

    <view class="u-margin-top-10" v-show="goodBrowsingData.data.length > 0">
      <u-loadmore nomore-text="" :status="goodBrowsingData.loading" />
    </view>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import { useUserLoginStore } from "@/stores/userLogin/index";
import routeHelper from "@/utils/routeHelper";

// 定义变量
const currentUser = ref<CurrentSysUserType>();
const goodBrowsingData = reactive({
  data: [] as GoodBrowsingType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    goodName: "",
    sorting: "CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return goodBrowsingData.data.length < goodBrowsingData.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) goodBrowsingData.loading = "loadmore";
  else goodBrowsingData.loading = "nomore";
  if (goodBrowsingData.data.length < 1) {
    goodBrowsingData.showEmptyData = Object(true);
  } else {
    goodBrowsingData.showEmptyData = null;
  }
};

// 加载足迹信息
const loadGoodBrowsings = () => {
  goodBrowsingData.param.skipCount =
    (goodBrowsingData.currentPage - 1) * goodBrowsingData.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<GoodBrowsingType[]>>({
    url: "/api/good/common/user-good-browsing",
    method: "GET",
    data: goodBrowsingData.param,
  }).then((result) => {
    goodBrowsingData.total = result.totalCount;
    goodBrowsingData.data.push(...result.items);
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

  loadGoodBrowsings();
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    goodBrowsingData.currentPage++;
    loadGoodBrowsings();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.good-browsing-body {
  .good-browsing-box {
    margin: 20rpx;
    background-color: white;
    border-radius: 10rpx;
    padding: 20rpx;
    .product-detail {
      padding-left: 10rpx;
      .product-title {
        font-size: 26rpx;
        color: #2c313b;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 3;
        max-height: 100rpx;
      }
      .product-date {
        font-size: 24rpx;
        color: #aaaaaa;
        margin: 10rpx 0rpx;
      }
    }
  }
  .good-browsing-empty-data {
    margin-top: 100rpx;
    text-align: center;
  }
}
</style>
