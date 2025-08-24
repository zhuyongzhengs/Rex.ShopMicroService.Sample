<template>
  <div class="order-detail-container">
    <el-dialog
      :title="state.dialog.title"
      :close-on-click-modal="false"
      v-model="state.dialog.isShowDialog"
      draggable
      width="1200px"
    >
      <div class="order-detail-box">
        <el-tabs v-loading="state.loading">
          <div class="order-detail-tabs" v-if="state.orderDetail">
            <el-tab-pane label="基本信息">
              <el-card shadow="never">
                <template #header>
                  <div class="order-detail-title">订单信息</div>
                </template>
                <div class="order-detail-item">
                  <table cellpadding="0" cellspacing="0" class="order-detail-table">
                    <tr>
                      <th>订单号</th>
                      <td>{{ state.orderDetail.no }}</td>
                      <th>下单来源</th>
                      <td>{{ state.orderDetail.sourceDisplay }}</td>
                      <th>下单时间</th>
                      <td>{{ state.orderDetail.creationTime }}</td>
                      <th>支付方式</th>
                      <td>{{ state.paymentName }}</td>
                    </tr>
                    <tr>
                      <th>订单状态</th>
                      <td>{{ state.orderDetail.statusDisplay }}</td>
                      <th>发货状态</th>
                      <td>{{ state.orderDetail.shipStatusDisplay }}</td>
                      <th>配送方式</th>
                      <td>{{ state.orderDetail.logisticsName }}</td>
                      <th>商品总重量(克)</th>
                      <td>{{ state.orderDetail.weight }}</td>
                    </tr>
                  </table>
                </div>
              </el-card>
              <el-card shadow="never" class="mt15">
                <template #header>
                  <div class="order-detail-title">付款信息</div>
                </template>
                <div class="order-detail-item">
                  <table cellpadding="0" cellspacing="0" class="order-detail-table">
                    <tr>
                      <th>商品总价</th>
                      <td>￥{{ state.orderDetail.goodAmount }}</td>
                      <th>配送费用</th>
                      <td>￥{{ state.orderDetail.costFreight }}</td>
                      <th>应付金额</th>
                      <td>
                        {{
                          payMoney(
                            state.orderDetail.goodAmount,
                            state.orderDetail.costFreight
                          )
                        }}
                      </td>
                      <th>已支付金额</th>
                      <td>￥{{ state.orderDetail.payedAmount }}</td>
                    </tr>
                    <tr>
                      <th>订单优惠金额</th>
                      <td>￥{{ state.orderDetail.orderDiscountAmount }}</td>
                      <th>商品优惠金额</th>
                      <td>￥{{ state.orderDetail.goodsDiscountAmount }}</td>
                      <th>优惠劵抵扣</th>
                      <td>￥{{ state.orderDetail.couponDiscountAmount }}</td>
                      <th>积分抵扣</th>
                      <td>￥{{ state.orderDetail.pointMoney }}</td>
                    </tr>
                    <tr>
                      <th>会员优惠金额</th>
                      <td>￥{{ state.orderDetail.gradeDiscountAmount }}</td>
                      <template v-if="state.orderDetail.orderType === 4">
                        <th>秒杀优惠金额</th>
                        <td>￥{{ state.orderDetail.seckillDiscountAmount }}</td>
                      </template>
                      <th><b>优惠后金额</b></th>
                      <td>
                        <b>￥{{ state.orderDetail.orderAmount }}</b>
                      </td>
                      <th>支付状态</th>
                      <td>{{ state.orderDetail.payStatusDisplay }}</td>
                    </tr>
                  </table>
                </div>
              </el-card>
              <el-card shadow="never" class="mt15">
                <template #header>
                  <div class="order-detail-title">收货人信息</div>
                </template>
                <div class="order-detail-item">
                  <table cellpadding="0" cellspacing="0" class="order-detail-table">
                    <tr>
                      <th>收货人姓名</th>
                      <td>{{ state.orderDetail.shipName }}</td>
                      <th>收货人电话</th>
                      <td>{{ state.orderDetail.shipMobile }}</td>
                      <th>收货状态</th>
                      <td>{{ state.orderDetail.confirmStatusDisplay }}</td>
                      <th>收货时间</th>
                      <td>{{ state.orderDetail.confirmTime }}</td>
                    </tr>
                    <tr>
                      <th>收货地址</th>
                      <td colspan="7">
                        {{
                          state.orderDetail.displayArea +
                          " " +
                          state.orderDetail.shipAddress
                        }}
                      </td>
                    </tr>
                  </table>
                </div>
              </el-card>
              <el-card shadow="never" class="mt15">
                <template #header>
                  <div class="order-detail-title">买家备注</div>
                </template>
                <div class="order-detail-item">
                  <table
                    cellpadding="0"
                    cellspacing="0"
                    class="order-detail-table"
                    v-show="state.orderDetail.memo"
                  >
                    <tr>
                      <td>
                        <div class="order-detail-memo">
                          {{ state.orderDetail.memo }}
                        </div>
                      </td>
                    </tr>
                  </table>
                </div>
              </el-card>
              <el-card shadow="never" class="mt15">
                <template #header>
                  <div class="order-detail-title">下单用户</div>
                </template>
                <div class="order-detail-item">
                  <table cellpadding="0" cellspacing="0" class="order-detail-table">
                    <tr>
                      <th>用户账号</th>
                      <td>{{ state.orderDetail.userName }}</td>
                      <th>用户昵称</th>
                      <td>{{ state.orderDetail.nickName }}</td>
                      <th>手机号码</th>
                      <td>{{ state.orderDetail.phoneNumber }}</td>
                      <th>用户等级</th>
                      <td>{{ state.orderDetail.userGradeName }}</td>
                    </tr>
                  </table>
                </div>
              </el-card>
            </el-tab-pane>
            <el-tab-pane label="商品信息">
              <el-table
                ref="orderItemTableRef"
                :data="state.orderDetail.orderItems"
                v-loading="state.loading"
                border
                width="100%"
                height="600px"
              >
                <el-table-column fixed type="index" width="65" label="序号" />
                <el-table-column prop="imageUrl" label="商品图片" width="100">
                  <template #default="scope">
                    <el-image
                      class="img-logo"
                      :src="scope.row.imageUrl"
                      :zoom-rate="1.2"
                      :max-scale="7"
                      :min-scale="0.2"
                      :preview-src-list="[scope.row.imageUrl]"
                      :preview-teleported="true"
                    >
                      <template #error>
                        <div class="image-slot">
                          <el-icon><icon-picture /></el-icon>
                        </div>
                      </template>
                    </el-image>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="name"
                  label="商品名称"
                  show-overflow-tooltip
                  min-width="450"
                />
                <el-table-column prop="price" label="商品单价" width="90">
                  <template #default="scope">
                    <span class="good-price">￥{{ scope.row.price }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="nums"
                  label="购买数量"
                  width="90"
                ></el-table-column>
                <el-table-column prop="amount" label="商品总价" width="90"
                  ><template #default="scope">
                    <span>￥{{ scope.row.amount }}</span>
                  </template></el-table-column
                >
                <el-table-column prop="sn" label="货品编码" width="170"></el-table-column>
                <el-table-column prop="bn" label="商品编码" width="170"></el-table-column>
                <el-table-column
                  prop="weight"
                  label="商品总重量(克)"
                  width="130"
                ></el-table-column>
                <el-table-column
                  prop="sendNums"
                  label="发货数量"
                  width="90"
                ></el-table-column>
                <el-table-column prop="reshipNums" label="退货数量" width="90">
                  <template #default="scope">
                    <span>{{ scope.row.reshipNums || "0" }}</span>
                  </template>
                </el-table-column>
              </el-table>
            </el-tab-pane>
            <el-tab-pane label="支付单/退款单">
              <el-card shadow="never">
                <template #header>
                  <div class="order-detail-title">支付单</div>
                </template>
                <div class="order-detail-item">
                  <el-table
                    :data="state.orderDetail.billPayments"
                    v-loading="state.loading"
                    border
                    width="100%"
                  >
                    <el-table-column fixed type="index" width="62" label="序号" />
                    <el-table-column
                      prop="no"
                      label="支付单号"
                      width="150"
                    ></el-table-column>
                    <el-table-column
                      prop="paymentCode"
                      label="支付方式"
                      width="120"
                    ></el-table-column>
                    <el-table-column
                      prop="tradeNo"
                      label="第三方支付单号"
                      width="254"
                    ></el-table-column>
                    <el-table-column prop="money" label="支付金额" width="90">
                      <template #default="scope">
                        <span>￥{{ scope.row.money }}</span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      prop="statusDisplay"
                      label="支付状态"
                      width="90"
                      show-overflow-tooltip
                    ></el-table-column>
                    <el-table-column
                      prop="payedMsg"
                      label="说明"
                      show-overflow-tooltip
                    ></el-table-column>
                    <el-table-column
                      prop="creationTime"
                      label="生成时间"
                      width="180"
                    ></el-table-column>
                  </el-table>
                </div>
              </el-card>
              <el-card shadow="never" class="mt15">
                <template #header>
                  <div class="order-detail-title">退款单</div>
                </template>
                <div class="order-detail-item">
                  <el-table
                    :data="state.orderDetail.billRefunds"
                    v-loading="state.loading"
                    border
                    width="100%"
                  >
                    <el-table-column fixed type="index" width="62" label="序号" />
                    <el-table-column
                      prop="no"
                      label="退款单号"
                      width="150"
                    ></el-table-column>
                    <el-table-column
                      prop="paymentCode"
                      label="退款方式"
                      width="120"
                    ></el-table-column>
                    <el-table-column
                      prop="tradeNo"
                      label="第三方支付单号"
                    ></el-table-column>
                    <el-table-column
                      prop="statusDisplay"
                      label="退款状态"
                      width="90"
                      show-overflow-tooltip
                    ></el-table-column>
                    <el-table-column
                      prop="creationTime"
                      label="申请时间"
                      width="170"
                    ></el-table-column>
                  </el-table>
                </div>
              </el-card>
            </el-tab-pane>
            <el-tab-pane label="发货单/退货单">
              <el-card shadow="never">
                <template #header>
                  <div class="order-detail-title">发货单</div>
                </template>
                <div class="order-detail-item">
                  <el-table
                    :data="state.orderDetail.billDeliverys"
                    v-loading="state.loading"
                    border
                    width="100%"
                  >
                    <el-table-column fixed type="index" width="62" label="序号" />
                    <el-table-column
                      prop="no"
                      label="发货单号"
                      width="150"
                    ></el-table-column>
                    <el-table-column
                      prop="logiCode"
                      label="快递公司编码"
                      width="120"
                    ></el-table-column>
                    <el-table-column
                      prop="logiNo"
                      label="快递单号"
                      width="180"
                    ></el-table-column>
                    <el-table-column prop="shipName" label="收货人姓名" width="120">
                    </el-table-column>
                    <el-table-column
                      prop="shipMobile"
                      label="收货人电话"
                      width="140"
                    ></el-table-column>
                    <el-table-column
                      prop="shipAddress"
                      label="收货人地址"
                      show-overflow-tooltip
                    >
                      <template #default="scope">
                        {{ `${scope.row.displayArea} ${scope.row.shipAddress}` }}
                      </template>
                    </el-table-column>
                  </el-table>
                </div>
              </el-card>
              <el-card shadow="never" class="mt15">
                <template #header>
                  <div class="order-detail-title">退货单</div>
                </template>
                <div class="order-detail-item">
                  <el-table
                    :data="state.orderDetail.billReships"
                    v-loading="state.loading"
                    border
                    width="100%"
                  >
                    <el-table-column fixed type="index" width="62" label="序号" />
                    <el-table-column
                      prop="no"
                      label="退货单号"
                      width="150"
                    ></el-table-column>
                    <el-table-column
                      prop="logiCode"
                      label="快递公司编码"
                      width="120"
                    ></el-table-column>
                    <el-table-column prop="logiNo" label="快递单号"></el-table-column>
                    <el-table-column prop="statusDisplay" label="退货状态" width="120">
                    </el-table-column>
                    <el-table-column
                      prop="creationTime"
                      label="退货时间"
                      width="170"
                    ></el-table-column>
                  </el-table>
                </div>
              </el-card>
            </el-tab-pane>
            <el-tab-pane label="优惠劵记录">
              <el-table
                :data="state.orderDetail.coupons"
                v-loading="state.loading"
                border
                width="100%"
              >
                <el-table-column fixed type="index" width="62" label="序号" />
                <el-table-column
                  prop="couponCode"
                  label="优惠劵编码"
                  width="150"
                ></el-table-column>
                <el-table-column
                  prop="creationTime"
                  label="创建时间"
                  width="170"
                ></el-table-column>
                <el-table-column
                  prop="lastModificationTime"
                  label="使用时间"
                  width="170"
                ></el-table-column>
                <el-table-column prop="remark" label="备注"> </el-table-column>
              </el-table>
            </el-tab-pane>
            <el-tab-pane label="订单记录">
              <el-table
                :data="state.orderDetail.orderLogs"
                v-loading="state.loading"
                border
                width="100%"
              >
                <el-table-column fixed type="index" width="62" label="序号" />
                <el-table-column prop="orderNo" label="订单号" width="150">
                  <template #default>
                    <span>{{ state.orderDetail.no }}</span>
                  </template>
                </el-table-column>
                <el-table-column
                  prop="typeDisplay"
                  label="操作类型"
                  width="100"
                ></el-table-column>
                <el-table-column prop="msg" label="描述"> </el-table-column>
                <el-table-column
                  prop="creationTime"
                  label="使用时间"
                  width="170"
                ></el-table-column>
              </el-table>
            </el-tab-pane>
            <el-tab-pane label="订单备注">
              <el-input
                v-model="state.orderDetail.mark"
                :rows="4"
                maxlength="200"
                placeholder="请输入备注内容…"
                show-word-limit
                type="textarea"
              />
              <div class="order-detail-mark">
                <el-button type="primary" @click="onSaveMark">保存备注</el-button>
              </div>
            </el-tab-pane>
          </div>
        </el-tabs>
      </div>
      <!--
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="onCancel" size="default">取 消</el-button>
        </span>
      </template>
      -->
    </el-dialog>
  </div>
</template>

<script setup lang="ts" name="orderDetail">
import { nextTick, reactive, ref, defineExpose } from "vue";
import { ElMessage } from "element-plus";
import _ from "lodash";
import { useOrderApi } from "/@/api/orderManage/order/index";

// 引入 Api 请求接口
const orderApi = useOrderApi();

// 定义变量
const state = reactive({
  loading: true,
  paymentName: "",
  orderDetail: {} as OrderDetailType,
  dialog: {
    isShowDialog: false,
    title: "订单详情",
    submitTxt: "取消",
  },
});

// 打开弹窗
const openDialog = (row: RowOrderType, paymentName: string) => {
  state.loading = true;
  state.paymentName = paymentName;
  getOrderDetail(row.id);
  nextTick(() => {
    // 查询订单信息
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

// 查询订单明细
const getOrderDetail = (orderId: string) => {
  orderApi
    .getOrderDetail(orderId)
    .then((result) => {
      Object.assign(state.orderDetail, result);
      state.loading = false;
    })
    .catch((err: any) => {
      console.error("查询查询订单明细出错：", err);
      state.loading = false;
    });
};

// 保存备注
const onSaveMark = () => {
  if (!state.orderDetail.mark) {
    ElMessage.warning("请输入备注内容！");
    return;
  }
  orderApi
    .updateOrderMark(state.orderDetail.id, state.orderDetail.mark)
    .then((result) => {
      if (result) {
        ElMessage.success("保存成功！");
      }
    })
    .catch((err: any) => {
      console.error("保存备注出错：", err);
    });
};

// 应付金额
const payMoney = (goodAmount: number | undefined, costFreight: number | undefined) => {
  if (!goodAmount || !costFreight) return null;
  return "￥" + Number(goodAmount + costFreight);
};

// 暴露变量
defineExpose({
  openDialog,
  closeDialog,
});
</script>

<style lang="scss">
.order-detail-container {
  .order-detail-tabs .el-card__header {
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
.order-detail-container {
  .order-detail-box {
    position: relative;
    top: -10px;
    .order-detail-tabs {
      height: 600px;
      overflow-y: auto;
    }
    .order-detail-item {
      .order-detail-table {
        border-collapse: collapse;
        width: 100%;
        tr {
          th {
            width: 110px;
            background-color: #fbfbfb;
            text-align: center;
            padding: 10px 5px;
            font-weight: 500;
            border: 1px solid #eeeeee;
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
    }

    .order-detail-mark {
      text-align: center;
      margin-top: 15px;
    }

    .img-logo {
      width: 50px;
      height: 50px;
    }

    .img-logo .image-slot {
      display: flex;
      justify-content: center;
      align-items: center;
      width: 100%;
      height: 100%;
      background: var(--el-fill-color-light);
      color: var(--el-text-color-secondary);
      font-size: 30px;
    }
    .img-logo .image-slot .el-icon {
      font-size: 30px;
    }
    .good-price {
      color: #f56c6c;
    }
  }
}
</style>
