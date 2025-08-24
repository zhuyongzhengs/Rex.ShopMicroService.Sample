<template>
  <div class="ship-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      draggable
      width="900px"
    >
      <el-form
        ref="shipDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="95px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="配送名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入配送名称"
                maxlength="50"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="物流公司" prop="logiCode">
              <el-select
                v-model="state.ruleForm.logiCode"
                filterable
                placeholder="请选择物料公司"
                style="width: 100%"
                @change="onLogisticsChange"
              >
                <el-option
                  v-for="logistics in logisticsList"
                  :key="logistics.id"
                  :label="logistics.logiName"
                  :value="logistics.logiCode"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item
              class="label-form-item"
              label="首重"
              prop="firstUnit"
              label-position="left"
            >
              <el-select
                v-model="state.ruleForm.firstUnit"
                placeholder="请选择首重"
                style="width: 100%"
              >
                <el-option
                  v-for="shipUnit in shipUnitList"
                  :key="shipUnit.value"
                  :label="shipUnit.description"
                  :value="shipUnit.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item
              class="label-form-item"
              label="续重"
              prop="continueUnit"
              label-position="left"
            >
              <el-select
                v-model="state.ruleForm.continueUnit"
                placeholder="请选择续重"
                style="width: 100%"
              >
                <el-option
                  v-for="shipUnit in shipUnitList"
                  :key="shipUnit.value"
                  :label="shipUnit.description"
                  :value="shipUnit.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item
              class="label-form-item"
              label="首重费用"
              prop="firstunitPrice"
              label-position="left"
            >
              <el-input
                v-model="state.ruleForm.firstunitPrice"
                placeholder="请输入首重费用"
                maxlength="10"
                clearable
                @input="
                  state.ruleForm.firstunitPrice = verifyNumberIntegerAndFloat(
                    state.ruleForm.firstunitPrice
                  )
                "
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item
              class="label-form-item"
              label="续重费用"
              prop="continueunitPrice"
              label-position="left"
            >
              <el-input
                v-model="state.ruleForm.continueunitPrice"
                placeholder="请输入续重费用"
                maxlength="10"
                clearable
                @input="
                  state.ruleForm.continueunitPrice = verifyNumberIntegerAndFloat(
                    state.ruleForm.continueunitPrice
                  )
                "
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="商品满多少" prop="goodsMoney">
              <el-input
                v-model="state.ruleForm.goodsMoney"
                placeholder="请输入商品满多少免运费"
                maxlength="50"
                clearable
                style="width: 100px"
                @input="
                  state.ruleForm.goodsMoney = verifyNumberIntegerAndFloat(
                    state.ruleForm.goodsMoney
                  )
                "
              ></el-input>
              <div class="form-item-tips">免运费(此项大于0时参与计算)</div>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-input-number
                v-model="state.ruleForm.sort"
                :precision="0"
                :min="0"
                style="width: 140px"
              />
              <div class="form-item-tips">越小越靠前</div>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
            <el-form-item label="是否包邮" prop="isfreePostage">
              <el-switch
                v-model="state.ruleForm.isfreePostage"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
            <el-form-item label="是否默认" prop="isDefault">
              <el-switch
                v-model="state.ruleForm.isDefault"
                inline-prompt
                active-text="是"
                inactive-text="否"
                size="default"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="4" :xl="24" class="mb20">
            <el-form-item label="按地区配置" prop="areaFee">
              <el-switch
                v-model="state.ruleForm.isdefaultAreaFee"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
                size="default"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="状态" prop="status">
              <el-radio-group v-model="state.ruleForm.status">
                <el-radio :label="1" size="default">启用</el-radio>
                <el-radio :label="2" size="default">禁用</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="10" :xl="24" class="mb20">
            <el-form-item label="地区类型" prop="areaType">
              <el-radio-group v-model="state.ruleForm.areaType">
                <el-radio size="default" :label="1">所有地区</el-radio>
                <el-radio size="default" :label="2">部分地区</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="9" :xl="24" class="mb20">
            <div
              class="form-item-tips"
              style="position: relative; left: -80px; bottom: 0px"
            >
              设置部分地区后，未配置地区采用默认配置
            </div>
          </el-col>
          <el-col
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
            v-show="state.ruleForm.areaType == 2"
          >
            <el-card shadow="never">
              <div class="form-table-item">
                <el-table :data="state.ruleForm.areaFees" border style="width: 100%">
                  <el-table-column prop="areas" label="选择地区" show-overflow-tooltip>
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">选择地区</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`areaFees.${scope.$index}.displayArea`"
                        :rules="[
                          { required: true, message: '地区不能为空！', trigger: 'blur' },
                        ]"
                      >
                        <el-cascader
                          ref="cascaderAreaRef"
                          v-model="scope.row.displayArea"
                          :props="areaProp"
                          placeholder="请选择区域"
                          clearable
                          class="w100"
                          filterable
                          collapse-tags
                          collapse-tags-tooltip
                          separator=" - "
                          @change="goodAreaChange($event, scope.$index)"
                        >
                          <template #default="{ node, data }">
                            <span>{{ data.name }}</span>
                            <span v-if="!node.isLeaf && node.children?.length > 0">
                              ({{ node.children.length }})
                            </span>
                          </template>
                        </el-cascader>
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="firstunitPrice"
                    label="首重费用"
                    show-overflow-tooltip
                    width="150"
                  >
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">首重费用</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`areaFees.${scope.$index}.areaFirstunitPrice`"
                        :rules="[
                          {
                            required: true,
                            message: '首重费用不能为空！',
                            trigger: 'blur',
                          },
                        ]"
                      >
                        <el-input
                          v-model="scope.row.areaFirstunitPrice"
                          size="default"
                          placeholder="请输入首重费用"
                          maxlength="10"
                          clearable
                          @input="
                            scope.row.areaFirstunitPrice = verifyNumberIntegerAndFloat(
                              scope.row.areaFirstunitPrice
                            )
                          "
                        />
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="continueunitPrice"
                    label="续重费用"
                    show-overflow-tooltip
                    width="150"
                  >
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">续重费用</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`areaFees.${scope.$index}.areaContinueunitPrice`"
                        :rules="[
                          {
                            required: true,
                            message: '续重费用不能为空！',
                            trigger: 'blur',
                          },
                        ]"
                      >
                        <el-input
                          v-model="scope.row.areaContinueunitPrice"
                          size="default"
                          placeholder="请输入续重费用"
                          maxlength="10"
                          clearable
                          @input="
                            scope.row.areaContinueunitPrice = verifyNumberIntegerAndFloat(
                              scope.row.areaContinueunitPrice
                            )
                          "
                        />
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column fixed="right" label="操作" width="75">
                    <template v-slot="scope">
                      <el-button
                        size="small"
                        type="danger"
                        @click="onDelFormItem(scope.$index)"
                        >删除</el-button
                      >
                    </template>
                  </el-table-column>
                </el-table>
                <el-button size="small" type="success" class="mt10" @click="addFormItem">
                  <el-icon>
                    <ele-Plus />
                  </el-icon>
                  添加行
                </el-button>
              </div>
            </el-card>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(shipDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="shipDialog">
import { nextTick, reactive, ref, defineExpose, onMounted } from "vue";
import type { FormInstance, CascaderNode, FormRules } from "element-plus";
import { ElMessage, ElMessageBox } from "element-plus";
import _ from "lodash";
import { useShipApi } from "/@/api/shopSetting/ship/index";
import { useLogisticsApi } from "/@/api/shopSetting/logistics/index";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";
import { verifyNumberIntegerAndFloat } from "/@/utils/toolsValidate";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入配送方式 Api 请求接口
const shipApi = useShipApi();
// 引入物流 Api 请求接口
const logisticsApi = useLogisticsApi();
const areaApi = useGoodAreaApi();

// 定义变量内容
const shipDialogFormRef = ref();
const cascaderAreaRef = ref<any>();
const logisticsList = ref<RowLogisticsType[]>();
const shipUnitList = ref<EnumValueType[]>();
const areaProp = ref({
  lazy: true,
  checkStrictly: false,
  multiple: true,
  value: "id",
  label: "name",
  lazyLoad(node: CascaderNode, resolve: Function) {
    loadTreeAreaNode(node, (result: AreaTreeType[]) => {
      resolve(result);
    });
  },
});
const state = reactive({
  ruleForm: {
    name: "", // 配送名称
    isCashOnDelivery: false, // 是否货到付款
    firstUnit: null, // 首重
    continueUnit: null, // 续重
    isdefaultAreaFee: false, // 是否按地区设置配送费用
    areaType: 1, // 地区类型
    firstunitPrice: "10", // 首重费用
    continueunitPrice: "5", // 续重费用
    exp: "", // 配送费用计算表达式
    logiName: "", // 物流公司名称
    logiCode: "", // 物流公司编码
    isDefault: false, // 是否默认,
    sort: 0, // 排序
    status: 2, // 状态 ==> 1：启用、2：禁用
    isfreePostage: false, // 是否包邮
    areaFees: [] as RowAreaFeeType[], // 地区配送费用
    goodsMoney: "50", // 商品总额满多少免运费
    concurrencyStamp: "",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    editId: "",
    title: "",
    submitTxt: "",
  },
});

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
  loadGoodAreaTree(parentId, depth, null, (result: AreaTreeType[]) => {
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

// 商品区域切换
function goodAreaChange(val: [], index: number): void {
  if (!val || val.length < 1) {
    state.ruleForm.areaFees[index].displayArea = [];
    state.ruleForm.areaFees[index].areas = "";
    return;
  }
  state.ruleForm.areaFees[index].displayArea = val;
  state.ruleForm.areaFees[index].areas = JSON.stringify(val);
}

// 表单验证规则
const formRules = reactive<FormRules>({
  name: [{ required: true, message: "请输入配送名称！", trigger: "blur" }],
  logiCode: [{ required: true, message: "请选择物料公司！", trigger: "blur" }],
  firstUnit: [{ required: true, message: "请选择首重！", trigger: "blur" }],
  continueUnit: [{ required: true, message: "请选择续重！", trigger: "blur" }],
  firstunitPrice: [{ required: true, message: "请输入首重费用！", trigger: "blur" }],
  continueunitPrice: [{ required: true, message: "请输入续重费用！", trigger: "blur" }],
  goodsMoney: [{ required: true, message: "请输入商品满多少免运费！", trigger: "blur" }],
});

// 提交验证
const submitValidate = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  await formEl.validate((valid, fields) => {
    if (valid) {
      if (state.ruleForm.areaType == 2 && state.ruleForm.areaFees.length < 1) {
        ElMessage.warning("未添加部分区域费用！");
        return;
      }
      onSubmit();
    } else {
      console.warn("未验证通过!", fields);
    }
  });
};

// 打开弹窗
const openDialog = (type: string, row: RowShipType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.areaType = 1;
  state.ruleForm.firstunitPrice = "10";
  state.ruleForm.continueunitPrice = "5";
  state.ruleForm.status = 2;
  state.ruleForm.goodsMoney = "50";
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改配送方式";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增配送方式";
      state.dialog.submitTxt = "新 增";
    }
    state.dialog.type = type;
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

// 提交
const onSubmit = () => {
  if (state.ruleForm.areaType == 1) state.ruleForm.areaFees = [];
  if (state.dialog.type == "edit") {
    // 修改
    shipApi
      .updateShip(state.dialog.editId, state.ruleForm)
      .then((result) => {
        if (result) {
          ElMessage.success("修改成功！");
          closeDialog(); // 关闭弹窗
          emit("refresh");
        }
      })
      .catch((err: any) => {
        console.error("提交出错：", err);
      });
    return;
  }

  // 添加
  shipApi
    .addShip(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("添加成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 添加表单项
const addFormItem = () => {
  state.ruleForm.areaFees.push({
    areas: "",
    displayArea: [],
    areaFirstunitPrice: "",
    areaContinueunitPrice: "",
  });
};

// 删除表单项
const onDelFormItem = (index: number) => {
  ElMessageBox.confirm("您确定要删除该项吗？", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    state.ruleForm.areaFees.splice(index, 1);
  });
};

// 获取物流列表
const getLogisticsList = () => {
  logisticsApi
    .searchLogisticsList({
      search: "",
    })
    .then((result) => {
      if (result) {
        logisticsList.value = result;
      }
    })
    .catch((err: any) => {
      console.error("查询物流列表出错：", err);
    });
};

// 获取配送单位列表
const getShipUnitList = () => {
  shipApi
    .getShipUnitList()
    .then((result) => {
      if (result) {
        shipUnitList.value = result;
      }
    })
    .catch((err: any) => {
      console.error("查询物流列表出错：", err);
    });
};

// 物料下拉切换
const onLogisticsChange = (value: string) => {
  const logistics = logisticsList.value?.find((item) => item.logiCode === value);
  if (logistics) {
    state.ruleForm.logiName = logistics.logiName;
  }
};

onMounted(() => {
  // 获取物流列表
  getLogisticsList();

  // 获取配送单位列表
  getShipUnitList();
});

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style lang="scss">
.label-form-item .el-form-item__label {
  background-color: #f5f7fa;
}

.form-item-tips {
  color: #999;
  margin-left: 10px;
  height: 32px;
  line-height: 32px;
  text-align: left;
  border-bottom: 1px dashed #999;
}

.form-table-item {
  text-align: center;
}
</style>
