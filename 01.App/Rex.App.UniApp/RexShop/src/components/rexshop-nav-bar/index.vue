<template>
  <view class="nav-bar-body bg-white" :style="'border-radius: ' + radius">
    <u-grid :col="navBarItem.limit" :border="false">
      <u-grid-item
        :customStyle="{ padding: '15rpx 0rpx' }"
        v-for="(item, index) in navBarItem.items"
        :key="index"
        @click="onSliderItem(item.linkType, item.linkValue)"
      >
        <u-icon
          width="90rpx"
          height="90rpx"
          label-pos="bottom"
          :name="item.image"
          :label="item.text"
          :label-size="14"
        ></u-icon>
      </u-grid-item>
    </u-grid>
  </view>
</template>

<script setup lang="ts" name="rexShopNavBar">
import { ref, onMounted, defineProps } from "vue";
import routeHelper from "@/utils/routeHelper";

// 定义父组件传过来的值
const props = defineProps({
  rexShopData: {
    type: Object,
    default: () => {},
  },
  radius: {
    type: String,
    default: () => "0rpx",
  },
});

// 导航组
const navBarItem = ref({
  items: [] as any,
  limit: 0,
});

const onSliderItem = (type: number, val: string) => {
  if (!val) {
    return;
  }
  console.log(type, val);
  if (type == 1) {
    if (val.indexOf("http") != -1) {
      // #ifdef H5
      window.location.href = val;
      // #endif
      return;
    }
    uni.navigateTo({
      url: val,
    });
  } else if (type == 2) {
    // 商品详情
    routeHelper.goGoodsDetail(val);
  } else if (type == 3) {
    // 文章详情
    routeHelper.goArticleDetail(val);
  } else if (type == 4) {
    // 文章列表
    routeHelper.goArticleList(val);
  } else if (type == 5) {
    //智能表单
    // this.$u.route("/pages/form/details/details?id=" + val);
  }
};

onMounted(() => {
  navBarItem.value.items = props.rexShopData.parameters.list;
  navBarItem.value.limit = props.rexShopData.parameters.limit;
});
</script>
<style lang="scss" scoped>
// ...
</style>
