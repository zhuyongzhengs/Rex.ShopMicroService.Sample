<template>
  <div class="reship-detail-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      draggable
      center
      width="1000px"
    >
      <div class="reship-detail-box">
        <div class="reship-detail-item">
          <table cellpadding="0" cellspacing="0" class="reship-detail-table">
            <tr>
              <th>退货单号</th>
              <td>{{ state.data?.no }}</td>
              <th>订单编号</th>
              <td>{{ state.data?.orderNo }}</td>
              <th>售后单号</th>
              <td>{{ state.data?.aftersalesNo }}</td>
            </tr>
            <tr>
              <th>物料公司</th>
              <td>{{ state.data?.logiCode }}</td>
              <th>物料单号</th>
              <td>{{ state.data?.logiNo }}</td>
              <th>状态</th>
              <td>
                <el-text size="default" :type="colorStatusType(state.data?.status)">{{
                  state.data?.statusDisplay
                }}</el-text>
              </td>
            </tr>
            <tr>
              <th>用户名称</th>
              <td>{{ state.data?.userName }}</td>
              <th>创建时间</th>
              <td>{{ state.data?.creationTime }}</td>
              <th>更新时间</th>
              <td>{{ state.data?.lastModificationTime }}</td>
            </tr>
            <tr>
              <th>备注</th>
              <td colspan="5">
                {{ state.data?.memo }}
              </td>
            </tr>
          </table>
        </div>
        <el-card shadow="never" class="mt15">
          <template #header>
            <div class="reship-item-title">退货明细</div>
          </template>
          <el-table
            ref="billReshipItemTableRef"
            :data="state.data?.billReshipItems"
            v-loading="state.loading"
            border
            width="100%"
          >
            <el-table-column fixed type="index" width="65" label="序号" />
            <el-table-column prop="imageUrl" label="商品图片" width="100">
              <template #default="scope">
                <el-image
                  class="img-logo"
                  :src="scope.row.imageUrl"
                  :zoom-rate="1.2"
                  :max-scale="7"
                  :min-scale="0.2"
                  :preview-src-list="[scope.row.imageUrl]"
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
              prop="name"
              label="商品名称"
              show-overflow-tooltip
              min-width="400"
            />
            <el-table-column
              prop="addon"
              label="货品信息"
              show-overflow-tooltip
              width="240"
            />
            <el-table-column prop="sn" label="货品编码" width="170"></el-table-column>
            <el-table-column prop="bn" label="商品编码" width="170"></el-table-column>
            <el-table-column prop="nums" label="数量" width="80"></el-table-column>
            <el-table-column
              prop="creationTime"
              label="创建时间"
              width="170"
            ></el-table-column>
          </el-table>
        </el-card>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="billReshipDetailDialog">
import { nextTick, reactive, defineExpose } from "vue";
import _ from "lodash";

// 定义变量
const state = reactive({
  loading: true,
  data: {} as RowBillReshipManyType,
  dialog: {
    isShowDialog: false,
    title: "退货单详情",
    submitTxt: "取消",
  },
});

// 状态颜色
const colorStatusType = (status: number) => {
  switch (status) {
    case 1:
      return "warning";
    case 2:
      return "primary";
    case 5:
      return "success";
    default:
      return "info";
  }
};

// 打开弹窗
const openDialog = (row: RowBillReshipManyType) => {
  state.loading = true;
  state.data = row;
  nextTick(() => {
    state.loading = false;
    // 查询退货单信息
    state.dialog.isShowDialog = true;
  });
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.reship-detail-container {
  .reship-detail-box .el-card__header {
    background-color: #f5f7fa !important;
    padding: 10px 15px !important;
  }
  .el-dialog__header {
    border-bottom: 1px #e4e7ed dashed !important;
    margin-right: 0px !important;
    padding-bottom: 14px !important;
  }
}
</style>

<style scoped lang="scss">
.reship-detail-container {
  .reship-detail-box {
    position: relative;
    top: -10px;
    .reship-detail-item {
      .reship-detail-table {
        border-collapse: collapse;
        width: 100%;
        tr {
          th {
            width: 110px;
            background-color: #fbfbfb;
            text-align: right;
            padding: 10px 5px;
            font-weight: 500;
            border: 1px solid #eeeeee;
          }
          td {
            min-width: 130px;
            text-align: left;
            padding: 10px 5px;
            color: #999999;
            border: 1px solid #eeeeee;
          }
        }
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
  .good-price {
    color: #f56c6c;
  }
}
</style>
