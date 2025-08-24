<template>
  <div class="select-map-container layout-padding">
    <el-dialog
      :title="dialog.title"
      v-model="dialog.isShowDialog"
      :close-on-click-modal="false"
      draggable
      center
      width="1000px"
    >
      <el-row :gutter="15">
        <el-col :span="8">
          <el-card shadow="never" class="mb15">
            <el-row :gutter="12">
              <el-col :span="16">
                <el-input
                  size="default"
                  maxlength="100"
                  placeholder="请输入详细地址"
                  clearable
                  v-model="tMapSearch.keyword"
                  @keyup.enter="onSearch"
                />
              </el-col>
              <el-col :span="4">
                <el-button size="default" type="primary" @click="onSearch">
                  <el-icon>
                    <ele-Search />
                  </el-icon>
                  查询
                </el-button>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <div class="address-list-box" v-loading="tMapSearch.loading">
                  <template v-for="(item, index) in tMapData" :key="index">
                    <div
                      class="address-list-item"
                      :class="item.active ? 'address-list-active' : ''"
                      @click="tMapActive(index)"
                    >
                      {{ item.address }}
                    </div>
                  </template>
                  <div class="address-not-data" v-if="!tMapData || tMapData.length === 0">
                    无搜索记录
                  </div>
                </div>
              </el-col>
            </el-row>
          </el-card>
        </el-col>
        <el-col :span="16">
          <el-card shadow="never" class="layout-padding-auto">
            <div class="map-container" v-loading="!tmapRef">
              <template v-if="tMapConfig.mapKey">
                <tmap-map
                  ref="tmapRef"
                  :mapKey="tMapConfig.mapKey"
                  :events="tMapEvents"
                  :center="tMapConfig.center"
                  :zoom="tMapConfig.zoom"
                  :doubleClickZoom="tMapConfig.doubleClickZoom"
                  :control="tMapConfig.control"
                >
                  <tmap-multi-marker
                    :styles="tMapMultiMarker.styles"
                    :geometries="tMapMultiMarker.geometries"
                  />
                </tmap-map>
              </template>
            </div>
            <p class="map-marker-text">
              {{ tMapConfig.center.lat }},{{ tMapConfig.center.lng }}
            </p>
          </el-card>
        </el-col>
      </el-row>

      <template #footer>
        <span class="dialog-footer">
          <el-button type="primary" size="default" @click="onConfirm">确 定</el-button>
          &nbsp;&nbsp;
          <el-button size="default" @click="onCancel">取 消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="tMapSelector">
import { reactive, onMounted, ref, nextTick } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { jsonp } from "vue-jsonp";
import { usePlatformSettingApi } from "/@/api/shopSetting/platformSetting/index";

const emit = defineEmits(["confirm"]);
const tmapRef = ref();
const tMapConfig = reactive({
  mapKey: "",
  center: {
    lat: 39.98481500648338,
    lng: 116.30571126937866,
  },
  zoom: 12,
  doubleClickZoom: true,
  control: {
    scale: {},
    zoom: {
      position: "bottomRight",
    },
  },
});
const tMapMultiMarker = reactive({
  styles: {
    mark: {
      width: 25,
      height: 30,
      faceTo: "map",
    },
  },
  geometries: [
    {
      id: "map-mark",
      styleId: "mark",
      position: tMapConfig.center,
    },
  ],
});
const tMapEvents = ref({
  click: (v: any) => {
    tMapConfig.center = v.latLng;
    tMapMultiMarker.geometries = [
      {
        id: "map-mark",
        styleId: "mark",
        position: v.latLng,
      },
    ];
  },
});
const tMapSearch = ref({
  keyword: "",
  loading: false,
});
const tMapData = ref<TMapDataType[]>([]);

// 查询地点
const onSearch = () => {
  if (!tMapSearch.value.keyword) {
    tMapData.value = [];
    return;
  }
  tMapSearch.value.loading = true;
  jsonp("https://apis.map.qq.com/ws/place/v1/suggestion", {
    // region: "北京",
    keyword: tMapSearch.value.keyword,
    key: tMapConfig.mapKey,
    output: "jsonp",
  })
    .then((res) => {
      tMapSearch.value.loading = false;
      if (res.status !== 0) {
        ElMessage.error(res.message);
        return;
      }
      tMapData.value = res.data as TMapDataType[];
    })
    .catch((err) => {
      console.log(err);
      tMapSearch.value.loading = false;
    });
};

// 选中地点
const tMapActive = (index: number) => {
  tMapData.value.forEach((item) => (item.active = false));
  tMapData.value[index].active = true;
  tMapConfig.center = tMapData.value[index].location;
  tMapMultiMarker.geometries = [
    {
      id: "map-mark",
      styleId: "mark",
      position: tMapData.value[index].location,
    },
  ];
};

// 对话框
const dialog = reactive({
  isShowDialog: false,
  title: "选择地图坐标",
});

// 打开弹窗
const openDialog = (title: string, lat: number | null, lng: number | null) => {
  if (title) dialog.title = title;
  if (lat && lng) {
    tMapConfig.center = { lat: Number(lat), lng: Number(lng) };
    tMapMultiMarker.geometries = [
      {
        id: "map-mark",
        styleId: "mark",
        position: { lat, lng },
      },
    ];
  }
  nextTick(() => {
    dialog.isShowDialog = true;
  });
  // let loadTmapIndex = setInterval(() => {
  //   if (tmapRef.value) {
  //     clearInterval(loadTmapIndex);
  //   }
  // }, 500);
};

// 关闭弹窗
const closeDialog = () => {
  dialog.isShowDialog = false;
};

// 确定
const onConfirm = () => {
  emit("confirm", tMapConfig.center);
  closeDialog();
};

// 取消
const onCancel = () => {
  closeDialog();
};

// 暴露变量
defineExpose({
  openDialog,
});

// 页面加载完时
onMounted(() => {
  usePlatformSettingApi()
    .getPlatformSetting()
    .then((result: any) => {
      let platformSetting = result.otherSettings.find(
        (x: any) => x.name === "BaseService.OtherSetting.QqMapKey"
      );
      if (platformSetting) tMapConfig.mapKey = platformSetting.value;
    })
    .catch((err: any) => {
      console.error("获取平台设置信息出错：", err);
    });
});
</script>
<style lang="scss">
.select-map-container {
  .el-card__body {
    padding: 10px;
  }

  .el-dialog__footer {
    padding-top: 0px !important;
  }
}
</style>
<style scoped lang="scss">
.select-map-container {
  .map-container {
    height: 420px;
  }

  .map-marker-text {
    text-align: right;
    color: #555;
    font-size: 14px;
    position: relative;
    top: 5px;
  }

  .address-list-box {
    height: 403px;
    padding-top: 10px;
    overflow-y: auto;

    .address-list-item {
      font-size: 16px;
      color: #555;
      padding: 5px;
      text-align: left;
      border-top: 1px solid #e4e7ed;
      cursor: pointer;
    }

    .address-list-active {
      background-color: #edf5fe;
    }

    .address-not-data {
      color: #999;
      text-align: center;
      line-height: 350px;
    }
  }
}
</style>
