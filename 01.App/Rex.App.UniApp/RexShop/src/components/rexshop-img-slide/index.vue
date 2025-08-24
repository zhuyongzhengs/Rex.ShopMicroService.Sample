<template>
  <view class="img-slide-body">
    <up-swiper
      indicator
      indicatorMode="dot"
      circular
      :displayMultipleItems="imgSlideItem.list.length > 0 ? 1 : 0"
      :radius="radius"
      :interval="imgSlideItem.duration"
      :height="imgSlideItem.height"
      :duration="750"
      :list="imgSlideItem.list"
      @click="onImgSlideHandle"
    />
    <u-toast ref="uToast" />
  </view>
</template>

<script setup lang="ts" name="rexShopImgSlide">
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
const uToast = ref();

// 轮播图项
const imgSlideItem = ref({
  duration: 3000,
  height: 175,
  list: [] as string[],
  datas: [] as any,
});

// 加载轮播图
const loadImgSlideItem = () => {
  imgSlideItem.value.duration = props.rexShopData.parameters.duration;
  imgSlideItem.value.height = props.rexShopData.parameters.height;
  for (let i = 0; i < props.rexShopData.parameters.list.length; i++) {
    const imgSlide = props.rexShopData.parameters.list[i];
    imgSlideItem.value.list.push(imgSlide.image);
    imgSlideItem.value.datas.push(imgSlide);
  }
};

// 点击轮播图
const onImgSlideHandle = (index: number) => {
  let type = imgSlideItem.value.datas[index].linkType;
  let val = imgSlideItem.value.datas[index].linkValue;
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

// 页面加载完时
onMounted(() => {
  loadImgSlideItem();
});
</script>
<style scoped lang="scss">
// .img-slide-body {
//   border: 1px red solid;
// }
</style>
