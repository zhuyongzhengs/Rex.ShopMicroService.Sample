<template>
  <div class="select-form-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="900px"
    >
      <el-card shadow="never" class="mb15">
        <div class="select-form-search">
          <el-row :gutter="15">
            <el-col :span="6">
              <el-input
                v-model="state.tableData.param.name"
                size="default"
                placeholder="请输入表单名称"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="4">
              <el-select
                v-model="state.tableData.param.type"
                size="default"
                placeholder="表单类型"
              >
                <el-option
                  v-for="formType in formTypes"
                  :key="formType.value"
                  :label="formType.description"
                  :value="formType.value"
                />
              </el-select>
            </el-col>
            <el-col :span="6">
              <el-button size="default" type="primary" class="ml10" @click="searchForm">
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
          ref="multipleFormTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          :highlight-current-row="props.selectionType == 'single'"
          @current-change="onSelectedCurrChange"
        >
          <el-table-column prop="sort" label="排序" width="60" />
          <el-table-column prop="name" label="表单名称" show-overflow-tooltip />
          <el-table-column prop="description" label="表单描述" show-overflow-tooltip />
          <el-table-column prop="type" label="表单类型" show-overflow-tooltip width="100">
            <template #default="scope">
              <span v-if="scope.row.type">{{ showFormType(scope.row.type) }}</span>
            </template>
          </el-table-column>
          <el-table-column
            prop="isLogin"
            label="需要登录"
            show-overflow-tooltip
            width="100"
          >
            <template #default="scope">
              <el-switch
                v-model="scope.row.isLogin"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
                :disabled="true"
              />
            </template>
          </el-table-column>
          <el-table-column prop="endDateTime" label="提交截止时间" width="165" />
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

<script setup lang="ts" name="form">
import { reactive, onMounted, ref, nextTick } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useFormApi } from "/@/api/operationManage/form/index";

const emit = defineEmits(["selResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 引入表单 Api 请求接口
const formApi = useFormApi();

// 定义变量内容
const formDialogRef = ref();
const formTypes = ref([] as EnumValueType[]);
const currentForm = ref({} as RowFormType);
const multipleFormTableRef = ref();
const state = reactive<FormState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      type: null,
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
  formApi
    .getFormList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询表单列表出错：", err);
    });
};

// 搜索
const searchForm = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.type = null;
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

// 获取表单类型
const getFormTypes = () => {
  formApi
    .getFormTypes()
    .then((result) => {
      if (result) {
        formTypes.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取表单类型出错：", err);
    });
};

// 显示表单类型
const showFormType = (value: number) => {
  for (let i = 0; i < formTypes.value.length; i++) {
    const formType = formTypes.value[i];
    if (formType.value == String(value)) {
      return formType.description;
    }
  }
  return null;
};

// 选择(单选)当前
const onSelectedCurrChange = (form: RowFormType) => {
  currentForm.value = form;
};

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择表单",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询表单
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleFormTableRef.value.getSelectionRows() as RowFormType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择表单！");
    return;
  }
  if (!currentForm.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择表单！");
    return;
  }
  emit("selResult", props.selectionType == "single" ? currentForm.value : selRows);
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
  getFormTypes();
  getTableData();
});
</script>

<style scoped lang="scss">
.select-form-container {
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
