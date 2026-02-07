<template>
  <div class="system-user-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="system-user-search">
        <el-row :gutter="15">
          <el-col :span="6">
            <el-input
              v-model="state.tableData.param.userName"
              size="default"
              placeholder="请输入用户名称"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="6">
            <el-button size="default" type="primary" class="ml10" @click="searchUser">
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
          v-auth="'AbpIdentity.Users.Create'"
          type="success"
          @click="onOpenAddUser('add')"
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
        <el-table-column prop="userName" label="账户名称" show-overflow-tooltip>
          <template #default="scope">
            {{ scope.row.userName }}
            <el-tag size="small" v-if="scope.row.userName === 'admin'"
              >&nbsp;系统&nbsp;</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column prop="name" label="用户昵称" show-overflow-tooltip>
        </el-table-column>
        <el-table-column
          prop="email"
          label="邮箱"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="phoneNumber"
          label="电话号码"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="isActive"
          label="是否激活"
          width="100"
          show-overflow-tooltip
        >
          <template #default="scope">
            <el-tag type="success" v-if="scope.row.isActive">已激活</el-tag>
            <el-tag type="info" v-else>未激活</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="180">
          <template #default="scope">
            <el-button
              v-auth="'AbpIdentity.Users.Update'"
              size="small"
              text
              type="info"
              @click="onResetPwd(scope.row)"
              >重置密码</el-button
            >
            <el-button
              v-auth="'AbpIdentity.Users.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditUser('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'AbpIdentity.Users.Delete'"
              :disabled="scope.row.userName === 'admin'"
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
    <user-dialog ref="userDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="systemUser">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useUserApi } from "/@/api/system/user/index";

// 引入组件
const UserDialog = defineAsyncComponent(
  () => import("/@/views/system/user/components/userDialog.vue")
);

// 引入用户 Api 请求接口
const userApi = useUserApi();

// 定义变量内容
const userDialogRef = ref();
const state = reactive<SysUserState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      userName: null,
      sorting: '"UserName"',
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
  userApi
    .getUserList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err) => {
      state.tableData.loading = false;
      console.error("查询用户列表出错：", err);
    });
};

// 搜索
const searchUser = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 打开新增用户弹窗
const onOpenAddUser = (type: string) => {
  userDialogRef.value.openDialog(type);
};

// 打开修改用户弹窗
const onOpenEditUser = (type: string, row: RowUserType) => {
  userDialogRef.value.openDialog(type, row);
};

// 重置密码
const onResetPwd = (row: RowUserType) => {
  ElMessageBox.confirm(
    `此操作会将账户名称：“${row.userName}”的密码进行重置，是否继续?`,
    "提示",
    {
      confirmButtonText: "是",
      cancelButtonText: "否",
      type: "warning",
    }
  ).then(() => {
    const initPwd = import.meta.env.VITE_USER_INIT_PWD;
    let uForm = _.cloneDeep(row) as any;
    uForm.password = initPwd;
    userApi
      .updateUser(row.id, uForm)
      .then((result) => {
        if (result) {
          getTableData();
          ElMessageBox.alert(
            `重置密码成功！新密码为：<strong class="init-pwd">${initPwd}</strong>。`,
            "提示",
            {
              dangerouslyUseHTMLString: true,
            }
          );
        }
      })
      .catch((err) => {
        console.error("提交出错：", err);
      });
  });
};

// 删除用户
const onRowDel = (row: RowUserType) => {
  ElMessageBox.confirm(`此操作将永久删除账户名称：“${row.userName}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    userApi
      .deleteUser(row.id)
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

<style lang="scss">
.init-pwd {
  color: green;
  background-color: azure;
  display: inline-block;
  padding: 2px 5px;
  margin-right: 10px;
}
</style>

<style scoped lang="scss">
.system-user-container {
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
