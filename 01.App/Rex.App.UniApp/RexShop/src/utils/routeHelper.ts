/**
 * 统一批量导出
 * @method goGoodsDetail 查看商品详情
 * @method goArticleDetail 查看文章详情
 * @method goArticleList 查看文章列表
 * @method goSeckillList 查看秒杀列表
 * @method goSeckillDetail 查看秒杀详情
 * @method phoneCall 拨打电话
 * @method goShopMap 查看商家地图
 */
const routeFunc = {
        goGoodsDetail: (goodsId: string) => {
                uni.navigateTo({
                        url: `/pages/goods/detail?id=${goodsId}`,
                });
        },
        goArticleDetail: (articleId: string) => {
                uni.navigateTo({
                        url: `/pages/article/detail?id=${articleId}`,
                });
        },
        goArticleList: (typeId: string) => {
                uni.navigateTo({
                        url: `/pages/article/index?typeId=${typeId}`,
                });
        },
        goSeckillList: () => {
                uni.navigateTo({
                url: "/pages/promotion/seckill/index",
                });
        },
        goPinTuanList: () => {
                console.log("跳转[查看拼团列表]页面");
        },
        goSeckillDetail: (id: string, promotionId: string) => {
                uni.navigateTo({
                        url: `/pages/promotion/seckill/detail?id=${id}&objectId=${promotionId}`,
                });
        },
        phoneCall: (phoneNumber: string | null) => {
                if(!phoneNumber) {
                        console.warn("电话号码为空，调用失败！");
                        return;
                }
                uni.makePhoneCall({
                        phoneNumber
                });
        },
        goShopMap: (reshipCoordinate: string | null) => {
                if(!reshipCoordinate) {
                        console.warn("商家位置为空，调用失败！");
                        return;
                }
                if (reshipCoordinate.indexOf(",") != -1) {
                    var arr = reshipCoordinate.split(',');
                    uni.navigateTo({
                        url: `/pages/map/index?id=1&latitude=${ arr[0] }&longitude=${ arr[1] }`
                    });
                }
        }
};

// 统一批量导出
export default routeFunc;