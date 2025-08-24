<template>
  <div class="article-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="article-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.title"
              size="default"
              placeholder="请输入文章标题"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.typeId"
              size="default"
              placeholder="文章分类"
            >
              <el-option
                v-for="articleType in articleTypeItems"
                :key="articleType.id"
                :label="articleType.name"
                :value="articleType.id"
              />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isPub"
              size="default"
              placeholder="是否发布"
            >
              <el-option label="是" :value="true" />
              <el-option label="否" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="6">
            <el-button size="default" type="primary" class="ml10" @click="searchArticle">
              <el-icon>
                <ele-Search />
              </el-icon>
              查询
            </el-button>
            <el-button size="default" class="ml10" @click="onReset">
              <el-icon>
                <ele-RefreshLeft />
              </el-icon>
              重置
            </el-button>
          </el-col>
          <el-col :span="9"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.Articles.Create'"
          type="success"
          @click="onOpenAddArticle('add')"
        >
          <el-icon>
            <ele-Plus />
          </el-icon>
          新增
        </el-button>
      </template>
      <el-table
        :data="state.tableData.data"
        v-loading="state.tableData.loading"
        style="width: 100%"
      >
        <el-table-column prop="sort" label="排序" width="60" />
        <el-table-column prop="title" label="标题" show-overflow-tooltip />
        <el-table-column
          prop="articleType"
          label="分类"
          show-overflow-tooltip
          width="100"
        >
          <template #default="scope">
            <span v-if="scope.row.articleType">{{ scope.row.articleType.name }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="coverImage"
          label="封面图"
          show-overflow-tooltip
          width="150"
        >
          <template #default="scope">
            <el-image
              class="img-logo"
              :src="scope.row.coverImage"
              :zoom-rate="1.2"
              fit="cover"
            >
              <template #error>
                <div class="image-slot">
                  <el-icon><icon-picture /></el-icon>
                </div>
              </template>
            </el-image>
          </template>
        </el-table-column>
        <el-table-column prop="isPub" label="是否发布" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-switch
              v-model="scope.row.isPub"
              inline-prompt
              active-text="是"
              inactive-text="否"
              size="default"
              @change="onIsPub($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column prop="pv" label="访问量" width="80" />
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.Articles.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditArticle('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.Articles.Delete'"
              size="small"
              text
              type="danger"
              @click="onRowDel(scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        @size-change="onHandleSizeChange"
        @current-change="onHandleCurrentChange"
        class="mt15"
        v-model:current-page="state.tableData.currentPage"
        background
        v-model:page-size="state.tableData.param.maxResultCount"
        layout="total, sizes, prev, pager, next, jumper"
        :total="state.tableData.total"
      >
      </el-pagination>
    </el-card>
    <article-dialog ref="articleDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" title="article">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { Picture as IconPicture } from "@element-plus/icons-vue";
import _ from "lodash";
import { useArticleApi } from "/@/api/operationManage/article/index";
import { useArticleTypeApi } from "/@/api/operationManage/articleType/index";

// 引入组件
const ArticleDialog = defineAsyncComponent(
  () => import("/@/views/operationManage/articles/components/articleDialog.vue")
);

// 引入文章 Api 请求接口
const articleApi = useArticleApi();
const articleTypeApi = useArticleTypeApi();

// 定义变量内容
const articleDialogRef = ref();
const articleTypeItems = ref([] as RowArticleTypeType[]);
const state = reactive<ArticleState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      title: null,
      isPub: null,
      typeId: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 获取文章分类
const getArticleTypeData = () => {
  articleTypeApi
    .getArticleTypeList({
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 1000,
    })
    .then((result) => {
      articleTypeItems.value = result.items;
    })
    .catch((err: any) => {
      console.error("查询文章分类列表出错：", err);
    });
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  articleApi
    .getArticleList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询文章列表出错：", err);
    });
};

// 搜索
const searchArticle = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.title = null;
  state.tableData.param.isPub = null;
  state.tableData.param.typeId = null;
};

// 打开新增文章弹窗
const onOpenAddArticle = (type: string) => {
  articleDialogRef.value.openDialog(type);
};

// 打开修改文章弹窗
const onOpenEditArticle = (type: string, row: RowArticleType) => {
  articleDialogRef.value.openDialog(type, row);
};

// 是否发布
const onIsPub = (val: boolean, article: RowArticleType) => {
  articleApi
    .updateArticleIsPub(article.id, val)
    .then((result) => {
      console.log("修改[" + article.title + "]是否发布，结果：", result);
    })
    .catch((err: any) => {
      article.isPub = !val;
      console.error("修改出错：", err);
    });
};

// 删除文章
const onRowDel = (row: RowArticleType) => {
  ElMessageBox.confirm(`此操作将永久删除标题：“${row.title}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    articleApi
      .deleteArticle(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
  });
};

// 分页改变
const onHandleSizeChange = (val: number) => {
  state.tableData.param.maxResultCount = val;
  getTableData();
};

// 分页改变
const onHandleCurrentChange = (val: number) => {
  state.tableData.currentPage = val;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  getTableData();
};

// 页面加载完时
onMounted(() => {
  getArticleTypeData();
  getTableData();
});
</script>

<style scoped lang="scss">
.article-container {
  :deep(.el-card__body) {
    display: flex;
    flex-direction: column;
    flex: 1;
    overflow: auto;
    .el-table {
      flex: 1;
    }
  }
}

.img-logo {
  width: 35px;
  height: 35px;
}

.img-logo .image-slot {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  background: var(--el-fill-color-light);
  color: var(--el-text-color-secondary);
  font-size: 30px;
}
.img-logo .image-slot .el-icon {
  font-size: 30px;
}
</style>
