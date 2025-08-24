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

-- 1.移除订单号
local function rollbackOrderNo()
    local orderGoodKey = KEYS[1]
    local hasOrderNo = redis.call('GET', orderGoodKey)
    if hasOrderNo then
        redis.call('DEL', orderGoodKey)
    end
    return 1
end

-- 2.恢复购买限制
local function recoverBuyLimit()
	local userBuyNumsKey = KEYS[2]; -- 用户购买数量Key
	local totalBuyNumsKey = KEYS[3]; -- 购买商品总量Key
	local userMaxNums = tonumber(ARGV[1]); -- 用户购买(商品)数量限制 ---> 0：表示不限制
	local maxGoodNums = tonumber(ARGV[2]); -- 商品总量 ---> 0：表示不限制
	local productNums = tonumber(ARGV[3]); -- 购买(商品)数量

	if userMaxNums > 0 then
		local userBuyNums = tonumber(redis.call('GET',userBuyNumsKey) or "0");
		if userBuyNums > 0 then 
			-- 恢复用户购买限制
			redis.call('INCRBY',userBuyNumsKey,0 - productNums);
		end
	end

	if maxGoodNums > 0 then
		local totalBuyNums = tonumber(redis.call('GET',totalBuyNumsKey) or "0");
		if totalBuyNums > 0 then 
			-- 恢复商品购买数量
			redis.call('INCRBY',totalBuyNumsKey,0 - productNums);
		end
	end

	return 1;
end

-- 3.验证并取消商品库存
local function recoverProductStock()
    local values = ARGV
    local productStockKey = KEYS[4]; -- 商品库存Key：Rex.GoodService:{xxx}:Product:{xxx}
	local productNums = tonumber(ARGV[3]); -- 购买(商品)数量

    local freezeStock = tonumber(redis.call('HGET', productStockKey, 'FreezeStock') or "0")
    if freezeStock and freezeStock > 0 then
        local newFreezeStock = freezeStock - productNums
        if newFreezeStock < 0 then
            return 0, "库存异常，冻结库存不能小于0，取消失败: " .. productId
        end
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
-- 1. 取消订单
local status = rollbackOrderNo()
if status ~= 1 then
    return '取消订单异常'
end

-- 2.恢复用户购买限制
local status,msg = recoverBuyLimit();
if status ~= 1 then
	return msg
end

-- 3.取消商品库存
local status,msg = recoverProductStock();
if status ~= 1 then
	return msg
end

-- 返回成功标识
return 1;