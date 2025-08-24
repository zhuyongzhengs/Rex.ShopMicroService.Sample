<template>
  <div class="form-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="form-search">
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
          <el-col :span="3">
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
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'GoodService.Forms.Create'"
          type="success"
          @click="onOpenAddForm('add')"
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
              @change="onIsLogin($event, scope.row)"
            />
          </template>
        </el-table-column>
        <el-table-column prop="endDateTime" label="提交截止时间" width="165" />
        <el-table-column prop="creationTime" label="创建时间" width="165" />
        <el-table-column prop="lastModificationTime" label="更新时间" width="165" />
        <el-table-column label="操作" width="100">
          <template #default="scope">
            <el-button
              v-auth="'GoodService.Forms.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditForm('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'GoodService.Forms.Delete'"
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
    <form-dialog ref="formDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="form">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { useFormApi } from "/@/api/operationManage/form/index";

// 引入组件
const FormDialog = defineAsyncComponent(
  () => import("/@/views/operationManage/customForms/components/formDialog.vue")
);

// 引入表单 Api 请求接口
const formApi = useFormApi();

// 定义变量内容
const formDialogRef = ref();
const formTypes = ref([] as EnumValueType[]);
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

// 打开新增表单弹窗
const onOpenAddForm = (type: string) => {
  formDialogRef.value.openDialog(type);
};

// 打开修改表单弹窗
const onOpenEditForm = (type: string, row: RowFormType) => {
  formDialogRef.value.openDialog(type, row);
};

// 删除表单
const onRowDel = (row: RowFormType) => {
  ElMessageBox.confirm(`此操作将永久删除表单名称：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    formApi
      .deleteForm(row.id)
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

// 需要登录
const onIsLogin = (val: boolean, form: RowFormType) => {
  formApi
    .updateFormIsLogin(form.id, val)
    .then((result) => {
      console.log("修改[" + form.name + "]需要登录，结果：", result);
    })
    .catch((err: any) => {
      form.isLogin = !val;
      console.error("修改出错：", err);
    });
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

// 页面加载完时
onMounted(() => {
  getFormTypes();
  getTableData();
});
</script>

<style scoped lang="scss">
.form-container {
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
