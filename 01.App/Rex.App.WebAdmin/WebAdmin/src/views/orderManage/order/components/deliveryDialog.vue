<template>
  <div class="delivery-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      center
      draggable
      width="1000px"
    >
      <div class="delivery-box">
        <el-form
          ref="deliveryDialogFormRef"
          :model="state.data"
          :rules="formRules"
          size="default"
          label-width="100px"
        >
          <el-row class="mb20">
            <el-col :span="8">
              <el-form-item label="订单号">
                <el-input v-model="state.data.no" readonly disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="配送方式">
                <el-input v-model="state.data.logisticsName" readonly disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <el-form-item label="配送费">
                <el-input v-model="state.data.costFreight" readonly disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <el-form-item label="商品重量">
                <el-input v-model="state.data.weight" readonly disabled></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mb20">
            <el-col :span="24">
              <el-form-item label="买家备注">
                <el-input
                  type="textarea"
                  v-model="state.data.memo"
                  :rows="2"
                  resize="none"
                  readonly
                  disabled
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mb20">
            <el-col :span="8">
              <el-form-item label="收货人姓名" prop="shipName">
                <el-input
                  v-model="state.data.shipName"
                  placeholder="请输入收货人姓名"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="收货人电话" prop="shipMobile">
                <el-input
                  v-model="state.data.shipMobile"
                  placeholder="请输入收货人电话"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="10">
              <el-form-item label="收货区域" prop="shipAreaId">
                <el-cascader
                  ref="cascaderAreaRef"
                  v-model="shipAreas"
                  :props="areaProp"
                  placeholder="请选择区域"
                  clearable
                  class="w100"
                  filterable
                  separator=" - "
                  @change="areaChange"
                >
                  <template #default="{ node, data }">
                    <span>{{ data.name }}</span>
                    <span v-if="!node.isLeaf && node.children?.length > 0">
                      ({{ node.children.length }})
                    </span>
                  </template>
                </el-cascader>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-form-item label="收货地址" prop="shipAddress">
                <el-input
                  v-model="state.data.shipAddress"
                  placeholder="请输入收货地址"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <div class="order-detail-content">
            <el-table
              ref="orderItemTableRef"
              :data="state.data?.orderItems"
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
                min-width="450"
              />
              <el-table-column prop="price" label="商品单价" width="90">
                <template #default="scope">
                  <span class="good-price">￥{{ scope.row.price }}</span>
                </template>
              </el-table-column>
              <el-table-column prop="nums" label="购买数量" width="90"></el-table-column>
              <el-table-column prop="amount" label="商品总价" width="90"
                ><template #default="scope">
                  <span>￥{{ scope.row.amount }}</span>
                </template></el-table-column
              >
              <el-table-column prop="sn" label="货品编码" width="170"></el-table-column>
              <el-table-column prop="bn" label="商品编码" width="170"></el-table-column>
              <el-table-column
                prop="weight"
                label="商品总重量(克)"
                width="120"
              ></el-table-column>
              <el-table-column prop="sendNums" label="发货数量" width="120">
                <template #default="scope">
                  <el-input
                    v-model="scope.row.SendNums"
                    readonly
                    placeholder="发货数量"
                  />
                </template>
              </el-table-column>
              <el-table-column prop="reshipNums" label="退货数量" width="90">
                <template #default="scope">
                  <span>{{ scope.row.reshipNums || "0" }}</span>
                </template>
              </el-table-column>
            </el-table>
          </div>
          <el-row>
            <el-col :span="12">
              <el-form-item label="物流公司" prop="logiName">
                <el-input
                  v-model="state.data.logiName"
                  placeholder="请选择物流公司"
                  readonly
                >
                  <template #append>
                    <el-button size="small" type="success" @click="onSelectLogistics">
                      <el-icon>
                        <ele-Plus />
                      </el-icon>
                    </el-button>
                  </template>
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="物流单号" prop="logiNo">
                <el-input
                  v-model="state.data.logiNo"
                  placeholder="请输入物流单号"
                  clearable
                >
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="24" class="mt20">
              <el-form-item label="发货备注">
                <el-input
                  type="textarea"
                  v-model="state.data.deliveryMemo"
                  :rows="2"
                  placeholder="请输入发货备注…"
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button
            type="primary"
            @click="submitValidate(deliveryDialogFormRef)"
            size="default"
            >确 认</el-button
          >
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
    <select-logistics-dialog
      ref="selectLogisticsDialogRef"
      selectionType="single"
      @selLogisticsResult="onSelectLogisticsResult"
    />
  </div>
</template>

<script setup lang="ts" name="delivery">
import { nextTick, ref, reactive, defineExpose, defineAsyncComponent } from "vue";
import { ElMessage } from "element-plus";
import type { FormInstance, CascaderNode, FormRules } from "element-plus";
import _ from "lodash";
import { cascaderAreaActiveIds } from "/@/utils/other";
import { useOrderApi } from "/@/api/orderManage/order/index";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";

const SelectLogisticsDialog = defineAsyncComponent(
  () => import("/@/views/shopSetting/logistics/components/selectLogisticsDialog.vue")
);

// 引入 Api 请求接口
const orderApi = useOrderApi();
const areaApi = useGoodAreaApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["success"]);

// 定义变量
const cascaderAreaRef = ref<any>();
const deliveryDialogFormRef = ref<any>();
const selectLogisticsDialogRef = ref<any>();
const shipAreas = ref<number[]>();
const areaProp = ref({
  lazy: true,
  value: "id",
  label: "name",
  lazyLoad(node: CascaderNode, resolve: Function) {
    loadTreeAreaNode(node, (result: AreaTreeType[]) => {
      resolve(result);
    });
  },
});
const state = reactive({
  loading: true,
  data: {} as OrderDeliveryType,
  dialog: {
    isShowDialog: false,
    title: "订单发货",
    submitTxt: "取消",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  shipName: [{ required: true, message: "请输入收货人姓名！", trigger: "blur" }],
  shipMobile: [{ required: true, message: "请输入收货人电话！", trigger: "blur" }],
  shipAreaId: [
    { required: true, message: "请选择收货区域！", trigger: "blur" },
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!value || value.length < 1) {
          callback(new Error("请选择收货区域！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  shipAddress: [{ required: true, message: "请输入收货地址！", trigger: "blur" }],
  logiName: [{ required: true, message: "请选择物流公司！", trigger: "blur" }],
  logiNo: [{ required: true, message: "请输入物流单号！", trigger: "blur" }],
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
  state.data.displayArea = cascaderAreaRef.value.presentText;
  orderApi
    .orderDelivery(state.data)
    .then((result) => {
      if (!result) return;
      emit("success");
      ElMessage.success("发货成功！");
      onCancel();
    })
    .catch((err: any) => {
      console.error("订单发货出错：", err);
    });
};

// 打开选择物流弹窗
const onSelectLogistics = () => {
  selectLogisticsDialogRef.value.openDialog("选择物流公司");
};

// 选择物流结果
const onSelectLogisticsResult = (logistics: RowLogisticsType) => {
  state.data.logiCode = logistics.logiCode;
  state.data.logiName = logistics.logiName;
};

// 加载区域树节点
const loadTreeAreaNode = (
  node: CascaderNode,
  resolve: (data: AreaTreeType[]) => void
) => {
  const { level, data } = node;
  let parentId = 0;
  let depth = null;
  if (level > 0) {
    if (data?.id) parentId = Number(data.id);
    if (data?.depth) depth = Number(data.depth) + 1;
  }
  // 获取节点数据
  loadGoodAreaTree(parentId, depth, state.data.shipAreaId, (result: AreaTreeType[]) => {
    resolve(result);
  });
};

// 加载区域树数据
const loadGoodAreaTree = (
  parentId: number,
  depth: number | null = null,
  activeId: number | null = null,
  callback: Function
) => {
  areaApi
    .loadGoodAreaTree(parentId, depth, activeId)
    .then((result) => {
      if (!result) callback([]);
      var areaList = result;
      for (let i = 0; i < areaList.length; i++) {
        if (!areaList[i].children) areaList[i].children = [];
      }
      callback(areaList);
    })
    .catch((err: any) => {
      console.error("查询区域树出错：", err);
    });
};

// 收货区域切换
function areaChange(val: []): void {
  if (!val || val.length < 1) {
    state.data.shipAreaId = 0;
    return;
  }
  state.data.shipAreaId = val[val.length - 1];
  shipAreas.value = val;
}

// 打开弹窗
const openDialog = (row: RowOrderType) => {
  state.loading = true;
  state.data.shipAreaId = row.shipAreaId;
  getOrderDelivery(row.id);
  nextTick(async () => {
    if (state.data.shipAreaId && state.data.shipAreaId > 0) {
      shipAreas.value = await cascaderAreaActiveIds(state.data.shipAreaId);
    }
    state.dialog.isShowDialog = true;
  });
};

// 查询订单明细
const getOrderDelivery = (orderId: string) => {
  orderApi
    .getOrderDelivery(orderId)
    .then((result) => {
      if (result) {
        for (let i = 0; i < result.orderItems.length; i++) {
          result.orderItems[i].SendNums = result.orderItems[i].nums;
        }
        let oId = result.id;
        Object.assign(state.data, result);
        state.data.id = "";
        setTimeout(() => {
          state.data.id = oId;
        }, 500);
      }
      state.loading = false;
    })
    .catch((err: any) => {
      console.error("查询查询订单明细出错：", err);
      state.loading = false;
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
.delivery-container {
  .delivery-box .el-card__header {
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
.delivery-container {
  .delivery-box {
    .order-detail-content {
      margin: 20px 0px;
    }
  }
}
</style>
