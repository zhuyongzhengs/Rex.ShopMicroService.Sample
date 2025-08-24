<template>
  <view class="store-body">
    <up-sticky>
      <view class="store-map-box">
        <map
          class="map-container"
          :scale="11"
          :enable-3D="true"
          :show-compass="true"
          :enable-overlooking="true"
          :enable-traffic="true"
          :latitude="storeMap.latitude"
          :longitude="storeMap.longitude"
          :markers="storeMap.mapMarkers"
        >
        </map>
      </view>
    </up-sticky>
    <view class="store-item-box">
      <view class="store-item-list" v-for="gStore in goodStore.data" :key="gStore.id">
        <up-row>
          <up-col span="3">
            <view class="store-image-item">
              <up-image
                width="140rpx"
                height="140rpx"
                :src="gStore.logoImage"
                radius="10rpx"
                :lazy-load="true"
              ></up-image>
            </view>
          </up-col>
          <up-col span="7">
            <up-text type="primary" :text="gStore.storeName" :lines="1"></up-text>
            <up-text
              size="24rpx"
              prefixIcon="phone"
              type="phone"
              margin="10rpx 0rpx"
              @click="routeHelper.phoneCall(gStore.mobile)"
              :text="gStore.mobile"
              :lines="1"
            ></up-text>
            <up-text size="24rpx" type="info" :text="'地址：' + gStore.address"></up-text>
          </up-col>
          <up-col span="2">
            <up-text
              margin="0rpx 0rpx 10rpx 0rpx"
              size="24rpx"
              align="center"
              type="info"
              :text="gStore.distanceDisplay"
            ></up-text>
            <view class="store-btn-item"
              ><up-tag
                text="查看"
                type="success"
                plain
                @click="goMarkers(gStore.latitude, gStore.longitude)"
              ></up-tag
            ></view>
          </up-col>
        </up-row>
      </view>
    </view>
  </view>
  <u-toast ref="uToastRef" />
</template>
<script setup lang="ts">
import { ref } from "vue";
import { onLoad, onReachBottom, onPageScroll } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import routeHelper from "@/utils/routeHelper";

// 定义变量
const uToastRef = ref();
const scrollTop = ref(0);
const storeMap = reactive({
  latitude: 0,
  longitude: 0,
  mapMarkers: [
    {
      id: "",
      longitude: 0,
      latitude: 0,
    },
  ],
});
const goodStore = reactive({
  data: [] as StoreType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    storeName: "",
    latitude: 0,
    longitude: 0,
    sorting: "CreationTime DESC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 查看Map标记点
const goMarkers = (latitude: string, longitude: string) => {
  storeMap.latitude = Number(latitude);
  storeMap.longitude = Number(longitude);
};

// 获取当前位置
const getCurrentLocation = () => {
  uni.getLocation({
    type: "wgs84",
    success: function (res) {
      storeMap.longitude = res.longitude;
      storeMap.latitude = res.latitude;
      goodStore.param.longitude = res.longitude;
      goodStore.param.latitude = res.latitude;
      getStoreList();
    },
    fail: function () {
      uToastRef.value.warning("获取您的经纬度坐标失败！");
      getStoreList();
    },
  });
};

// 获取门店信息
const getStoreList = () => {
  goodStore.param.skipCount =
    (goodStore.currentPage - 1) * goodStore.param.maxResultCount;
  http<TableResultType<StoreType[]>>({
    method: "GET",
    url: "/api/good/common/store-coordinate-list",
    data: goodStore.param,
  })
    .then((result) => {
      goodStore.total = result.totalCount;
      goodStore.data.push(...result.items);

      for (let i = 0; i < goodStore.data.length; i++) {
        const item = goodStore.data[i];
        storeMap.mapMarkers.push({
          id: item.id,
          latitude: Number(item.latitude),
          longitude: Number(item.longitude),
        });
      }

      checkMoreData();
      uni.hideLoading();
    })
    .catch((err: any) => {
      console.error("获取[门店信息]数据出错：", err);
    });
};

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) goodStore.loading = "loadmore";
  else goodStore.loading = "nomore";
  if (goodStore.data.length < 1) {
    goodStore.showEmptyData = Object(true);
  } else {
    goodStore.showEmptyData = null;
  }
};

// 是否还有更多数据
const isMoreData = computed(() => {
  return goodStore.data.length < goodStore.total;
});

onLoad(() => {
  getCurrentLocation();
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    goodStore.currentPage++;
    getStoreList();
    console.log("触发了[加载更多]！");
  }
});

onPageScroll((e) => {
  scrollTop.value = e.scrollTop;
});
</script>
<style lang="scss" scoped>
.store-body {
  .store-map-box {
    width: 100%;
    height: 500rpx;
    .map-container {
      width: 100%;
      height: 100%;
    }
  }
  .store-item-box {
    .store-item-list {
      background-color: white;
      padding: 15rpx 0rpx;
      margin-bottom: 3rpx;
      .store-image-item,
      .store-btn-item {
        display: flex;
        justify-content: center;
      }
    }
  }
}
</style>
