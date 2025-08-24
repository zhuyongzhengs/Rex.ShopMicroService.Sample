<template>
  <div class="system-role-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="system-role-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.filter"
              size="default"
              placeholder="请输入角色名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button size="default" type="primary" class="ml10" @click="searchRole">
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
          v-auth="'AbpIdentity.Roles.Create'"
          size="default"
          type="success"
          @click="onOpenAddRole('add')"
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
        <el-table-column prop="name" label="角色名称" show-overflow-tooltip>
          <template #default="scope">
            {{ scope.row.name }}
            <el-tag size="small" v-if="scope.row.name === 'admin'"
              >&nbsp;系统&nbsp;</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column prop="status" label="是否默认" width="100" show-overflow-tooltip>
          <template #default="scope">
            <el-tag type="success" v-if="scope.row.isDefault"><b>是</b></el-tag>
            <el-tag type="info" v-else>否</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="150">
          <template #default="scope">
            <el-button
              v-auth="'AbpIdentity.Roles.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditRole('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'AbpIdentity.Roles.Delete'"
              :disabled="scope.row.name === 'admin'"
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
    <role-dialog ref="roleDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="systemRole">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import { useRoleApi } from "/@/api/system/role/index";

// 引入组件
const RoleDialog = defineAsyncComponent(
  () => import("/@/views/system/role/components/roleDialog.vue")
);

// 引入角色 Api 请求接口
const roleApi = useRoleApi();

// 定义变量内容
const roleDialogRef = ref();
const state = reactive<SysRoleState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      filter: "",
      sorting: "name",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 搜索
const searchRole = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  roleApi
    .getRoleList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err) => {
      state.tableData.loading = false;
      console.error("查询角色列表出错：", err);
    });
};

// 打开新增角色弹窗
const onOpenAddRole = (type: string) => {
  roleDialogRef.value.openDialog(type);
};

// 打开修改角色弹窗
const onOpenEditRole = (type: string, row: Object) => {
  roleDialogRef.value.openDialog(type, row);
};

// 删除角色
const onRowDel = (row: RowRoleType) => {
  ElMessageBox.confirm(`此操作将永久删除角色名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    roleApi
      .deleteRole(row.id)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessage.success("删除成功");
        }
      })
      .catch((err) => {
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
.system-role-container {
  .system-role-padding {
    padding: 15px;
    .el-table {
      flex: 1;
    }
  }
}
</style>
