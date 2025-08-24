<template>
  <up-popup
    :show="modelValue"
    mode="bottom"
    :popup="false"
    :mask="true"
    :closeable="true"
    :safe-area-inset-bottom="true"
    close-icon-color="#ffffff"
    :z-index="uZIndex"
    :maskCloseAble="maskCloseAble"
    @close="close"
  >
    <up-tabs
      v-if="modelValue"
      :list="genTabsList"
      :scrollable="true"
      :current="tabsIndex"
      @change="tabsChange"
      ref="tabs"
    ></up-tabs>
    <view class="area-box">
      <view class="u-flex" :class="{ change: isChange }">
        <view class="area-item">
          <view class="u-padding-10 u-bg-gray" style="height: 100%">
            <scroll-view :scroll-y="true" style="height: 100%">
              <up-cell-group>
                <up-cell
                  v-for="(item, index) in provinces"
                  :title="item.name"
                  :arrow="false"
                  :index="index"
                  :key="item.id"
                  @click="provinceChange(item, index)"
                >
                  <template #right-icon>
                    <up-icon
                      v-if="isChooseP && province === index"
                      size="17"
                      name="checkbox-mark"
                    ></up-icon>
                  </template>
                </up-cell>
              </up-cell-group>
            </scroll-view>
          </view>
        </view>
        <view class="area-item">
          <view class="u-padding-10 u-bg-gray" style="height: 100%">
            <scroll-view :scroll-y="true" style="height: 100%">
              <up-cell-group v-if="isChooseP">
                <up-cell
                  v-for="(item, index) in citys"
                  :title="item.name"
                  :arrow="false"
                  :index="index"
                  :key="item.id"
                  @click="cityChange(item, index)"
                >
                  <template #right-icon>
                    <up-icon
                      v-if="isChooseC && city === index"
                      size="17"
                      name="checkbox-mark"
                    ></up-icon>
                  </template>
                </up-cell>
              </up-cell-group>
            </scroll-view>
          </view>
        </view>
        <view class="area-item">
          <view class="u-padding-10 u-bg-gray" style="height: 100%">
            <scroll-view :scroll-y="true" style="height: 100%">
              <up-cell-group v-if="isChooseC">
                <up-cell
                  v-for="(item, index) in areas"
                  :title="item.name"
                  :arrow="false"
                  :index="index"
                  :key="item.id"
                  @click="areaChange(item, index)"
                >
                  <template #right-icon>
                    <up-icon
                      v-if="isChooseA && area === index"
                      size="17"
                      name="checkbox-mark"
                    ></up-icon>
                  </template>
                </up-cell>
              </up-cell-group>
            </scroll-view>
          </view>
        </view>
      </view>
    </view>
  </up-popup>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import { http } from "@/utils/http";

// props
const props = defineProps<{
  modelValue: boolean;
  maskCloseAble?: boolean;
  zIndex?: string | number;
}>();

const emit = defineEmits<{
  (e: "update:modelValue", value: boolean): void;
  (
    e: "city-change",
    value: { province: AreaTreeType; city: AreaTreeType; area: AreaTreeType }
  ): void;
}>();

// 响应式数据
const provinces = ref<AreaTreeType[]>([]);
const citys = ref<AreaTreeType[]>([]);
const areas = ref<AreaTreeType[]>([]);

const isChooseP = ref(false);
const isChooseC = ref(false);
const isChooseA = ref(false);

const province = ref(0);
const city = ref(0);
const area = ref(0);

const tabsIndex = ref(0);

const isChange = computed(() => tabsIndex.value > 1);

const genTabsList = computed(() => {
  const tabsList = [{ name: "请选择" }];
  if (isChooseP.value) {
    tabsList[0].name = provinces.value[province.value]?.name || "请选择";
    tabsList[1] = { name: "请选择" };
  }
  if (isChooseC.value) {
    tabsList[1].name = citys.value[city.value]?.name || "请选择";
    tabsList[2] = { name: "请选择" };
  }
  if (isChooseA.value) {
    tabsList[2].name = areas.value[area.value]?.name || "请选择";
  }
  return tabsList;
});

const uZIndex = computed(() => props.zIndex ?? 1075);

// 初始化加载省份
const loadProvinces = async () => {
  const res = await http<AreaTreeType[]>({
    method: "GET",
    url: `/api/good/area/tree/0`,
  });
  provinces.value = res || [];
};

const loadCities = async (provinceId: number) => {
  const res = await http<AreaTreeType[]>({
    method: "GET",
    url: `/api/good/area/tree/${provinceId}`,
  });
  citys.value = res || [];
};

const loadAreas = async (cityId: number) => {
  const res = await http<AreaTreeType[]>({
    method: "GET",
    url: `/api/good/area/tree/${cityId}`,
  });
  areas.value = res || [];
};

const close = () => {
  emit("update:modelValue", false);
};

const tabsChange = (index: number) => {
  tabsIndex.value = index;
};

const provinceChange = async (item: AreaTreeType, index: number) => {
  isChooseP.value = true;
  isChooseC.value = false;
  isChooseA.value = false;
  province.value = index;
  city.value = 0;
  area.value = 0;
  await loadCities(item.id);
  citys.value = citys.value || [];
  areas.value = [];
  tabsIndex.value = 1;
};

const cityChange = async (item: AreaTreeType, index: number) => {
  isChooseC.value = true;
  isChooseA.value = false;
  city.value = index;
  area.value = 0;
  await loadAreas(item.id);
  areas.value = areas.value || [];
  tabsIndex.value = 2;
};

const areaChange = (item: AreaTreeType, index: number) => {
  isChooseA.value = true;
  area.value = index;
  emit("city-change", {
    province: provinces.value[province.value],
    city: citys.value[city.value],
    area: areas.value[area.value],
  });
  close();
};

// 初始化
onMounted(async () => {
  await loadProvinces();
});
</script>

<style lang="scss" scoped>
.area-box {
  width: 100%;
  overflow: hidden;
  height: 800rpx;

  > view {
    width: 150%;
    transition: transform 0.3s ease-in-out 0s;
    transform: translateX(0);

    &.change {
      transform: translateX(-33.3333333%);
    }
  }

  .area-item {
    width: 33.3333333%;
    height: 800rpx;
  }
}
</style>
