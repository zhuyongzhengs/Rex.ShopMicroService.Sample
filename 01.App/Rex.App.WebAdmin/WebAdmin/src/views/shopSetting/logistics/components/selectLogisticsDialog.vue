<template>
  <div class="logistics-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="950px"
    >
      <el-card shadow="never" class="mb15">
        <div class="logistics-search">
          <el-row :gutter="15">
            <el-col :span="8">
              <el-input
                v-model="state.tableData.param.logiCode"
                size="default"
                placeholder="请输入物流编码"
                maxlength="100"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="8">
              <el-input
                v-model="state.tableData.param.logiName"
                size="default"
                placeholder="请输入物流名称"
                maxlength="100"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="8">
              <el-button
                size="default"
                type="primary"
                class="ml10"
                @click="searchLogistics"
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
          ref="multipleLogisticsTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          max-height="300"
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
          <el-table-column prop="sort" label="排序" width="60" />
          <el-table-column
            prop="logiCode"
            label="物流编码"
            show-overflow-tooltip
            width="180"
          />
          <el-table-column prop="logiName" label="物流名称" show-overflow-tooltip />
          <el-table-column prop="imgUrl" label="Logo" show-overflow-tooltip width="150">
            <template #default="scope">
              <el-image
                class="img-logo"
                fit="scale-down"
                :src="scope.row.imgUrl"
                :zoom-rate="1.2"
                :max-scale="7"
                :min-scale="0.2"
                :preview-src-list="[scope.row.imgUrl]"
                :preview-teleported="true"
              >
                <template #error>
                  <div class="image-slot">
                    <el-icon><icon-picture /></el-icon>
                  </div>
                </template>
              </el-image>
            </template>
          </el-table-column>
          <el-table-column
            prop="phone"
            label="物流电话"
            show-overflow-tooltip
            width="200"
          />
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

<script setup lang="ts" name="selLogistics">
import { reactive, onMounted, ref, nextTick } from "vue";
import _ from "lodash";
import { useLogisticsApi } from "/@/api/shopSetting/logistics/index";
import { ElMessage } from "element-plus";

// 引入商品物流 Api 请求接口
const logisticsApi = useLogisticsApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["selLogisticsResult"]);

// 定义变量内容
const multipleLogisticsTableRef = ref();
const currentLogistics = ref<RowLogisticsType>();
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});
const state = reactive<LogisticsState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      logiCode: null,
      logiName: null,
      phone: null,
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
  logisticsApi
    .getLogisticsList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询商品物流列表出错：", err);
    });
};

// 搜索
const searchLogistics = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.logiCode = null;
  state.tableData.param.logiName = null;
  state.tableData.param.phone = null;
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

// 选择(单选)当前
const onSelectedCurrChange = (logistics: RowLogisticsType) => {
  currentLogistics.value = logistics;
};

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择物流公司",
});

// 打开弹窗
const openDialog = (title: string) => {
  if (title) dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询物流公司
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleLogisticsTableRef.value.getSelectionRows() as RowLogisticsType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择物流公司！");
    return;
  }
  if (!currentLogistics.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择物流公司！");
    return;
  }
  emit(
    "selLogisticsResult",
    props.selectionType == "single" ? currentLogistics.value : selRows
  );
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
  getTableData();
});
</script>

<style scoped lang="scss">
.logistics-container {
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
