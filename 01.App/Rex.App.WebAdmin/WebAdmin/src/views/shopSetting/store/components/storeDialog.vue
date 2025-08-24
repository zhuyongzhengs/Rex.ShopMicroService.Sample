<template>
  <div class="good-store-container" v-if="state.dialog.isShowDialog">
    <el-dialog
      :title="state.dialog.title"
      v-model="state.dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      width="800px"
    >
      <el-form
        ref="storeDialogFormRef"
        :model="state.ruleForm"
        :rules="formRules"
        size="default"
        label-width="110px"
      >
        <el-row :gutter="35">
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="门店Logo" prop="logoImage">
              <el-input
                v-model="state.ruleForm.logoImage"
                :readonly="true"
                placeholder="请选择门店Logo"
                maxlength="100"
              >
                <template #prepend>
                  <el-button
                    :icon="Picture"
                    title="预览"
                    @click="imageViewer.show = true"
                  />
                </template>
                <template #append>
                  <el-upload
                    class="upload-image-box"
                    accept=".jpg, .jpeg, .png, .gif"
                    :action="uploadFileConfig().action"
                    :headers="uploadFileConfig().headers"
                    :multiple="false"
                    :show-file-list="false"
                    :on-success="handleImageSuccess"
                  >
                    <template #trigger
                      ><el-button
                        type="primary"
                        plain
                        size="default"
                        title="上传"
                        :icon="Upload"
                      ></el-button
                    ></template>
                  </el-upload>
                </template>
              </el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="门店名称" prop="storeName">
              <el-input
                v-model="state.ruleForm.storeName"
                placeholder="请输入门店名称"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="联系人" prop="linkMan">
              <el-input
                v-model="state.ruleForm.linkMan"
                placeholder="请输入联系人"
                maxlength="50"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="门店手机号码" prop="mobile">
              <el-input
                v-model="state.ruleForm.mobile"
                placeholder="请输入门店电话/手机号码"
                clearable
                maxlength="12"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="门店区域" prop="areaId">
              <el-cascader
                ref="cascaderAreaRef"
                :props="areaCascaderProp.areaProp"
                placeholder="请选择区域"
                clearable
                class="w100"
                filterable
                separator=" - "
                v-model="areaCascaderProp.current"
                @change="goodAreaChange"
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
          <el-col :xs="24" :sm="24" :md="24" :lg="12" :xl="24" class="mb20">
            <el-form-item label="坐标位置" prop="coordinate">
              <el-input
                v-model="state.ruleForm.coordinate"
                maxlength="50"
                readonly
                clearable
              >
                <template #append>
                  <el-button
                    type="primary"
                    plain
                    size="default"
                    @click="onOpenMapSelector"
                    :icon="Location"
                  ></el-button>
                </template>
              </el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="门店详细地址" prop="address">
              <el-input
                v-model="state.ruleForm.address"
                placeholder="请输入门店详细地址"
                maxlength="50"
                clearable
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
            <el-form-item label="是否默认" prop="isDefault">
              <el-switch
                v-model="state.ruleForm.isDefault"
                inline-prompt
                active-text="开启"
                inactive-text="关闭"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
          <el-button
            type="primary"
            @click="submitValidate(storeDialogFormRef)"
            size="default"
            >{{ state.dialog.submitTxt }}</el-button
          >
        </span>
      </template>
    </el-dialog>

    <el-image-viewer
      v-if="imageViewer.show"
      :url-list="imageViewer.list"
      show-progress
      :initial-index="imageViewer.index"
      @close="imageViewer.show = false"
    />

    <t-map-selector ref="tMapSelectorRef" @confirm="tMapConfirm" />
  </div>
</template>

<script setup lang="ts" name="storeDialog">
import { nextTick, onMounted, reactive, ref, defineAsyncComponent } from "vue";
import type { FormInstance, FormRules, CascaderProps, UploadProps } from "element-plus";
import { ElMessage } from "element-plus";
import { Picture, Upload, Location } from "@element-plus/icons-vue";
import _ from "lodash";
import { useGoodStoreApi } from "/@/api/shopSetting/store/index";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";
import { cascaderAreaActiveIds, getDefaultSubObject } from "/@/utils/other";
import { uploadFileConfig } from "/@/utils/other";

// 引入组件
const tMapSelector = defineAsyncComponent(
  () => import("/@/components/tMapSelector/index.vue")
);

// 定义子组件向父组件传值/事件
const emit = defineEmits(["refresh"]);

// 引入门店 Api 请求接口
const storeApi = useGoodStoreApi();

// 定义变量内容
const imageViewer = ref({
  show: false,
  index: 0,
  list: [] as string[],
});
const tMapSelectorRef = ref();
const storeDialogFormRef = ref();
const cascaderAreaRef = ref();
const areaCascaderProp = ref({
  current: [] as number[],
  areaProp: {
    checkStrictly: true,
    value: "id",
    label: "name",
    lazy: true,
    lazyLoad(node, resolve) {
      let parentId = 0;
      let depth: number | null = null;
      if (node.level > 0) {
        parentId = Number(node.data?.id);
        depth = Number(node.data?.depth) + 1;
      }
      // 获取节点数据
      loadGoodAreaTree(
        parentId,
        depth,
        state.ruleForm.areaId,
        (result: AreaTreeType[]) => {
          resolve(result);
        }
      );
    },
  } as CascaderProps,
});

const state = reactive({
  ruleForm: {
    storeName: null,
    mobile: null,
    linkMan: null,
    logoImage: "",
    areaId: null,
    address: null,
    coordinate: "",
    latitude: "",
    longitude: "",
    isDefault: false,
    distance: 0,

    concurrencyStamp: "",
  },
  dialog: {
    isShowDialog: false,
    type: "",
    editId: 0,
    title: "",
    submitTxt: "",
  },
});

// 打开地图选择
const onOpenMapSelector = () => {
  let lat = state.ruleForm.latitude ?? null;
  let lng = state.ruleForm.longitude ?? null;
  tMapSelectorRef.value.openDialog("选择地图坐标", lat, lng);
};

// 地图选择确认
const tMapConfirm = (latLng: any) => {
  state.ruleForm.coordinate = `${latLng.lat},${latLng.lng}`;
  state.ruleForm.latitude = latLng.lat;
  state.ruleForm.longitude = latLng.lng;
};

// 图片上传成功回调
const handleImageSuccess: UploadProps["onSuccess"] = (result: string, uploadFile) => {
  if (!result) return;
  state.ruleForm.logoImage = result;
  imageViewer.value.list = [result];
};

// 商品分类切换
function goodAreaChange(val: []): void {
  if (!val || val.length < 1) {
    state.ruleForm.areaId = null;
    return;
  }
  state.ruleForm.areaId = val[val.length - 1];
  areaCascaderProp.value.current = val;
}

// 表单验证规则
const formRules = reactive<FormRules>({
  logoImage: [{ required: true, message: "请上传门店Logo！", trigger: "change" }],
  storeName: [{ required: true, message: "请输入门店名称！", trigger: "blur" }],
  linkMan: [{ required: true, message: "请输入联系人！", trigger: "blur" }],
  mobile: [{ required: true, message: "请输入门店手机号码！", trigger: "blur" }],
  areaId: [{ required: true, message: "请输入门店区域！", trigger: "blur" }],
  coordinate: [{ required: true, message: "请输入坐标位置！", trigger: "change" }],
  address: [{ required: true, message: "请输入门店详细地址！", trigger: "blur" }],
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
const openDialog = (type: string, row: RowStoreType) => {
  state.ruleForm = getDefaultSubObject(state.ruleForm);
  nextTick(async () => {
    if (type === "edit") {
      state.dialog.editId = row.id;
      Object.assign(state.ruleForm, row);
      imageViewer.value.list = [state.ruleForm.logoImage];
      state.dialog.title = "修改门店";
      state.dialog.submitTxt = "修 改";
      if (state.ruleForm.areaId && state.ruleForm.areaId > 0) {
        areaCascaderProp.value.current = await cascaderAreaActiveIds(
          state.ruleForm.areaId
        );
      }
    } else {
      state.dialog.title = "新增门店";
      state.dialog.submitTxt = "新 增";
      state.dialog.editId = 0;
      areaCascaderProp.value.current = [];
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
  if (state.ruleForm.coordinate) {
    state.ruleForm.latitude = state.ruleForm.coordinate.split(",")[0];
    state.ruleForm.longitude = state.ruleForm.coordinate.split(",")[1];
  }
  if (state.dialog.type == "edit") {
    // 修改
    storeApi
      .updateGoodStore(state.dialog.editId, state.ruleForm)
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
  storeApi
    .addGoodStore(state.ruleForm)
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

// 加载区域树数据
const loadGoodAreaTree = (
  parentId: number,
  depth: number | null = null,
  activeId: number | null = null,
  callback: Function
) => {
  useGoodAreaApi()
    .loadGoodAreaTree(parentId, depth, activeId)
    .then((result) => {
      callback(result);
    })
    .catch((err: any) => {
      console.error("查询区域树出错：", err);
    });
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  // 获取区域信息
});
</script>
