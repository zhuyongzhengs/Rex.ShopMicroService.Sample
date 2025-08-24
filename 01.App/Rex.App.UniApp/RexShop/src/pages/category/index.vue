<template>
  <view class="category-body u-wrap">
    <!--无网络组件-->
    <u-no-network></u-no-network>

    <!-- #ifdef MP-WEIXIN -->
    <rexshop-status-bar :isFixedTop="false" />
    <!-- #endif -->

    <view class="u-search-box" id="search_box">
      <u-search
        bgColor="#F2F3F7"
        height="56rpx"
        maxlength="100"
        placeholder="请输入搜索内容"
        v-model="categoryData.searchKey"
        :showAction="false"
        :margin="searchMarginRight"
        @custom="goSearchList"
        @search="goSearchList"
      />
    </view>
    <view class="u-menu-wrap bg-white">
      <scroll-view
        scroll-y
        scroll-with-animation
        class="u-tab-view menu-scroll-view"
        :scroll-top="categoryData.scrollTop"
        :scroll-into-view="categoryData.itemId"
      >
        <view
          class="u-tab-item"
          v-for="(item, index) in categoryData.tabBar"
          :key="index"
          :class="[categoryData.current == index ? 'u-tab-item-active' : '']"
          @tap.stop="swichMenu(index)"
        >
          <text class="u-line-1">{{ item.name }}</text>
        </view>
      </scroll-view>
      <scroll-view
        :scroll-top="categoryData.scrollRightTop"
        scroll-y
        scroll-with-animation
        class="right-box"
        @scroll="rightScroll"
      >
        <view class="page-view">
          <view
            class="class-item"
            v-for="(item, index) in categoryData.categoryList"
            :key="index"
            :id="'item_' + item.id"
          >
            <view class="item-title">
              <text>{{ item.name }}</text>
            </view>
            <view class="item-container">
              <view
                class="thumb-box"
                v-for="(childItem, childIndex) in item.children"
                :key="childIndex"
                @click="goList(item.parentId, item.id, childItem.id)"
              >
                <image class="item-menu-image" :src="childItem.imageUrl"></image>
                <view class="item-menu-name">{{ childItem.name }}</view>
              </view>
            </view>
          </view>
        </view>
      </scroll-view>
    </view>

    <u-toast ref="uToastRef" />
  </view>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { onLoad, onPullDownRefresh } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import _ from "lodash";

const uToastRef = ref();
const searchMarginRight = ref("0px");

// 分类数据
const categoryData = ref({
  searchKey: "",
  searchHeight: 0,
  scrollTop: 0, //tab标题的滚动条位置
  oldScrollTop: 0,
  current: 0, // 预设当前项的值
  itemId: "", // 栏目右边scroll-view用于滚动的id
  tabBar: [] as GoodCategoryType[],
  categoryList: [] as GoodCategoryType[],
  scrollRightTop: 0, // 右边栏目scroll-view的滚动条高度
});

// 点击左边的栏目切换
const swichMenu = async (index: number) => {
  categoryData.value.current = index;
  let oneItem = categoryData.value.tabBar[categoryData.value.current]?.children[0];
  if (oneItem) {
    let selectorQuery = uni.createSelectorQuery();
    selectorQuery
      .select("#item_" + oneItem.id)
      .boundingClientRect((data: any) => {
        let calcRightTop = Number(data.top - categoryData.value.searchHeight);
        categoryData.value.scrollRightTop =
          calcRightTop + categoryData.value.oldScrollTop;
        categoryData.value.oldScrollTop = _.cloneDeep(categoryData.value.scrollRightTop);
      })
      .exec();
  }
};

// 右边菜单滚动
const rightScroll = async (e: any) => {
  categoryData.value.oldScrollTop = e.detail.scrollTop;
  let selectorQuery = uni.createSelectorQuery();
  selectorQuery
    .selectAll(".class-item")
    .boundingClientRect((rects: any) => {
      // 如果节点尚未生成，rects值为[](因为用selectAll，所以返回的是数组)，循环调用执行
      for (let i = 0; i < rects.length; i++) {
        const rect = rects[i];
        let id = rect.id.split("_")[1];
        let categoryItem = categoryData.value.categoryList.find(
          (p) => p.id == id
        ) as GoodCategoryType;
        let top = rect.top - categoryData.value.searchHeight;
        if (top < 1) {
          leftCategoryActive(categoryItem.parentId);
        }
      }
    })
    .exec();
};

// 选中[左侧]商品分类
const leftCategoryActive = (id: string) => {
  for (let i = 0; i < categoryData.value.tabBar.length; i++) {
    const category = categoryData.value.tabBar[i];
    if (id == category.id) {
      categoryData.value.current = i;
    }
  }
};

// 加载分类数据
const loadGoodClassifyAsync = async () => {
  uni.showLoading({
    title: "加载中",
  });
  const goodCategorys = await http<GoodCategoryType[]>({
    method: "GET",
    url: "/api/front/aggregation/common/goodCategorysTree",
  });
  if (!goodCategorys || goodCategorys.length < 1) return;
  uni.setStorage({
    key: "GOOD_CATEGORYS_TREE",
    data: goodCategorys,
    success: function () {
      console.log("存储[商品分类数据]成功！");
    },
  });
  goodCategorys.forEach((category: GoodCategoryType) => {
    if (!category.parentId) {
      categoryData.value.tabBar.push(category);
      for (let i = 0; i < category.children.length; i++) {
        const categoryChild = category.children[i];
        categoryData.value.categoryList.push(categoryChild);
      }
    }
  });
};

// 查询[商品列表]
const goSearchList = () => {
  if (!categoryData.value.searchKey) {
    uToastRef.value.warning("请输入搜索关键字！");
    return;
  }
  uni.navigateTo({
    url: `/pages/category/list?name=${categoryData.value.searchKey}`,
  });
};

// 跳转到分类[筛选]列表
const goList = (oneId: string, twoId: string, threeId: string) => {
  uni.navigateTo({
    url: `/pages/category/list?oneId=${oneId}&twoId=${twoId}&threeId=${threeId}`,
  });
};

// 初始化分类数据
const initCategoryData = () => {
  categoryData.value.searchHeight = 0;
  categoryData.value.scrollTop = 0;
  categoryData.value.oldScrollTop = 0;
  categoryData.value.current = 0;
  categoryData.value.itemId = "";
  categoryData.value.tabBar = [] as GoodCategoryType[];
  categoryData.value.categoryList = [] as GoodCategoryType[];
  categoryData.value.scrollRightTop = 0;
};

// 监听下拉刷新
onPullDownRefresh(async () => {
  initCategoryData();
  await loadGoodClassifyAsync();
  uni.stopPullDownRefresh();
  console.log("【分类】下拉刷新，重新加载数据！");
});

// 数据加载
onLoad(async () => {
  // 查询商品分类
  await loadGoodClassifyAsync();
});

// 页面加载完时
onMounted(() => {
  // #ifdef MP-WEIXIN
  searchMarginRight.value = "0rpx 175rpx 0rpx 0rpx";
  // #endif

  let selectorQuery = uni.createSelectorQuery();
  selectorQuery
    .select("#search_box")
    .boundingClientRect((data: any) => {
      categoryData.value.searchHeight = data.height;
    })
    .exec();
});
</script>

<style lang="scss" scoped>
.u-wrap {
  height: calc(100vh);
  /* #ifdef H5 */
  height: calc(100vh - var(--window-top));
  /* #endif */
  display: flex;
  flex-direction: column;
}

.u-search-box {
  padding: 15rpx 30rpx;
  background-color: #e54d42;
}

.u-menu-wrap {
  flex: 1;
  display: flex;
  overflow: hidden;
}

.u-search-inner {
  background-color: rgb(234, 234, 234);
  border-radius: 100rpx;
  display: flex;
  align-items: center;
  padding: 10rpx 16rpx;
}

.u-search-text {
  font-size: 26rpx;
  color: $u-tips-color;
  margin-left: 10rpx;
}

.u-tab-view {
  width: 200rpx;
  height: 100%;
  background-color: #f2f4f8;
}

.u-tab-item {
  height: 110rpx;
  box-sizing: border-box;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 26rpx;
  color: #444;
  font-weight: 400;
  line-height: 1;
}

.u-tab-item-active {
  position: relative;
  color: #000;
  font-size: 30rpx;
  font-weight: 600;
  background: #fff;
}

.u-tab-item-active::before {
  content: "";
  position: absolute;
  border-left: 4px solid $u-error;
  height: 32rpx;
  left: 0;
  top: 39rpx;
}

.u-tab-view {
  height: 100%;
}

.right-box {
  background-color: rgb(250, 250, 250);
}

.page-view {
  padding: 16rpx;
}

.class-item {
  margin-bottom: 30rpx;
  background-color: #fff;
  padding: 16rpx;
  border-radius: 8rpx;
}

.class-item:last-child {
  min-height: 100vh;
}

.item-title {
  font-size: 26rpx;
  color: $u-main-color;
  font-weight: bold;
}

.item-menu-name {
  font-weight: normal;
  font-size: 24rpx;
  color: $u-main-color;
}

.item-container {
  display: flex;
  flex-wrap: wrap;
}

.thumb-box {
  width: 33.333333%;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  margin-top: 20rpx;
}

.item-menu-image {
  width: 120rpx;
  height: 120rpx;
  border-radius: 10rpx;
}
</style>
