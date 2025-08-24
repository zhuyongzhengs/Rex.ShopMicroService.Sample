<template>
  <div class="bill-aftersales-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      center
      draggable
      width="950px"
    >
      <el-text v-if="!billAftersalesDtl" type="info">加载中…</el-text>
      <div class="bill-aftersales-box" v-if="billAftersalesDtl">
        <el-form
          ref="billAftersalesDialogFormRef"
          :model="billAftersalesDtl"
          :rules="formRules"
          size="default"
          label-width="100px"
        >
          <el-row class="mb20">
            <el-col :span="8">
              <el-form-item label="售后单号">
                <el-input readonly disabled v-model="billAftersalesDtl.no"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="订单号">
                <el-input
                  readonly
                  disabled
                  v-model="billAftersalesDtl.orderNo"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="用户名">
                <el-input
                  readonly
                  disabled
                  v-model="billAftersalesDtl.userName"
                  :title="billAftersalesDtl.userName"
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mt10">
            <el-col :span="24">
              <el-form-item label="退货原因">
                <el-input
                  type="textarea"
                  :rows="2"
                  readonly
                  disabled
                  v-model="billAftersalesDtl.reason"
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mt10">
            <el-col :span="24">
              <el-form-item label="图片凭证">
                <div class="form-img-box">
                  <template v-for="url in billAftersalesDtl.images">
                    <el-image
                      style="width: 100px; height: 100px"
                      :src="url"
                      :zoom-rate="1.2"
                      :max-scale="7"
                      :min-scale="0.2"
                      :preview-src-list="billAftersalesDtl.images"
                      show-progress
                      fit="cover"
                    />
                  </template>
                  <el-text
                    v-if="
                      !billAftersalesDtl.images || billAftersalesDtl.images.length < 1
                    "
                    type="info"
                    >无</el-text
                  >
                </div>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mt10">
            <el-col :span="24">
              <el-form-item label="退货商品" prop="productReturns">
                <el-table
                  ref="orderItemTableRef"
                  :data="billAftersalesDtl.productItems"
                  v-loading="state.loading"
                  border
                  width="100%"
                >
                  <el-table-column fixed width="40">
                    <template #default="scope">
                      <el-checkbox
                        v-model="scope.row.checked"
                        size="large"
                        @change="onProductChange($event, scope.row)"
                      />
                    </template>
                  </el-table-column>
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
                    min-width="350"
                  />
                  <el-table-column
                    prop="sn"
                    label="货品编码"
                    width="170"
                  ></el-table-column>
                  <el-table-column
                    prop="bn"
                    label="商品编码"
                    width="170"
                  ></el-table-column>
                  <el-table-column
                    prop="buyNums"
                    label="购买数量"
                    width="90"
                  ></el-table-column>
                  <el-table-column
                    prop="sendNums"
                    label="发货数量"
                    width="90"
                  ></el-table-column>
                  <el-table-column prop="amount" label="商品价格" width="90">
                    <template #default="scope">
                      <span class="good-price">￥{{ scope.row.amount }}</span>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="nums"
                    label="退货数量"
                    width="90"
                    fixed="right"
                  ></el-table-column>
                </el-table>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mt15">
            <el-col :span="8">
              <el-form-item label="退款金额" prop="refundAmount">
                <el-input
                  v-model="billAftersalesDtl.refundAmount"
                  clearable
                  @input="
                    billAftersalesDtl.refundAmount = verifyNumberIntegerAndFloat(
                      billAftersalesDtl.refundAmount
                    )
                  "
                  placeholder="请输入退款金额"
                >
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="收货已否" prop="type">
                <el-radio-group v-model="billAftersalesDtl.type">
                  <el-radio :value="1" label="未收到货" />
                  <el-radio :value="2" label="已收到货" />
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="审核结果" prop="status">
                <el-radio-group v-model="billAftersalesDtl.status">
                  <el-radio :value="2" label="通过" />
                  <el-radio :value="3" label="拒绝" />
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mt15">
            <el-col :span="24">
              <el-form-item label="备注">
                <el-input
                  type="textarea"
                  placeholder="请输入备注…"
                  show-word-limit
                  maxlength="200"
                  :rows="2"
                  clearable
                  v-model="billAftersalesDtl.mark"
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
        <div class="form-explain-box">
          <p>
            选择【未收到货】是退未发货的商品，选择【已收到货】是退已发货的商品，选择【未收到货】不会生成退货单，选择【已收到货】会生成退货单，未发货的商品和已发货的商品不能混合着退。
          </p>
        </div>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button
            type="primary"
            @click="submitValidate(billAftersalesDialogFormRef)"
            size="default"
            >确 认</el-button
          >
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="billAftersalesAuditDialog">
import { ref, reactive, defineExpose } from "vue";
import { ElMessage, type FormInstance, type FormRules } from "element-plus";
import _ from "lodash";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import { useBillAftersalesApi } from "/@/api/orderManage/billAftersales/index";

// 引入 Api 请求接口
const billAftersalesApi = useBillAftersalesApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["success"]);

// 定义变量
const billAftersalesDialogFormRef = ref<any>();
const billAftersalesDtl = ref<RowBillAftersalesDtlType>();
const state = reactive({
  loading: true,
  dialog: {
    editId: "",
    isShowDialog: false,
    title: "编辑&审核售后单",
    submitTxt: "取消",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  productReturns: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (
          !billAftersalesDtl.value ||
          !billAftersalesDtl.value.productItems ||
          billAftersalesDtl.value.productItems.length < 1 ||
          billAftersalesDtl.value.productItems.filter((item) => item.checked).length < 1
        ) {
          callback(new Error("请选择退货商品！"));
          return;
        }
        callback();
      },
      trigger: ["change", "blur"],
    },
  ],
  refundAmount: [
    { required: true, message: "请输入退款金额！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!value || Number(value) <= 0) {
          callback(new Error("退款金额不能小于或等于0！"));
          return;
        }
        callback();
      },
      trigger: ["change", "blur"],
    },
  ],
  type: [{ required: true, message: "请选择收货与否！", trigger: "change" }],
  status: [
    { required: true, message: "请选择审核结果！", trigger: "change" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (value !== 2 && value !== 3) {
          callback(new Error("请选择审核结果！"));
          return;
        }
        callback();
      },
      trigger: ["change", "blur"],
    },
  ],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate((valid, fields) => {
    if (valid) {
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 提交
const onSubmit = () => {
  if(!billAftersalesDtl.value) return;
  let rData = {
    orderItemIds: billAftersalesDtl.value.productItems.filter((item) => item.checked).map((item) => item.orderItemId),
    refundAmount: Number(billAftersalesDtl.value.refundAmount),
    type: billAftersalesDtl.value.type,
    status: billAftersalesDtl.value.status,
    mark: billAftersalesDtl.value.mark,
  }
  billAftersalesApi
    .auditBillAftersales(state.dialog.editId, rData)
    .then((result) => {
      console.log("操作成功！", result);
      emit("success");
      ElMessage.success("操作成功！");
      onCancel();
    })
    .catch((err: any) => {
      console.error("操作出错：", err);
    });
};

// 打开弹窗
const openDialog = (row: RowBillAftersalesType) => {
  state.loading = true;
  state.dialog.editId = row.id;
  getBillAftersalesById(row.id);
  state.dialog.isShowDialog = true;
};

// 关闭弹窗
const closeDialog = () => {
  state.dialog.isShowDialog = false;
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 退货商品选中
const onProductChange = (value: boolean, row: RowBillAftersalesDtlItemType) => {
  if (row.checked) {
    row.nums = row.buyNums;
  } else {
    row.nums = 0;
  }
  if (!billAftersalesDtl.value) return;
  let refundAmount = billAftersalesDtl.value.productItems.reduce(
    (accumulator, currentValue) => {
      let val = currentValue.checked ? currentValue.amount : 0;
      return accumulator + val;
    },
    0
  );
  billAftersalesDtl.value.refundAmount = String(refundAmount);
};

// 查询售后单信息
const getBillAftersalesById = (id: string) => {
  billAftersalesApi
    .getBillAftersalesById(id)
    .then((result) => {
      if (!result) return;
      billAftersalesDtl.value = result;
      console.log("查询售后单信息成功！", result);
      state.loading = false;
    })
    .catch((err: any) => {
      console.error("查询售后单信息出错：", err);
      state.loading = false;
    });
};

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.bill-aftersales-container {
  .bill-aftersales-tabs .el-card__header {
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
<style lang="scss" scoped>
.form-img-box {
  display: flex;
  flex-wrap: wrap;
  .el-image {
    width: 145px !important;
    height: 145px !important;
    margin-right: 10px;
    margin-bottom: 10px;
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

.form-explain-box {
  margin-top: 10px;
  font-size: 14px;
  color: #909399;
}
</style>
