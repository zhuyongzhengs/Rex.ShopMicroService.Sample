<template>
  <div class="good-comment-container">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="750px"
      center
    >
      <div class="dialog-loading-box" v-if="!state.data || !state.data.id">加载中...</div>
      <el-form
        v-else
        ref="commentDialogFormRef"
        :model="state.data"
        :rules="formRules"
        size="default"
        label-width="90px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
            <table cellpadding="0" cellspacing="0" class="comment-detail-table">
              <tr>
                <td colspan="4" style="text-align: center; vertical-align: middle">
                  <el-row>
                    <el-col :span="24">
                      <el-avatar :size="50" :src="state.data.avatar" />
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="24">
                      {{ state.data.userName }}『{{ state.data.phoneNumber }}』
                    </el-col>
                  </el-row>
                </td>
              </tr>
              <tr>
                <th>商品名称</th>
                <td colspan="3">{{ state.data.goodName }}</td>
              </tr>
              <tr>
                <th>订单号</th>
                <td>{{ state.data.orderNo }}</td>
                <th>货品规格</th>
                <td>{{ state.data.addon }}</td>
              </tr>
              <tr>
                <th>收货人</th>
                <td>{{ state.data.shipName }}</td>
                <th>收货人电话</th>
                <td>{{ state.data.shipMobile }}</td>
              </tr>
              <tr>
                <th>评价星级</th>
                <td>
                  <el-rate v-model="state.data.score" disabled />
                </td>
                <th>是否前台显示</th>
                <td>
                  <el-switch
                    v-model="state.data.isDisplay"
                    inline-prompt
                    active-text="开启"
                    inactive-text="关闭"
                    size="default"
                    disabled
                  />
                </td>
              </tr>
              <tr>
                <th>评价图片</th>
                <td colspan="3">
                  <el-text type="info" v-if="!state.data.images">无</el-text>
                  <template
                    v-else
                    v-for="(url, index) in state.data.images.split(';')"
                    :key="index"
                  >
                    <el-image
                      class="comment-image"
                      :src="url"
                      fit="fit"
                      :preview-src-list="state.data.images.split(';')"
                    />
                  </template>
                </td>
              </tr>
              <tr>
                <th>评价内容</th>
                <td colspan="3">{{ state.data.contentBody }}</td>
              </tr>
              <tr>
                <th>评价日期</th>
                <td colspan="3">{{ state.data.creationTime }}</td>
              </tr>
            </table>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mt10">
            <el-form-item label="商家回复" prop="sellerContent" label-position="top">
              <el-input
                type="textarea"
                v-model="state.data.sellerContent"
                :placeholder="state.dialog.isSellerContent ? '请输入回复内容' : ''"
                :rows="2"
                :disabled="!state.dialog.isSellerContent"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            v-if="state.dialog.isSellerContent"
            type="primary"
            @click="submitValidate(commentDialogFormRef)"
            size="default"
            >确 认</el-button
          >
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="goodCommentDialog">
import { nextTick, reactive, ref } from "vue";
import type { FormInstance, FormRules } from "element-plus";
import { ElInput, ElMessage } from "element-plus";
import _ from "lodash";
import { useGoodCommentApi } from "/@/api/good/comment/index";

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入用户 Api 请求接口
const commentApi = useGoodCommentApi();

// 定义变量内容
const commentDialogFormRef = ref();
const state = reactive({
  data: {} as RowGoodCommentType,
  dialog: {
    isShowDialog: false,
    isSellerContent: false,
    editId: "",
    title: "",
  },
});

// 表单验证规则
const formRules = reactive<FormRules>({
  sellerContent: [{ required: true, message: "请输入回复内容！", trigger: "blur" }],
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
const openDialog = (title: string, row: RowGoodCommentType) => {
  state.data = row;
  state.dialog.isSellerContent = title === "商家回复";
  nextTick(() => {
    state.dialog.editId = row.id;
    state.dialog.title = title;
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
  commentApi
    .updateSellerReply(state.dialog.editId, state.data.sellerContent)
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
};

// 暴露变量
defineExpose({
  openDialog,
});
</script>

<style scoped lang="scss">
.good-comment-container {
  .dialog-loading-box {
    width: 100%;
    text-align: center;
    margin: 10px auto;
  }
  .comment-detail-table {
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
        vertical-align: middle;
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
  .comment-image {
    width: 100px;
    height: 100px;
    margin: 5px;
  }
}
</style>
