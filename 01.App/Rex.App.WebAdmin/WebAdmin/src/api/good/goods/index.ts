import request from '/@/utils/request';

export function useGoodsApi() {
    return {
        /**
         * 查询(分页)列表信息
         * @param { object } params
         * @returns { any }
         */
        getGoodsList: (params?: object): Promise<DataTableType<RowGoodsType>> => {
			return request({
				url: '/api/good/goods',
				method: 'get',
                params
			});
		},

		/**
         * 获取商品数量
         * @returns { any }
         */
        getGoodCount: (goodsStocksWarn: number): Promise<any> => {
			return request({
				url: '/api/good/common/good-count?goodsStocksWarn=' + goodsStocksWarn,
				method: 'get'
			});
		},

		/**
         * 获取商品价格类型
         * @returns { any }
         */
        getGoodPriceType: (): Promise<EnumValueType[]> => {
			return request({
				url: '/api/backend/aggregation/good/priceType',
				method: 'get'
			});
		},

		/**
         * 根据ID查询商品信息
         * @param { string } id
         * @returns { any }
         */
        getGoodById: (id: string): Promise<any> => {
			return request({
				url: '/api/good/goods/' + id + '/good-detail',
				method: 'get'
			});
		},

		/**
         * 根据ID获取商品信息
         * @param { string[] } ids
         * @returns { any }
         */
        getGoodByIds: (ids: string[]): Promise<RowGoodsType[]> => {
			return request({
				url: '/api/good/goods/good-by-ids',
				method: 'get',
				params: ids
			});
		},

		/**
         * 获取商品库存详情
         * @param { string } goodId
         * @returns { any }
         */
        getProductStockDetails: (goodId: string): Promise<RowProductStockDetailType[]> => {
			return request({
				url: `/api/backend/aggregation/good/stockDetail/${goodId}`,
				method: 'get'
			});
		},

        /**
		 * 添加
		 * @param { object } data 
		 * @returns { any }
		 */
		addGoods: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods',
				method: 'post',
				data
			});
		},

		/**
		 * 修改
		 * @param { object } data 
		 * @returns { any }
		 */
		updateGoods: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods',
				method: 'put',
				data
			});
		},

		/**
		 * 修改商品是否上架
		 * @param { string } id
		 * @param { boolean } isMarketable
		 * @returns { any }
		 */
		updateGoodIsMarketable: (id: string, isMarketable: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/goods/' + id + '/is-marketable',
				method: 'put',
				params: {
					isMarketable
				}
			});
		},

		/**
		 * 修改商品是否热门
		 * @param { string } id
		 * @param { boolean } isHot
		 * @returns { any }
		 */
		updateGoodIsHot: (id: string, isHot: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/goods/' + id + '/is-hot',
				method: 'put',
				params: {
					isHot
				}
			});
		},

		/**
		 * 修改商品是否推荐
		 * @param { string } id
		 * @param { boolean } isRecommend
		 * @returns { any }
		 */
		updateGoodIsRecommend: (id: string, isRecommend: boolean): Promise<boolean> => {
			return request({
				url: '/api/good/goods/' + id + '/is-recommend',
				method: 'put',
				params: {
					isRecommend
				}
			});
		},

		/**
		 * 修改商品价格
		 * @param { object } data 
		 * @returns { any }
		 */
		updatePrice: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods/price',
				method: 'put',
				data
			});
		},

		/**
		 * 修改商品库存
		 * @param { object } data 
		 * @returns { any }
		 */
		updateStock: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods/stock',
				method: 'put',
				data
			});
		},

		/**
		 * 更新货品库存信息
		 * @param { object } data 
		 * @returns { any }
		 */
		updateProductStocksAsync: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods/product-stocks',
				method: 'put',
				data
			});
		},

		/**
		 * 批量修改商品是否上|下架
		 * @param { object } data
		 * @returns { any }
		 */
		updateMarketableMany: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods/is-marketable-many',
				method: 'put',
				data
			});
		},

		/**
		 * 批量打标签
		 * @param { object } data
		 * @returns { any }
		 */
		updateLabelMany: (data: object): Promise<boolean> => {
			return request({
				url: '/api/good/goods/good-label-many',
				method: 'put',
				data
			});
		},
		
		/**
		 * 删除
		 * @param { string } id 
		 * @returns { any }
		 */
		deleteGoods: (id: string): Promise<boolean> => {
			return request({
				url: '/api/good/goods/' + id,
				method: 'delete'
			});
		},

		/**
		 * 批量删除
		 * @param { string[] } data 
		 * @returns { any }
		 */
		deleteGoodsMany: (data: string[]): Promise<boolean> => {
			return request({
				url: '/api/good/goods',
				method: 'delete',
				data
			});
		}
    };
}