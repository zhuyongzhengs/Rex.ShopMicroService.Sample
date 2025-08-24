<template>
  <view class="category-list-body">
    <!--无网络组件-->
    <u-no-network></u-no-network>
    <view class="navbar">
      <view class="nav-item" :class="{ current: selectedSort }" @click="tabClick(0)">
        综合排序
      </view>
      <view class="nav-item" :class="{ current: selectedBuyCount }" @click="tabClick(1)">
        销量优先
      </view>
      <view class="nav-item" :class="{ current: selectedPrice }" @click="tabClick(2)">
        <text>价格</text>
        <!-- #ifdef MP-WEIXIN -->
        &nbsp;
        <!-- #endif -->
        <view class="p-box">
          <u-icon
            name="arrow-up"
            size="20rpx"
            class="price-icon"
            :color="selectedPriceAsc ? '#fa3534' : '#303133'"
          ></u-icon>
          <u-icon
            name="arrow-down"
            size="20rpx"
            class="price-icon"
            :color="selectedPriceDesc ? '#fa3534' : '#303133'"
          ></u-icon>
        </view>
      </view>
      <view class="cate-item">
        <u-icon
          class="yticon"
          name="grid"
          size="45rpx"
          v-show="classifyList && classifyList.length > 0"
          @click="toggleCateMask('show')"
        ></u-icon>
      </view>
    </view>
    <u-gap height="80rpx" bg-color="#F2F3F7" />
    <view class="goods-container column2">
      <view class="goods-content" v-if="goodsState.tableData.data">
        <u-grid :col="2" :border="false" align="left">
          <u-grid-item
            bg-color="transparent"
            :custom-style="{ padding: '0rpx' }"
            v-for="(good, index) in goodsState.tableData.data"
            :key="index"
            @click="routeHelper.goGoodsDetail(good.id)"
          >
            <view class="good-item-box">
              <view class="good-content">
                <image
                  class="good-img"
                  mode="scaleToFill"
                  :src="good.image"
                  :lazy-load="true"
                />
                <view class="good-title u-line-2">
                  {{ good.name }}
                </view>
                <view class="good-price">
                  ￥{{ good.price }}
                  <span
                    class="u-font-xs rexshop-text-through u-margin-left-15 rexshop-text-gray"
                    v-show="good.price != good.mktPrice"
                    >￥{{ good.mktPrice }}</span
                  >
                </view>
                <view class="good-tag-recommend" v-if="good.isRecommend"> 推荐 </view>
                <view class="good-tag-hot" v-if="good.isHot"> 热门 </view>
              </view>
            </view>
          </u-grid-item>
        </u-grid>
      </view>
    </view>
    <view class="loadmore-box">
      <u-loadmore :status="goodsState.tableData.loading" />
    </view>
    <view
      class="cate-mask"
      :class="
        categoryItem.cateMaskState === 0
          ? 'none'
          : categoryItem.cateMaskState === 1
          ? 'show'
          : ''
      "
      @click="toggleCateMask('none')"
    >
      <view class="cate-content">
        <scroll-view scroll-y class="cate-list">
          <view v-for="item in classifyList" :key="item.id" class="cate-box">
            <view class="cate-item two">{{ item.name }}</view>
            <view
              v-for="childItem in item.children"
              :key="childItem.id"
              class="cate-item"
              :class="{
                active: childItem.id == goodsState.tableData.param.goodCategoryId,
              }"
              @click="onChangeCate(childItem)"
            >
              {{ childItem.name }}
            </view>
          </view>
        </scroll-view>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts">
import { ref, reactive, onBeforeMount, onMounted, computed } from "vue";
import {
  onLoad,
  onPageScroll,
  onPullDownRefresh,
  onReachBottom,
} from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import routeHelper from "@/utils/routeHelper";

// 分类筛选项
const categoryItem = ref({
  cateMaskState: 0, // 分类面板展开状态
  headerPosition: "fixed",
  headerTop: "0px",
});

// 商品分类
const classifyList = ref([] as GoodCategoryType[]);

// 商品数据声明
const goodsState = reactive<GoodsState>({
  tableData: {
    data: [],
    total: 0,
    loading: "loadmore",
    currentPage: 1,
    param: {
      isMarketable: true,
      name: "",
      goodCategoryId: "",
      brandId: "",
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 初始化查询参数
const initParam = () => {
  goodsState.tableData.currentPage = 1;
  goodsState.tableData.param.isMarketable = true;
  goodsState.tableData.param.name = "";
  goodsState.tableData.param.brandId = "";
  goodsState.tableData.param.skipCount = 0;
  goodsState.tableData.param.maxResultCount = 10;
};

// 选中[综合排序]
const selectedSort = computed(() => {
  return goodsState.tableData.param.sorting == "Sort";
});

// 选中[销量优先]
const selectedBuyCount = computed(() => {
  return goodsState.tableData.param.sorting == "BuyCount DESC";
});

// 选中[价格]
const selectedPrice = computed(() => {
  return goodsState.tableData.param.sorting.indexOf("Price") >= 0;
});

// 选中[价格-升序]
const selectedPriceAsc = computed(() => {
  return goodsState.tableData.param.sorting == "Price ASC";
});

// 选中[价格-降序]
const selectedPriceDesc = computed(() => {
  return goodsState.tableData.param.sorting == "Price DESC";
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return goodsState.tableData.data.length < goodsState.tableData.total;
});

// 筛选点击
const tabClick = (index: number) => {
  initParam();
  if (index == 0) {
    // 综合排序
    goodsState.tableData.param.sorting = "Sort";
  } else if (index == 1) {
    // 销量优先
    goodsState.tableData.param.sorting = "BuyCount DESC";
  } else if (index == 2) {
    // 价格
    if (
      goodsState.tableData.param.sorting == "Price ASC" ||
      goodsState.tableData.param.sorting == "Price DESC"
    ) {
      goodsState.tableData.param.sorting = selectedPriceAsc.value
        ? "Price DESC"
        : "Price ASC";
    } else {
      goodsState.tableData.param.sorting = "Price ASC";
    }
  }
  uni.pageScrollTo({
    duration: 300,
    scrollTop: 0,
  });
  goodsState.tableData.data = [];
  searchGoods();
};

// 查询商品数据
const searchGoods = () => {
  goodsState.tableData.currentPage = 1;
  getGoodsTableData();
};

// 获取商品数据信息
const getGoodsTableData = () => {
  goodsState.tableData.loading = "loading";
  goodsState.tableData.param.skipCount =
    (goodsState.tableData.currentPage - 1) * goodsState.tableData.param.maxResultCount;

  http<TableResultType>({
    method: "GET",
    url: "/api/front/aggregation/common/goods",
    data: goodsState.tableData.param,
  })
    .then((result) => {
      goodsState.tableData.total = result.totalCount;
      goodsState.tableData.data.push(...result.items);
      checkMoreData();
    })
    .catch((err: any) => {
      console.error("查询商品列表出错：", err);
    });
};

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) goodsState.tableData.loading = "loadmore";
  else goodsState.tableData.loading = "nomore";
  uni.stopPullDownRefresh();
};

//显示分类面板
const toggleCateMask = (type: string) => {
  let timer = type === "show" ? 10 : 300;
  let state = type === "show" ? 1 : 0;
  categoryItem.value.cateMaskState = 2;
  setTimeout(() => {
    categoryItem.value.cateMaskState = state;
  }, timer);
};

// 分类点击
const onChangeCate = (goodClassify: any) => {
  initParam();
  goodsState.tableData.param.goodCategoryId = goodClassify.id;
  goodsState.tableData.param.sorting = "Sort";
  toggleCateMask("none");
  uni.pageScrollTo({
    duration: 300,
    scrollTop: 0,
  });
  goodsState.tableData.data = [];
  searchGoods();
};

// 加载[树形]商品分类
const loadGoodClassifys = (oneId: string) => {
  uni.getStorage({
    key: "GOOD_CATEGORYS_TREE",
    success: function (res) {
      if (res.data) {
        let goodCategorys = res.data as GoodCategoryType[];
        if (oneId) {
          goodCategorys = goodCategorys.filter((p) => p.id == oneId);
        }
        if (goodCategorys && goodCategorys.length > 0) classifyList.value = [];
        for (let i = 0; i < goodCategorys.length; i++) {
          const goodCategory = goodCategorys[i];
          for (let j = 0; j < goodCategory.children.length; j++) {
            const twoGoodCategory = goodCategory.children[j];
            classifyList.value.push(twoGoodCategory);
          }
        }
      }
    },
  });
};

// 数据加载
onLoad((opt: any) => {
  if (opt.threeId) {
    goodsState.tableData.param.goodCategoryId = opt.threeId;
  }

  if (opt.goodCategoryId) {
    goodsState.tableData.param.goodCategoryId = opt.goodCategoryId;
  }

  if (opt.brandId) {
    goodsState.tableData.param.brandId = opt.brandId;
  }

  if (opt.name) {
    goodsState.tableData.param.name = opt.name;
  }

  if (opt.oneId) {
    // 加载商品分类
    loadGoodClassifys(opt.oneId);
  }

  // 查询商品信息
  searchGoods();
});

// 组件挂载到节点上之前
const statusBarHeight = ref(uni.getWindowInfo().statusBarHeight);
onBeforeMount(() => {
  // #ifdef MP-WEIXIN
  categoryItem.value.headerTop = statusBarHeight.value + "px";
  // #endif
});

// 滚动监听
onPageScroll((e) => {
  // 兼容iOS端下拉时顶部漂移
  if (e.scrollTop >= 0) {
    categoryItem.value.headerPosition = "fixed";
  } else {
    categoryItem.value.headerPosition = "absolute";
  }
});

// 下拉刷新
onPullDownRefresh(() => {
  initParam();
  goodsState.tableData.data = [];
  searchGoods();
  console.log("触发了[下拉刷新]！");
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    goodsState.tableData.currentPage++;
    getGoodsTableData();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.category-list-body {
  .navbar {
    position: fixed;
    left: 0;
    top: var(--window-top);
    display: flex;
    width: 100%;
    height: 80upx;
    background: #fff;
    box-shadow: 0 2upx 10upx rgba(0, 0, 0, 0.06);
    z-index: 10;
    .nav-item {
      flex: 1;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100%;
      font-size: 30upx;
      color: $shop-main-color;
      position: relative;
      &.current {
        color: $shop-type-error;
        &:after {
          content: "";
          position: absolute;
          left: 50%;
          bottom: 0;
          transform: translateX(-50%);
          width: 120upx;
          height: 0;
          border-bottom: 4upx solid $shop-type-error;
        }
      }
    }
    .p-box {
      display: flex;
      flex-direction: column;
      .price-icon {
        position: relative;
        top: 2rpx;
        left: 8rpx;
      }
      .yticon {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 30upx;
        height: 14upx;
        line-height: 1;
        margin-left: 4upx;
        font-size: 26upx;
        color: #888;
        &.active {
          color: $shop-type-error;
        }
      }
      .xia {
        transform: scaleY(-1);
      }
    }
    .cate-item {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100%;
      width: 80upx;
      position: relative;
      font-size: 44upx;
      &:after {
        content: "";
        position: absolute;
        left: 0;
        top: 50%;
        transform: translateY(-50%);
        border-left: 1px solid #ddd;
        width: 0;
        height: 36upx;
      }
    }
  }

  .loadmore-box {
    position: fixed;
    bottom: 50rpx;
    width: 100%;
  }

  /* 分类 */
  .cate-mask {
    position: fixed;
    left: 0;
    top: var(--window-top);
    bottom: 0;
    width: 100%;
    background: rgba(0, 0, 0, 0);
    z-index: 95;
    transition: 0.3s;

    .cate-content {
      width: 630upx;
      height: 100%;
      background: #fff;
      float: right;
      transform: translateX(100%);
      transition: 0.3s;
    }
    &.none {
      display: none;
    }
    &.show {
      background: rgba(0, 0, 0, 0.4);

      .cate-content {
        transform: translateX(0);
      }
    }
  }
  .cate-list {
    display: flex;
    flex-direction: column;
    height: 100%;

    .cate-item {
      display: flex;
      align-items: center;
      height: 90upx;
      padding-left: 30upx;
      font-size: 28upx;
      color: #555;
      position: relative;
      border-bottom: 1rpx #f1f2f5 solid;
    }

    .cate-item:last-child {
      border-bottom: none !important;
    }

    .two {
      height: 64upx;
      color: #303133;
      font-size: 30upx;
      background: #f8f8f8;
      border: 1rpx #f1f2f5 solid;
    }
    .active {
      color: $shop-type-error;
    }
  }

  /* 商品列表 */

  /* 两三列部分 */
  .goods-container {
    padding: 0rpx 8rpx;
    .goods-content {
      .good-item-box {
        width: 100%;

        .good-content {
          background-color: white;
          margin: 0rpx 6rpx;
          padding: 12rpx;
          margin-top: 12rpx;
          border-radius: 10rpx;

          .good-img {
            width: 100%;
            height: 320rpx;
            border-radius: 5rpx;
          }

          .good-title {
            font-size: 26rpx;
            margin-top: 5px;
            height: 68rpx;
            color: $u-main-color;
          }
          .good-price {
            font-size: 30rpx;
            color: $u-error;
            margin-top: 5px;
          }
          .good-tag-hot {
            display: flex;
            margin-top: 5px;
            position: absolute;
            top: 20rpx;
            left: 25rpx;
            background-color: $u-error;
            color: #ffffff;
            display: flex;
            align-items: center;
            padding: 4rpx 14rpx;
            border-radius: 50rpx;
            font-size: 20rpx;
            line-height: 1;
          }
          .good-tag-recommend {
            display: flex;
            margin-top: 5px;
            position: absolute;
            top: 20rpx;
            right: 25rpx;
            background-color: $u-primary;
            color: #ffffff;
            margin-left: 10px;
            border-radius: 50rpx;
            line-height: 1;
            padding: 4rpx 14rpx;
            display: flex;
            align-items: center;
            border-radius: 50rpx;
            font-size: 20rpx;
          }
        }
      }
    }
  }
}
</style>
