<template>
  <div class="pinTuan-pinTuanRule-container layout-padding">
    <el-card shadow="hover" class="mb15">
      <div class="pinTuan-pinTuanRule-search">
        <el-row :gutter="15">
          <el-col :span="4">
            <el-input
              v-model="state.tableData.param.name"
              size="default"
              placeholder="请输入活动名称"
              maxlength="40"
              clearable
            >
            </el-input>
          </el-col>
          <el-col :span="3">
            <el-select
              v-model="state.tableData.param.isStatusOpen"
              size="default"
              placeholder="是否开启"
            >
              <el-option label="是" :value="true" />
              <el-option label="否" :value="false" />
            </el-select>
          </el-col>
          <el-col :span="7">
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
          <el-col :span="10">
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
    <el-card shadow="hover" class="layout-padding-auto">
      <template #header>
        <el-button
          size="default"
          v-auth="'PromotionService.PinTuanRules.Create'"
          type="success"
          @click="onOpenAddPinTuan('add')"
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

        <el-table-column
          prop="isStatusOpen"
          label="是否开启"
          show-overflow-tooltip
          width="90"
        >
          <template #default="scope">
            <el-switch
              v-model="scope.row.isStatusOpen"
              inline-prompt
              active-text="是"
              inactive-text="否"
              size="default"
              @change="onIsStatusOpen($event, scope.row)"
            />
          </template>
        </el-table-column>

        <el-table-column fixed="right" label="操作" width="120">
          <template #default="scope">
            <el-button
              v-auth="'PromotionService.PinTuanRules.Update'"
              size="small"
              text
              type="warning"
              @click="onOpenEditPinTuan('edit', scope.row)"
              >修改</el-button
            >
            <el-button
              v-auth="'PromotionService.PinTuanRules.Delete'"
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
    <pinTuanRule-dialog ref="pinTuanRuleDialogRef" @refresh="getTableData()" />
  </div>
</template>

<script setup lang="ts" name="pinTuanRule">
import { defineAsyncComponent, reactive, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import { usePinTuanRuleApi } from "/@/api/promotion/pinTuanRule/index";

// 引入组件
const PinTuanRuleDialog = defineAsyncComponent(
  () =>
    import(
      "/@/views/promotion/pinTuanManage/pinTuanList/components/pinTuanRuleDialog.vue"
    )
);

// 引入拼团规则 Api 请求接口
const pinTuanRuleApi = usePinTuanRuleApi();

// 定义变量内容
const pinTuanRuleDialogRef = ref();
const startAndEndTime = ref([]);
const state = reactive<PinTuanRuleState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      name: null,
      isStatusOpen: null,
      startTime: null,
      endTime: null,
      sorting: "sort asc",
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
  pinTuanRuleApi
    .getPinTuanRuleList(state.tableData.param)
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

// 修改是否开启
const onIsStatusOpen = (val: boolean, pinTuanRule: RowPinTuanRuleType) => {
  pinTuanRuleApi
    .updatePinTuanRuleIsStatusOpen(pinTuanRule.id, val)
    .then((result) => {
      console.log("修改[" + pinTuanRule.name + "]是否启用显示，结果：", result);
    })
    .catch((err: any) => {
      pinTuanRule.isStatusOpen = !val;
      console.error("修改是否启用出错：", err);
    });
};

// 打开新增拼团规则弹窗
const onOpenAddPinTuan = (type: string) => {
  pinTuanRuleDialogRef.value.openDialog(type);
};

// 打开修改拼团规则弹窗
const onOpenEditPinTuan = (type: string, row: RowPinTuanRuleType) => {
  pinTuanRuleDialogRef.value.openDialog(type, row);
};

// 删除拼团规则
const onRowDel = (row: RowPinTuanRuleType) => {
  ElMessageBox.confirm(`此操作将永久删除活动：“${row.name}”，是否继续?`, "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    pinTuanRuleApi
      .deletePinTuanRule(row.id)
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
.pinTuan-pinTuanRule-container {
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
