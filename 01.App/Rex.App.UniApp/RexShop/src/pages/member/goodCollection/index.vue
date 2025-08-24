<template>
  <view class="good-collection-body">
    <view
      class="good-collection-empty-data"
      v-if="!goodCollectionData.data || goodCollectionData.data.length < 1"
    >
      <u-empty mode="list" text="无收藏信息" />
    </view>
    <template v-else>
      <view
        class="good-collection-box"
        v-for="gCollection in goodCollectionData.data"
        :key="gCollection.id"
      >
        <up-row @click="routeHelper.goGoodsDetail(gCollection.goodId)">
          <up-col span="3">
            <up-image
              width="150rpx"
              height="150rpx"
              radius="6rpx"
              :show-loading="true"
              :src="gCollection.image"
          /></up-col>
          <up-col span="9">
            <view class="product-detail">
              <view class="product-title">{{ gCollection.goodName }}</view>
              <view class="product-date">{{ gCollection.creationTime }}</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </template>

    <view class="u-margin-top-10" v-show="goodCollectionData.data.length > 0">
      <u-loadmore nomore-text="" :status="goodCollectionData.loading" />
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
const goodCollectionData = reactive({
  data: [] as GoodCollectionType[],
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
  return goodCollectionData.data.length < goodCollectionData.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) goodCollectionData.loading = "loadmore";
  else goodCollectionData.loading = "nomore";
  if (goodCollectionData.data.length < 1) {
    goodCollectionData.showEmptyData = Object(true);
  } else {
    goodCollectionData.showEmptyData = null;
  }
};

// 加载收藏信息
const loadGoodCollections = () => {
  goodCollectionData.param.skipCount =
    (goodCollectionData.currentPage - 1) * goodCollectionData.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<GoodCollectionType[]>>({
    url: "/api/good/common/user-good-collection",
    method: "GET",
    data: goodCollectionData.param,
  }).then((result) => {
    goodCollectionData.total = result.totalCount;
    goodCollectionData.data.push(...result.items);
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

  loadGoodCollections();
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    goodCollectionData.currentPage++;
    loadGoodCollections();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.good-collection-body {
  .good-collection-box {
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
  .good-collection-empty-data {
    margin-top: 100rpx;
    text-align: center;
  }
}
</style>
