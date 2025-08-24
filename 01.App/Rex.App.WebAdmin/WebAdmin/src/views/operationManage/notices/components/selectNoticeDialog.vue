<template>
  <div class="notice-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="900px"
    >
      <el-card shadow="never" class="mb15">
        <div class="notice-search">
          <el-row :gutter="15">
            <el-col :span="6">
              <el-input
                v-model="state.tableData.param.title"
                size="default"
                placeholder="请输入公告标题"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="6">
              <el-button size="default" type="primary" class="ml10" @click="searchNotice">
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
      <el-card shadow="never" class="layout-padding-auto">
        <el-table
          ref="multipleNoticeTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          height="300"
          :highlight-current-row="props.selectionType == 'single'"
          @current-change="onSelectedCurrChange"
        >
          <el-table-column
            v-if="props.selectionType == 'selection'"
            fixed
            type="selection"
            width="55"
          />
          <el-table-column prop="sort" label="排序" width="60" />
          <el-table-column prop="title" label="公告标题" show-overflow-tooltip />
          <el-table-column prop="contentBody" label="公告内容">
            <template #default="scope">
              <label>{{ formatContentBody(scope.row.contentBody) }}</label>
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
      <template #footer>
        <span class="dialog-footer">
          <el-button size="default" @click="onCancel">取 消</el-button>
          <el-button type="primary" size="default" @click="onConfirm">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" title="notice">
import { reactive, onMounted, ref, nextTick } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useNoticeApi } from "/@/api/operationManage/notice/index";

// 引入公告 Api 请求接口
const noticeApi = useNoticeApi();

const emit = defineEmits(["selNoticeResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 定义变量内容
const multipleNoticeTableRef = ref();
const currentNotice = ref({} as RowNoticeType);
const state = reactive<NoticeState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      title: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择公告",
});

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  noticeApi
    .getNoticeList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询公告列表出错：", err);
    });
};

// 搜索
const searchNotice = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.title = null;
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

// 格式化公告内容
const formatContentBody = (html: string) => {
  var tempElement = document.createElement("div");
  tempElement.innerHTML = html;
  return tempElement.textContent || tempElement.innerText || "";
};

// 打开弹窗
const openDialog = (title: string = "选择公告") => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询公告
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleNoticeTableRef.value.getSelectionRows() as RowNoticeType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择公告！");
    return;
  }
  if (!currentNotice.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择公告！");
    return;
  }
  emit(
    "selNoticeResult",
    props.selectionType == "single" ? currentNotice.value : selRows
  );
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

// 选择(单选)当前
const onSelectedCurrChange = (notice: RowNoticeType) => {
  currentNotice.value = notice;
};

// 页面加载完时
onMounted(() => {
  getTableData();
});
</script>

<style scoped lang="scss">
.notice-container {
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
