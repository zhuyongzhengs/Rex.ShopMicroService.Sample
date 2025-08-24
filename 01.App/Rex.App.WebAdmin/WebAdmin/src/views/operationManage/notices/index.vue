<template>
  <div class="notice-container layout-padding">
    <el-card shadow="hover" class="mb15">
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
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.Notices.Create'"
          type="success"
          @click="onOpenAddNotice('add')"
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
        <el-table-column
          prop="title"
          label="公告标题"
          show-overflow-tooltip
          width="350"
        />
        <el-table-column prop="contentBody" label="公告内容">
          <template #default="scope">
            <label>{{ formatContentBody(scope.row.contentBody) }}</label>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.Notices.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditNotice('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.Notices.Delete'"
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
    <notice-dialog ref="noticeDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" title="notice">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useNoticeApi } from "/@/api/operationManage/notice/index";

// 引入组件
const NoticeDialog = defineAsyncComponent(
  () => import("/@/views/operationManage/notices/components/noticeDialog.vue")
);

// 引入公告 Api 请求接口
const noticeApi = useNoticeApi();

// 定义变量内容
const noticeDialogRef = ref();
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

// 打开新增公告弹窗
const onOpenAddNotice = (type: string) => {
  noticeDialogRef.value.openDialog(type);
};

// 打开修改公告弹窗
const onOpenEditNotice = (type: string, row: RowNoticeType) => {
  noticeDialogRef.value.openDialog(type, row);
};

// 删除公告
const onRowDel = (row: RowNoticeType) => {
  ElMessageBox.confirm(`此操作将永久删除公告标题：“${row.title}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    noticeApi
      .deleteNotice(row.id)
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

// 格式化公告内容
const formatContentBody = (html: string) => {
  var tempElement = document.createElement("div");
  tempElement.innerHTML = html;
  return tempElement.textContent || tempElement.innerText || "";
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
