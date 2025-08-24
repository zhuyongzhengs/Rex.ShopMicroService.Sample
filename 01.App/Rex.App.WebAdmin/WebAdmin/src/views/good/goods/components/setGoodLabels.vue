<template>
  <div class="good-label-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="500px"
    >
      <el-form
        ref="labelDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="80px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-card shadow="never">
              <template #header>
                <div class="label-title">选择标签</div>
              </template>
              <div class="select-content">
                <div class="sel-tag-item mb20">
                  <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                    <el-form-item label-width="0">
                      <el-tag
                        effect="dark"
                        size="large"
                        type="danger"
                        class="tag-label"
                        @click="addLabel('热卖', 'danger')"
                        >热卖</el-tag
                      >
                      <el-tag
                        effect="dark"
                        size="large"
                        type="success"
                        class="tag-label"
                        @click="addLabel('新品', 'success')"
                        >新品</el-tag
                      >
                      <el-tag
                        effect="dark"
                        size="large"
                        type="warning"
                        class="tag-label"
                        @click="addLabel('推荐', 'warning')"
                        >推荐</el-tag
                      >
                      <el-tag
                        effect="dark"
                        size="large"
                        type="primary"
                        class="tag-label"
                        @click="addLabel('促销', 'primary')"
                        >促销</el-tag
                      >
                    </el-form-item>
                  </el-col>
                </div>
                <div class="sel-input-item">
                  <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
                    <el-row>
                      <el-col :span="16">
                        <el-form-item label-width="0">
                          <el-input
                            v-model="state.newLabel.name"
                            placeholder="请输入标签名称"
                            clearable
                            maxlength="4"
                          ></el-input>
                        </el-form-item>
                      </el-col>
                      <el-col :span="6">
                        <el-form-item label-width="0" class="ml5 mr5">
                          <el-select
                            size="default"
                            v-model="state.newLabel.style"
                            placeholder="颜色"
                          >
                            <el-option label="红色" value="danger" />
                            <el-option label="绿色" value="success" />
                            <el-option label="黄色" value="warning" />
                            <el-option label="蓝色" value="primary" />
                          </el-select>
                        </el-form-item>
                      </el-col>
                      <el-col :span="2">
                        <el-form-item label-width="0">
                          <el-button
                            type="primary"
                            :icon="Plus"
                            circle
                            @click="inputLabel"
                          />
                        </el-form-item>
                      </el-col>
                    </el-row>
                  </el-col>
                </div>
              </div>
            </el-card>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <el-card shadow="never">
              <template #header>
                <div class="head-label">已选标签</div>
              </template>
              <div class="selected-content">
                <el-form-item label-width="0" prop="label">
                  <el-tag
                    size="large"
                    class="mr10"
                    effect="dark"
                    v-for="(label, index) in state.ruleForm.labels"
                    :type="label.style"
                    :key="index"
                    closable
                    @close="removeLabel(index)"
                  >
                    {{ label.name }}
                  </el-tag>
                  <div class="not-label-item" v-show="state.ruleForm.labels.length < 1">
                    无.
                  </div>
                </el-form-item>
              </div>
            </el-card>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <div class="sel-data">
            <label>当前选中&nbsp;{{ state.goodItem.length }}&nbsp;条数据</label>
          </div>
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(labelDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="goodLabelDialog">
import { nextTick, onMounted, reactive, ref } from "vue";
import { ElLoading, FormInstance, FormRules } from "element-plus";
import { Plus } from "@element-plus/icons-vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodsApi } from "/@/api/good/goods/index";
import { useGoodLabelApi } from "/@/api/good/label/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const goodsApi = useGoodsApi();
const goodLabelApi = useGoodLabelApi();

// 定义变量内容
const labelDialogFormRef = ref();
const state = reactive({
  goodItem: [] as RowGoodsType[],
  newLabel: {
    name: "",
    style: "danger",
  },
  ruleForm: {
    goodIds: new Array(),
    labels: [] as any,
  },
  dialog: {
    isShowDialog: false,
    title: "",
    submitTxt: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  /*
  label: [
    {
      validator: (rule: any, value: any, callback: any) => {
        if (!state.ruleForm.labels || state.ruleForm.labels.length < 1) {
          callback(new Error("请选择标签！"));
          return;
        }
        callback();
      },
      trigger: "blur",
    },
  ],
  */
});

// 添加标签
const addLabel = (name: string, style: string) => {
  // 验证是否包含重复
  let names: [] = state.ruleForm.labels.map((p: any) => p.name);
  for (let i = 0; i < names.length; i++) {
    const labelName = names[i];
    if (name == labelName) {
      ElMessage.warning(`[${name}]标签已存在！`);
      return false;
    }
  }
  state.ruleForm.labels.push({
    name,
    style,
  });
  return true;
};

// 输入标签
const inputLabel = () => {
  if (!state.newLabel.name || !state.newLabel.style) return;
  let addResutl = addLabel(state.newLabel.name, state.newLabel.style);
  if (addResutl) {
    state.newLabel.name = "";
    // state.newLabel.style = "";
  }
};

// 移除标签
const removeLabel = (index: number) => {
  state.ruleForm.labels.splice(index, 1);
};

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
const openDialog = (rows: RowGoodsType[]) => {
  reset();
  state.goodItem = rows;
  getGoodLabels();
  nextTick(() => {
    state.dialog.title = "设置标签";
    state.dialog.submitTxt = "确 定";
    state.dialog.isShowDialog = true;
    state.ruleForm.goodIds = state.goodItem.map((p) => p.id);
  });
};

// 重置
const reset = () => {
  state.goodItem = [];
  state.ruleForm.goodIds = new Array();
  state.ruleForm.labels = new Array();
  state.newLabel.name = "";
  state.newLabel.style = "danger";
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
  goodsApi
    .updateLabelMany(state.ruleForm)
    .then((result) => {
      if (result) {
        ElMessage.success("标签设置成功！");
        closeDialog(); // 关闭弹窗
        emit("refresh");
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 查询选中的标签
const getGoodLabels = () => {
  let labelIds = new Array();
  for (let i = 0; i < state.goodItem.length; i++) {
    const good = state.goodItem[i];
    if (!good.labelIds) continue;
    let ids = good.labelIds.split(",");
    labelIds.push(...ids);
  }
  if (labelIds.length < 1) return;
  labelIds = [...new Set(labelIds)];

  const getLabelLoading = ElLoading.service({
    lock: true,
    text: "加载中，请稍等！",
    background: "rgba(0, 0, 0, 0.1)",
  });
  goodLabelApi
    .getLabelByIds(labelIds)
    .then((result) => {
      if (result) {
        state.ruleForm.labels = [];
        result.forEach((label) => {
          state.ruleForm.labels.push({
            name: label.name,
            style: label.style,
          });
        });
      }
      getLabelLoading.close();
    })
    .catch((err: any) => {
      getLabelLoading.close();
      console.error("查询标签出错：", err);
    });
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

<style lang="scss">
.good-label-container .el-card__header {
  padding: 10px !important;
  background-color: #f5f7fa;
}
</style>

<style lang="scss" scoped>
.select-content {
  position: relative;
  left: -22px;
  padding: 0px;
  text-align: left;
  .tag-label {
    margin-right: 30px;
    cursor: pointer;
  }
}

.select-content .tag-label:last-child {
  margin-right: 0px;
}

.not-label-item {
  width: 100%;
  text-align: center;
  color: #dcdfe6;
}

.dialog-footer {
  .sel-data {
    float: left;
    height: 32px;
    line-height: 32px;
  }
}
</style>
