<template>
  <div class="promotion-groupSeckill-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="900px"
    >
      <el-card shadow="never" class="mb15">
        <div class="promotion-groupSeckill-search">
          <el-row :gutter="15">
            <el-col :span="5">
              <el-input
                v-model="state.tableData.param.name"
                size="default"
                placeholder="请输入促销名称"
                maxlength="40"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="13">
              <el-date-picker
                v-model="state.tableData.param.startAndEndTime"
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
              <el-button size="default" type="primary" @click="searchPromotion">
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
          ref="multipleGroupSeckillTableRef"
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
          <el-table-column prop="name" label="促销名称" show-overflow-tooltip />
          <el-table-column prop="weight" label="权重" width="70" />
          <!--
          <el-table-column prop="type" label="类型" width="90">
            <template #default="scope">
              <el-tag
                v-if="scope.row.type == 3"
                type="success"
                effect="dark"
                size="small"
              >
                团购
              </el-tag>
              <el-tag
                v-if="scope.row.type == 4"
                type="warning"
                effect="dark"
                size="small"
              >
                秒杀
              </el-tag>
            </template>
          </el-table-column>
          <el-table-column
            prop="isEnable"
            label="是否开启"
            show-overflow-tooltip
            width="90"
          >
            <template #default="scope">
              <el-switch
                v-model="scope.row.isEnable"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
                :disabled="true"
              />
            </template>
          </el-table-column>
          -->
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
          <el-button type="primary" @click="onConfirm" size="default">{{
            dialog.submitTxt
          }}</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="groupSeckill">
import { reactive, onMounted, ref, nextTick } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGroupSeckillApi } from "/@/api/promotion/groupSeckill/index";

const emit = defineEmits(["selGroupSeckillResult"]);
const props = defineProps({
  selectionType: {
    type: String,
    default: () => "selection", // 默认多选
  },
});

// 引入团购|秒杀 Api 请求接口
const groupSeckillApi = useGroupSeckillApi();

// 定义变量内容
const multipleGroupSeckillTableRef = ref();
const dialog = reactive({
  isShowDialog: false,
  title: "",
  submitTxt: "",
});
const currentGroupSeckill = ref({} as RowPromotionType);
const state = reactive<PromotionState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      isExclusive: null,
      startAndEndTime: [],
      sorting: "creationTime asc",
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
  groupSeckillApi
    .getPromotionCommonTagList(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询团购|秒杀列表出错：", err);
    });
};

// 搜索
const searchPromotion = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.name = null;
  state.tableData.param.isExclusive = null;
  state.tableData.param.startAndEndTime = [];
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
const onSelectedCurrChange = (groupSeckill: RowPromotionType) => {
  currentGroupSeckill.value = groupSeckill;
};

// 打开弹窗
const openDialog = (title: string = "选择商品秒杀") => {
  nextTick(() => {
    dialog.title = title;
    dialog.submitTxt = "确 定";
    dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  const selRows = multipleGroupSeckillTableRef.value.getSelectionRows() as RowPromotionType[];
  if (selRows.length < 1 && props.selectionType == "selection") {
    ElMessage.warning("您还未选择商品秒杀！");
    return;
  }
  if (!currentGroupSeckill.value?.id && props.selectionType == "single") {
    ElMessage.warning("您还未选择商品秒杀！");
    return;
  }
  emit(
    "selGroupSeckillResult",
    props.selectionType == "single" ? currentGroupSeckill.value : selRows
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
.promotion-groupSeckill-container {
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
