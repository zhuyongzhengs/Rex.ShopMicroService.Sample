<template>
  <view class="notice-body" v-if="noticeItem.list.length > 0">
    <u-notice-bar
      linkType="navigateTo"
      :color="noticeItem.color"
      :bgColor="noticeItem.bgColor"
      :direction="noticeItem.list.length > 1 ? 'column' : 'row'"
      :text="noticeItem.list.length > 1 ? noticeItem.list : noticeItem.list[0]"
      :duration="4500"
      :style="'border-radius: ' + radius"
      @click="onNoticeHandle"
    />
  </view>
</template>

<script setup lang="ts" name="rexShopNotice">
import { ref, onMounted, defineProps } from "vue";

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

// 公告项
const noticeItem = ref({
  color: "#f9ae3d", // 文字颜色
  bgColor: "#fdf6ec", // 背景颜色
  list: [] as string[],
  datas: [] as any,
});

// 加载公告信息
const loadNoticeItems = () => {
  var noticeParameter = props.rexShopData.parameters;
  noticeItem.value.color = noticeParameter.color;
  noticeItem.value.bgColor = noticeParameter.bgColor;
  for (var i = 0; i < noticeParameter.list.length; i++) {
    let data = {
      title: noticeParameter.list[i].title,
      url: "/pages/notices/detail?id=" + noticeParameter.list[i].id,
    };
    noticeItem.value.list.push(noticeParameter.list[i].title);
    noticeItem.value.datas.push(data);
  }
};

// 页面加载完时
onMounted(() => {
  loadNoticeItems();
});

// 点击[公告]
const onNoticeHandle = (index: number) => {
  if (!index) index = 0;
  let url = noticeItem.value.datas[index].url;
  uni.navigateTo({
    url,
  });
};
</script>
<style scoped lang="scss">
// ...
</style>
