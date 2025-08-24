<template>
  <div class="member-user-grade-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="member-user-grade-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.title"
              size="default"
              placeholder="请输入标题"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button
              size="default"
              type="primary"
              class="ml10"
              @click="searchUserGrade"
            >
              <el-icon>
                <ele-Search />
              </el-icon>
              查询
            </el-button>
          </el-col>
          <el-col :span="12"></el-col>
        </el-row>
      </div>
    </el-card>
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'BaseService.UserGrades.Create'"
          type="success"
          @click="onOpenAddUserGrade('add')"
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
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="title" label="标题" show-overflow-tooltip />
        <el-table-column
          prop="isDefault"
          label="是否默认"
          show-overflow-tooltip
          width="100"
        >
          <template #default="scope">
            <el-tag type="success" v-if="scope.row.isDefault">开启</el-tag>
            <el-tag type="info" v-else>关闭</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'BaseService.UserGrades.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditUserGrade('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'BaseService.UserGrades.Delete'"
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
    <userGrade-dialog ref="userGradeDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="memberUserGrade">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useUserGradeApi } from "/@/api/system/userGrade/index";

// 引入组件
const UserGradeDialog = defineAsyncComponent(
  () => import("/@/views/member/userGrade/components/userGradeDialog.vue")
);

// 引入用户等级 Api 请求接口
const userGradeApi = useUserGradeApi();

// 定义变量内容
const userGradeDialogRef = ref();
const state = reactive<MemberUserGradeState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      title: "",
      sorting: "creationTime desc",
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
  userGradeApi
    .getUserGradeList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询用户等级列表出错：", err);
    });
};

// 搜索
const searchUserGrade = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 打开新增用户等级弹窗
const onOpenAddUserGrade = (type: string) => {
  userGradeDialogRef.value.openDialog(type);
};

// 打开修改用户等级弹窗
const onOpenEditUserGrade = (type: string, row: RowUserGradeType) => {
  userGradeDialogRef.value.openDialog(type, row);
};

// 删除用户等级
const onRowDel = (row: RowUserGradeType) => {
  ElMessageBox.confirm(`此操作将永久删除标题：“${row.title}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    userGradeApi
      .deleteUserGrade(row.id)
      .then((result) => {
        getTableData();
        ElMessage.success("删除成功");
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
.member-user-grade-container {
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
