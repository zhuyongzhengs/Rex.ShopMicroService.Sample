<template>
  <div class="user-ou-container">
    <div class="system-user-search mb15">
      <el-input
        v-model="state.tableData.param.filter"
        size="default"
        placeholder="请输入用户名称"
        style="max-width: 180px"
      >
      </el-input>
      <el-button size="default" type="primary" class="ml10" @click="searchOuUser('')">
        <el-icon>
          <ele-Search />
        </el-icon>
        查询
      </el-button>
      <el-button size="default" type="success" class="ml10" @click="onOpenUouAddUser">
        <el-icon>
          <ele-CirclePlus />
        </el-icon>
        添加用户
      </el-button>
      <el-button size="default" type="danger" class="ml10" @click="onDelUser(null)">
        <el-icon>
          <ele-Delete />
        </el-icon>
        批量删除
      </el-button>
    </div>
    <el-table
      ref="multipleUserOuTableRef"
      :data="state.tableData.data"
      v-loading="state.tableData.loading"
      style="width: 100%"
      height="365"
    >
      <el-table-column type="selection" width="55" />
      <el-table-column
        prop="userName"
        label="账户名称"
        show-overflow-tooltip
      ></el-table-column>
      <el-table-column
        prop="creationTime"
        label="创建时间"
        show-overflow-tooltip
        width="165"
      ></el-table-column>
      <el-table-column label="操作" width="100">
        <template #default="scope">
          <el-button size="small" text type="danger" @click="onDelUser(scope.row)"
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

    <user-select-ou-dialog
      ref="userSelectOuDialogRef"
      @selUserResult="selUserResultData"
    />
  </div>
</template>

<script setup lang="ts" name="userOrganizationUnit">
import { defineAsyncComponent, reactive, ref } from "vue";
import { ElMessageBox, ElMessage, ElLoading } from "element-plus";
import { useUserOrganizationUnitApi } from "/@/api/system/userOrganizationUnit/index";

// 引入[用户选择]组件
const UserSelectOuDialog = defineAsyncComponent(
  () => import("/@/views/system/organizationUnit/components/selectUserOu.vue")
);

// 引入[用户]组织单元 Api 请求接口
const userOrganizationUnitApi = useUserOrganizationUnitApi();

const userSelectOuDialogRef = ref();
const multipleUserOuTableRef = ref();
const state = reactive<SysUserOrganizationUnitState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      filter: "",
      organizationUnitId: null,
      sorting: "userId",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 搜索
const searchOuUser = (ouId: string = "") => {
  if (ouId) {
    state.tableData.param.organizationUnitId = ouId;
  }
  state.tableData.currentPage = 1;
  getTableData();
};

// 获取表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  userOrganizationUnitApi
    .getUserOrganizationUnitList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
      console.log("查询用户组织单元", state.tableData.data);
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询用户列表出错：", err);
    });
};

// 打开新增用户弹窗
const onOpenUouAddUser = () => {
  userSelectOuDialogRef.value.openDialog(
    state.tableData.param.organizationUnitId,
    "选择用户"
  );
};

// 选择用户结果
const selUserResultData = (userData: RowUserType[]) => {
  let uOuData = [];
  for (let index = 0; index < userData.length; index++) {
    uOuData.push({
      userId: userData[index].id,
      organizationUnitId: state.tableData.param.organizationUnitId,
    });
  }
  const addUserLoading = ElLoading.service({
    lock: true,
    text: "添加中，请稍等！",
    background: "rgba(0, 0, 0, 0.1)",
  });
  userOrganizationUnitApi
    .addUserOrganizationUnit(uOuData)
    .then((result) => {
      if (result) {
        searchOuUser();
        addUserLoading.close();
      }
    })
    .catch((err: any) => {
      addUserLoading.close();
      console.error("添加[用户]组织单元出错：", err);
    });
};

// 删除用户
const onDelUser = (row: any) => {
  let ouId = state.tableData.param.organizationUnitId;
  const selUouRows = multipleUserOuTableRef.value.getSelectionRows() as RowUserOrganizationUnitType[];
  let confirmMsg = "";
  let userIds: string[] = [];
  if (row) {
    confirmMsg = `此操作将删除账户名称：“${row.userName}”，是否继续?`;
    userIds.push(row.userId);
  } else {
    const uIds = selUouRows.map((p) => {
      return p.userId;
    });
    userIds.push(...uIds);
    confirmMsg = `您选择了 ${userIds.length} 条数据，确定要删除吗？`;
  }
  if (userIds.length < 1) {
    ElMessage.warning("您尚未选择要删除的数据！");
    return;
  }
  ElMessageBox.confirm(confirmMsg, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    userOrganizationUnitApi
      .deleteUserOrganizationUnit(ouId, userIds)
      .then((result) => {
        if (result) {
          searchOuUser();
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

// 暴露变量
defineExpose({
  searchOuUser,
});
</script>

<style scoped lang="scss"></style>
