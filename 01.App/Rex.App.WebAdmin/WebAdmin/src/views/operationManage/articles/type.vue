<template>
  <div class="article-type-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="article-type-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入分类名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchArticleType"
            >
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
          v-auth="'GoodService.ArticleTypes.Create'"
          type="success"
          @click="onOpenAddArticleType('add')"
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
        <el-table-column prop="name" label="分类名称" show-overflow-tooltip />
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.ArticleTypes.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditArticleType('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.ArticleTypes.Delete'"
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
    <article-type-dialog ref="articleTypeDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="articleType">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useArticleTypeApi } from "/@/api/operationManage/articleType/index";

// 引入组件
const ArticleTypeDialog = defineAsyncComponent(
  () => import("/@/views/operationManage/articles/components/articleTypeDialog.vue")
);

// 引入文章分类 Api 请求接口
const articleTypeApi = useArticleTypeApi();

// 定义变量内容
const articleTypeDialogRef = ref();
const state = reactive<ArticleTypeState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  articleTypeApi
    .getArticleTypeList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询文章分类列表出错：", err);
    });
};

// 搜索
const searchArticleType = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
};

// 打开新增文章分类弹窗
const onOpenAddArticleType = (type: string) => {
  articleTypeDialogRef.value.openDialog(type);
};

// 打开修改文章分类弹窗
const onOpenEditArticleType = (type: string, row: RowArticleTypeType) => {
  articleTypeDialogRef.value.openDialog(type, row);
};

// 删除文章分类
const onRowDel = (row: RowArticleTypeType) => {
  ElMessageBox.confirm(`此操作将永久删除分类名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    articleTypeApi
      .deleteArticleType(row.id)
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
  getTableData();
});
</script>

<style scoped lang="scss">
.article-type-container {
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
</style>
