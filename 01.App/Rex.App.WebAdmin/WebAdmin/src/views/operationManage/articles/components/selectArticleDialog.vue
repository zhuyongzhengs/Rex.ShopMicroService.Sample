<template>
  <div class="select-article-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="900px"
    >
      <el-card shadow="never" class="mb15">
        <div class="select-article-search">
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
            <el-col :span="4">
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
            <el-col :span="4">
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
              <el-button
                size="default"
                type="primary"
                class="ml10"
                @click="searchArticle"
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
          </el-row>
        </div>
      </el-card>
      <el-card shadow="never" class="layout-padding-auto">
        <el-table
          ref="multipleArticleTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          :highlight-current-row="props.selectionType == 'single'"
          @current-change="onSelectedCurrChange"
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
          <el-table-column
            prop="isPub"
            label="是否发布"
            show-overflow-tooltip
            width="100"
          >
            <template #default="scope">
              <el-switch
                v-model="scope.row.isPub"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
                :disabled="true"
              />
            </template>
          </el-table-column>
          <el-table-column prop="pv" label="访问量" width="80" />
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

      <template #footer>
        <span class="dialog-footer">
          <el-button size="default" @click="onCancel">取 消</el-button>
          <el-button type="primary" size="default" @click="onConfirm">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" title="selectArticle">
import { reactive, onMounted, ref, nextTick, defineExpose } from "vue";
import { ElMessage } from "element-plus";
import { useArticleApi } from "/@/api/operationManage/article/index";
import { useArticleTypeApi } from "/@/api/operationManage/articleType/index";

// 引入文章 Api 请求接口
const articleApi = useArticleApi();
const articleTypeApi = useArticleTypeApi();

const emit = defineEmits(["selResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 定义变量内容
const articleTypeItems = ref([] as RowArticleTypeType[]);
const currentArticle = ref({} as RowArticleType);
const multipleArticleTableRef = ref();
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

// 选择(单选)当前
const onSelectedCurrChange = (article: RowArticleType) => {
  currentArticle.value = article;
};

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择文章",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询文章
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleArticleTableRef.value.getSelectionRows() as RowArticleType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择文章！");
    return;
  }
  if (!currentArticle.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择文章！");
    return;
  }
  emit("selResult", props.selectionType == "single" ? currentArticle.value : selRows);
  closeDialog();
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  getArticleTypeData();
  getTableData();
});
</script>

<style scoped lang="scss">
.select-article-container {
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
