<template>
  <view class="agreement-body">
    <view class="setting-content-box">
      <up-parse :content="content"></up-parse>
    </view>
    <u-toast ref="uToastRef" />
  </view>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { onLoad } from "@dcloudio/uni-app";
import { http } from "@/utils/http";

// 定义变量
const uToastRef = ref();
const content = ref("");
const barTitle = ref("");

// 获取内容信息
const getContent = (id: string) => {
  http<ArticleType>({
    method: "GET",
    url: `/api/good/common/${id}/article-by-id`,
    data: {},
  })
    .then((result) => {
      if (!result) return;
      content.value = result.contentBody;
      if (!barTitle.value) {
        uni.setNavigationBarTitle({
          title: result.title,
        });
      }
    })
    .catch((err: any) => {
      console.error("获取内容信息出错：", err);
    });
};

onLoad((e: any) => {
  if (!e.id) {
    uni.navigateBack();
    return;
  }
  if (e.title) {
    barTitle.value = e.title;
    uni.setNavigationBarTitle({
      title: e.title,
    });
  }
  getContent(e.id);
});
</script>
<style lang="scss" scoped>
.agreement-body {
  background-color: white;

  .setting-content-box {
    padding: 10rpx;
  }
}
</style>
