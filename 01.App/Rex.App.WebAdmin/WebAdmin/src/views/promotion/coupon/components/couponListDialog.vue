<template>
  <div class="promotion-coupon-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="1100px"
    >
      <el-card shadow="never" class="mb15">
        <div class="promotion-coupon-search">
          <el-row :gutter="15">
            <el-col :span="4">
              <el-input
                v-model="state.tableData.param.couponCode"
                size="default"
                placeholder="请输入优惠劵编码"
                maxlength="40"
                clearable
              >
              </el-input>
            </el-col>
            <el-col :span="4">
              <el-select
                v-model="state.tableData.param.isUsed"
                size="default"
                placeholder="是否使用"
              >
                <el-option label="是" :value="true" />
                <el-option label="否" :value="false" />
              </el-select>
            </el-col>
            <el-col :span="10">
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
              <el-button size="default" type="primary" @click="searchCoupon">
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
          :data="state.tableData.data"
          v-loading="state.tableData.loading"
          style="width: 100%"
          height="300"
        >
          <el-table-column type="index" label="序号" width="60" />
          <el-table-column prop="couponCode" label="优惠劵编码" width="150" />
          <el-table-column
            prop="isUsed"
            label="是否使用"
            show-overflow-tooltip
            width="90"
          >
            <template #default="scope">
              <el-switch
                v-model="scope.row.isUsed"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
                :disabled="true"
              />
            </template>
          </el-table-column>

          <el-table-column prop="userName" label="领取者" show-overflow-tooltip />
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
      <!--
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
      -->
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="coupon">
import { reactive, nextTick } from "vue";
import _ from "lodash";
import { useCouponApi } from "/@/api/promotion/coupon/index";

// 引入全局优惠劵 Api 请求接口
const couponApi = useCouponApi();

// 定义变量内容
const state = reactive<CouponState>({
  tableData: {
    data: [],
    total: 0,
    loading: false,
    currentPage: 1,
    param: {
      promotionId: "",
      couponCode: null,
      isUsed: null,
      startAndEndTime: [],
      sorting: "creationTime desc",
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
  couponApi
    .getCouponListTables(state.tableData.param)
    .then((result) => {
      state.tableData.total = result.totalCount;
      state.tableData.data = result.items;
      state.tableData.loading = false;
    })
    .catch((err: any) => {
      state.tableData.loading = false;
      console.error("查询卷码列表出错：", err);
    });
};

// 搜索
const searchCoupon = () => {
  state.tableData.currentPage = 1;
  getTableData();
};

// 重置
const onReset = () => {
  state.tableData.param.couponCode = null;
  state.tableData.param.isUsed = null;
  state.tableData.param.startAndEndTime = [];
  state.tableData.param.startTime = null;
  state.tableData.param.endTime = null;
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

// 打开弹窗
const openDialog = (promotionId: string) => {
  state.tableData.param.promotionId = promotionId;
  searchCoupon();
  nextTick(() => {
    dialog.title = "卷码列表";
    dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style scoped lang="scss">
.promotion-coupon-container {
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
