<template>
  <div class="form-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="750px"
    >
      <el-form
        ref="formDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="5">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="表单名称" prop="name">
              <el-input
                v-model="state.ruleForm.name"
                placeholder="请输入表单名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="表单类型" prop="type">
              <el-select
                v-model="state.ruleForm.type"
                size="default"
                placeholder="请选择表单类型"
                class="select-type-item"
              >
                <el-option
                  v-for="formType in formTypes"
                  :key="formType.value"
                  :label="formType.description"
                  :value="formType.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="表头类型" prop="headType">
              <el-select
                v-model="state.ruleForm.headType"
                size="default"
                placeholder="请选择表头类型"
                class="select-type-item"
              >
                <el-option
                  v-for="formHeadType in formHeadTypes"
                  :key="formHeadType.value"
                  :label="formHeadType.description"
                  :value="formHeadType.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col
            v-show="state.ruleForm.headType !== 3"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="缩略图集" prop="images">
              <el-input
                v-model="state.ruleForm.images"
                placeholder="请输入缩略图集（后续加上传功能）"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col
            v-show="state.ruleForm.headType === 3"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="头部视频" prop="headTypeVideo">
              <el-input
                v-model="state.ruleForm.headTypeVideo"
                placeholder="请输入视频播放地址"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col
            v-show="state.ruleForm.headType === 3"
            :xs="24"
            :sm="24"
            :md="24"
            :lg="24"
            :xl="24"
            class="mb20"
          >
            <el-form-item label="视频封面" prop="headTypeValue">
              <el-input
                v-model="state.ruleForm.headTypeValue"
                placeholder="请输入视频封面（后续加上传功能）"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="表单描述" prop="brief">
              <el-input
                v-model="state.ruleForm.description"
                placeholder="请输入描述内容"
                :autosize="{ minRows: 2, maxRows: 4 }"
                type="textarea"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-card shadow="never">
              <template #header>
                <div class="form-item-tip">
                  <p class="mb5">
                    1、[字段值-默认值]填写的内容如果为多词组选择，请使用小写的逗号","进行分隔，如：<b
                      >张三,李四,王五…</b
                    >
                  </p>
                  <p class="mb5">
                    2、类型是商品时：可不输入字段值，默认值为默认下单数量。
                  </p>
                  <p class="mb5">3、类型是金额时：字段值可不填。</p>
                </div>
              </template>
              <div class="form-table-item">
                <el-table :data="state.ruleForm.formItems" border style="width: 100%">
                  <el-table-column
                    prop="name"
                    label="名称"
                    show-overflow-tooltip
                    width="150"
                  >
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">名称</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`formItems.${scope.$index}.name`"
                        :rules="[
                          { required: true, message: '名称不能为空！', trigger: 'blur' },
                        ]"
                      >
                        <el-input
                          v-model="scope.row.name"
                          size="default"
                          placeholder="请输入名称"
                          clearable
                        />
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="type"
                    label="类型"
                    show-overflow-tooltip
                    width="170"
                  >
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">类型</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`formItems.${scope.$index}.type`"
                        :rules="[
                          { required: true, message: '请选择类型！', trigger: 'change' },
                        ]"
                      >
                        <el-select
                          v-model="scope.row.type"
                          size="default"
                          placeholder="请选择表单类型"
                          class="select-type-item"
                          @change="onTypeChange(scope.$index)"
                        >
                          <el-option
                            v-for="formFieldType in formFieldTypes"
                            :key="formFieldType.value"
                            :label="formFieldType.description"
                            :value="formFieldType.value"
                          />
                        </el-select>
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="value"
                    label="字段值"
                    show-overflow-tooltip
                    width="150"
                  >
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">字段值</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`formItems.${scope.$index}.value`"
                        :rules="[
                          {
                            required: scope.row.type != 7 && scope.row.type != 8,
                            message: '请输入字段值！',
                            trigger: 'input',
                          },
                        ]"
                      >
                        <el-button
                          :icon="Handbag"
                          size="small"
                          type="success"
                          v-if="scope.row.type == 7"
                          @click="onOpenSelectGoods(scope.$index)"
                          >选择商品</el-button
                        >
                        <el-input
                          v-else
                          v-model="scope.row.value"
                          size="default"
                          placeholder="请输入字段值"
                          clearable
                        />
                        <el-tag
                          class="mt5"
                          v-show="scope.row.type == 7 && scope.row.value"
                          >{{ scope.row.value.split("¦")[1] }}</el-tag
                        >
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="validationType"
                    label="验证类型"
                    show-overflow-tooltip
                    width="170"
                  >
                    <template v-slot:header>
                      <span class="color-danger">*</span>
                      <span class="pl5">验证类型</span>
                    </template>
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`formItems.${scope.$index}.validationType`"
                        :rules="[
                          {
                            required: true,
                            message: '请选择验证类型！',
                            trigger: 'change',
                          },
                        ]"
                      >
                        <el-select
                          v-model="scope.row.validationType"
                          size="default"
                          placeholder="请选择验证类型"
                          class="select-type-item"
                        >
                          <el-option
                            v-for="formValidationType in formValidationTypes"
                            :key="formValidationType.value"
                            :label="formValidationType.description"
                            :value="formValidationType.value"
                          />
                        </el-select>
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="defaultValue"
                    label="默认值"
                    show-overflow-tooltip
                    width="150"
                  >
                    <template v-slot="scope">
                      <el-form-item label-width="0">
                        <el-input
                          v-model="scope.row.defaultValue"
                          size="default"
                          placeholder="请输入默认值"
                          clearable
                        />
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="required"
                    label="是否必填"
                    show-overflow-tooltip
                    width="100"
                  >
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`formItems.${scope.$index}.required`"
                      >
                        <el-switch
                          v-model="scope.row.required"
                          inline-prompt
                          active-text="是"
                          inactive-text="否"
                        />
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column
                    prop="sort"
                    label="排序"
                    show-overflow-tooltip
                    width="150"
                  >
                    <template v-slot="scope">
                      <el-form-item
                        label-width="0"
                        :prop="`formItems.${scope.$index}.sort`"
                      >
                        <el-input-number
                          v-model="scope.row.sort"
                          :precision="0"
                          :min="0"
                        />
                      </el-form-item>
                    </template>
                  </el-table-column>
                  <el-table-column fixed="right" label="操作" width="73">
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
          <el-col :xs="24" :sm="24" :md="24" :lg="15" :xl="24" class="mb20">
            <el-form-item label="按钮名称" prop="buttonName" label-width="110">
              <el-input
                v-model="state.ruleForm.buttonName"
                placeholder="请输入表单提交按钮名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="9" :xl="24" class="mb20">
            <el-form-item label="按钮颜色" prop="buttonColor">
              <el-input
                v-model="state.ruleForm.buttonColor"
                placeholder="请选择按钮颜色"
                readonly
                class="field-input-item"
              ></el-input>
              <el-color-picker v-model="state.ruleForm.buttonColor" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="15" :xl="24" class="mb20">
            <el-form-item label="提交后提示语" prop="returnMsg" label-width="110">
              <el-tooltip placement="bottom">
                <template #content>表单提交后给用户展示</template>
                <el-input
                  v-model="state.ruleForm.returnMsg"
                  placeholder="请输入表单提交后提示语"
                  clearable
                ></el-input>
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="9" :xl="24" class="mb20">
            <el-form-item label="结束时间" prop="endDateTime">
              <el-date-picker
                v-model="state.ruleForm.endDateTime"
                type="date"
                placeholder="请选择结束时间"
                size="default"
              />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="8" :xl="24" class="mb20">
            <el-form-item label="是否需要登录" prop="isLogin" label-width="110">
              <el-tooltip placement="bottom">
                <template #content>订单和付款码类型请一定要选择登录哦~</template>
                <el-switch
                  v-model="state.ruleForm.isLogin"
                  inline-prompt
                  active-text="开启"
                  inactive-text="关闭"
                />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="7" :xl="24" class="mb20">
            <el-form-item label="可提交次数" prop="times">
              <el-tooltip placement="bottom">
                <template #content
                  >为0时不限制用户提交次数，不为0时，请选择需要登录</template
                >
                <el-input-number v-model="state.ruleForm.times" :precision="0" :min="0" />
              </el-tooltip>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="9" :xl="24" class="mb20">
            <el-form-item label="排序" prop="sort">
              <el-tooltip placement="bottom">
                <template #content>数字越小越靠前</template>
                <el-input-number v-model="state.ruleForm.sort" :precision="0" :min="0" />
              </el-tooltip>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(formDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
    <select-goods
      selectionType="single"
      ref="selectGoodsRef"
      @selGoodsResult="onSelectGoodsResult"
    />
  </div>
</template>

<script setup lang="ts" title="formDialog">
import { defineAsyncComponent, nextTick, onMounted, reactive, ref } from "vue";
import { ElMessageBox, FormInstance, FormRules } from "element-plus";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useFormApi } from "/@/api/operationManage/form/index";
import { Handbag } from "@element-plus/icons-vue";
import { getDefaultSubObject } from "/@/utils/other";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const formApi = useFormApi();

const SelectGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);

// 定义变量内容
const formDialogFormRef = ref<FormInstance>();
const formTypes = ref([] as EnumValueType[]);
const formHeadTypes = ref([] as EnumValueType[]);
const formFieldTypes = ref([] as EnumValueType[]);
const formValidationTypes = ref([] as EnumValueType[]);
const selectGoodsRef = ref();
let selectGoodsIndex = ref(0);
const state = reactive({
  ruleForm: {
    id: null,
    name: null,
    type: null,
    sort: 0,
    images: null,
    videoPath: null,
    description: null,
    headType: null,
    headTypeValue: null,
    headTypeVideo: null,
    buttonName: null,
    buttonColor: "",
    isLogin: false,
    times: 0,
    qrcode: null,
    returnMsg: null,
    endDateTime: null,
    formItems: [] as RowFormItemType[],

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

// 表单验证规则
const formRules = reactive<FormRules>({
  name: [{ required: true, message: "请输入表单名称！", trigger: "blur" }],
  type: [{ required: true, message: "请选择表单类型！", trigger: "blur" }],
  headType: [{ required: true, message: "请选择表头类型！", trigger: "blur" }],
  description: [{ required: true, message: "请输入表单描述内容！", trigger: "blur" }],
  buttonName: [{ required: true, message: "请输入按钮名称！", trigger: "blur" }],
  buttonColor: [{ required: true, message: "请输入按钮颜色！", trigger: "blur" }],
  returnMsg: [{ required: true, message: "请输入提交后提示语！", trigger: "blur" }],
  endDateTime: [{ required: true, message: "请输入结束时间！", trigger: "blur" }],
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

// 打开弹窗
const openDialog = (type: string, row: RowFormType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  state.ruleForm.sort = 1;
  state.ruleForm.buttonColor = "#409EFF";
  nextTick(() => {
    state.dialog.editId = "";
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      state.dialog.title = "修改表单";
      state.dialog.submitTxt = "修 改";
    } else {
      state.dialog.title = "新增表单";
      state.dialog.submitTxt = "新 增";
      state.ruleForm.formItems = [] as RowFormItemType[];
      addFormItem();
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

// 添加表单项
const addFormItem = () => {
  state.ruleForm.formItems.push({
    id: "",
    tenantId: "",
    name: "",
    type: 1,
    validationType: 1,
    value: "",
    defaultValue: "",
    formId: "",
    form: null,
    required: false,
    sort: 100,
    concurrencyStamp: "",
    creationTime: "",
  });
};

// 删除表单项
const onDelFormItem = (index: number) => {
  ElMessageBox.confirm("您确定要删除表单项吗？", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    state.ruleForm.formItems.splice(index, 1);
  });
};

// 提交
const onSubmit = () => {
  if (!state.ruleForm.formItems || state.ruleForm.formItems.length < 1) {
    ElMessage.warning("您尚未添加表单字段！");
    return;
  }
  let form = _.cloneDeep(state.ruleForm) as any;
  delete form.formItems;
  let formItems = _.cloneDeep(state.ruleForm.formItems);
  if (state.dialog.type == "edit") {
    // 修改
    formApi
      .updateForm(state.dialog.editId, {
        Form: form,
        FormItems: formItems,
      })
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
  formApi
    .addForm({
      Form: form,
      FormItems: formItems,
    })
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

// 选择类型切换
const onTypeChange = (formItemIndex: number) => {
  let rowFormItemType = state.ruleForm.formItems[formItemIndex];
  rowFormItemType.value = "";
};

// 打开选择商品
const onOpenSelectGoods = (index: number) => {
  selectGoodsRef.value.openDialog("选择商品");
  selectGoodsIndex.value = index;
};

// 选择的商品返回结果
const onSelectGoodsResult = (goods: RowGoodsType) => {
  state.ruleForm.formItems[selectGoodsIndex.value].value = `${goods.id}¦${goods.name}`;
};

// 获取表单类型
const getFormTypes = () => {
  formApi
    .getFormTypes()
    .then((result) => {
      if (result) {
        formTypes.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取表单类型出错：", err);
    });
};

// 获取表头类型
const getFormHeadTypes = () => {
  formApi
    .getFormHeadTypes()
    .then((result) => {
      if (result) {
        formHeadTypes.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取表单类型出错：", err);
    });
};

// 获取表单字段类型
const getFormFieldTypes = () => {
  formApi
    .getFormFieldTypes()
    .then((result) => {
      if (result) {
        formFieldTypes.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取表单类型出错：", err);
    });
};

// 获取表单验证类型
const getFormValidationTypes = () => {
  formApi
    .getFormValidationTypes()
    .then((result) => {
      if (result) {
        formValidationTypes.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取表单类型出错：", err);
    });
};

// 页面加载完时
onMounted(() => {
  getFormTypes();
  getFormHeadTypes();
  getFormFieldTypes();
  getFormValidationTypes();
});

// 暴露变量
defineExpose({
  openDialog,
});
</script>
<style lang="scss">
.select-type-item {
  width: 100%;
}

.field-input-item {
  width: 75%;
  margin-right: 5px;
}
</style>

<style lang="scss" scoped>
.form-table-item {
  text-align: center;
}
</style>
