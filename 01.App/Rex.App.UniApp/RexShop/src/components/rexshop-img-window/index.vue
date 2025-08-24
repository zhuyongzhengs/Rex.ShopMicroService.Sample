<template>
  <view class="img-window-body bg-white" :style="'border-radius: ' + radius">
    <!-- 2：两列、3：三列、4：四列 -->
    <u-grid
      align="left"
      :border="false"
      :col="rexShopData.parameters.style"
      v-if="
        rexShopData.parameters.style == '2' ||
        rexShopData.parameters.style == '3' ||
        rexShopData.parameters.style == '4'
      "
    >
      <u-grid-item
        v-for="(item, index) in rexShopData.parameters.list"
        :key="index"
        :custom-style="{ padding: rexShopData.parameters.margin * 2 + 'rpx' }"
      >
        <image
          class="w100"
          mode="widthFix"
          :src="item.image"
          :lazy-load="true"
          @click="showSliderInfo(item.linkType, item.linkValue)"
        ></image>
      </u-grid-item>
    </u-grid>

    <!-- 0：橱窗 -->
    <up-row customStyle="padding: 8rpx" v-if="rexShopData.parameters.style == '0'">
      <up-col
        span="6"
        :customStyle="'padding-right: ' + rexShopData.parameters.margin * 2 + 'px'"
      >
        <up-image
          width="100%"
          height="400rpx"
          :src="rexShopData.parameters.list[0].image"
          mode="scaleToFill"
          v-if="rexShopData.parameters.list[0]"
          @click="
            showSliderInfo(
              rexShopData.parameters.list[0].linkType,
              rexShopData.parameters.list[0].linkValue
            )
          "
          :lazy-load="true"
        />
      </up-col>
      <up-col span="6">
        <up-row>
          <up-col
            span="12"
            :customStyle="'padding-bottom: ' + rexShopData.parameters.margin * 2 + 'px'"
          >
            <up-image
              width="100%"
              height="200rpx"
              :src="rexShopData.parameters.list[1].image"
              mode="scaleToFill"
              v-if="rexShopData.parameters.list[1]"
              @click="
                showSliderInfo(
                  rexShopData.parameters.list[1].linkType,
                  rexShopData.parameters.list[1].linkValue
                )
              "
              :lazy-load="true"
            />
          </up-col>
        </up-row>
        <up-row>
          <up-col
            span="6"
            :customStyle="'padding-right: ' + rexShopData.parameters.margin + 'px'"
          >
            <up-image
              width="100%"
              height="180rpx"
              :src="rexShopData.parameters.list[2].image"
              mode="scaleToFill"
              v-if="rexShopData.parameters.list[2]"
              @click="
                showSliderInfo(
                  rexShopData.parameters.list[2].linkType,
                  rexShopData.parameters.list[2].linkValue
                )
              "
              :lazy-load="true"
            />
          </up-col>
          <up-col
            span="6"
            :customStyle="'padding-left: ' + rexShopData.parameters.margin + 'px'"
          >
            <up-image
              width="100%"
              height="180rpx"
              :src="rexShopData.parameters.list[3].image"
              mode="scaleToFill"
              v-if="rexShopData.parameters.list[3]"
              @click="
                showSliderInfo(
                  rexShopData.parameters.list[3].linkType,
                  rexShopData.parameters.list[3].linkValue
                )
              "
              :lazy-load="true"
            />
          </up-col>
        </up-row>
      </up-col>
    </up-row>

    <u-toast ref="uToast" />
  </view>
</template>

<script setup lang="ts" name="rexShopImgWindow">
import { ref, defineProps } from "vue";
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

// 显示信息
const showSliderInfo = (type: number, val: string) => {
  if (!val) {
    return;
  }
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
.img-window-body {
  padding: 5rpx;
  .w100 {
    width: 100% !important;
  }
}
</style>
