<template>
  <div class="system-user-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="1000px"
    >
      <el-card shadow="never" class="layout-padding-auto">
        <div class="system-user-search mb15">
          <el-row :gutter="15">
            <el-col :span="6">
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
            <el-col :span="3">
              <el-select
                v-model="state.tableData.param.gender"
                size="default"
                placeholder="性别"
              >
                <el-option label="男" :value="1" />
                <el-option label="女" :value="2" />
              </el-select>
            </el-col>
            <el-col :span="11">
              <el-button
                size="default"
                type="primary"
                class="ml10"
                @click="searchSysUser"
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
        <el-table
          ref="multipleUserTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          height="320"
          style="width: 100%"
          :highlight-current-row="props.selectionType == 'single'"
          @current-change="onSelectedCurrChange"
        >
          <el-table-column
            v-if="props.selectionType == 'selection'"
            fixed
            type="selection"
            width="55"
          />
          <el-table-column prop="avatar" label="头像" show-overflow-tooltip width="100">
            <template #default="scope">
              <el-avatar :size="50" :src="scope.row.avatar" />
            </template>
          </el-table-column>
          <el-table-column
            prop="userName"
            label="账号"
            show-overflow-tooltip
            width="240"
          />
          <el-table-column prop="name" label="昵称" show-overflow-tooltip width="240" />
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
          <el-table-column
            prop="phoneNumber"
            label="手机号码"
            show-overflow-tooltip
            width="150"
          />
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

<script setup lang="ts" name="memberSysUser">
import { reactive, ref, nextTick } from "vue";
import { ElMessage, ElTable } from "element-plus";
import { useSysUserApi } from "/@/api/system/sysUser/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["selUserResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 引入用户 Api 请求接口
const sysUserApi = useSysUserApi();

// 定义变量内容
const multipleUserTableRef = ref();
const currentUser = ref<RowSysUserType>();
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
      isActive: true,
      beginDate: null,
      endDate: null,
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择用户",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询用户
    searchSysUser();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleUserTableRef.value.getSelectionRows() as RowUserType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择用户！");
    return;
  }
  if (!currentUser.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择用户！");
    return;
  }
  emit("selUserResult", props.selectionType == "single" ? currentUser.value : selRows);
  closeDialog();
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 重置
const onReset = () => {
  state.tableData.param.userName = null;
  state.tableData.param.phoneNumber = null;
  state.tableData.param.gender = null;
  state.tableData.param.gradeId = null;
  state.tableData.param.beginDate = null;
  state.tableData.param.endDate = null;
};

// 搜索
const searchSysUser = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 选择(单选)当前
const onSelectedCurrChange = (user: RowSysUserType) => {
  currentUser.value = user;
};

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
  openDialog,
});
</script>

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
