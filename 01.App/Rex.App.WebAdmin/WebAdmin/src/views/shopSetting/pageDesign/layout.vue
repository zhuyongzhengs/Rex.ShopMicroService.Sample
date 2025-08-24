<template>
  <div class="layout-container layout-padding">
    <el-row :gutter="10">
      <!-- 组件库 -->
      <el-col :span="8" class="layout-widget-body">
        <el-card
          :style="'height:' + itemHeight + 'px;'"
          shadow="hover"
          class="layout-item"
        >
          <template #header>
            <div class="card-title">组件库</div>
          </template>
          <div class="component-item" :style="'height:' + (itemHeight - 50) + 'px;'">
            <el-divider class="component-divider" content-position="left"
              >媒体组件</el-divider
            >
            <draggable
              element="ul"
              class="draggable-component"
              :list="mediaComponentItems"
              :group="{ name: 'widget', pull: 'clone', put: false }"
              :animation="150"
              item-key="name"
            >
              <template #item="{ element }">
                <li class="component-item-li">
                  <p>
                    <SvgIcon class="icon-comp" size="30" :name="element.icon" />
                  </p>
                  <p class="text">{{ element.name }}</p>
                </li>
              </template>
            </draggable>
            <div class="item-clear"></div>
            <el-divider class="component-divider" content-position="left"
              >商城组件</el-divider
            >
            <draggable
              element="ul"
              class="draggable-component"
              :list="shopComponentItems"
              :group="{ name: 'widget', pull: 'clone', put: false }"
              :animation="150"
              item-key="name"
            >
              <template #item="{ element }">
                <li class="component-item-li">
                  <p>
                    <SvgIcon class="icon-comp" size="30" :name="element.icon" />
                  </p>
                  <p class="text">{{ element.name }}</p>
                </li>
              </template>
            </draggable>
            <div class="item-clear"></div>
            <el-divider class="component-divider" content-position="left"
              >工具组件</el-divider
            >
            <draggable
              element="ul"
              class="draggable-component"
              :list="utilsComponentItems"
              :group="{ name: 'widget', pull: 'clone', put: false }"
              :animation="150"
              item-key="name"
            >
              <template #item="{ element }">
                <li class="component-item-li">
                  <p>
                    <SvgIcon class="icon-comp" size="30" :name="element.icon" />
                  </p>
                  <p class="text">{{ element.name }}</p>
                </li>
              </template>
            </draggable>
            <div class="item-clear"></div>
            <br />
          </div>
        </el-card>
      </el-col>

      <!-- 小程序布局 -->
      <el-col :span="7" class="layout-applet-body">
        <div class="layout-item" :style="'height:' + itemHeight + 'px'">
          <el-image class="model-img" src="/src/assets/design/model-title.png" />
          <div v-loading="appletBodyLoading" class="grag-container">
            <draggable
              class="layout-list"
              :style="'height:' + (itemHeight - 85) + 'px'"
              :list="layoutData"
              :animation="150"
              group="widget"
              ghostClass="ghost"
              dragClass="dragItem"
              item-key="name"
              @add="handleWidgetAdd"
              @update="datadragUpdate"
            >
              <template #item="{ element, index }">
                <div
                  class="layout-main"
                  :class="{
                    active: selectWg.key === element.key,
                    npr: element.type == 'record',
                  }"
                  @click="handleSelectWidget(index)"
                >
                  <!-- 搜索框 -->
                  <div v-if="element.type === 'search'" class="drag lay-item lay-search">
                    <div class="lay-search-c">
                      <input
                        v-model="element.value.keywords"
                        class="lay-search-input"
                        :class="element.value.style"
                      />
                      <i class="iconfont icon-wxbsousuotuiguang"></i>
                    </div>
                  </div>

                  <!-- 购买记录 -->
                  <div
                    v-if="element.type === 'record'"
                    class="drag lay-record"
                    :class="element.value.style.align"
                    @click="handleSelectRecord(index)"
                    :style="{ top: element.value.style.top + '%' }"
                  >
                    <div class="record-item">
                      <i class="layui-icon layui-icon-user"></i>
                      <span class="text">xxx刚刚0.01元买到了xxx</span>
                    </div>
                    <div
                      @click.stop="handleWidgetDelete(index)"
                      class="btn-delete"
                      v-if="selectWg.key === element.key"
                    >
                      删除
                    </div>
                    <div class="item-clear"></div>
                  </div>

                  <!-- 导航组 -->
                  <div
                    v-if="element.type === 'navBar'"
                    class="drag lay-navBar clearfix"
                    :class="'row' + element.value.limit"
                  >
                    <div class="item" v-for="(nav, key) in element.value.list" :key="key">
                      <div class="item-image">
                        <img :src="nav.image" alt="" />
                      </div>
                      <p class="item-text">{{ nav.text }}</p>
                    </div>
                  </div>

                  <!-- 商品组 -->
                  <div
                    v-if="element.type === 'goods'"
                    class="drag clearfix lay-goods"
                    :class="element.value.display"
                  >
                    <div class="goods-head">
                      <div>{{ element.value.title }}</div>
                      <div v-if="element.value.lookMore">查看更多></div>
                    </div>
                    <div
                      class="goods-item"
                      v-for="(goods, key) in element.value.list"
                      :key="key"
                      :class="'column' + element.value.column"
                    >
                      <div class="goods-image">
                        <img :src="goods.image_url || goods.image" alt="" />
                      </div>
                      <div class="goods-detail">
                        <p class="goods-name twolist-hidden">
                          {{ goods.name || "此处显示商品名称" }}
                        </p>
                        <p class="goods-price">￥{{ goods.price || "99.00" }}</p>
                      </div>
                    </div>
                  </div>

                  <!-- 商品选项卡 -->
                  <div
                    v-if="element.type === 'goodTabBar'"
                    class="drag clearfix lay-goods list"
                  >
                    <div class="goods-tab-head">
                      <div v-for="(goods, key) in element.value.list" :key="key">
                        {{ goods.title }}
                      </div>
                    </div>
                    <div
                      v-for="(goods, key) in element.value.list"
                      :key="key"
                      v-show="key == 0"
                    >
                      <div
                        class="goods-item"
                        :class="'column' + goods.column"
                        v-for="(goodsitem, itemkey) in goods.list"
                        :key="itemkey"
                      >
                        <div class="goods-image">
                          <img :src="goodsitem.image" alt="" />
                        </div>
                        <div class="goods-detail">
                          <p class="goods-name twolist-hidden">
                            {{ goodsitem.name || "此处显示商品名称" }}
                          </p>
                          <p class="goods-price">￥{{ goodsitem.price || "99.00" }}</p>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- 商品秒杀 -->
                  <div
                    v-if="element.type === 'groupPurchase'"
                    class="drag clearfix lay-goods slide group"
                  >
                    <div class="goods-head">
                      <div>{{ element.value.title }}</div>
                    </div>
                    <div
                      class="group-item"
                      v-for="(seckill, key) in element.value.list"
                      :key="key"
                    >
                      <div class="group-image">
                        <img src="/src/assets/design/empty-banner.png" alt="" />
                      </div>
                      <div class="group-detail">
                        <p class="group-name twolist-hidden">
                          {{ seckill.name || "此处显示商品名称" }}
                        </p>
                        <p class="group-price">￥{{ seckill.price || "99.00" }}</p>
                        <p class="group-time">
                          <span>剩余：</span><span class="time">21</span>:<span
                            class="time"
                            >30</span
                          >:<span class="time">45</span>
                        </p>
                        <span class="buy-icon">
                          <img src="/src/assets/design/ic-car.png" alt="" />
                        </span>
                      </div>
                    </div>
                  </div>

                  <!-- 拼团 -->
                  <div
                    v-if="element.type === 'pinTuan'"
                    class="drag clearfix lay-goods slide group"
                  >
                    <div class="goods-head">
                      <div>{{ element.value.title }}</div>
                    </div>
                    <div
                      class="group-item"
                      v-for="(goods, key) in element.value.list"
                      :key="key"
                    >
                      <div class="group-image">
                        <img
                          :src="
                            !goods.pinTuanGoods
                              ? goods.goodsImage
                              : goods.pinTuanGoods[0].goodImage
                          "
                          alt=""
                        />
                      </div>
                      <div class="group-detail">
                        <p class="group-name twolist-hidden">
                          {{ goods.name || "此处显示商品名称" }}
                        </p>
                        <p class="group-price">
                          ￥{{
                            !goods.pinTuanGoods
                              ? "99.00"
                              : goods.pinTuanGoods[0].goodPrice
                          }}
                        </p>
                        <p class="group-time">
                          <span>剩余：</span><span class="time">21</span>:<span
                            class="time"
                            >30</span
                          >:<span class="time">45</span>
                        </p>
                        <span class="buy-icon">
                          <img src="/src/assets/design/ic-car.png" alt="" />
                        </span>
                      </div>
                    </div>
                  </div>

                  <!-- 优惠券组 -->
                  <div v-if="element.type === 'coupon'" class="drag lay-item lay-coupon">
                    <div class="coupon-item">
                      <div class="coupon-left">
                        <p>满300减30</p>
                      </div>
                      <div class="coupon-right">
                        <p class="conpon-f">订单减1.44元 减100元</p>
                        <p class="conpon-purchase">购买订单满2元</p>
                        <p>2019-05-01 - 2019-05-31</p>
                      </div>
                      <div class="coupon-btn">立即领取</div>
                    </div>
                  </div>

                  <!-- 服务卡 -->
                  <div
                    v-if="element.type === 'service'"
                    class="drag clearfix lay-goods slide group"
                  >
                    <div class="goods-head">
                      <div>{{ element.value.title }}</div>
                    </div>
                    <div
                      class="group-item"
                      v-for="(goods, key) in element.value.list"
                      :key="key"
                    >
                      <div class="group-image">
                        <img
                          :src="goods.thumbnail || '/src/assets/design/empty-banner.png'"
                          alt=""
                        />
                      </div>
                      <div class="group-detail">
                        <p class="group-name twolist-hidden">
                          {{ goods.title || "此处显示服务卡名称" }}
                        </p>
                        <p class="group-price">
                          ￥{{ goods.money ? goods.money : "99.00" }}
                        </p>
                        <p class="group-time">
                          <span>剩余：</span><span class="time">21</span>:<span
                            class="time"
                            >30</span
                          >:<span class="time">45</span>
                        </p>
                        <span class="buy-icon">
                          <img src="/src/assets/design/ic-car.png" alt="" />
                        </span>
                      </div>
                    </div>
                  </div>

                  <!-- 公告组 -->
                  <div
                    v-if="element.type === 'notice'"
                    class="drag lay-item lay-notice"
                    :style="'background-color:' + element.value.bgColor"
                  >
                    <SvgIcon
                      class="icon-delete"
                      :style="'color:' + element.value.color"
                      size="20"
                      name="iconfont icon-gonggao"
                      @click="handleDeleteGoods(index)"
                    />
                    <div class="notice-right">
                      <div
                        v-for="(notice, key) in element.value.list"
                        :key="key"
                        class="notice-text"
                        :style="'color:' + element.value.color"
                      >
                        {{ notice.title }}
                      </div>
                    </div>
                  </div>

                  <!-- 图片轮播 -->
                  <div
                    v-if="element.type === 'imgSlide'"
                    class="drag lay-item lay-imgSlide"
                  >
                    <el-carousel
                      :interval="element.value.duration"
                      :height="element.value.height"
                      arrow="never"
                      :autoplay="true"
                    >
                      <el-carousel-item
                        v-for="(list, key) in element.value.list"
                        :key="key"
                      >
                        <img
                          :src="list.image"
                          alt="banner"
                          style="width: 100%; height: 100%"
                        />
                      </el-carousel-item>
                    </el-carousel>
                  </div>

                  <!-- 单图组 -->
                  <div v-if="element.type === 'imgSingle'" class="drag lay-imgSingle">
                    <div
                      class="img-wrap"
                      v-for="(img, key) in element.value.list"
                      :key="key"
                    >
                      <img :src="img.image" alt="" />
                      <div
                        class="img-btn"
                        :style="{
                          backgroundColor: img.buttonColor,
                          color: img.textColor,
                        }"
                        v-show="img.buttonShow"
                      >
                        {{ img.buttonText }}
                      </div>
                    </div>
                  </div>

                  <!-- 图片橱窗 -->
                  <div
                    v-if="element.type === 'imgWindow'"
                    class="drag lay-imgWindow clearfix"
                    :class="'row' + element.value.style"
                    :style="{}"
                  >
                    <template v-if="element.value.style == 0">
                      <div class="display">
                        <div
                          class="display-left"
                          :style="{ padding: element.value.margin + 'px' }"
                        >
                          <img
                            v-if="element.value.list.length >= 1"
                            :src="element.value.list[0].image"
                            alt=""
                          />
                        </div>
                        <div class="display-right">
                          <div
                            class="display-right1"
                            :style="{ padding: element.value.margin + 'px' }"
                          >
                            <img
                              v-if="element.value.list.length >= 2"
                              :src="element.value.list[1].image"
                              alt=""
                            />
                          </div>
                          <div class="display-right2">
                            <div
                              class="left"
                              :style="{ padding: element.value.margin + 'px' }"
                            >
                              <img
                                v-if="element.value.list.length >= 3"
                                :src="element.value.list[2].image"
                                alt=""
                              />
                            </div>
                            <div
                              class="right"
                              :style="{ padding: element.value.margin + 'px' }"
                            >
                              <img
                                v-if="element.value.list.length >= 4"
                                :src="element.value.list[3].image"
                                alt=""
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </template>
                    <template v-else>
                      <div
                        class="img-wrap"
                        v-for="(img, key) in element.value.list"
                        :key="key"
                        :style="{ padding: element.value.margin + 'px' }"
                      >
                        <img :src="img.image" alt="" />
                      </div>
                    </template>
                  </div>

                  <!-- 视频组 -->
                  <div v-if="element.type === 'video'" class="drag lay-item lay-video">
                    <div
                      class="video-wrap"
                      v-for="(video, key) in element.value.list"
                      :key="key"
                    >
                      <video
                        :src="video.url"
                        :poster="video.image"
                        :controls="element.value.autoplay"
                        height="200"
                      ></video>
                    </div>
                  </div>

                  <!-- 文章组 -->
                  <div
                    v-if="element.type === 'article'"
                    class="drag lay-article"
                    style="background-color: white"
                  >
                    <div
                      class="article-wrap clearfix"
                      v-for="(article, key) in element.value.list"
                      :key="key"
                    >
                      <div class="article-left">
                        <div class="article-left-title">
                          {{ article.title || "此处显示文章标题" }}
                        </div>
                        <div class="article-left-brief">
                          {{ article.brief || "此处显示文章简介" }}
                        </div>
                      </div>
                      <div class="article-img">
                        <img :src="article.coverImage || design.defaultBanner()" alt="" />
                      </div>
                    </div>
                  </div>

                  <!-- 文章分类 -->
                  <div v-if="element.type === 'articleClassify'" class="drag lay-article">
                    <div class="article-wrap clearfix">
                      <div class="article-left">
                        <div class="article-left-title">此处显示文章标题</div>
                      </div>
                      <div class="article-img">
                        <img :src="design.defaultBanner()" alt="" />
                      </div>
                    </div>
                    <div class="article-wrap clearfix">
                      <div class="article-left">
                        <div class="article-left-title">此处显示文章标题</div>
                      </div>
                      <div class="article-img">
                        <img :src="design.defaultBanner()" alt="" />
                      </div>
                    </div>
                  </div>

                  <!-- 辅助空白 -->
                  <div
                    v-if="element.type === 'blank'"
                    class="drag lay-item lay-blank"
                    :style="{
                      height: element.value.height + 'px',
                      backgroundColor: element.value.backgroundColor,
                    }"
                  ></div>

                  <!-- 文本域 -->
                  <div
                    v-if="element.type === 'textarea'"
                    class="drag lay-item lay-textarea"
                  >
                    <div class="lay-search-c">
                      <div v-html="element.value"></div>
                    </div>
                  </div>

                  <!-- 控制操作按钮 -->
                  <div class="item-clear"></div>
                  <div
                    @click.stop="handleWidgetDelete(index)"
                    class="btn-delete"
                    v-if="selectWg.key === element.key"
                  >
                    删除
                  </div>
                  <div
                    @click.stop="handleWidgetClone(index)"
                    class="btn-clone"
                    v-if="selectWg.key === element.key"
                  >
                    复制
                  </div>
                </div>
              </template>
            </draggable>
          </div>
        </div>
      </el-col>

      <!-- 组件布局配置 -->
      <el-col :span="9" class="layout-applet-config-body">
        <el-card shadow="hover" class="layout-item">
          <template #header>
            <div class="card-title">
              {{ design.selectWgName(selectWg.type) || "组件配置" }}
            </div>
          </template>
          <div class="layout-config-body" :style="'height:' + (itemHeight - 52) + 'px'">
            <template v-if="selectWg && Object.keys(selectWg).length > 0">
              <br />
              <el-form ref="layoutFormRef" size="default" label-width="100px">
                <!-- 搜索框 -->
                <template v-if="selectWg.type == 'search'">
                  <el-form-item label="提示内容">
                    <el-input
                      v-model="selectWg.value.keywords"
                      :placeholder="selectWg.placeholder"
                      size="default"
                      clearable
                    ></el-input>
                  </el-form-item>
                  <el-form-item label="搜索框样式">
                    <el-radio-group v-model="selectWg.value.style">
                      <el-radio label="square">方形</el-radio>
                      <el-radio label="radius">圆角</el-radio>
                      <el-radio label="round">圆弧</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </template>

                <!-- 购买记录 -->
                <template v-if="selectWg.type == 'record'">
                  <div class="content-item">
                    <el-form-item label="位置">
                      <el-radio-group v-model="selectWg.value.style.align">
                        <el-radio label="left">居左</el-radio>
                        <el-radio label="right">居右</el-radio>
                      </el-radio-group>
                    </el-form-item>
                  </div>
                  <div class="content-item">
                    <el-form-item label="上边距">
                      <el-slider
                        v-model="selectWg.value.style.top"
                        :min="0"
                        :max="100"
                      ></el-slider>
                      <span>{{ selectWg.value.style.top }}%</span>
                    </el-form-item>
                  </div>
                </template>

                <!-- 商品组 -->
                <template v-if="selectWg.type == 'goods'">
                  <div>
                    <el-form-item label="商品来源">
                      <el-radio-group
                        v-model="selectWg.value.type"
                        @change="changeGoodsType"
                      >
                        <el-radio label="auto">自动获取</el-radio>
                        <el-radio label="choose">手动选择</el-radio>
                      </el-radio-group>
                    </el-form-item>
                    <div v-show="selectWg.value.type == 'auto'">
                      <el-form-item label="商品分类">
                        <el-cascader
                          :options="layoutDesign.goodCategoryTrees"
                          :props="goodCategorProp"
                          placeholder="请选择商品分类"
                          clearable
                          class="w100"
                          size="default"
                          v-model="selectWg.value.classifyId"
                          @change="goodGroupCategoryChange"
                        >
                          <template #default="{ node, data }">
                            <span>{{ data.name }}</span>
                            <span v-if="!node.isLeaf">
                              ({{ data.children.length }})
                            </span>
                          </template>
                        </el-cascader>
                      </el-form-item>
                      <el-form-item label="品牌分类">
                        <el-select
                          v-model="selectWg.value.brandId"
                          placeholder="请选择品牌"
                        >
                          <el-option
                            v-for="item in layoutDesign.brands"
                            :value="item.id"
                            :label="item.name"
                            :key="item.id"
                          ></el-option>
                        </el-select>
                      </el-form-item>
                      <el-form-item label="显示数量">
                        <el-input-number v-model="selectWg.value.limit" :min="1" />
                      </el-form-item>
                    </div>
                    <div v-show="selectWg.value.type == 'choose'">
                      <div class="select_seller_goods_box">
                        <ul id="selectGoods" class="sellect_seller_goods_list clearfix">
                          <draggable
                            element="ul"
                            :list="selectWg.value.list"
                            :group="{
                              name: 'selectGoodsList',
                              pull: 'clone',
                              put: false,
                            }"
                            :animation="150"
                            ghostClass="ghost"
                            item-key="name"
                          >
                            <template #item="{ element, index }">
                              <li>
                                <SvgIcon
                                  class="icon-delete"
                                  size="20"
                                  name="iconfont icon-shanchu"
                                  @click="handleDeleteGoods(index)"
                                />
                                <el-image
                                  fit="cover"
                                  class="sel-good-img"
                                  :src="element.image"
                                />
                              </li>
                            </template>
                          </draggable>
                        </ul>
                        <div class="item-clear"></div>
                        <div class="addImg" @click="onOpenSelectGoods">
                          <SvgIcon name="iconfont icon-choice" />
                          <span>选择商品</span>
                        </div>
                      </div>
                    </div>
                    <el-divider style="margin: 20px auto" />
                    <el-form-item label="显示类型">
                      <el-radio-group v-model="selectWg.value.display">
                        <el-radio label="list">列表平铺</el-radio>
                        <el-radio label="slide" :disabled="selectWg.value.column == 1"
                          >横向滚动</el-radio
                        >
                      </el-radio-group>
                    </el-form-item>
                    <el-form-item label="分列数量">
                      <el-radio-group v-model="selectWg.value.column">
                        <el-radio :label="1" :disabled="selectWg.value.display == 'slide'"
                          >单列</el-radio
                        >
                        <el-radio :label="2">两列</el-radio>
                        <el-radio :label="3">三列</el-radio>
                      </el-radio-group>
                    </el-form-item>
                    <el-form-item label="商品组名称">
                      <el-input v-model="selectWg.value.title" size="default" clearable />
                    </el-form-item>
                    <el-form-item label="是否查看更多">
                      <el-radio-group v-model="selectWg.value.lookMore">
                        <el-radio :label="true">是</el-radio>
                        <el-radio :label="false">否</el-radio>
                      </el-radio-group>
                    </el-form-item>
                  </div>
                </template>

                <!-- 商品选项卡 -->
                <template v-if="selectWg.type == 'goodTabBar'">
                  <div>
                    <el-form-item label="是否固定头部">
                      <el-radio-group v-model="selectWg.value.isFixedHead">
                        <el-radio :label="true">是</el-radio>
                        <el-radio :label="false">否</el-radio>
                      </el-radio-group>
                    </el-form-item>

                    <div v-for="(child, key) in selectWg.value.list" :key="key">
                      <div class="drag-block">
                        <div class="handle-icon" title="删除这一项">
                          <SvgIcon
                            name="iconfont icon-shanchu21"
                            @click="handleRemoveTabBar(key)"
                          />
                        </div>
                      </div>
                      <div class="tabBarItem">
                        <el-form-item label="商品来源">
                          <el-radio-group
                            v-model="child.type"
                            @change="changeTabBarGoodsType(child.type, key)"
                          >
                            <el-radio label="auto">自动获取</el-radio>
                            <el-radio label="choose">手动选择</el-radio>
                          </el-radio-group>
                        </el-form-item>
                        <div v-show="child.type == 'auto'">
                          <el-form-item label="商品分类">
                            <el-cascader
                              :options="layoutDesign.goodCategoryTrees"
                              :props="goodCategorProp"
                              placeholder="请选择商品分类"
                              clearable
                              class="w100"
                              size="default"
                              v-model="child.classifyId"
                              @change="goodTabBarCategoryChange($event, key)"
                            >
                              <template #default="{ node, data }">
                                <span>{{ data.name }}</span>
                                <span v-if="!node.isLeaf">
                                  ({{ data.children.length }})
                                </span>
                              </template>
                            </el-cascader>
                          </el-form-item>
                          <el-form-item label="品牌分类">
                            <el-select v-model="child.brandId" placeholder="请选择品牌">
                              <el-option
                                v-for="itemBrand in layoutDesign.brands"
                                :value="itemBrand.id"
                                :label="itemBrand.name"
                                :key="itemBrand.id"
                              ></el-option>
                            </el-select>
                          </el-form-item>
                          <el-form-item label="显示数量">
                            <el-input-number v-model="child.limit" :min="1" />
                          </el-form-item>
                        </div>
                        <div v-show="child.type == 'choose'">
                          <div class="select_seller_goods_box">
                            <ul
                              id="selectGoods"
                              class="sellect_seller_goods_list clearfix"
                            >
                              <draggable
                                element="ul"
                                :list="child.list"
                                :group="{
                                  name: 'selectGoodsList',
                                  pull: 'clone',
                                  put: false,
                                }"
                                :animation="150"
                                ghostClass="ghost"
                                item-key="name"
                              >
                                <template #item="{ element, index }">
                                  <li>
                                    <SvgIcon
                                      class="icon-delete"
                                      size="20"
                                      name="iconfont icon-shanchu"
                                      @click="handleDeleteTabBarGoods(key, index)"
                                    />
                                    <img :src="element.image" alt="" />
                                  </li>
                                </template>
                              </draggable>
                            </ul>
                            <div class="item-clear"></div>
                            <div class="addImg" @click="onOpenSelectGoods(key)">
                              <SvgIcon name="iconfont icon-choice" />
                              <span>选择商品</span>
                            </div>
                          </div>
                        </div>
                        <el-divider style="margin: 20px auto" />
                        <el-form-item label="分列数量">
                          <el-radio-group v-model="child.column">
                            <el-radio :label="1" :disabled="child.display == 'slide'"
                              >单列</el-radio
                            >
                            <el-radio :label="2">两列</el-radio>
                            <el-radio :label="3">三列</el-radio>
                          </el-radio-group>
                        </el-form-item>
                        <el-form-item label="Tab大标题名称" label-width="110">
                          <el-input
                            v-model="child.title"
                            placeholder="请输入Tab大标题名称"
                            size="default"
                            clearable
                          />
                        </el-form-item>
                        <el-form-item label="Tab子标题名称" label-width="110">
                          <el-input
                            v-model="child.subTitle"
                            placeholder="请输入Tab子标题名称"
                            size="default"
                            clearable
                          />
                        </el-form-item>
                      </div>
                    </div>
                    <div class="addImg" @click="handleAddTabBarGoods">
                      <SvgIcon name="iconfont icon-choice" />
                      <span>添加一个Tab</span>
                    </div>
                  </div>
                </template>

                <!-- 商品秒杀 -->
                <template v-if="selectWg.type == 'groupPurchase'">
                  <div>
                    <div class="select_seller_goods_box">
                      <ul
                        id="selectGroupPurchaseGoods"
                        class="sellect_seller_goods_list clearfix"
                      >
                        <draggable
                          element="ul"
                          :list="selectWg.value.list"
                          :group="{
                            name: 'selectGoodsList',
                            pull: 'clone',
                            put: false,
                          }"
                          :animation="150"
                          ghostClass="ghost"
                          item-key="name"
                        >
                          <template #item="{ element, index }">
                            <li>
                              <SvgIcon
                                class="icon-delete"
                                size="20"
                                name="iconfont icon-shanchu"
                                @click="handleDeleteGoods(index)"
                              />
                              <div class="left">
                                <img src="/src/assets/design/empty-banner.png" alt="" />
                              </div>
                              <div class="right">
                                <p>{{ element.name }}</p>
                              </div>
                            </li>
                          </template>
                        </draggable>
                      </ul>
                      <div class="item-clear"></div>
                      <div class="addImg" @click="selectGroupGoods">
                        <SvgIcon name="iconfont icon-choice" />
                        <span>选择商品</span>
                      </div>
                    </div>
                    <el-divider style="margin: 20px auto" />
                    <el-form-item label="商品组名称">
                      <el-input
                        v-model="selectWg.value.title"
                        placeholder="请输入商品组名称"
                        size="default"
                        clearable
                      />
                    </el-form-item>
                    <el-form-item label="显示数量">
                      <el-input-number v-model="selectWg.value.limit" :min="1" />
                    </el-form-item>
                  </div>
                </template>

                <!-- 拼团 -->
                <template v-if="selectWg.type == 'pinTuan'">
                  <div>
                    <div class="select_seller_goods_box">
                      <ul id="selectGoods" class="sellect_seller_goods_list clearfix">
                        <draggable
                          element="ul"
                          :list="selectWg.value.list"
                          :group="{
                            name: 'selectGoodsList',
                            pull: 'clone',
                            put: false,
                          }"
                          :animation="150"
                          ghostClass="ghost"
                          item-key="name"
                        >
                          <template #item="{ element, index }">
                            <li>
                              <SvgIcon
                                class="icon-delete"
                                size="20"
                                name="iconfont icon-shanchu"
                                @click="handleDeleteGoods(index)"
                              />
                              <img
                                :src="
                                  !element.pinTuanGoods
                                    ? element.goodsImage
                                    : element.pinTuanGoods[0].goodImage
                                "
                                alt=""
                              />
                            </li>
                          </template>
                        </draggable>
                      </ul>
                      <div class="item-clear"></div>
                      <div class="addImg" @click="selectPinTuanGoods">
                        <SvgIcon name="iconfont icon-choice" />
                        <span>选择商品</span>
                      </div>
                    </div>
                    <el-divider style="margin: 20px auto" />
                    <el-form-item label="商品组名称">
                      <el-input
                        v-model="selectWg.value.title"
                        placeholder="请输入商品组名称"
                        size="default"
                        clearable
                      />
                    </el-form-item>
                    <el-form-item label="显示数量">
                      <el-input-number v-model="selectWg.value.limit" :min="1" />
                    </el-form-item>
                  </div>
                </template>

                <!-- 优惠券 -->
                <template v-if="selectWg.type == 'coupon'">
                  <div>
                    <el-form-item label="显示数量">
                      <el-input-number v-model="selectWg.value.limit" :min="0" />
                    </el-form-item>
                  </div>
                </template>

                <!-- 服务 -->
                <template v-if="selectWg.type == 'service'">
                  <div>
                    <div class="select_seller_goods_box">
                      <ul id="selectGoods" class="sellect_seller_goods_list clearfix">
                        <draggable
                          element="ul"
                          :list="selectWg.value.list"
                          :group="{
                            name: 'selectGoodsList',
                            pull: 'clone',
                            put: false,
                          }"
                          :animation="150"
                          ghostClass="ghost"
                          item-key="name"
                        >
                          <template #item="{ element, index }">
                            <li>
                              <SvgIcon
                                class="icon-delete"
                                size="20"
                                name="iconfont icon-shanchu"
                                @click="handleDeleteGoods(index)"
                              />
                              <img :src="element.thumbnail" alt="" />
                            </li>
                          </template>
                        </draggable>
                      </ul>
                      <div class="item-clear"></div>
                      <div class="addImg" @click="selectServices">
                        <SvgIcon name="iconfont icon-choice" />
                        <span>选择服务</span>
                      </div>
                    </div>
                    <el-divider style="margin: 20px auto" />
                    <el-form-item label="服务组名称">
                      <el-input
                        v-model="selectWg.value.title"
                        placeholder="请输入服务组名称"
                        size="default"
                        clearable
                      />
                    </el-form-item>
                    <el-form-item label="显示数量">
                      <el-input-number v-model="selectWg.value.limit" :min="1" />
                    </el-form-item>
                  </div>
                </template>

                <!-- 公告 -->
                <template v-if="selectWg.type == 'notice'">
                  <el-row>
                    <el-col :span="10">
                      <el-form-item label="背景颜色">
                        <el-color-picker v-model="selectWg.value.bgColor" />
                      </el-form-item>
                    </el-col>
                    <el-col :span="14">
                      <el-form-item label="文字颜色">
                        <el-color-picker v-model="selectWg.value.color" />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-form-item label="公告获取">
                    <el-radio-group v-model="selectWg.value.type">
                      <el-radio label="auto">自动获取</el-radio>
                      <el-radio label="choose">手动选择</el-radio>
                    </el-radio-group>
                  </el-form-item>
                  <div v-if="selectWg.value.type == 'choose'">
                    <div class="select_seller_notice_box">
                      <ul class="sellect_seller_brands_list">
                        <draggable
                          element="ul"
                          :list="selectWg.value.list"
                          :group="{
                            name: 'noticeList',
                            pull: 'clone',
                            put: false,
                          }"
                          :animation="150"
                          ghostClass="ghost"
                          item-key="name"
                        >
                          <template #item="{ element, index }">
                            <el-tag
                              class="tag-notice mr10"
                              type="info"
                              size="large"
                              closable
                              @close="handleDeleteNotice(index)"
                              >{{ element.title }}</el-tag
                            >
                          </template>
                        </draggable>
                      </ul>
                    </div>
                    <div>
                      <div class="item-clear"></div>
                      <div class="addImg" @click="selectNotices">
                        <SvgIcon name="iconfont icon-choice" />
                        <span>选择公告</span>
                      </div>
                    </div>
                  </div>
                </template>

                <!-- 导航组-->
                <template v-if="selectWg.type == 'navBar'">
                  <div>
                    <el-form-item label="每行数量">
                      <el-radio-group v-model="selectWg.value.limit">
                        <el-radio :label="3">3个</el-radio>
                        <el-radio :label="4">4个</el-radio>
                        <el-radio :label="5">5个</el-radio>
                      </el-radio-group>
                    </el-form-item>
                    <draggable
                      element="ul"
                      :list="selectWg.value.list"
                      :group="{
                        name: 'slideList',
                        pull: 'clone',
                        put: false,
                      }"
                      :animation="150"
                      ghostClass="ghost"
                      item-key="name"
                      handle=".drag-block"
                    >
                      <template #item="{ element, index }">
                        <li>
                          <div class="drag-block">
                            <div class="handle-icon" title="删除这一项">
                              <i
                                class="iconfont icon-shanchu21"
                                @click="handleSlideRemove(index)"
                              ></i>
                            </div>
                          </div>
                          <div class="content mt5">
                            <div class="content-item">
                              <el-form-item label="图片">
                                <!--
                                <upload-img
                                  @upload-img="upImage(index, element)"
                                  :index="index"
                                  :item="element"
                                ></upload-img>
                                -->
                                <el-input
                                  v-model="element.image"
                                  placeholder="请输入图片链接（后续添加上传功能）"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="按钮文字">
                                <el-input
                                  v-model="element.text"
                                  placeholder="请输入按钮文字"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="链接类型">
                                <el-select
                                  v-model="element.linkType"
                                  placeholder="请选择链接类型"
                                  @change="onLinkTypeChange($event, index)"
                                >
                                  <el-option :value="1" label="URL链接" />
                                  <el-option :value="2" label="商品" />
                                  <el-option :value="3" label="文章" />
                                  <el-option :value="4" label="文章分类" />
                                  <!-- <el-option :value="5" label="智能表单" /> -->
                                </el-select>
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="链接指向">
                                <el-input
                                  :title="element.showLinkValue"
                                  v-model="element.linkValue"
                                  :placeholder="element.placeholder"
                                  :readonly="element.readonly"
                                  @click="onOpenLinkValue(index)"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                          </div>
                        </li>
                      </template>
                    </draggable>
                    <div class="addImg" @click="handleAddNav">
                      <i class="iconfont icon-tianjia"></i>
                      <span>添加一个导航组</span>
                    </div>
                  </div>
                </template>

                <!-- 轮播图 -->
                <template v-if="selectWg.type == 'imgSlide'">
                  <div>
                    <div class="content-item mb15">
                      <el-form-item
                        label="切换时间"
                        title="轮播图自动切换的间隔时间，单位：毫秒"
                      >
                        <el-input-number
                          v-model="selectWg.value.duration"
                          :step="500"
                          :min="0"
                        />
                      </el-form-item>
                    </div>
                    <!--
                    <div class="content-item mb15">
                      <el-form-item label="高度" title="轮播图高度，单位：px">
                        <el-input-number
                          v-model="selectWg.value.height"
                          :step="5"
                          :min="0"
                        />
                      </el-form-item>
                    </div>
                    -->
                    <draggable
                      element="ul"
                      :list="selectWg.value.list"
                      :group="{
                        name: 'slideList',
                        pull: 'clone',
                        put: false,
                      }"
                      :animation="150"
                      ghostClass="ghost"
                      item-key="name"
                      handle=".drag-block"
                    >
                      <template #item="{ element, index }">
                        <li>
                          <div class="drag-block">
                            <div class="handle-icon" title="删除这一项">
                              <i
                                class="iconfont icon-shanchu21"
                                @click="handleSlideRemove(index)"
                              ></i>
                            </div>
                          </div>
                          <div class="content mt5">
                            <div class="content-item">
                              <el-form-item label="图片">
                                <!-- <upload-img @upload-img="upImage(index,item)" :index="index" :item="item"></upload-img> -->
                                <el-input
                                  v-model="element.image"
                                  placeholder="图片链接地址（后续添加上传）"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="链接类型">
                                <el-select
                                  v-model="element.linkType"
                                  placeholder="请选择链接类型"
                                  @change="onLinkTypeChange($event, index)"
                                >
                                  <el-option :value="1" label="URL链接" />
                                  <el-option :value="2" label="商品" />
                                  <el-option :value="3" label="文章" />
                                  <el-option :value="4" label="文章分类" />
                                  <!-- <el-option :value="5" label="智能表单" /> -->
                                </el-select>
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="链接指向">
                                <el-input
                                  :title="element.showLinkValue"
                                  v-model="element.linkValue"
                                  :placeholder="element.placeholder"
                                  :readonly="element.readonly"
                                  @click="onOpenLinkValue(index)"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                          </div>
                        </li>
                      </template>
                    </draggable>
                    <div class="addImg" @click="handleAddSlide">
                      <i class="iconfont icon-tianjia"></i>
                      <span>添加一个图片</span>
                    </div>
                  </div>
                </template>

                <!-- 单图组 -->
                <template v-if="selectWg.type == 'imgSingle'">
                  <li v-for="(imgSingleItem, index) in selectWg.value.list" :key="index">
                    <div class="content mt10 mb10">
                      <div class="content-item">
                        <el-form-item label="图片">
                          <el-input
                            v-model="imgSingleItem.image"
                            placeholder="请输入图片链接（后续添加上传功能）"
                            size="default"
                            clearable
                          />
                        </el-form-item>
                      </div>
                      <div class="content-item">
                        <el-form-item label="链接类型">
                          <el-select
                            v-model="imgSingleItem.linkType"
                            placeholder="请选择链接类型"
                            @change="onLinkTypeChange($event, index)"
                          >
                            <el-option :value="1" label="URL链接" />
                            <el-option :value="2" label="商品" />
                            <el-option :value="3" label="文章" />
                            <el-option :value="4" label="文章分类" />
                            <!-- <el-option :value="5" label="智能表单" /> -->
                          </el-select>
                        </el-form-item>
                      </div>
                      <div class="content-item">
                        <el-form-item label="链接指向">
                          <el-input
                            :title="imgSingleItem.showLinkValue"
                            v-model="imgSingleItem.linkValue"
                            :placeholder="imgSingleItem.placeholder"
                            :readonly="imgSingleItem.readonly"
                            @click="onOpenLinkValue(index)"
                            size="default"
                            clearable
                          />
                        </el-form-item>
                      </div>
                      <div class="content-item">
                        <el-form-item label="添加按钮">
                          <el-switch
                            v-model="imgSingleItem.buttonShow"
                            active-color="#13ce66"
                          />
                        </el-form-item>
                      </div>
                      <el-row v-show="imgSingleItem.buttonShow">
                        <el-col :span="6">
                          <el-form-item label="按钮颜色">
                            <el-color-picker v-model="imgSingleItem.buttonColor" />
                          </el-form-item>
                        </el-col>
                        <el-col :span="11">
                          <el-form-item label="按钮文字">
                            <el-input
                              v-model="imgSingleItem.buttonText"
                              placeholder="按钮文字"
                              size="default"
                              clearable
                            />
                          </el-form-item>
                        </el-col>
                        <el-col :span="6">
                          <el-form-item label="文字颜色" label-width="85">
                            <el-color-picker v-model="imgSingleItem.textColor" />
                          </el-form-item>
                        </el-col>
                      </el-row>
                    </div>
                  </li>
                </template>

                <!-- 图片橱窗 -->
                <template v-if="selectWg.type == 'imgWindow'">
                  <div>
                    <div class="content-item">
                      <el-form-item label="布局方式">
                        <div class="tpl-block">
                          <div
                            class="tpl-item"
                            v-for="(item, i) in imgWindowStyle"
                            :key="i"
                            @click="slectTplStyle(item)"
                            :class="{ active: selectWg.value.style == item.value }"
                          >
                            <div class="tpl-item-image">
                              <img :src="item.image" alt="" />
                            </div>
                            <div class="tpl-item-text">
                              {{ item.title }}
                            </div>
                          </div>
                          <p class="layout-tip">建议添加比例/尺寸一致的图片</p>
                        </div>
                      </el-form-item>
                    </div>
                    <div class="content-item">
                      <el-form-item label="图片间距">
                        <el-slider v-model="selectWg.value.margin" :min="0" :max="30" />
                      </el-form-item>
                      <span
                        style="
                          display: inline-block;
                          height: 30px;
                          line-height: 30px;
                          font-size: 12px;
                          margin-left: 10px;
                        "
                        >{{ selectWg.value.margin }}px</span
                      >
                    </div>
                    <draggable
                      element="ul"
                      :list="selectWg.value.list"
                      :group="{
                        name: 'slideList',
                        pull: 'clone',
                        put: false,
                      }"
                      :animation="150"
                      ghostClass="ghost"
                      item-key="name"
                      handle=".drag-block"
                    >
                      <template #item="{ element, index }">
                        <li>
                          <div class="drag-block">
                            <div class="handle-icon" title="删除这一项">
                              <i
                                class="iconfont icon-shanchu21"
                                @click="handleSlideRemove(index)"
                              ></i>
                            </div>
                          </div>
                          <div class="content mt5">
                            <div class="content-item">
                              <el-form-item label="图片">
                                <el-input
                                  v-model="element.image"
                                  placeholder="请输入图片链接（后续添加上传功能）"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="链接类型">
                                <el-select
                                  v-model="element.linkType"
                                  placeholder="请选择链接类型"
                                  @change="onLinkTypeChange($event, index)"
                                >
                                  <el-option :value="1" label="URL链接" />
                                  <el-option :value="2" label="商品" />
                                  <el-option :value="3" label="文章" />
                                  <el-option :value="4" label="文章分类" />
                                  <!-- <el-option :value="5" label="智能表单" /> -->
                                </el-select>
                              </el-form-item>
                            </div>
                            <div class="content-item">
                              <el-form-item label="链接指向">
                                <el-input
                                  :title="element.showLinkValue"
                                  v-model="element.linkValue"
                                  :placeholder="element.placeholder"
                                  :readonly="element.readonly"
                                  @click="onOpenLinkValue(index)"
                                  size="default"
                                  clearable
                                />
                              </el-form-item>
                            </div>
                          </div>
                        </li>
                      </template>
                    </draggable>
                    <div class="addImg" @click="handleAddImgWindow">
                      <i class="iconfont icon-tianjia"></i>
                      <span>添加一个图片</span>
                    </div>
                  </div>
                </template>

                <!-- 视频组 -->
                <template v-if="selectWg.type == 'video'">
                  <div>
                    <div class="content-item">
                      <el-form-item label="自动播放">
                        <el-switch
                          v-model="selectWg.value.autoplay"
                          :active-value="true"
                          :inactive-value="false"
                          active-color="#13ce66"
                        />
                      </el-form-item>
                    </div>
                    <li v-for="(videoItem, index) in selectWg.value.list" :key="index">
                      <div class="content mt5">
                        <div class="content-item">
                          <el-form-item label="视频封面">
                            <el-input
                              v-model="videoItem.image"
                              placeholder="请输入视频封面链接（后续添加上传功能）"
                              size="default"
                              clearable
                            />
                          </el-form-item>
                        </div>
                        <div class="content-item">
                          <el-form-item label="视频地址">
                            <el-input
                              v-model="videoItem.url"
                              placeholder="请输入视频地址"
                              size="default"
                              clearable
                            />
                          </el-form-item>
                        </div>
                      </div>
                    </li>
                  </div>
                </template>

                <!-- 文章组 -->
                <template v-if="selectWg.type == 'article'">
                  <div>
                    <div class="content-item">
                      <el-form-item label="添加文章">
                        <el-input
                          v-model="selectWg.value.list[0].title"
                          :readonly="true"
                          placeholder="请选择广告文章"
                          size="default"
                          clearable
                          @click="onOpenArticleHandle(0)"
                        />
                      </el-form-item>
                    </div>
                  </div>
                </template>

                <!-- 文章分类 -->
                <template v-if="selectWg.type == 'articleClassify'">
                  <div>
                    <div class="content-item">
                      <el-form-item label="文章分类">
                        <el-select
                          v-model="selectWg.value.articleClassifyId"
                          placeholder="请选择分类"
                          clearable
                        >
                          <el-option
                            v-for="articleType in articleTypeItems"
                            :key="articleType.id"
                            :label="articleType.name"
                            :value="articleType.id"
                          />
                        </el-select>
                      </el-form-item>
                    </div>
                    <div class="content-item">
                      <el-form-item label="显示数量">
                        <el-input-number v-model="selectWg.value.limit" :min="1" />
                      </el-form-item>
                    </div>
                  </div>
                </template>

                <!-- 辅助空白 -->
                <template v-if="selectWg.type == 'blank'">
                  <el-form-item label="背景颜色">
                    <el-color-picker v-model="selectWg.value.backgroundColor" />
                    <el-button size="small" class="ml10" @click="resetColor">
                      <el-icon>
                        <ele-RefreshLeft />
                      </el-icon>
                      重置
                    </el-button>
                  </el-form-item>
                  <el-form-item label="组件高度">
                    <el-slider v-model="selectWg.value.height" :min="1" :max="200" />
                  </el-form-item>
                </template>

                <!-- 文本域 -->
                <template v-if="selectWg.type == 'textarea'">
                  <div>
                    <editor height="240px" v-model:get-html="selectWg.value" />
                  </div>
                </template>
              </el-form>
              <br />
            </template>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- 底部 -->
    <el-row :gutter="0">
      <el-col :span="24">
        <div class="card-submit-content">
          <el-button
            type="success"
            size="default"
            class="footer-btn mr10"
            @click="layoutSubmit"
            >保存页面</el-button
          >
          <el-button
            type="default"
            size="default"
            class="footer-btn"
            @click="layoutCancel"
            >返回</el-button
          >
        </div>
      </el-col>
    </el-row>

    <!-- 选择组件部分 -->
    <select-service-good
      ref="selectServiceGoodRef"
      @selServiceGoodResult="onSelectServiceGoodResult"
    />
    <select-goods ref="selectGoodsRef" @selGoodsResult="onSelectGoodsResult" />
    <select-group-seckill
      ref="selectGroupSeckillRef"
      @selGroupSeckillResult="onSelGroupSeckillResult"
    />
    <select-pin-tuan-rule
      ref="selectPinTuanRuleRef"
      @selPinTuanRuleResult="onSelPinTuanRuleResult"
    />
    <select-notice ref="selectNoticeRef" @selNoticeResult="onSelNoticeResult" />
    <select-single-goods
      ref="selectSingleGoodsRef"
      selectionType="single"
      @selGoodsResult="onSelectSingleGoodsResult"
    />
    <select-article
      ref="selectArticleRef"
      selectionType="single"
      @selResult="onSelectArticleResult"
    />
    <select-article-type
      ref="selectArticleTypeRef"
      selectionType="single"
      @selResult="onSelectArticleTypeResult"
    />
    <select-form
      ref="selectFormRef"
      selectionType="single"
      @selResult="onSelectFormResult"
    />
    <select-create-article
      ref="selectCreateArticleRef"
      selectionType="single"
      @selResult="onSelectCreateArticleResult"
    />
  </div>
</template>

<script setup lang="ts" name="pageDesign">
import { defineAsyncComponent, onMounted, ref } from "vue";
import { ElMessageBox, ElMessage } from "element-plus";
import _ from "lodash";
import draggable from "vuedraggable";
import mittBus from "/@/utils/mitt";
import design from "/@/utils/design";

import { useRoute } from "vue-router";
import { getCode, getGuidEmpty } from "/@/utils/other";
import { usePageDesignItemApi } from "/@/api/shopSetting/pageDesignItem/index";
import { usePageDesignApi } from "/@/api/shopSetting/pageDesign/index";
import { useArticleTypeApi } from "/@/api/operationManage/articleType/index";

const SelectServiceGood = defineAsyncComponent(
  () => import("/@/views/good/serviceGood/components/selectServiceGoodDialog.vue")
);
const SelectGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);
const SelectGroupSeckill = defineAsyncComponent(
  () => import("/@/views/promotion/groupSeckill/components/selectGroupSeckillDialog.vue")
);
const SelectPinTuanRule = defineAsyncComponent(
  () =>
    import(
      "/@/views/promotion/pinTuanManage/pinTuanList/components/selectPinTuanRuleDialog.vue"
    )
);
const SelectNotice = defineAsyncComponent(
  () => import("/@/views/operationManage/notices/components/selectNoticeDialog.vue")
);
const SelectSingleGoods = defineAsyncComponent(
  () => import("/@/views/good/goods/components/selectGoodsDialog.vue")
);
const SelectArticle = defineAsyncComponent(
  () => import("/@/views/operationManage/articles/components/selectArticleDialog.vue")
);
const SelectArticleType = defineAsyncComponent(
  () => import("/@/views/operationManage/articles/components/selectArticleTypeDialog.vue")
);
const SelectForm = defineAsyncComponent(
  () => import("/@/views/operationManage/customForms/components/selectFormDialog.vue")
);
const SelectCreateArticle = defineAsyncComponent(
  () => import("/@/views/operationManage/articles/components/selectArticleDialog.vue")
);
const Editor = defineAsyncComponent(() => import("/@/components/editor/index.vue"));

// 引入Api请求接口
const pageDesignItemApi = usePageDesignItemApi();
const pageDesignApi = usePageDesignApi();
const articleTypeApi = useArticleTypeApi();

// 定义变量内容
const itemHeight = ref(0);
const route = useRoute();
const selectGoodsRef = ref();
const selectSingleGoodsRef = ref();
const selectArticleRef = ref();
const selectArticleTypeRef = ref();
const selectFormRef = ref();
const selectGroupSeckillRef = ref();
const selectPinTuanRuleRef = ref();
const selectServiceGoodRef = ref();
const selectNoticeRef = ref();
const selectCreateArticleRef = ref();
const mediaComponentItems = ref(design.mediaComponents());
const shopComponentItems = ref(design.shopComponents());
const utilsComponentItems = ref(design.utilsComponents());
const defaultGoods = ref(design.defaultGoods());
const layoutData = ref([] as LayoutItem[]);
const selectWg = ref({} as any);
const hasChooseGoods = ref([] as RowGoodsType[]);
const layoutDesign = ref({} as LayoutDesignType);
const maxSelectGoods = ref(10); // 选择商品最大数量
const selectImgIndex = ref(0);
const goodCategorProp = ref({
  checkStrictly: true,
  value: "id",
  label: "name",
});
const imgWindowStyle = ref(design.imgWindowStyle());
const articleTypeItems = ref([] as RowArticleTypeType[]);

// 设置当前(选择)控件
const setSelectWg = (data: any) => {
  selectWg.value = data;
};

// 控件拖拽添加
const handleWidgetAdd = (evt: any) => {
  const newIndex = evt.newIndex;
  let newLayout = _.cloneDeep(layoutData.value[newIndex]) as any;
  const dataType = layoutData.value[newIndex].type;
  newLayout.key = getCode(dataType + "_");
  layoutData.value[newIndex] = newLayout;
  setSelectWg(layoutData.value[newIndex]);
  if (
    dataType == "articleClassify" &&
    (!articleTypeItems || articleTypeItems.value.length < 1)
  ) {
    getArticleTypeItems();
  }
};

// 控件拖拽修改
const datadragUpdate = (evt: any) => {
  console.log("拖拽修改", evt);
};

// 删除控件
const handleWidgetDelete = (delIndex: number) => {
  ElMessageBox.confirm("您确定要删除吗?", "提示", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(() => {
    // 设置新的当前控件
    if (layoutData.value.length - 1 === delIndex) {
      if (delIndex === 0) {
        setSelectWg([]);
      } else {
        setSelectWg(layoutData.value[delIndex - 1]);
      }
    } else {
      setSelectWg(layoutData.value[delIndex + 1]);
    }
    // 移除控件
    layoutData.value.splice(delIndex, 1);
  });
};

// 复制控件
const handleWidgetClone = (index: number) => {
  let copyLayoutItem = _.cloneDeep(layoutData.value[index]) as any;
  copyLayoutItem.key = getCode(copyLayoutItem.type + "_");
  layoutData.value.splice(index, 0, copyLayoutItem);
};

// 选择控件
const handleSelectWidget = (index: number) => {
  setSelectWg(layoutData.value[index]);
};

// 选择购买记录
const handleSelectRecord = (index: number) => {
  setSelectWg(layoutData.value[index]);
};

// 辅助空白重置
const resetColor = () => {
  selectWg.value.value.backgroundColor = "#FFFFFF";
};

// 商品来源切换
const changeGoodsType = (val: string) => {
  if (val == "auto") {
    hasChooseGoods.value = selectWg.value.value.list;
    selectWg.value.value.list = defaultGoods.value;
  } else {
    selectWg.value.value.list =
      hasChooseGoods.value.length > 0 ? hasChooseGoods.value : defaultGoods.value;
  }
};

// 打开选择商品
const onOpenSelectGoods = (index: any) => {
  selectGoodsRef.value.openDialog("选择商品");
  if (typeof index === "number") {
    selectImgIndex.value = index;
  }
};

// 选择的商品返回结果
const onSelectGoodsResult = (goods: RowGoodsType[]) => {
  if (goods.length > maxSelectGoods.value) {
    ElMessage.warning(`最多只能选择 ${maxSelectGoods.value} 个！`);
    return;
  }
  if (selectWg.value.type == "goods") {
    setGoodsValue(goods);
  } else if (selectWg.value.type == "goodTabBar") {
    setGoodTabBar(goods, selectImgIndex.value);
  }
};

// 商品选项卡选择商品赋值
const setGoodTabBar = (goods: RowGoodsType[], index: number) => {
  // 移除默认(商品)数据
  if (selectWg.value.value.list[index].list.filter((p: any) => !p.name).length > 0)
    selectWg.value.value.list[index].list = [];

  // 商品数据
  let goodItems = new Array();
  for (let i = 0; i < goods.length; i++) {
    const good = goods[i];

    /* 验证：已存在的则不添加 */
    if (
      selectWg.value.value.list[index].list.filter((p: any) => p.id == good.id).length > 0
    ) {
      continue;
    }

    /* 筛选所需(商品)字段 */
    let stock = 0;
    let products = good.products.filter((p) => p.isDefault);
    if (products && products.length > 0) stock = products[0].stock;
    goodItems.push({
      id: good.id,
      image: good.image,
      images: good.images,
      name: good.name,
      price: good.price,
      stock,
    });
  }
  if (goodItems.length > 0) {
    hasChooseGoods.value = goodItems;
    selectWg.value.value.list[index].list.push(...goodItems);
  }
};

// 商品组选择商品赋值
const setGoodsValue = (goods: RowGoodsType[]) => {
  // 移除默认(商品)数据
  if (selectWg.value.value.list.filter((p: any) => !p.name).length > 0)
    selectWg.value.value.list = [];

  // 商品数据
  let goodItems = new Array();
  for (let i = 0; i < goods.length; i++) {
    const good = goods[i];
    /* 验证：已存在的则不添加 */
    if (selectWg.value.value.list.filter((p: any) => p.id == good.id).length > 0)
      continue;

    /* 筛选所需(商品)字段 */
    let stock = 0;
    let products = good.products.filter((p) => p.isDefault);
    if (products && products.length > 0) stock = products[0].stock;
    goodItems.push({
      id: good.id,
      image: good.image,
      images: good.images,
      name: good.name,
      price: good.price,
      stock,
    });
  }
  if (goodItems.length > 0) {
    hasChooseGoods.value = goodItems;
    selectWg.value.value.list.push(...goodItems);
  }
};

// 获取布局(存储)数据
const appletBodyLoading = ref(false);
const getLayoutData = (pageDesignId: string) => {
  appletBodyLoading.value = true;
  pageDesignItemApi
    .getPageDesignItemLayout(pageDesignId)
    .then((result) => {
      if (result) {
        layoutData.value = result;
        appletBodyLoading.value = false;
      }
    })
    .catch((err: any) => {
      appletBodyLoading.value = false;
      console.error("获取布局(存储)数据出错：", err);
    });
};

// 获取版面设计数据
const getLayoutDesign = (pageDesignId: string) => {
  pageDesignApi
    .getLayoutDesigns(pageDesignId)
    .then((result) => {
      if (result) {
        layoutDesign.value = result;
      }
    })
    .catch((err: any) => {
      console.error("获取版面设计数据出错：", err);
    });
};

// 商品组分类切换
function goodGroupCategoryChange(val: []): void {
  if (!val || val.length < 1) {
    selectWg.value.value.classifyId = null;
    return;
  }
  selectWg.value.value.classifyId = val[val.length - 1];
}

// 处理删除商品
const handleDeleteGoods = (index: number) => {
  selectWg.value.value.list.splice(index, 1);
};

// 删除选项卡
const handleRemoveTabBar = (index: number) => {
  ElMessageBox.confirm(
    `您确定要删除“${selectWg.value.value.list[index].title}”的商品选项卡吗?`,
    "提示",
    {
      confirmButtonText: "确认",
      cancelButtonText: "取消",
      type: "warning",
    }
  ).then(() => {
    selectWg.value.value.list.splice(index, 1);
    ElMessage.success("商品选项卡已删除！");
  });
};

//切换TabBar商品来源类型
const changeTabBarGoodsType = (type: any, index: number) => {
  if (type == "auto") {
    selectWg.value.value.list[index].hasChooseGoods =
      selectWg.value.value.list[index].list;
    selectWg.value.value.list[index].list = defaultGoods.value;
  } else {
    selectWg.value.value.list[index].list =
      selectWg.value.value.list[index].hasChooseGoods.length > 0
        ? selectWg.value.value.list[index].hasChooseGoods
        : defaultGoods.value;
  }
};

// 验证商品选项卡
const handleDeleteTabBarGoods = (key: any, index: number) => {
  selectWg.value.value.list[key].list.splice(index, 1);
};

// 商品组分类切换
function goodTabBarCategoryChange(val: [], index: number): void {
  if (!val || val.length < 1) {
    selectWg.value.value.list[index].classifyId = null;
    return;
  }
  selectWg.value.value.list[index].classifyId = val[val.length - 1];
}

// 添加新的商品选项卡
const handleAddTabBarGoods = () => {
  let list = _.cloneDeep(defaultGoods.value);
  selectWg.value.value.list.push({
    title: "新选项卡名称",
    subTitle: "子标题",
    type: "auto", //auto自动获取  choose 手动选择
    classifyId: "", //所选分类id
    brandId: "", //所选品牌id
    limit: 10,
    column: 2, //分裂数量
    isShow: false,
    list,
    hasChooseGoods: [],
  });
};

// 选择商品秒杀商品
const selectGroupGoods = () => {
  selectGroupSeckillRef.value.openDialog();
};

// 选择的商品秒杀返回结果
const onSelGroupSeckillResult = (groupSeckills: RowPromotionType[]) => {
  if (groupSeckills.length > maxSelectGoods.value) {
    ElMessage.warning(`最多只能选择 ${maxSelectGoods.value} 个！`);
    return;
  }
  if (selectWg.value.type == "groupPurchase") {
    setGroupSeckillValue(groupSeckills);
  }
};

// 商品秒杀选择商品赋值
const setGroupSeckillValue = (groupSeckills: RowPromotionType[]) => {
  // 移除默认(商品)数据
  if (selectWg.value.value.list.filter((p: any) => !p.name).length > 0)
    selectWg.value.value.list = [];

  // 商品秒杀数据
  for (let i = 0; i < groupSeckills.length; i++) {
    const groupSeckill = groupSeckills[i];
    /* 验证：已存在的则不添加 */
    if (selectWg.value.value.list.filter((p: any) => p.id == groupSeckill.id).length > 0)
      continue;
    selectWg.value.value.list.push(groupSeckill);
  }
};

// 选择拼团商品
const selectPinTuanGoods = () => {
  selectPinTuanRuleRef.value.openDialog();
};

// 选择的拼团商品返回结果
const onSelPinTuanRuleResult = (pinTuanRules: RowPinTuanRuleType[]) => {
  if (pinTuanRules.length > maxSelectGoods.value) {
    ElMessage.warning(`最多只能选择 ${maxSelectGoods.value} 个！`);
    return;
  }
  if (selectWg.value.type == "pinTuan") {
    setPinTuanRuleValue(pinTuanRules);
  }
};

// 拼团商品选择商品赋值
const setPinTuanRuleValue = (pinTuanRules: RowPinTuanRuleType[]) => {
  // 移除默认(拼团商品)数据
  if (selectWg.value.value.list.filter((p: any) => !p.name).length > 0)
    selectWg.value.value.list = [];

  // 拼团商品数据
  for (let i = 0; i < pinTuanRules.length; i++) {
    const pinTuanRule = pinTuanRules[i];
    /* 验证：已存在的则不添加 */
    if (selectWg.value.value.list.filter((p: any) => p.id == pinTuanRule.id).length > 0)
      continue;
    selectWg.value.value.list.push(pinTuanRule);
  }
};

// 选择服务商品
const selectServices = () => {
  selectServiceGoodRef.value.openDialog();
};

// 选择服务商品返回结果
const onSelectServiceGoodResult = (serviceGoods: RowServiceGoodType[]) => {
  if (serviceGoods.length > maxSelectGoods.value) {
    ElMessage.warning(`最多只能选择 ${maxSelectGoods.value} 个！`);
    return;
  }
  if (selectWg.value.type == "service") {
    setServiceGoodValue(serviceGoods);
  }
};

// 服务商品选择商品赋值
const setServiceGoodValue = (serviceGoods: RowServiceGoodType[]) => {
  // 移除默认(服务商品)数据
  if (selectWg.value.value.list.filter((p: any) => !p.name).length > 0)
    selectWg.value.value.list = [];

  // 服务商品数据
  for (let i = 0; i < serviceGoods.length; i++) {
    const serviceGood = serviceGoods[i];
    /* 验证：已存在的则不添加 */
    if (selectWg.value.value.list.filter((p: any) => p.id == serviceGood.id).length > 0)
      continue;
    selectWg.value.value.list.push(serviceGood);
  }
};

// 选择公告项
const selectNotices = () => {
  selectNoticeRef.value.openDialog("选择商品");
};

// 选择公告返回结果
const onSelNoticeResult = (notices: RowNoticeType[]) => {
  if (notices.length > maxSelectGoods.value) {
    ElMessage.warning(`最多只能选择 ${maxSelectGoods.value} 个！`);
    return;
  }
  if (selectWg.value.type == "notice") {
    setNoticeValue(notices);
  }
};

// 公告选择商品赋值
const setNoticeValue = (notices: RowNoticeType[]) => {
  // 移除默认(公告)数据
  if (selectWg.value.value.list.filter((p: any) => !p.name).length > 0)
    selectWg.value.value.list = [];

  // 公告数据
  for (let i = 0; i < notices.length; i++) {
    const notice = notices[i];
    /* 验证：已存在的则不添加 */
    if (selectWg.value.value.list.filter((p: any) => p.id == notice.id).length > 0)
      continue;
    selectWg.value.value.list.push(notice);
  }
};

// 删除公告项
const handleDeleteNotice = (index: number) => {
  selectWg.value.value.list.splice(index, 1);
};

// 移除导航项
const handleSlideRemove = (index: number) => {
  selectWg.value.value.list.splice(index, 1);
};

// 添加导航组
const handleAddNav = () => {
  selectWg.value.value.list.push({
    image: design.defaultEmpty(),
    text: "按钮文字",
    linkType: 1,
    linkValue: "",
    showLinkValue: "",
    placeholder: "http开头为webview跳转，其他为站内页面跳转",
    readonly: false,
  });
};

// 链接类型选择
const onLinkTypeChange = (linkType: number, index: number) => {
  switch (linkType) {
    case 1:
      selectWg.value.value.list[index].placeholder =
        "http开头为webview跳转，其他为站内页面跳转";
      selectWg.value.value.list[index].readonly = false;
      break;
    case 2:
      selectWg.value.value.list[index].placeholder = "请选择商品";
      selectWg.value.value.list[index].readonly = true;
      break;
    case 3:
      selectWg.value.value.list[index].placeholder = "请选择文章";
      selectWg.value.value.list[index].readonly = true;
      break;
    case 4:
      selectWg.value.value.list[index].placeholder = "请选择文章分类";
      selectWg.value.value.list[index].readonly = true;
      break;
    /*
    case 5:
      selectWg.value.value.list[index].placeholder = "请选择智能表单";
      selectWg.value.value.list[index].readonly = true;
      break;
      */
  }
  selectWg.value.value.list[index].linkValue = "";
  selectWg.value.value.list[index].showLinkValue = "";
};

// 打开链接指向
let openLinkValueIndex = ref(0);
const onOpenLinkValue = (index: number) => {
  let selectItem = selectWg.value.value.list[index];
  if (selectItem.linkType == 1) return;
  openLinkValueIndex.value = index;
  if (selectItem.linkType == 2) {
    selectSingleGoodsRef.value.openDialog("选择商品");
  } else if (selectItem.linkType == 3) {
    selectArticleRef.value.openDialog("选择文章");
  } else if (selectItem.linkType == 4) {
    selectArticleTypeRef.value.openDialog("选择文章分类");
  } else if (selectItem.linkType == 5) {
    selectFormRef.value.openDialog("选择表单");
  }
};

// 选择的商品返回结果
const onSelectSingleGoodsResult = (goods: RowGoodsType) => {
  selectWg.value.value.list[openLinkValueIndex.value].linkValue = goods.id;
  selectWg.value.value.list[openLinkValueIndex.value].showLinkValue = goods.name;
};

// 选择的文章返回结果
const onSelectArticleResult = (article: RowArticleType) => {
  selectWg.value.value.list[openLinkValueIndex.value].linkValue = article.id;
  selectWg.value.value.list[openLinkValueIndex.value].showLinkValue = article.title;
};

// 选择的文章分类返回结果
const onSelectArticleTypeResult = (articleType: RowArticleTypeType) => {
  selectWg.value.value.list[openLinkValueIndex.value].linkValue = articleType.id;
  selectWg.value.value.list[openLinkValueIndex.value].showLinkValue = articleType.name;
};

// 选择的智能表单返回结果
const onSelectFormResult = (form: RowFormType) => {
  selectWg.value.value.list[openLinkValueIndex.value].linkValue = form.id;
  selectWg.value.value.list[openLinkValueIndex.value].showLinkValue = form.name;
};

// 添加轮播图组
const handleAddSlide = () => {
  selectWg.value.value.list.push({
    image: design.defaultBanner(),
    linkType: 1,
    linkValue: "",
    showLinkValue: "",
    placeholder: "http开头为webview跳转，其他为站内页面跳转",
    readonly: false,
  });
};

// 添加图片分组
const handleAddImgWindow = () => {
  selectWg.value.value.list.push({
    image: design.defaultBanner(),
    linkType: 1,
    linkValue: "",
    showLinkValue: "",
    placeholder: "http开头为webview跳转，其他为站内页面跳转",
    readonly: false,
  });
};

// 选择[布局方式]
const slectTplStyle = (item: any) => {
  selectWg.value.value.style = item.value;
};

// 打开选择文章
let selectCreateArticleIndex = ref(0);
const onOpenArticleHandle = (index: number) => {
  selectCreateArticleRef.value.openDialog("选择文章");
  selectCreateArticleIndex.value = index;
};

// 选择的创建文章返回结果
const onSelectCreateArticleResult = (article: RowArticleType) => {
  selectWg.value.value.list[selectCreateArticleIndex.value].id = article.id;
  selectWg.value.value.list[selectCreateArticleIndex.value].coverImage =
    article.coverImage;
  selectWg.value.value.list[selectCreateArticleIndex.value].title = article.title;
  selectWg.value.value.list[selectCreateArticleIndex.value].brief = article.brief;
};

// 获取文章分类
const getArticleTypeItems = () => {
  articleTypeApi
    .getArticleTypeList({
      name: null,
      sorting: "Sort",
      skipCount: 0,
      maxResultCount: 1000,
    })
    .then((result) => {
      articleTypeItems.value = result.items;
    })
    .catch((err: any) => {
      console.error("查询文章分类列表出错：", err);
    });
};

// 提交
const layoutSubmit = () => {
  if (layoutData.value.length < 1) {
    ElMessage.warning("您还未设计新的页面布局，请检查！");
    return;
  }
  let pageDesignId = String(route.query.id);
  let pageDesignItems = [] as RowPageDesignItemType[];
  for (let i = 0; i < layoutData.value.length; i++) {
    const layoutItem = layoutData.value[i];
    let parameters = JSON.stringify(layoutItem.value);
    if (layoutItem.type == "textarea") {
      parameters = layoutItem.value;
    }
    pageDesignItems.push({
      id: getGuidEmpty(),
      tenantId: null,
      widgetCode: layoutItem.type,
      pageCode: pageDesignId,
      positionId: i,
      sort: i + 1,
      parameters,
      concurrencyStamp: getCode(),
    });
  }
  pageDesignItemApi
    .submitPageDesignItem(pageDesignId, pageDesignItems)
    .then((result) => {
      if (result) {
        ElMessage.success("保存成功！");
        layoutCancel();
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 返回
const layoutCancel = () => {
  mittBus.emit(
    "onCurrentContextmenuClick",
    Object.assign({}, { contextMenuClickId: 1, ...route })
  );
};

// 页面加载完时
onMounted(() => {
  itemHeight.value = window.innerHeight - 180;
  getArticleTypeItems();
  if (route.query.id) {
    getLayoutData(String(route.query.id));
    getLayoutDesign(String(route.query.id));
  }
});
</script>

<style lang="scss">
@import "/@/assets/design/style.css";

.layout-applet-body .layout-item,
.layout-widget-body .layout-item {
  .el-card__header {
    background-color: #f5f7fa;
  }
  .el-card__body {
    padding: 0px 0px 0px 20px !important;
  }
}

.layout-applet-config-body .layout-item {
  .el-card__header {
    background-color: #f5f7fa;
  }
  .el-card__body {
    padding: 0px 15px !important;
  }
}
</style>

<style scoped lang="scss">
.layout-applet-body {
  position: relative;
}

.layout-config-body {
  height: 100%;
  overflow-y: auto;
}

.layout-item {
  background-color: white;
  border-radius: 5px;
  border: 1px #e4e7ed solid;
  .model-img {
    width: 100%;
    height: 80px;
  }
  .component-item {
    overflow-y: auto;
    overflow-x: hidden;
  }
}

.component-divider {
  width: calc(100% - 20px);
}

.draggable-component {
  .component-item-li {
    width: 116px;
    float: left;
    padding: 8px 0;
    margin: 5px;
    text-align: center;
    display: block;
    background-color: #f5f5f5;
    border: 1px solid #ddd;
    font-size: 12px;
    color: #333;
    cursor: move;
    -webkit-transition: all 0.5s;
    transition: all 0.5s;
    list-style: none;
    list-style-type: none;

    .icon-comp {
      color: #5a5a5a;
    }
  }
}

.item-clear {
  clear: both;
}

.grag-container {
  .layout-list {
    overflow-y: auto;
    .layout-main {
      background-color: white;
    }
  }
}

.sel-good-img {
  width: 60px;
  height: 60px;
}

.card-submit-content {
  margin-top: 10px;
  height: 55px;
  line-height: 55px;
  background-color: white;
  text-align: center;
  .footer-btn {
    width: 80px;
  }
}

.tag-notice {
  cursor: move;
}
</style>
