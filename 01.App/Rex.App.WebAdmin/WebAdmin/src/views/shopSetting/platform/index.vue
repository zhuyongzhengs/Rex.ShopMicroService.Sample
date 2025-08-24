<template>
  <div class="platform-container layout-padding">
    <el-row>
      <el-col :span="24">
        <el-form
          ref="basicInfoFormRef"
          :model="platformSettingForm"
          size="default"
          label-width="124px"
        >
          <el-tabs
            type="border-card"
            v-model="activeName"
            class="tabs-box"
            :style="'height:' + tabsHeight + 'px'"
          >
            <el-tab-pane label="特殊开关" name="SpecialSwitch">
              <el-card class="mb20">
                <div class="special-switch-prompt">
                  1、如果开启【显示门店列表】模块，则微信小程序审核如果发现可能会失败，并提示“<el-text
                    type="danger"
                    size="default"
                    >【小程序涉及多个商家提供商品的在线交易及配送，请补充选择：电商平台-电商平台类目。】</el-text
                  >”，可先关闭，审核后再开启。
                </div>
                <div class="special-switch-prompt">
                  2、如果开启【显示充值】模块，则微信小程序审核如果发现可能会失败，并提示“<el-text
                    type="danger"
                    size="default"
                    >【小程序页面内容涉及账户充值服务，需补充商家自营-预付卡销售-发行方类目。】</el-text
                  >”，可先关闭，审核后再开启。
                </div>
              </el-card>
              <el-form-item :label="platformSettingForm.specialSwitchs[0].label">
                <el-radio-group v-model="platformSettingForm.specialSwitchs[0].value">
                  <el-radio label="True" size="default">开启</el-radio>
                  <el-radio label="False" size="default">不开启</el-radio>
                </el-radio-group>
              </el-form-item>
              <el-form-item :label="platformSettingForm.specialSwitchs[1].label">
                <el-radio-group v-model="platformSettingForm.specialSwitchs[1].value">
                  <el-radio label="True" size="default">开启</el-radio>
                  <el-radio label="False" size="default">不开启</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="平台设置" name="PlatformSetting">
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[0].label">
                    <el-input
                      :placeholder="
                        '请输入' + platformSettingForm.platformSettings[0].label
                      "
                      v-model="platformSettingForm.platformSettings[0].value"
                      maxlength="200"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >平台名称会显示到前台，请合理输入此名称</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[1].label">
                    <el-input
                      type="textarea"
                      maxlength="450"
                      :autosize="{ minRows: 4, maxRows: 6 }"
                      :placeholder="
                        '请输入' + platformSettingForm.platformSettings[1].label
                      "
                      v-model="platformSettingForm.platformSettings[1].value"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[2].label">
                    <el-input
                      :placeholder="
                        '请输入' + platformSettingForm.platformSettings[2].label
                      "
                      v-model="platformSettingForm.platformSettings[2].value"
                      maxlength="200"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="5">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">如：湘ICP备15004965号-2</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="11">
                  <el-form-item :label="platformSettingForm.platformSettings[3].label">
                    <el-input
                      :readonly="true"
                      :placeholder="
                        '请选择' + platformSettingForm.platformSettings[3].label
                      "
                      v-model="platformSettingForm.platformSettings[3].value"
                      maxlength="200"
                      clearable
                    >
                      <template #append>
                        <el-button
                          type="primary"
                          plain
                          size="default"
                          :icon="Edit"
                          @click="openSelectArticle(3)"
                        ></el-button>
                      </template>
                    </el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="11">
                  <el-form-item :label="platformSettingForm.platformSettings[4].label">
                    <el-input
                      :readonly="true"
                      :placeholder="
                        '请选择' + platformSettingForm.platformSettings[4].label
                      "
                      v-model="platformSettingForm.platformSettings[4].value"
                      maxlength="200"
                      clearable
                    >
                      <template #append>
                        <el-button
                          type="primary"
                          plain
                          size="default"
                          :icon="Edit"
                          @click="openSelectArticle(4)"
                        ></el-button>
                      </template>
                    </el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="11">
                  <el-form-item :label="platformSettingForm.platformSettings[5].label">
                    <el-input
                      :readonly="true"
                      :placeholder="
                        '请选择' + platformSettingForm.platformSettings[5].label
                      "
                      v-model="platformSettingForm.platformSettings[5].value"
                      maxlength="200"
                      clearable
                    >
                      <template #append>
                        <el-button
                          type="primary"
                          plain
                          size="default"
                          :icon="Edit"
                          @click="openSelectArticle(5)"
                        ></el-button>
                      </template>
                    </el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[6].label">
                    <el-input
                      :placeholder="
                        '请输入' + platformSettingForm.platformSettings[6].label
                      "
                      v-model="platformSettingForm.platformSettings[6].value"
                      :readonly="true"
                      maxlength="1000"
                    >
                      <template #prepend>
                        <el-button
                          :icon="Picture"
                          title="预览"
                          @click="
                            onImageViewer(platformSettingForm.platformSettings[6].value)
                          "
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
                          :on-success="handlePlatformSetting6ImageSuccess"
                        >
                          <template #trigger
                            ><el-button
                              type="primary"
                              plain
                              size="default"
                              :icon="Upload"
                            ></el-button
                          ></template>
                        </el-upload>
                      </template>
                    </el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[7].label">
                    <el-input
                      :readonly="true"
                      :placeholder="
                        '请输入' + platformSettingForm.platformSettings[7].label
                      "
                      v-model="platformSettingForm.platformSettings[7].value"
                      maxlength="1000"
                    >
                      <template #prepend>
                        <el-button
                          :icon="Picture"
                          title="预览"
                          @click="
                            onImageViewer(platformSettingForm.platformSettings[7].value)
                          "
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
                          :on-success="handleSetting7ImageSuccess"
                        >
                          <template #trigger
                            ><el-button
                              type="primary"
                              plain
                              size="default"
                              :icon="Upload"
                            ></el-button
                          ></template>
                        </el-upload>
                      </template>
                    </el-input>
                  </el-form-item>
                </el-col>
              </el-row>
              <!--
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[8].label">
                    <el-radio-group
                      v-model="platformSettingForm.platformSettings[8].value">
                      <el-radio label="True" size="default">开启</el-radio>
                      <el-radio label="False" size="default">不开启</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[9].label">
                    <el-radio-group
                      v-model="platformSettingForm.platformSettings[9].value">
                      <el-radio label="True" size="default">开启</el-radio>
                      <el-radio label="False" size="default">不开启</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              -->
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.platformSettings[10].label">
                    <el-input
                      :placeholder="
                        '请输入' + platformSettingForm.platformSettings[10].label
                      "
                      v-model="platformSettingForm.platformSettings[10].value"
                      maxlength="200"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="5">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >多个【搜索发现】关键字请用 | 分割</el-text
                    >
                  </div>
                </el-col>
              </el-row>
            </el-tab-pane>
            <el-tab-pane label="分享设置" name="ShareSetting">
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.shareSettings[0].label">
                    <el-input
                      :placeholder="'请输入' + platformSettingForm.shareSettings[0].label"
                      v-model="platformSettingForm.shareSettings[0].value"
                      maxlength="200"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="5">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">微信小程序首页分享的标题</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.shareSettings[1].label">
                    <el-input
                      :placeholder="'请输入' + platformSettingForm.shareSettings[1].label"
                      v-model="platformSettingForm.shareSettings[1].value"
                      maxlength="1000"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="5">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >只在支付宝小程序分享中显示</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.shareSettings[2].label">
                    <el-input
                      :placeholder="
                        '请输入' + platformSettingForm.shareSettings[2].label + '链接'
                      "
                      v-model="platformSettingForm.shareSettings[2].value"
                      maxlength="1000"
                      :readonly="true"
                    >
                      <template #prepend>
                        <el-button
                          :icon="Picture"
                          title="预览"
                          @click="
                            onImageViewer(platformSettingForm.shareSettings[2].value)
                          "
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
                          :on-success="handleShareSetting2ImageSuccess"
                        >
                          <template #trigger
                            ><el-button
                              type="primary"
                              plain
                              size="default"
                              :icon="Upload"
                            ></el-button
                          ></template>
                        </el-upload>
                      </template>
                    </el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="5">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">微信小程序首页分享的图片</el-text>
                  </div>
                </el-col>
              </el-row>
            </el-tab-pane>
            <el-tab-pane label="会员设置" name="UsersSetting">
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.usersSettings[0].label">
                    <el-radio-group v-model="platformSettingForm.usersSettings[0].value">
                      <el-radio label="True" size="default">绑定</el-radio>
                      <el-radio label="False" size="default">不绑定</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
                <el-col :span="10">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >第三方登录的时候是否需要绑定手机号码，强烈建议绑定手机号码</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.usersSettings[1].label">
                    <el-input
                      :placeholder="'请输入' + platformSettingForm.usersSettings[1].label"
                      v-model="platformSettingForm.usersSettings[1].value"
                      maxlength="20"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="5">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >前台下单时给商家发送短信通知</el-text
                    >
                  </div>
                </el-col>
              </el-row>
            </el-tab-pane>
            <el-tab-pane label="商品设置" name="GoodsSetting"
              ><el-row>
                <el-col :span="5">
                  <el-form-item :label="platformSettingForm.goodsSettings[0].label">
                    <el-input
                      v-model="platformSettingForm.goodsSettings[0].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.goodsSettings[0].label"
                      @input="
                        platformSettingForm.goodsSettings[0].value = verifiyNumberInteger(
                          platformSettingForm.goodsSettings[0].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >商品中只要有货品库存低于报警数量，就会在后台提示</el-text
                    >
                  </div>
                </el-col>
              </el-row></el-tab-pane
            >
            <el-tab-pane label="订单设置" name="OrderSetting">
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.orderSettings[0].label">
                    <el-input
                      v-model="platformSettingForm.orderSettings[0].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.orderSettings[0].label"
                      @input="
                        platformSettingForm.orderSettings[0].value = verifiyNumberInteger(
                          platformSettingForm.orderSettings[0].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="14">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >未付款订单取消的时间间隔，单位为【分钟】，请设置10分钟以上时间，因为订单取消时间5分钟执行一次</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.orderSettings[1].label">
                    <el-input
                      v-model="platformSettingForm.orderSettings[1].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.orderSettings[1].label"
                      @input="
                        platformSettingForm.orderSettings[1].value = verifiyNumberInteger(
                          platformSettingForm.orderSettings[1].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="8">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >已付款的订单完成的时间间隔，单位为【天】，一般设置3天</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.orderSettings[2].label">
                    <el-input
                      v-model="platformSettingForm.orderSettings[2].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.orderSettings[2].label"
                      @input="
                        platformSettingForm.orderSettings[2].value = verifiyNumberInteger(
                          platformSettingForm.orderSettings[2].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="10">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >发货后的订单自动确认收货时间,单位为【天】，一般设置12天</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.orderSettings[3].label">
                    <el-input
                      v-model="platformSettingForm.orderSettings[3].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.orderSettings[3].label"
                      @input="
                        platformSettingForm.orderSettings[3].value = verifiyNumberInteger(
                          platformSettingForm.orderSettings[3].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="10">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >确认收货后的订单自动评价时间间隔,单位为【天】，一般设置3天</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.orderSettings[4].label">
                    <el-input
                      v-model="platformSettingForm.orderSettings[4].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.orderSettings[4].label"
                      @input="
                        platformSettingForm.orderSettings[4].value = verifiyNumberInteger(
                          platformSettingForm.orderSettings[4].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >订单催付款时间,单位为【分钟】，请设置10分钟以上时间，因为催付款任务5分钟执行一次</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="7">
                  <el-form-item :label="platformSettingForm.orderSettings[5].label">
                    <el-input
                      :placeholder="'请输入' + platformSettingForm.orderSettings[5].label"
                      v-model="platformSettingForm.orderSettings[5].value"
                      maxlength="20"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">用户退货时的收货人姓名</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="7">
                  <el-form-item :label="platformSettingForm.orderSettings[6].label">
                    <el-input
                      :placeholder="'请输入' + platformSettingForm.orderSettings[6].label"
                      v-model="platformSettingForm.orderSettings[6].value"
                      maxlength="20"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >用户退货时的收货人联系方式</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="7">
                  <el-form-item :label="platformSettingForm.orderSettings[7].label">
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
                <el-col :span="2">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">退货区域设置</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="7">
                  <el-form-item :label="platformSettingForm.orderSettings[8].label">
                    <el-input
                      :placeholder="'请选择' + platformSettingForm.orderSettings[8].label"
                      v-model="platformSettingForm.orderSettings[8].value"
                      maxlength="20"
                      :readonly="true"
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
                <el-col :span="2">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">退货坐标</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="7">
                  <el-form-item :label="platformSettingForm.orderSettings[9].label">
                    <el-input
                      :placeholder="'请输入' + platformSettingForm.orderSettings[9].label"
                      v-model="platformSettingForm.orderSettings[9].value"
                      maxlength="20"
                      clearable
                    ></el-input>
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">退货的详细地址</el-text>
                  </div>
                </el-col>
              </el-row>
              <!--
              <el-row class="mb10">
                <el-col :span="24">
                  <div class="form-item-desc">退货相关</div>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="24">
                  <el-form-item :label="platformSettingForm.orderSettings[10].label">
                    <el-radio-group v-model="platformSettingForm.orderSettings[10].value">
                      <el-radio label="True" size="default">开启</el-radio>
                      <el-radio label="False" size="default">不开启</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              -->
            </el-tab-pane>
            <el-tab-pane label="积分设置" name="PointsSetting">
              <el-row class="mb20">
                <el-col :span="24">
                  <el-form-item :label="platformSettingForm.pointsSettings[0].label">
                    <el-radio-group v-model="platformSettingForm.pointsSettings[0].value">
                      <el-radio label="True" size="default">开启</el-radio>
                      <el-radio label="False" size="default">不开启</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.pointsSettings[1].label">
                    <el-input
                      v-model="platformSettingForm.pointsSettings[1].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.pointsSettings[1].label
                      "
                      @input="
                        platformSettingForm.pointsSettings[1].value = verifiyNumberInteger(
                          platformSettingForm.pointsSettings[1].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >多少积分可以折现1元人民币</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.pointsSettings[2].label">
                    <el-input
                      v-model="platformSettingForm.pointsSettings[2].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.pointsSettings[2].label
                      "
                      @input="
                        platformSettingForm.pointsSettings[2].value = verifiyNumberInteger(
                          platformSettingForm.pointsSettings[2].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >（%）单个订单积分折现最大百分比</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="6">
                  <el-form-item :label="platformSettingForm.pointsSettings[3].label">
                    <el-input
                      v-model="platformSettingForm.pointsSettings[3].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.pointsSettings[3].label
                      "
                      @input="
                        platformSettingForm.pointsSettings[3].value = verifiyNumberInteger(
                          platformSettingForm.pointsSettings[3].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >订单多少人民币奖励1个积分</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="24">
                  <el-form-item :label="platformSettingForm.pointsSettings[4].label">
                    <el-radio-group v-model="platformSettingForm.pointsSettings[4].value">
                      <el-radio label="1" size="default">固定奖励</el-radio>
                      <el-radio label="2" size="default">随机奖励</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>

              <!-- 随机奖励 -->
              <template v-if="platformSettingForm.pointsSettings[4].value === '2'">
                <el-row class="mb20">
                  <el-col :span="6">
                    <el-form-item
                      label-width="140"
                      :label="platformSettingForm.pointsSettings[5].label"
                    >
                      <el-input
                        v-model="platformSettingForm.pointsSettings[5].value"
                        size="default"
                        maxlength="4"
                        clearable
                        :placeholder="
                          '请输入' + platformSettingForm.pointsSettings[5].label
                        "
                        @input="
                          platformSettingForm.pointsSettings[5].value = verifiyNumberInteger(
                            platformSettingForm.pointsSettings[5].value
                          )
                        "
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="4">
                    <div class="form-item-hint">
                      <el-text type="info" size="default">签到随机最小奖励积分</el-text>
                    </div>
                  </el-col>
                </el-row>
                <el-row>
                  <el-col :span="6">
                    <el-form-item
                      label-width="140"
                      :label="platformSettingForm.pointsSettings[6].label"
                    >
                      <el-input
                        v-model="platformSettingForm.pointsSettings[6].value"
                        size="default"
                        maxlength="4"
                        clearable
                        :placeholder="
                          '请输入' + platformSettingForm.pointsSettings[6].label
                        "
                        @input="
                          platformSettingForm.pointsSettings[6].value = verifiyNumberInteger(
                            platformSettingForm.pointsSettings[6].value
                          )
                        "
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="4">
                    <div class="form-item-hint">
                      <el-text type="info" size="default">签到随机最大奖励积分</el-text>
                    </div>
                  </el-col>
                </el-row>
              </template>

              <!-- 固定奖励 -->
              <template v-else>
                <el-row class="mb20">
                  <el-col :span="6">
                    <el-form-item
                      label-width="140"
                      :label="platformSettingForm.pointsSettings[7].label"
                    >
                      <el-input
                        v-model="platformSettingForm.pointsSettings[7].value"
                        size="default"
                        maxlength="4"
                        clearable
                        :placeholder="
                          '请输入' + platformSettingForm.pointsSettings[5].label
                        "
                        @input="
                          platformSettingForm.pointsSettings[7].value = verifiyNumberInteger(
                            platformSettingForm.pointsSettings[7].value
                          )
                        "
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="4">
                    <div class="form-item-hint">
                      <el-text type="info" size="default">起始签到奖励积分</el-text>
                    </div>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="6">
                    <el-form-item
                      label-width="140"
                      :label="platformSettingForm.pointsSettings[8].label"
                    >
                      <el-input
                        v-model="platformSettingForm.pointsSettings[8].value"
                        size="default"
                        maxlength="4"
                        clearable
                        :placeholder="
                          '请输入' + platformSettingForm.pointsSettings[8].label
                        "
                        @input="
                          platformSettingForm.pointsSettings[8].value = verifiyNumberInteger(
                            platformSettingForm.pointsSettings[8].value
                          )
                        "
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="4">
                    <div class="form-item-hint">
                      <el-text type="info" size="default">连续签到追加积分 </el-text>
                    </div>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="6">
                    <el-form-item
                      label-width="140"
                      :label="platformSettingForm.pointsSettings[9].label"
                    >
                      <el-input
                        v-model="platformSettingForm.pointsSettings[9].value"
                        size="default"
                        maxlength="4"
                        clearable
                        :placeholder="
                          '请输入' + platformSettingForm.pointsSettings[9].label
                        "
                        @input="
                          platformSettingForm.pointsSettings[9].value = verifiyNumberInteger(
                            platformSettingForm.pointsSettings[9].value
                          )
                        "
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="4">
                    <div class="form-item-hint">
                      <el-text type="info" size="default"
                        >连续签到奖励积分单日上限
                      </el-text>
                    </div>
                  </el-col>
                </el-row>
              </template>
            </el-tab-pane>
            <el-tab-pane label="提现设置" name="CashSetting">
              <el-row class="mb20">
                <el-col :span="5">
                  <el-form-item
                    label-width="140"
                    :label="platformSettingForm.cashSettings[0].label"
                  >
                    <el-input
                      v-model="platformSettingForm.cashSettings[0].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.cashSettings[0].label"
                      @input="
                        platformSettingForm.cashSettings[0].value = verifyNumberIntegerAndFloat(
                          platformSettingForm.cashSettings[0].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >最低提现标准，默认0不限制</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="5">
                  <el-form-item
                    label-width="140"
                    :label="platformSettingForm.cashSettings[1].label"
                  >
                    <el-input
                      v-model="platformSettingForm.cashSettings[1].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.cashSettings[1].label"
                      @input="
                        platformSettingForm.cashSettings[1].value = verifiyNumberInteger(
                          platformSettingForm.cashSettings[1].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >提现费率，默认0% 没有费率</el-text
                    >
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="5">
                  <el-form-item
                    label-width="140"
                    :label="platformSettingForm.cashSettings[2].label"
                  >
                    <el-input
                      v-model="platformSettingForm.cashSettings[2].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="'请输入' + platformSettingForm.cashSettings[2].label"
                      @input="
                        platformSettingForm.cashSettings[2].value = verifiyNumberInteger(
                          platformSettingForm.cashSettings[2].value
                        )
                      "
                    />
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >每日提现上限，默认0不限制</el-text
                    >
                  </div>
                </el-col>
              </el-row>
            </el-tab-pane>
            <!--
            <el-tab-pane label="邀请好友设置" name="InviteFriendSetting">
              <el-row class="mb10">
                <el-col :span="24">
                  <div class="form-item-desc">
                    佣金设置（当不开启三级分销时候，推广好友三级统一返现比例）
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="24">
                  <el-form-item
                    :label="platformSettingForm.inviteFriendSettings[0].label">
                    <el-radio-group
                      v-model="platformSettingForm.inviteFriendSettings[0].value">
                      <el-radio label="1" size="default">百分比</el-radio>
                      <el-radio label="2" size="default">固定金额</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="5">
                  <el-form-item
                    :label="platformSettingForm.inviteFriendSettings[1].label">
                    <el-input
                      v-model="platformSettingForm.inviteFriendSettings[1].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.inviteFriendSettings[1].label
                      "
                      @input="
                        platformSettingForm.inviteFriendSettings[1].value = verifiyNumberInteger(
                          platformSettingForm.inviteFriendSettings[1].value
                        )
                      " />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="5">
                  <el-form-item
                    :label="platformSettingForm.inviteFriendSettings[2].label">
                    <el-input
                      v-model="platformSettingForm.inviteFriendSettings[2].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.inviteFriendSettings[2].label
                      "
                      @input="
                        platformSettingForm.inviteFriendSettings[2].value = verifiyNumberInteger(
                          platformSettingForm.inviteFriendSettings[2].value
                        )
                      " />
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row>
                <el-col :span="5">
                  <el-form-item
                    :label="platformSettingForm.inviteFriendSettings[3].label">
                    <el-input
                      v-model="platformSettingForm.inviteFriendSettings[3].value"
                      size="default"
                      maxlength="4"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.inviteFriendSettings[3].label
                      "
                      @input="
                        platformSettingForm.inviteFriendSettings[3].value = verifiyNumberInteger(
                          platformSettingForm.inviteFriendSettings[3].value
                        )
                      " />
                  </el-form-item>
                </el-col>
              </el-row>
            </el-tab-pane>
            <el-tab-pane label="附件设置" name="FilesStorageSetting">
              <el-row class="mb20">
                <el-col :span="24">
                  <el-form-item :label="platformSettingForm.fileStorageSettings[0].label">
                    <el-radio-group
                      v-model="platformSettingForm.fileStorageSettings[0].value">
                      <el-radio label="LocalStorage" size="default">本地存储</el-radio>
                      <el-radio label="AliYunOSS" size="default">阿里云OSS</el-radio>
                      <el-radio label="QCloudOSS" size="default">腾讯云OSS</el-radio>
                      <el-radio label="QiNiuKoDo" size="default">七牛云Kodu</el-radio>
                    </el-radio-group>
                  </el-form-item>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.fileStorageSettings[1].label">
                    <el-input
                      v-model="platformSettingForm.fileStorageSettings[1].value"
                      size="default"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.fileStorageSettings[1].label
                      " />
                  </el-form-item>
                </el-col>
                <el-col :span="12">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >请使用路径，如"/upload/doc/"，路径前后都要加“/”（七牛云为key/value结构不支持）</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.fileStorageSettings[2].label">
                    <el-input
                      v-model="platformSettingForm.fileStorageSettings[2].value"
                      size="default"
                      clearable
                      :placeholder="
                        '请输入' + platformSettingForm.fileStorageSettings[2].label
                      " />
                  </el-form-item>
                </el-col>
                <el-col :span="4">
                  <div class="form-item-hint">
                    <el-text type="info" size="default">使用小写逗号分割</el-text>
                  </div>
                </el-col>
              </el-row>
              <el-row class="mb20">
                <el-col :span="8">
                  <el-form-item :label="platformSettingForm.fileStorageSettings[3].label">
                    <el-input
                      v-model="platformSettingForm.fileStorageSettings[3].value"
                      size="default"
                      maxlength="4"
                      clearable
                      @input="
                        platformSettingForm.fileStorageSettings[3].value = verifyNumberIntegerAndFloat(
                          platformSettingForm.fileStorageSettings[3].value
                        )
                      "
                      :placeholder="
                        '请输入' + platformSettingForm.fileStorageSettings[3].label
                      " />
                  </el-form-item>
                </el-col>
                <el-col :span="6">
                  <div class="form-item-hint">
                    <el-text type="info" size="default"
                      >M（兆），一般20M即可，超过网络容易缓慢</el-text>
                  </div>
                </el-col>
              </el-row>
              <template
                v-if="platformSettingForm.fileStorageSettings[0].value == 'AliYunOSS'">
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储绑定域名">
                      <el-input
                        v-model="aliYunOssItem.storageBucketBindUrl"
                        size="default"
                        clearable
                        placeholder="请输入云存储绑定域名" />
                    </el-form-item>
                  </el-col>
                  <el-col :span="6">
                    <div class="form-item-hint">
                      <el-text type="info" size="default"
                        >独立绑定的域名最好，也可以用云存储提供的多级域名</el-text>
                    </div>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储授权账户">
                      <el-input
                        v-model="aliYunOssItem.storageAccessKeyId"
                        size="default"
                        clearable
                        placeholder="请输入云存储授权账户" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储授权密钥">
                      <el-input
                        v-model="aliYunOssItem.storageAccessKeySecret"
                        size="default"
                        clearable
                        placeholder="请输入云存储授权密钥" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="阿里云节点">
                      <el-input
                        v-model="aliYunOssItem.storageAliYunEndpoint"
                        size="default"
                        clearable
                        placeholder="请输入阿里云节点" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="阿里云桶名称">
                      <el-input
                        v-model="aliYunOssItem.storageAliYunBucketName"
                        size="default"
                        clearable
                        placeholder="请输入阿里云桶名称" />
                    </el-form-item>
                  </el-col>
                </el-row>
              </template>
              <template
                v-if="platformSettingForm.fileStorageSettings[0].value == 'QCloudOSS'">
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储绑定域名">
                      <el-input
                        v-model="qCloudOssItem.storageBucketBindUrl"
                        size="default"
                        clearable
                        placeholder="请输入云存储绑定域名" />
                    </el-form-item>
                  </el-col>
                  <el-col :span="6">
                    <div class="form-item-hint">
                      <el-text type="info" size="default"
                        >独立绑定的域名最好，也可以用云存储提供的多级域名</el-text>
                    </div>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储授权账户">
                      <el-input
                        v-model="qCloudOssItem.storageAccessKeyId"
                        size="default"
                        clearable
                        placeholder="请输入云存储授权账户" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储授权密钥">
                      <el-input
                        v-model="qCloudOssItem.storageAccessKeySecret"
                        size="default"
                        clearable
                        placeholder="请输入云存储授权密钥" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="腾讯云账户标识">
                      <el-input
                        v-model="qCloudOssItem.storageTencentAccountId"
                        size="default"
                        clearable
                        placeholder="请输入腾讯云账户标识" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="腾讯云桶地域">
                      <el-input
                        v-model="qCloudOssItem.storageTencentCosRegion"
                        size="default"
                        clearable
                        placeholder="请输入腾讯云桶地域" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="腾讯云桶名称">
                      <el-input
                        v-model="qCloudOssItem.storageTencentBucketName"
                        size="default"
                        clearable
                        placeholder="请输入腾讯云桶名称" />
                    </el-form-item>
                  </el-col>
                </el-row>
              </template>
              <template
                v-if="platformSettingForm.fileStorageSettings[0].value == 'QiNiuKoDo'">
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储绑定域名">
                      <el-input
                        v-model="qiNiuKoDoItem.storageBucketBindUrl"
                        size="default"
                        clearable
                        placeholder="请输入云存储绑定域名" />
                    </el-form-item>
                  </el-col>
                  <el-col :span="6">
                    <div class="form-item-hint">
                      <el-text type="info" size="default"
                        >独立绑定的域名最好，也可以用云存储提供的多级域名</el-text>
                    </div>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储授权账户">
                      <el-input
                        v-model="qiNiuKoDoItem.storageAccessKeyId"
                        size="default"
                        clearable
                        placeholder="请输入云存储授权账户" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="云存储授权密钥">
                      <el-input
                        v-model="qiNiuKoDoItem.storageAccessKeySecret"
                        size="default"
                        clearable
                        placeholder="请输入云存储授权密钥" />
                    </el-form-item>
                  </el-col>
                </el-row>
                <el-row class="mb20">
                  <el-col :span="8">
                    <el-form-item label="七牛云桶名称">
                      <el-input
                        v-model="qiNiuKoDoItem.storageQiNiuBucketName"
                        size="default"
                        clearable
                        placeholder="请输入七牛云桶名称" />
                    </el-form-item>
                  </el-col>
                </el-row>
              </template>
            </el-tab-pane>
            -->
            <el-tab-pane label="微信平台" name="WeChatPlatform">
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>微信小程序</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row class="mb10">
                    <el-col :span="8">
                      <el-form-item
                        :label="platformSettingForm.weChatMiniPrograms[0].label"
                      >
                        <el-input
                          v-model="platformSettingForm.weChatMiniPrograms[0].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatMiniPrograms[0].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <el-form-item
                        :label="platformSettingForm.weChatMiniPrograms[1].label"
                      >
                        <el-input
                          v-model="platformSettingForm.weChatMiniPrograms[1].value"
                          size="default"
                          :placeholder="
                            '请输入' + platformSettingForm.weChatMiniPrograms[1].label
                          "
                          type="password"
                          show-password
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <el-form-item
                        label-width="180"
                        :label="platformSettingForm.weChatMiniPrograms[2].label"
                      >
                        <el-input
                          v-model="platformSettingForm.weChatMiniPrograms[2].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatMiniPrograms[2].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="8">
                      <el-form-item
                        :label="platformSettingForm.weChatMiniPrograms[3].label"
                      >
                        <el-input
                          v-model="platformSettingForm.weChatMiniPrograms[3].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatMiniPrograms[3].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>微信支付</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row class="mb10">
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[0].label">
                        <el-input
                          v-model="platformSettingForm.weChatPays[0].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatPays[0].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[1].label">
                        <el-input
                          v-model="platformSettingForm.weChatPays[1].value"
                          size="default"
                          :placeholder="
                            '请输入' + platformSettingForm.weChatPays[1].label
                          "
                          type="password"
                          show-password
                        />
                      </el-form-item>
                    </el-col>
                    <!--
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[2].label">
                        <el-radio-group v-model="platformSettingForm.weChatPays[2].value">
                          <el-radio label="True" size="default">是</el-radio>
                          <el-radio label="False" size="default">否</el-radio>
                        </el-radio-group>
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[5].label">
                        <el-input
                          v-model="platformSettingForm.weChatPays[5].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatPays[5].label
                          " />
                      </el-form-item>
                    </el-col>
                    -->
                    <el-col :span="8">
                      <el-form-item label="证书文件">
                        <el-input
                          v-model="platformSettingForm.weChatPays[6].value"
                          size="default"
                          :readonly="true"
                          :placeholder="
                            '请选择' + platformSettingForm.weChatPays[6].label
                          "
                        >
                          <template #append>
                            <el-upload
                              class="upload-cert-box"
                              accept=".p12,.pfx,.pem,.cer,.crt,.key"
                              :action="uploadCertFileConfig().action"
                              :headers="uploadCertFileConfig().headers"
                              :multiple="false"
                              :show-file-list="false"
                              :on-success="handleWeChatPaysCertSuccess"
                            >
                              <template #trigger
                                ><el-button
                                  type="primary"
                                  plain
                                  size="default"
                                  :icon="Upload"
                                ></el-button
                              ></template>
                            </el-upload>
                          </template>
                        </el-input>
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row class="mb10">
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[7].label">
                        <el-input
                          v-model="platformSettingForm.weChatPays[7].value"
                          size="default"
                          :placeholder="
                            '请输入' + platformSettingForm.weChatPays[7].label
                          "
                          type="password"
                          show-password
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[3].label">
                        <el-input
                          v-model="platformSettingForm.weChatPays[3].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatPays[3].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.weChatPays[4].label">
                        <el-input
                          v-model="platformSettingForm.weChatPays[4].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.weChatPays[4].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
            </el-tab-pane>
            <el-tab-pane label="其它设置" name="OtherSetting">
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>客服</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[0].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[0].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[0].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="3">
                      <div class="form-item-hint">
                        <el-text type="info" size="default">客服ID，找客服开通</el-text>
                      </div>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>腾讯地图</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[1].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[1].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[1].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <div class="form-item-hint">
                        <el-text type="info" size="default"
                          >腾讯地图key，申请地址：https://lbs.qq.com/</el-text
                        >
                      </div>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>易源接口</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row class="mb10">
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[2].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[2].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[2].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <div class="form-item-hint">
                        <el-text type="info" size="default"
                          >通用接口平台，接口非常便宜，暂时用于快递查询，申请地址：https://www.showapi.com</el-text
                        >
                      </div>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[3].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[3].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[3].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>快递100</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row class="mb10">
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[4].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[4].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[4].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <div class="form-item-hint">
                        <el-text type="info" size="default"
                          >快递100公司编码，申请地址：https://www.kuaidi100.com</el-text
                        >
                      </div>
                    </el-col>
                  </el-row>
                  <el-row>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[5].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[5].value"
                          size="default"
                          clearable
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[5].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="8">
                      <div class="form-item-hint">
                        <el-text type="info" size="default"
                          >快递100授权key，申请地址：https://www.kuaidi100.com</el-text
                        >
                      </div>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
              <el-card shadow="hover" class="setting-card">
                <template #header>
                  <div class="card-header">
                    <span>统计代码</span>
                  </div>
                </template>
                <div class="setting-content-item">
                  <el-row>
                    <el-col :span="8">
                      <el-form-item :label="platformSettingForm.otherSettings[6].label">
                        <el-input
                          v-model="platformSettingForm.otherSettings[6].value"
                          size="default"
                          clearable
                          :autosize="{ minRows: 4, maxRows: 10 }"
                          type="textarea"
                          :placeholder="
                            '请输入' + platformSettingForm.otherSettings[6].label
                          "
                        />
                      </el-form-item>
                    </el-col>
                    <el-col :span="12">
                      <div class="form-item-hint">
                        <el-text type="info" size="default"
                          >只需要粘贴 &lt;script&gt;&lt;/script&gt;
                          内的代码，只统计H5端。微信小程序请使用"小程序数据助手"</el-text
                        >
                      </div>
                    </el-col>
                  </el-row>
                </div>
              </el-card>
            </el-tab-pane>
          </el-tabs>
        </el-form>
      </el-col>
      <el-col :span="24">
        <div class="card-submit-content">
          <el-button type="success" class="mr10" @click="platformSubmit">保存</el-button>
          <el-button type="default" @click="platformCancel">返回</el-button>
        </div>
      </el-col>
    </el-row>

    <select-article
      ref="selectArticleRef"
      selectionType="single"
      @selResult="onSelectArticleResult"
    />

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

<script setup lang="ts" name="platform">
import { defineAsyncComponent, reactive, ref, onMounted, computed } from "vue";
import { ElMessage } from "element-plus";
import type { CascaderProps } from "element-plus";
import { useRoute } from "vue-router";
import mittBus from "/@/utils/mitt";
import type { UploadProps } from "element-plus";
import { Picture, Edit, Upload, Location } from "@element-plus/icons-vue";
import { useGoodAreaApi } from "/@/api/shopSetting/area/index";
import { usePlatformSettingApi } from "/@/api/shopSetting/platformSetting/index";
import { cascaderAreaActiveIds } from "/@/utils/other";
import { uploadFileConfig, uploadCertFileConfig } from "/@/utils/other";
import {
  verifiyNumberInteger,
  verifyNumberIntegerAndFloat,
} from "/@/utils/toolsValidate";

// 引入组件
const SelectArticle = defineAsyncComponent(
  () => import("/@/views/operationManage/articles/components/selectArticleDialog.vue")
);
const tMapSelector = defineAsyncComponent(
  () => import("/@/components/tMapSelector/index.vue")
);

// 引入 Api 请求接口
const platformSettingApi = usePlatformSettingApi();

// 定义变量
const route = useRoute();
const selectArticleRef = ref();
const selectArticleIndex = ref(0);
const tabsHeight = ref(0);
const tMapSelectorRef = ref();
const activeName = ref("SpecialSwitch"); // 默认选中[特殊开关]
const cascaderAreaRef = ref();
const imageViewer = ref({
  show: false,
  index: 0,
  list: [] as string[],
});
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
      let areaId = null;
      if (platformSettingForm.orderSettings[7].value) {
        areaId = Number(platformSettingForm.orderSettings[7].value);
      }
      loadGoodAreaTree(parentId, depth, areaId, (result: AreaTreeType[]) => {
        resolve(result);
      });
    },
  } as CascaderProps,
});
const platformSettingForm = reactive<PlatformSettingType>({
  specialSwitchs: [
    {
      label: "显示门店列表",
      name: "BaseService.SpecialSwitch.ShowStore",
      value: "",
    },
    {
      label: "显示充值功能",
      name: "BaseService.SpecialSwitch.ShowCharge",
      value: "",
    },
  ],
  platformSettings: [
    {
      label: "平台名称",
      name: "BaseService.PlatformSetting.ShopName",
      value: "",
    },
    {
      label: "平台描述",
      name: "BaseService.PlatformSetting.ShopDesc",
      value: "",
    },
    {
      label: "备案信息",
      name: "BaseService.PlatformSetting.ShopBeiAn",
      value: "",
    },
    {
      label: "关于我们",
      name: "BaseService.PlatformSetting.AboutArticle",
      value: "",
    },
    {
      label: "用户协议",
      name: "BaseService.PlatformSetting.UserAgreement",
      value: "",
    },
    {
      label: "隐私政策",
      name: "BaseService.PlatformSetting.PrivacyPolicy",
      value: "",
    },
    {
      label: "平台Logo",
      name: "BaseService.PlatformSetting.PlatformLogo",
      value: "",
    },
    {
      label: "默认图片",
      name: "BaseService.PlatformSetting.DefaultPic",
      value: "",
    },
    {
      label: "开启门店自提",
      name: "BaseService.PlatformSetting.StoreSwitch",
      value: "",
    },
    {
      label: "发票功能",
      name: "BaseService.PlatformSetting.InvoiceSwitch",
      value: "",
    },
    {
      label: "搜索发现关键词",
      name: "BaseService.PlatformSetting.RecommendKeys",
      value: "",
    },
  ],
  shareSettings: [
    {
      label: "分享标题",
      name: "BaseService.ShareSetting.ShareTitle",
      value: "",
    },
    {
      label: "分享描述",
      name: "BaseService.ShareSetting.ShareDesc",
      value: "",
    },
    {
      label: "分享图片",
      name: "BaseService.ShareSetting.ShareImage",
      value: "",
    },
  ],
  usersSettings: [
    {
      label: "绑定手机号码",
      name: "BaseService.UsersSetting.IsBindMobile",
      value: "",
    },
    {
      label: "商家手机号码",
      name: "BaseService.UsersSetting.ShopMobile",
      value: "",
    },
  ],
  goodsSettings: [
    {
      label: "库存警报数量",
      name: "BaseService.GoodsSetting.GoodsStocksWarn",
      value: "",
    },
  ],
  orderSettings: [
    {
      label: "订单取消时间",
      name: "BaseService.OrderSetting.OrderCancelTime",
      value: "",
    },
    {
      label: "订单完成时间",
      name: "BaseService.OrderSetting.OrderCompleteTime",
      value: "",
    },
    {
      label: "订单确认收货时间",
      name: "BaseService.OrderSetting.OrderAutoSignTime",
      value: "",
    },
    {
      label: "订单自动评价时间",
      name: "BaseService.OrderSetting.OrderAutoEvalTime",
      value: "",
    },
    {
      label: "订单提醒付款时间",
      name: "BaseService.OrderSetting.RemindOrderTime",
      value: "",
    },
    {
      label: "退货联系人",
      name: "BaseService.OrderSetting.ReshipName",
      value: "",
    },
    {
      label: "退货联系方式",
      name: "BaseService.OrderSetting.ReshipMobile",
      value: "",
    },
    {
      label: "退货区域",
      name: "BaseService.OrderSetting.ReshipAreaId",
      value: "",
    },
    {
      label: "退货坐标",
      name: "BaseService.OrderSetting.ReshipCoordinate",
      value: "",
    },
    {
      label: "退货详细地址",
      name: "BaseService.OrderSetting.ReshipAddress",
      value: "",
    },
    {
      label: "门店自提自动发货",
      name: "BaseService.OrderSetting.StoreOrderAutomaticDelivery",
      value: "",
    },
  ],
  pointsSettings: [
    {
      label: "开启积分功能",
      name: "BaseService.PointsSetting.PointSwitch",
      value: "",
    },
    {
      label: "订单积分折现比例",
      name: "BaseService.PointsSetting.PointDiscountedProportion",
      value: "",
    },
    {
      label: "订单积分使用比例",
      name: "BaseService.PointsSetting.OrdersPointProportion",
      value: "",
    },
    {
      label: "订单积分奖励比例",
      name: "BaseService.PointsSetting.OrdersRewardProportion",
      value: "",
    },
    {
      label: "签到奖励类型",
      name: "BaseService.PointsSetting.SignPointType",
      value: "",
    },
    {
      label: "随机奖励积分最小值",
      name: "BaseService.PointsSetting.SignRandomMin",
      value: "",
    },
    {
      label: "随机奖励积分最大值",
      name: "BaseService.PointsSetting.SignRandomMax",
      value: "",
    },
    {
      label: "首次奖励积分",
      name: "BaseService.PointsSetting.FirstSignPoint",
      value: "",
    },
    {
      label: "连续签到追加",
      name: "BaseService.PointsSetting.ContinuitySignAdditional",
      value: "",
    },
    {
      label: "单日最大奖励",
      name: "BaseService.PointsSetting.SignMostPoint",
      value: "",
    },
  ],
  cashSettings: [
    {
      label: "最低提现金额",
      name: "BaseService.CashSetting.TocashMoneyLow",
      value: "",
    },
    {
      label: "提现服务费率",
      name: "BaseService.CashSetting.TocashMoneyRate",
      value: "",
    },
    {
      label: "每日提现上限",
      name: "BaseService.CashSetting.TocashMoneyLimit",
      value: "",
    },
  ],
  inviteFriendSettings: [
    {
      label: "佣金类型",
      name: "BaseService.InviteFriendSetting.CommissionType",
      value: "",
    },
    {
      label: "一级佣金",
      name: "BaseService.InviteFriendSetting.CommissionFirst",
      value: "",
    },
    {
      label: "二级佣金",
      name: "BaseService.InviteFriendSetting.CommissionSecond",
      value: "",
    },
    {
      label: "三级佣金",
      name: "BaseService.InviteFriendSetting.CommissionThird",
      value: "",
    },
  ],
  fileStorageSettings: [
    {
      label: "存储方式",
      name: "BaseService.FileStorageSetting.StorageType",
      value: "",
    },
    {
      label: "存储路径",
      name: "BaseService.FileStorageSetting.StoragePath",
      value: "",
    },
    {
      label: "文件后缀类型",
      name: "BaseService.FileStorageSetting.StorageSuffix",
      value: "",
    },
    {
      label: "文件最大大小",
      name: "BaseService.FileStorageSetting.StorageMaxSize",
      value: "",
    },
    {
      label: "存储服务[Json对象]",
      name: "BaseService.FileStorageSetting.StorageServerJson",
      value: "",
    },
  ],
  weChatMiniPrograms: [
    {
      label: "AppId",
      name: "EasyAbp.Abp.WeChat.MiniProgram.AppId",
      value: "",
    },
    {
      label: "AppSecret",
      name: "EasyAbp.Abp.WeChat.MiniProgram.AppSecret",
      value: "",
    },

    {
      label: "Token",
      name: "EasyAbp.Abp.WeChat.MiniProgram.Token",
      value: "",
    },
    {
      label: "EncodingAesKey",
      name: "EasyAbp.Abp.WeChat.MiniProgram.EncodingAesKey",
      value: "",
    },
  ],
  weChatPays: [
    {
      label: "MchId",
      name: "EasyAbp.Abp.WeChat.Pay.MchId",
      value: "",
    },
    {
      label: "ApiKey",
      name: "EasyAbp.Abp.WeChat.Pay.ApiKey",
      value: "",
    },
    {
      label: "IsSandBox",
      name: "EasyAbp.Abp.WeChat.Pay.IsSandBox",
      value: "False",
    },
    {
      label: "NotifyUrl",
      name: "EasyAbp.Abp.WeChat.Pay.NotifyUrl",
      value: "",
    },
    {
      label: "RefundNotifyUrl",
      name: "EasyAbp.Abp.WeChat.Pay.RefundNotifyUrl",
      value: "",
    },
    {
      label: "CertificateBlobContainerName",
      name: "EasyAbp.Abp.WeChat.Pay.CertificateBlobContainerName",
      value: "",
    },
    {
      label: "CertificateBlobName",
      name: "EasyAbp.Abp.WeChat.Pay.CertificateBlobName",
      value: "",
    },
    {
      label: "CertificateSecret",
      name: "EasyAbp.Abp.WeChat.Pay.CertificateSecret",
      value: "",
    },
  ],
  otherSettings: [
    {
      label: "客服ID",
      name: "BaseService.OtherSetting.EntId",
      value: "",
    },
    {
      label: "腾讯地图Key",
      name: "BaseService.OtherSetting.QqMapKey",
      value: "",
    },
    {
      label: "AppId",
      name: "BaseService.OtherSetting.ShowApiAppId",
      value: "",
    },
    {
      label: "授权Secret",
      name: "BaseService.OtherSetting.ShowApiSecret",
      value: "",
    },
    {
      label: "公司编号",
      name: "BaseService.OtherSetting.Kuaidi100Customer",
      value: "",
    },
    {
      label: "授权Key",
      name: "BaseService.OtherSetting.Kuaidi100Key",
      value: "",
    },
    {
      label: "百度统计代码",
      name: "BaseService.OtherSetting.StatisticsCode",
      value: "",
    },
  ],
});

// 图片上传成功回调
const handlePlatformSetting6ImageSuccess: UploadProps["onSuccess"] = (
  result: string,
  uploadFile
) => {
  if (!result) return;
  platformSettingForm.platformSettings[6].value = result;
};
const handleSetting7ImageSuccess: UploadProps["onSuccess"] = (
  result: string,
  uploadFile
) => {
  if (!result) return;
  platformSettingForm.platformSettings[7].value = result;
};
const handleShareSetting2ImageSuccess: UploadProps["onSuccess"] = (
  result: string,
  uploadFile
) => {
  if (!result) return;
  platformSettingForm.shareSettings[2].value = result;
};

// 证书上传成功回调
const handleWeChatPaysCertSuccess: UploadProps["onSuccess"] = (
  result: any,
  uploadFile
) => {
  if (!result) return;
  platformSettingForm.weChatPays[5].value = result.certificateBlobContainerName;
  platformSettingForm.weChatPays[6].value = result.certificateBlobName;
};

// 打开选择文章
const openSelectArticle = (index: number) => {
  selectArticleIndex.value = index;
  selectArticleRef.value.openDialog("选择文章");
};

// 选择文章返回结果
const onSelectArticleResult = (article: RowArticleType) => {
  let articleItem = {
    title: article.title,
    id: article.id,
  };
  platformSettingForm.platformSettings[selectArticleIndex.value].value = JSON.stringify(
    articleItem
  );
};

// 打开地图选择
const onOpenMapSelector = () => {
  let latLng = platformSettingForm.orderSettings[8].value;
  let lat = null;
  let lng = null;
  if (latLng) {
    let latLngArr = latLng.split(",");
    lat = Number(latLngArr[0]);
    lng = Number(latLngArr[1]);
  }
  tMapSelectorRef.value.openDialog("选择地图坐标", lat, lng);
};

// 地图选择确认
const tMapConfirm = (latLng: any) => {
  platformSettingForm.orderSettings[8].value = `${latLng.lat},${latLng.lng}`;
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

// 商品区域切换
function goodAreaChange(val: []): void {
  if (!val || val.length < 1) {
    platformSettingForm.orderSettings[7].value = "";
    return;
  }
  platformSettingForm.orderSettings[7].value = String(val[val.length - 1]);
  areaCascaderProp.value.current = val;
}

// 图片预览
const onImageViewer = (url: string) => {
  imageViewer.value.list = [url];
  imageViewer.value.show = true;
};

// 获取平台设置
const getPlatformSetting = () => {
  platformSettingApi
    .getPlatformSetting()
    .then(async (result: any) => {
      if (result) {
        areaCascaderProp.value.current = [];
        Object.assign(platformSettingForm, result);
        // setOssVal();
        let areaId = platformSettingForm.orderSettings[7].value;
        if (areaId) {
          areaCascaderProp.value.current = await cascaderAreaActiveIds(Number(areaId));
        }
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 保存[提交]
const platformSubmit = () => {
  // platformSettingForm.fileStorageSettings[4].value = storageServerJson.value;
  platformSettingApi
    .setPlatformSetting(platformSettingForm)
    .then((result) => {
      if (result) {
        ElMessage.success("保存成功！");
      }
    })
    .catch((err: any) => {
      console.error("提交出错：", err);
    });
};

// 返回
const platformCancel = () => {
  mittBus.emit(
    "onCurrentContextmenuClick",
    Object.assign({}, { contextMenuClickId: 1, ...route })
  );
};

onMounted(() => {
  tabsHeight.value = window.innerHeight - 180;
  // 获取平台设置信息
  getPlatformSetting();
});
</script>

<style lang="scss">
.platform-container .setting-card {
  margin-bottom: 15px;
  .el-card__header {
    background-color: #f5f7fa;
  }
  .el-card__header,
  .el-card__body {
    padding: 10px !important;
  }
}
</style>

<style scoped lang="scss">
.platform-container {
  .tabs-box {
    // height: 550px;
    overflow-y: auto;
    scroll-behavior: smooth;
    transition: scroll-behavior 0.3s;

    .special-switch-prompt {
      font-size: 14px;
      padding: 2px 0px;
    }

    .form-item-hint {
      height: 32px;
      line-height: 32px;
      margin-left: 10px;
      border-bottom: 1px #eee dashed;
    }

    .form-item-desc {
      height: 32px;
      line-height: 32px;
      background-color: #f5f7fa;
      padding: 0px 10px;
      font-size: 14px;
      border-left: 5px green solid;
    }

    .upload-image-box,
    .upload-cert-box {
      width: 100%;
      .img-logo {
        width: 32px;
        height: 32px;
      }
    }
  }
}

.card-submit-content {
  height: 65px;
  line-height: 65px;
  background-color: white;
  text-align: center;
}
</style>
