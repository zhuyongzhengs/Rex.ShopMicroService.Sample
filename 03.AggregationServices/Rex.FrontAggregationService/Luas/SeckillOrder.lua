--[[
	一、函数定义
]]--
-- 0.自定义 Split 函数
local function split(input, delimiter)
    local result = {};
    -- 将输入字符串中的每个分隔符替换为唯一的标记（如\t），然后分割
    for part in string.gmatch(input, "[^" .. delimiter .. "]+") do
        table.insert(result, part);
    end
    return result
end

-- 1.单品限流
local function seckillLimit()
	local seckillLimitKey = KEYS[1];
	local orderRateLimitCount = tonumber(ARGV[1]); -- 单品限流请求数
	local orderRateLimitKeyExpire = tonumber(ARGV[2]); -- 单品限流请求过期时间

	-- 获取单品已经请求数量
	local limitCount = tonumber(redis.call('get',seckillLimitKey) or "0");
	if limitCount + 1 > orderRateLimitCount then 
		-- 失败：超出限流大小
		return 0, orderRateLimitKeyExpire .. "内只能请求" .. orderRateLimitCount .. "次";
	else 
		-- 成功：请求数+1，并设置过期时间
		redis.call('INCRBY',orderRateLimitKey,1);
		redis.call('EXPIRE',orderRateLimitKey, orderRateLimitKeyExpire);
		return 1;
	end
end

-- 2.防止订单重复：创建订单方法幂等性,调用方网络超时可以重复调用,存在订单号直接返回抢购成功,不至于超卖
local function recordOrderNo()
	local orderGoodKey = KEYS[2]; -- 商品订单键
	local hasOrderNo = tostring(redis.call('GET',orderGoodKey) or "");
	if string.len(hasOrderNo) == 0 then
		-- 订单不存在：保存订单号
		redis.call('SET',orderGoodKey,"CREATED");
		redis.call('EXPIRE',orderGoodKey, 86400); -- 有效期：24小时
		return 1;
	else
	    -- 订单已存在：提示错误信息(防止重复)
		return 0,"已下单，请勿重复下单";
	end
end

-- 3.用户购买限制
local function userBuyLimit()
	local userBuyNumsKey = KEYS[3]; -- 用户购买数量Key
	local totalBuyNumsKey = KEYS[4]; -- 购买商品总量Key
	local userMaxNums = tonumber(ARGV[3]); -- 用户购买(商品)数量限制 ---> 0：表示不限制
	local maxGoodNums = tonumber(ARGV[4]); -- 商品总量 ---> 0：表示不限制
	local productNums = tonumber(ARGV[5]); -- 购买(商品)数量

	if userMaxNums > 0 then
		local userBuyNums = tonumber(redis.call('GET',userBuyNumsKey) or "0");
		if userBuyNums > userMaxNums then 
			-- 失败：已超出购买(数量)限制
			return 0, "该商品您已购买了" .. userBuyNums .. "件，每个用户只能购买" .. userMaxNums .. "件";
		else 
			-- 成功：未超出购买(数量)限制，购买数量
			redis.call('INCRBY',userBuyNumsKey,productNums);
		end
	end

	if maxGoodNums > 0 then
		local totalBuyNums = tonumber(redis.call('GET',totalBuyNumsKey) or "0");
		if totalBuyNums + productNums > maxGoodNums then 
			-- 失败：已超出购买(总量)限制
			return 0, "秒杀活动商品数量只剩余" .. (maxGoodNums - totalBuyNums) .. "件，请重新选择";
		else 
			-- 成功：未超出购买(总量)限制，购买数量
			redis.call('INCRBY',totalBuyNumsKey,productNums);
		end
	end

	return 1;
end

-- 4.扣减库存
local function subtractProductStock()
	local productStockKey = KEYS[5]; -- 商品库存Key：Rex.GoodService:{xxx}:Product:{xxx}
	local productNums = tonumber(ARGV[5]); -- 购买(商品)数量

	local pStockValue = tonumber(redis.call('HGET',productStockKey, 'Stock') or "0");
	if pStockValue < 1 then
		return 0, "商品库存不存在，请稍后重试";
	end
	local pFreezeStockValue = tonumber(redis.call('HGET',productStockKey, 'FreezeStock') or "0");
	if pFreezeStockValue >= pStockValue then
		-- 失败：库存不足
		return 0, "该商品库存已售罄，感谢您的参与";
	end

	local stockNums = pStockValue - pFreezeStockValue; -- 剩余库存数量
	if productNums > stockNums then 
		-- 失败：库存数量不够
		return 0, "该商品库存不足，只剩余" .. stockNums .."件";
	else 
		-- 成功：扣减库存
		local newFreezeStock = pFreezeStockValue + productNums;
		redis.call('HSET', productStockKey, 'FreezeStock', newFreezeStock);
		
		-- 将Key存到集合，用于后台同步到数据库
		local productItem = split(productStockKey, ":");
		local prefix = productItem[1] .. ":" .. productItem[2]
		local goodStockDirtyKey = prefix .. ":StockDirtyKey"; -- Rex.GoodService:{0}:StockDirtyKey
		redis.call('LPUSH', goodStockDirtyKey, productStockKey);
	end

	return 1;
end

--[[
	二、函数调用
]]--
-- 1.单品限流
local status,msg = seckillLimit();
if status == 0 then
	return msg
end

-- 2.下单幂等存储
local status,msg = recordOrderNo();
if status == 0 then
	return msg
end

-- 3.用户购买限制
local status,msg = userBuyLimit();
if status == 0 then
	return msg
end

-- 4.扣减秒杀库存
local status,msg = subtractProductStock();
if status == 0 then
	return msg
end

-- 返回成功标识
return 1;