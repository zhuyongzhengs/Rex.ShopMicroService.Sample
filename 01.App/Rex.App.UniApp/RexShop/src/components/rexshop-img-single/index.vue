<template>
  <!-- 单图 -->
  <view
    class="img-single-body"
    v-if="rexShopData.parameters.list && rexShopData.parameters.list.length > 0"
  >
    <view v-for="(item, index) in rexShopData.parameters.list" :key="index">
      <up-image
        width="100%"
        mode="widthFix"
        :show-loading="true"
        :src="item.image"
        :radius="radius"
        @click="onSliderHandle(item.linkType, item.linkValue)"
      />
      <view
        class="imgup-btn"
        v-if="item.buttonShow"
        @click="onSliderHandle(item.linkType, item.linkValue)"
      >
        <button
          class="rexshop-btn"
          :style="{ background: item.buttonColor, color: item.textColor }"
        >
          {{ item.buttonText }}
        </button>
      </view>
    </view>
  </view>
</template>

<script setup lang="ts" name="rexShopImgSingle">
import { defineProps } from "vue";
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

// 图片触发事件
const onSliderHandle = (type: number, val: string) => {
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
</script>

<style lang="scss" scoped>
.img-single-body {
  .imgup-btn {
    position: relative;
    z-index: 668;
    bottom: 100upx;
    left: 60upx;
    float: left;
    .rexshop-btn {
      line-height: 2;
      font-size: 28upx;
      padding: 0 50upx;
      border-radius: 8rpx;
    }
  }
}
</style>
