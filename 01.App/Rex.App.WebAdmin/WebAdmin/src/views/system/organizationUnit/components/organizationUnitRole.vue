<template>
  <div class="ou-role-container">
    <div class="system-role-search mb15">
      <el-input
        v-model="state.tableData.param.filter"
        size="default"
        placeholder="请输入角色名称"
        style="max-width: 180px"
      >
      </el-input>
      <el-button size="default" type="primary" class="ml10" @click="searchOuRole('')">
        <el-icon>
          <ele-Search />
        </el-icon>
        查询
      </el-button>
      <el-button size="default" type="success" class="ml10" @click="onOpenOuAddRole">
        <el-icon>
          <ele-CirclePlus />
        </el-icon>
        添加角色
      </el-button>
      <el-button size="default" type="danger" class="ml10" @click="onDelRole(null)">
        <el-icon>
          <ele-Delete />
        </el-icon>
        批量删除
      </el-button>
    </div>
    <el-table
      ref="multipleRoleOuTableRef"
      :data="state.tableData.data"
      v-loading="state.tableData.loading"
      style="width: 100%"
      height="365"
    >
      <el-table-column type="selection" width="55" />
      <el-table-column
        prop="roleName"
        label="角色名称"
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
          <el-button size="small" text type="danger" @click="onDelRole(scope.row)"
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

    <select-ou-role-dialog
      ref="selectOuRoleDialogRef"
      @selRoleResult="selRoleResultData"
    />
  </div>
</template>

<script setup lang="ts" name="organizationUnitRole">
import { defineAsyncComponent, reactive, ref } from "vue";
import { ElMessageBox, ElMessage, ElLoading } from "element-plus";
import { useOrganizationUnitRoleApi } from "/@/api/system/organizationUnitRole/index";

// 引入[角色选择]组件
const SelectOuRoleDialog = defineAsyncComponent(
  () => import("/@/views/system/organizationUnit/components/selectOuRole.vue")
);

// 引入[角色]组织单元 Api 请求接口
const organizationUnitRoleApi = useOrganizationUnitRoleApi();

const selectOuRoleDialogRef = ref();
const multipleRoleOuTableRef = ref();
const state = reactive<SysOrganizationUnitRoleState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      filter: "",
      organizationUnitId: null,
      sorting: "roleId",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 搜索
const searchOuRole = (ouId: string = "") => {
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
  organizationUnitRoleApi
    .getOrganizationUnitRoleList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
      console.log("查询角色组织单元", state.tableData.data);
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询角色列表出错：", err);
    });
};

// 打开新增角色弹窗
const onOpenOuAddRole = () => {
  selectOuRoleDialogRef.value.openDialog(
    state.tableData.param.organizationUnitId,
    "选择角色"
  );
};

// 选择角色结果
const selRoleResultData = (roleData: RowRoleType[]) => {
  let oUrData = [];
  for (let index = 0; index < roleData.length; index++) {
    oUrData.push({
      roleId: roleData[index].id,
      organizationUnitId: state.tableData.param.organizationUnitId,
    });
  }
  const addRoleLoading = ElLoading.service({
    lock: true,
    text: "添加中，请稍等！",
    background: "rgba(0, 0, 0, 0.1)",
  });
  organizationUnitRoleApi
    .addOrganizationUnitRole(oUrData)
    .then((result) => {
      if (result) {
        searchOuRole();
        addRoleLoading.close();
      }
    })
    .catch((err: any) => {
      addRoleLoading.close();
      console.error("添加[角色]组织单元出错：", err);
    });
};

// 删除角色
const onDelRole = (row: any) => {
  let ouId = state.tableData.param.organizationUnitId;
  const selUouRows = multipleRoleOuTableRef.value.getSelectionRows() as RowOrganizationUnitRoleType[];
  let confirmMsg = "";
  let roleIds: string[] = [];
  if (row) {
    confirmMsg = `此操作将删除角色名称：“${row.roleName}”，是否继续?`;
    roleIds.push(row.roleId);
  } else {
    const uIds = selUouRows.map((p) => {
      return p.roleId;
    });
    roleIds.push(...uIds);
    confirmMsg = `您选择了 ${roleIds.length} 条数据，确定要删除吗？`;
  }
  if (roleIds.length < 1) {
    ElMessage.warning("您尚未选择要删除的数据！");
    return;
  }
  ElMessageBox.confirm(confirmMsg, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    organizationUnitRoleApi
      .deleteOrganizationUnitRole(ouId, roleIds)
      .then((result) => {
        if (result) {
          searchOuRole();
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

// 暴露变量
defineExpose({
  searchOuRole,
});
</script>

<style scoped lang="scss"></style>
