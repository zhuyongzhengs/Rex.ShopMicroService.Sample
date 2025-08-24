<template>
  <div class="promotion-pinTuanRule-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="900px"
    >
      <el-card shadow="never" class="mb15">
        <div class="promotion-pinTuanRule-search">
          <el-row :gutter="15">
            <el-col :span="6">
              <el-input
                v-model="state.tableData.param.name"
                size="default"
                placeholder="请输入活动名称"
                maxlength="40"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="12">
              <el-date-picker
                v-model="startAndEndTime"
                type="datetimerange"
                range-separator="~"
                start-placeholder="开始时间"
                end-placeholder="结束时间"
                size="default"
                format="YYYY-MM-DD HH:mm:ss"
                value-format="YYYY-MM-DD HH:mm:ss"
              />
            </el-col>
            <el-col :span="6">
              <el-button size="default" type="primary" @click="searchPinTuan">
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
          ref="multiplePinTuanRuleTableRef"
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          height="300"
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
          <el-table-column prop="name" label="活动名称" show-overflow-tooltip />
          <el-table-column
            prop="startTime"
            label="开始时间"
            show-overflow-tooltip
            width="165"
          />
          <el-table-column
            prop="endTime"
            label="结束时间"
            show-overflow-tooltip
            width="165"
          />
          <el-table-column prop="peopleNumber" label="开团人数" width="90">
            <template #default="scope">
              <span>{{ scope.row.peopleNumber }}人</span>
            </template>
          </el-table-column>
          <el-table-column prop="discountAmount" label="优惠金额" width="90">
            <template #default="scope">
              <span>￥{{ scope.row.discountAmount }}</span>
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
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button type="primary" @click="onConfirm" size="default">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="pinTuanRule">
import { reactive, onMounted, ref, nextTick } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { usePinTuanRuleApi } from "/@/api/promotion/pinTuanRule/index";

// 引入拼团规则 Api 请求接口
const pinTuanRuleApi = usePinTuanRuleApi();

const emit = defineEmits(["selPinTuanRuleResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 定义变量内容
const multiplePinTuanRuleTableRef = ref();
const startAndEndTime = ref([]);
const currentPinTuanRule = ref({} as RowPinTuanRuleType);
const state = reactive<PinTuanRuleState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      isStatusOpen: true,
      startTime: null,
      endTime: null,
      sorting: "sort asc",
      skipCount: 0,
      maxResultCount: 10,
    },
  },
});
const dialog = reactive({
  isShowDialog: false,
  title: "",
});

// 初始化表格数据
const getTableData = () => {
  state.tableData.loading = true;
  state.tableData.param.skipCount =
    (state.tableData.currentPage - 1) * state.tableData.param.maxResultCount;
  pinTuanRuleApi
    .getPinTuanRuleLoadGoodList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询拼团规则列表出错：", err);
    });
};

// 搜索
const searchPinTuan = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.isStatusOpen = null;
  state.tableData.param.startTime = null;
  state.tableData.param.endTime = null;
  startAndEndTime.value = [];
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
const onSelectedCurrChange = (pinTuanRule: RowPinTuanRuleType) => {
  currentPinTuanRule.value = pinTuanRule;
};

// 打开弹窗
const openDialog = (title: string = "选择拼团商品") => {
  dialog.title = title;
  nextTick(() => {
    dialog.isShowDialog = true;
    // 查询商品信息
    getTableData();
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multiplePinTuanRuleTableRef.value.getSelectionRows() as RowPinTuanRuleType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择拼团商品！");
    return;
  }
  if (!currentPinTuanRule.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择拼团商品！");
    return;
  }
  emit(
    "selPinTuanRuleResult",
    props.selectionType == "single" ? currentPinTuanRule.value : selRows
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
  // ...
});
</script>

<style scoped lang="scss">
.promotion-pinTuanRule-container {
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
