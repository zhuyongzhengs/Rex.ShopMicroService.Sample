<template>
  <view class="article-list-body">
    <view class="article-list-tabs-box">
      <up-tabs
        lineColor="#2979ff"
        :list="articleTypeList"
        :current="currArticleType.index"
        :activeStyle="{ color: '#2979ff', fontSize: '26rpx' }"
        :inactiveStyle="{ fontSize: '26rpx' }"
        :itemStyle="{ height: '75rpx' }"
        @click="articleTypeHandle"
      ></up-tabs>
    </view>
    <template v-for="article in articleData.data" :key="article.id">
      <view class="article-list-box">
        <up-row @click="routeHelper.goArticleDetail(article.id)">
          <up-col span="3">
            <view class="article-image">
              <up-image
                width="164rpx"
                height="164rpx"
                radius="6rpx"
                :show-loading="true"
                :src="article.coverImage"
              />
            </view>
          </up-col>
          <up-col span="9">
            <view class="article-detail-item">
              <view class="article-title">{{ article.title }}</view>
              <view class="article-brief">{{ article.brief }}</view>
            </view>
          </up-col>
        </up-row>
      </view>
    </template>

    <view
      class="article-list-empty-data"
      v-show="!articleData.data || articleData.data.length < 1"
    >
      <u-empty mode="list" :text="`无${currArticleType.name}信息`" />
    </view>
    <view class="u-margin-top-10" v-show="articleData.data.length > 0">
      <u-loadmore nomore-text="" :status="articleData.loading" />
    </view>
  </view>
</template>

<script setup lang="ts">
import { computed, reactive } from "vue";
import { onLoad, onReachBottom } from "@dcloudio/uni-app";
import { http } from "@/utils/http";
import routeHelper from "@/utils/routeHelper";

// 定义变量
const articleClassifys = ref<ArticleClassifyType[]>();
const articleTypeList = reactive([{ name: "全部" }]);
const currArticleType = reactive({
  index: 0,
  name: "全部",
});
const articleData = reactive({
  data: [] as ArticleType[],
  total: 0,
  currentPage: 1,
  loading: "loadmore",
  showEmptyData: null,
  param: {
    title: "",
    typeId: "",
    isPub: true,
    sorting: "Sort ASC",
    skipCount: 0,
    maxResultCount: 10,
  },
});

// 是否还有更多数据
const isMoreData = computed(() => {
  return articleData.data.length < articleData.total;
});

// 验证更多数据
const checkMoreData = () => {
  if (isMoreData.value) articleData.loading = "loadmore";
  else articleData.loading = "nomore";
  if (articleData.data.length < 1) {
    articleData.showEmptyData = Object(true);
  } else {
    articleData.showEmptyData = null;
  }
};

// 触发文章分类状态
const articleTypeHandle = (item: any) => {
  articleData.showEmptyData = null;
  articleData.param.typeId = "";
  currArticleType.name = item.name;
  if (articleClassifys.value && articleClassifys.value.length > 0) {
    let aClassifyIndex = articleClassifys.value.findIndex((x) => x.name == item.name);
    if (aClassifyIndex >= 0) {
      let aClassify = articleClassifys.value[aClassifyIndex];
      articleData.param.typeId = aClassify.id;
    }
  }
  articleData.currentPage = 1;
  articleData.data = [];
  articleData.total = 0;
  loadArticles();
};

// 加载文章信息
const loadArticles = () => {
  articleData.param.skipCount =
    (articleData.currentPage - 1) * articleData.param.maxResultCount;
  uni.showLoading({
    title: "加载中，请稍等！",
  });
  http<TableResultType<ArticleType[]>>({
    url: "/api/good/common/article-page-list",
    method: "GET",
    data: articleData.param,
  }).then((result) => {
    articleData.total = result.totalCount;
    articleData.data.push(...result.items);
    checkMoreData();
    uni.hideLoading();
  });
};

// 查询文章分类
const loadArticleClassifys = () => {
  http<ArticleClassifyType[]>({
    url: "/api/good/common/article-type",
    method: "GET",
    data: {
      limit: 1000,
    },
  }).then((result) => {
    if (!result) return;
    articleClassifys.value = result;
    for (let i = 0; i < articleClassifys.value.length; i++) {
      const articleClassify = articleClassifys.value[i];
      articleTypeList.push({
        name: articleClassify.name,
      });
      if (articleData.param.typeId == articleClassify.id) {
        currArticleType.index = i + 1;
        currArticleType.name = articleClassify.name;
      }
    }
    articleTypeHandle(currArticleType);
  });
};

onLoad((evt: any) => {
  if (evt.typeId) articleData.param.typeId = evt.typeId;
  loadArticleClassifys();
});

// 加载更多
onReachBottom(() => {
  if (isMoreData.value) {
    articleData.currentPage++;
    loadArticles();
    console.log("触发了[加载更多]！");
  }
});
</script>

<style lang="scss" scoped>
.article-list-body {
  .article-list-tabs-box {
    background-color: white;
  }
  .article-list-box {
    margin: 20rpx;
    background-color: white;
    border-radius: 10rpx;
    padding: 15rpx;
    .article-detail-item {
      padding-left: 10rpx;
      .article-title {
        font-size: 30rpx;
        color: #2c313b;
        overflow: hidden;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
      }
      .article-brief {
        font-size: 26rpx;
        color: #909399;
        margin: 8rpx 0rpx;
        word-break: break-all;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
      }
    }
  }
  .article-list-empty-data {
    margin-top: 100rpx;
    text-align: center;
  }
}
</style>
