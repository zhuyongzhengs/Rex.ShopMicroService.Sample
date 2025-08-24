<template>
  <div class="ship-detail-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      center
      draggable
      width="650px"
    >
      <div class="ship-detail-box">
        <el-form
          ref="shipDialogFormRef"
          :model="state.data"
          :rules="formRules"
          size="default"
          label-width="100px"
        >
          <el-row class="mb20">
            <el-col :span="12">
              <el-form-item label="订单号">
                <el-input v-model="state.data.no" readonly disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="订单总额">
                <el-input v-model="state.data.orderAmount" readonly disabled></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mb20">
            <el-col :span="12">
              <el-form-item label="收货人姓名" prop="shipName">
                <el-input
                  v-model="state.data.shipName"
                  placeholder="请输入收货人姓名"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="收货人电话" prop="shipMobile">
                <el-input
                  v-model="state.data.shipMobile"
                  placeholder="请输入收货人电话"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row class="mb20" v-if="state.data.id">
            <el-col :span="24">
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
        </el-form>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button
            type="primary"
            @click="submitValidate(shipDialogFormRef)"
            size="default"
            >确 认</el-button
          >
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="shipDetail">
import { nextTick, ref, reactive, defineExpose } from "vue";
import { ElMessage } from "element-plus";
import type { FormInstance, CascaderNode, FormRules } from "element-plus";
import _ from "lodash";
import { cascaderAreaActiveIds } from "/@/utils/other";
import { useOrderApi } from "/@/api/orderManage/order/index";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";

// 引入 Api 请求接口
const orderApi = useOrderApi();
const areaApi = useGoodAreaApi();

// 定义子组件向父组件传值/事件
const emit = defineEmits(["success"]);

// 定义变量
const cascaderAreaRef = ref<any>();
const shipDialogFormRef = ref<any>();

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
  data: {
    id: "",
    no: "",
    orderAmount: 0,
    shipAreaId: 0,
    shipName: "",
    shipMobile: "",
    displayArea: "",
    shipAddress: "",
  },
  dialog: {
    isShowDialog: false,
    title: "编辑收货地址",
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
  let orderShip = _.cloneDeep(state.data) as any;
  delete orderShip.id;
  delete orderShip.no;
  delete orderShip.orderAmount;
  orderApi
    .updateOrderShip(state.data.id, orderShip)
    .then((result) => {
      console.log("修改收货地址成功！", result);
      emit("success");
      ElMessage.success("修改收货地址成功！");
      onCancel();
    })
    .catch((err: any) => {
      console.error("修改收货地址出错：", err);
    });
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
  state.data.id = row.id;
  state.loading = true;
  state.data.no = row.no;
  state.data.orderAmount = row.orderAmount;
  state.data.shipAreaId = row.shipAreaId;
  state.data.shipName = row.shipName;
  state.data.shipMobile = row.shipMobile;
  state.data.shipAddress = row.shipAddress;
  state.data.displayArea = row.displayArea;
  nextTick(async () => {
    if (state.data.shipAreaId && state.data.shipAreaId > 0) {
      shipAreas.value = await cascaderAreaActiveIds(state.data.shipAreaId);
    }
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
.ship-detail-container {
  .ship-detail-tabs .el-card__header {
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
.ship-detail-container {
  .ship-detail-box {
  }
}
</style>
