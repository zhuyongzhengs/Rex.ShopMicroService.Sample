<template>
  <div class="good-param-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="850px"
    >
      <el-card shadow="never" class="mb15">
        <div class="good-param-search">
          <el-row :gutter="15">
            <el-col :span="6">
              <el-input
                v-model="state.tableData.param.name"
                size="default"
                placeholder="请输入参数名称"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="4">
              <el-select
                v-model="state.tableData.param.type"
                size="default"
                placeholder="参数类型"
              >
                <el-option
                  v-for="(pType, index) in paramTypes"
                  :key="index"
                  :label="pType.description"
                  :value="pType.title"
                />
              </el-select>
            </el-col>
            <el-col :span="8">
              <el-button
                size="default"
                type="primary"
                class="ml10"
                @click="searchGoodParam"
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
      </el-card>
      <el-card shadow="never" class="layout-padding-auto">
        <el-table
          ref="multipleParamTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          max-height="300"
          style="width: 100%"
        >
          <el-table-column type="selection" width="55" />
          <el-table-column
            prop="name"
            label="参数名称"
            width="200"
            show-overflow-tooltip
          />
          <el-table-column prop="value" label="参数值" show-overflow-tooltip>
            <template #default="scope">
              <el-tag
                class="mr10"
                size="default"
                v-for="(val, index) in scope.row.values"
                :key="index"
                >{{ val }}</el-tag
              >
            </template>
          </el-table-column>
          <el-table-column prop="type" label="参数类型" width="100" show-overflow-tooltip>
            <template #default="scope">
              <span>
                {{ showGoodParamType(scope.row.type) }}
              </span>
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

<script setup lang="ts" name="selGoodParam">
import { reactive, onMounted, ref, nextTick } from "vue";
import _ from "lodash";
import { useGoodParamApi } from "/@/api/good/param/index";
import { ElMessage } from "element-plus";

// 引入商品参数 Api 请求接口
const paramApi = useGoodParamApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["selParamResult"]);

// 定义变量内容
const multipleParamTableRef = ref();
let paramTypes = ref<EnumValueType[]>([]);
const state = reactive<GoodParamState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      type: null,
      sorting: "creationTime desc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});

// 显示参数类型
const showGoodParamType = (val: string) => {
  for (let i = 0; i < paramTypes.value.length; i++) {
    const pType = paramTypes.value[i];
    if (pType.title == val) {
      return pType.description;
    }
  }
  return val;
};

// 查询参数类型
const getGoodParamTypes = () => {
  paramApi
    .getGoodParamTypes()
    .then((result) => {
      paramTypes.value = result;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询参数类型出错：", err);
    });
};

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  paramApi
    .getGoodParamList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询商品参数列表出错：", err);
    });
};

// 搜索
const searchGoodParam = () => {
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

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择参数模型",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询参数模型
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleParamTableRef.value.getSelectionRows() as RowGoodParamType[];
  if (selRows.length < 1) {
    ElMessage.warning("您还未选择参数模型！");
    return;
  }
  emit("selParamResult", selRows);
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
  getGoodParamTypes();
  getTableData();
});
</script>

<style scoped lang="scss">
.good-param-container {
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

.img-logo {
  width: 50px;
  height: 50px;
}

.img-logo .image-slot {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  background: var(--el-fill-color-light);
  color: var(--el-text-color-secondary);
  font-size: 30px;
}
.img-logo .image-slot .el-icon {
  font-size: 30px;
}
</style>
