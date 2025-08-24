<template>
  <div class="member-sys-user-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="member-sys-user-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入昵称"
              maxlength="20"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.phoneNumber"
              size="default"
              class="search-input mr15"
              placeholder="请输入手机号码"
              maxlength="20"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="2">
            <el-select
              v-model="state.tableData.param.gender"
              size="default"
              placeholder="性别"
            >
              <el-option label="男" :value="1" />
              <el-option label="女" :value="2" />
            </el-select>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isActive"
              size="default"
              placeholder="是否激活"
            >
              <el-option label="是" :value="true" />
              <el-option label="否" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="11">
            <el-button size="default" type="primary" class="ml10" @click="searchSysUser">
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
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'BaseService.SysUsers.Create'"
          type="success"
          @click="onOpenAddSysUser('add')"
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
        <el-table-column fixed type="index" label="序号" width="60" />
        <el-table-column prop="avatar" label="头像" show-overflow-tooltip width="100">
          <template #default="scope">
            <el-avatar :size="50" :src="scope.row.avatar" />
          </template>
        </el-table-column>
        <el-table-column prop="userName" label="账号" show-overflow-tooltip width="240" />
        <el-table-column prop="name" label="昵称" show-overflow-tooltip width="240" />
        <el-table-column
          prop="phoneNumber"
          label="手机号码"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column prop="email" label="邮箱" show-overflow-tooltip width="200" />
        <el-table-column prop="gender" label="性别" show-overflow-tooltip width="90">
          <template #default="scope">
            <span v-if="scope.row.gender == 1">男</span>
            <span v-if="scope.row.gender == 2">女</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="parentId"
          label="第三方账号"
          show-overflow-tooltip
          width="150"
        />
        <el-table-column prop="balance" label="余额" show-overflow-tooltip width="150" />
        <el-table-column prop="point" label="积分" show-overflow-tooltip width="150" />
        <el-table-column
          prop="gradeId"
          label="用户等级"
          show-overflow-tooltip
          width="170"
        >
          <template #default="scope">
            <el-tag v-if="scope.row.gradeId" class="ml-2" type="info">{{
              scope.row.grade?.title
            }}</el-tag>
          </template>
        </el-table-column>
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
        <el-table-column
          prop="creationTime"
          label="出生日期"
          width="170"
          show-overflow-tooltip
        >
          <template #default="scope">
            <span>{{ scope.row.birthDate }}</span>
          </template>
        </el-table-column>
        <el-table-column
          prop="creationTime"
          label="创建日期"
          width="170"
          show-overflow-tooltip
        >
          <template #default="scope">
            <span>{{ scope.row.creationTime }}</span>
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'BaseService.SysUsers.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditSysUser('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'BaseService.SysUsers.Delete'"
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
    <sys-user-dialog ref="sysUserDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="memberSysUser">
import { reactive, onMounted, defineAsyncComponent, ref } from "vue";
import _ from "lodash";
import { useSysUserApi } from "/@/api/system/sysUser/index";
import { ElMessage, ElMessageBox } from "element-plus";

// 引入组件
const SysUserDialog = defineAsyncComponent(
  () => import("/@/views/member/sysUser/components/sysUserDialog.vue")
);

// 引入[注册]用户 Api 请求接口
const sysUserApi = useSysUserApi();

// 定义变量内容
const sysUserDialogRef = ref();
const state = reactive<MemberSysUserState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      userName: null,
      phoneNumber: null,
      gender: null,
      gradeId: null,
      isActive: null,
      beginDate: null,
      endDate: null,
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
  sysUserApi
    .getSysUserList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询[注册]用户列表出错：", err);
    });
};

// 搜索
const searchSysUser = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.userName = null;
  state.tableData.param.phoneNumber = null;
  state.tableData.param.gender = null;
  state.tableData.param.gradeId = null;
  state.tableData.param.isActive = null;
  state.tableData.param.beginDate = null;
  state.tableData.param.endDate = null;
};

// 打开新增用户弹窗
const onOpenAddSysUser = (type: string) => {
  sysUserDialogRef.value.openDialog(type);
};

// 打开修改用户弹窗
const onOpenEditSysUser = (type: string, row: RowSysUserType) => {
  sysUserDialogRef.value.openDialog(type, row);
};

// 删除用户
const onRowDel = (row: RowUserType) => {
  ElMessageBox.confirm(`此操作将永久删除账户名称：“${row.userName}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    sysUserApi
      .deleteSysUser(row.id)
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
.member-sys-user-container {
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

.detail-item {
  word-wrap: break-word;
  word-break: break-all;
}

.u-avatar {
  text-align: center;
}
</style>
