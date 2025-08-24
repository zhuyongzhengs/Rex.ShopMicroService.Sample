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

-- 1.防止订单重复：创建订单方法幂等性,调用方网络超时可以重复调用,存在订单号直接返回抢购成功,不至于超卖
local function recordOrderNo()
	local orderGoodKey = KEYS[1]; -- 商品订单键
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

-- 2.验证商品库存是否充足
local function checkProductStock()
	local values = ARGV;
	local prefix = KEYS[2]; -- 商品库存前缀
	local productStockMany = {};

	for i, value in ipairs(values) do
		local pItem = split(value, ":");
		local productId = pItem[1];
		local buyNum = tonumber(pItem[2]);
		local productName = pItem[3];

		local productStockKey = prefix .. ":Product:{" .. productId .. "}"; -- Rex.GoodService:{0}:Product:{1}
		local stock = tonumber(redis.call('HGET',productStockKey,'Stock') or "0");
		if stock < 1 then
			return 0, "商品【" .. productName .. "】库存不存在，请稍后重试";
		end

		local freezeStock = tonumber(redis.call('HGET',productStockKey, 'FreezeStock') or "0");
		if freezeStock >= stock then
			-- 失败：库存不足
			return 0, "该商品【" .. productName .. "】库存已售罄，感谢您的参与";
		end

		local stockNum = stock - freezeStock; -- 剩余库存数量
		if buyNum > stockNum then 
			-- 失败：库存数量不够
			return 0, "该商品【" .. productName .. "】库存不足，只剩余" .. stockNum .."件";
		end
		local newFreezeStock = freezeStock + buyNum;
		productStockMany[i] = productId .. "|" .. newFreezeStock;
	end

	-- 成功：扣减库存
	for i, pStock in ipairs(productStockMany) do
		local pItem = split(pStock, "|");
		local productId = pItem[1];
		local fStock = tonumber(pItem[2] or "0");
		local deductionStockKey = prefix .. ":Product:{" .. productId .. "}"; -- Rex.GoodService:{0}:Product:{1}
		redis.call('HSET',deductionStockKey,'FreezeStock',fStock);

		-- 将Key存到集合，用于后台同步到数据库
		local goodStockDirtyKey = prefix .. ":StockDirtyKey"; -- Rex.GoodService:{0}:StockDirtyKey
		redis.call('LPUSH', goodStockDirtyKey, deductionStockKey);
	end
	return 1;
end

--[[
	二、函数调用
]]--
-- 1.下单幂等存储
local status,msg = recordOrderNo();
if status == 0 then
	return msg
end

-- 2.验证商品库存是否充足
local status,msg = checkProductStock();
if status == 0 then
	return msg
end

-- 返回成功标识
return 1;