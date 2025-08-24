--[[
    一、函数定义
]]--

-- 0. 自定义 Split 函数
local function split(input, delimiter)
    if not input or input == '' then
        return {}
    end
    local result = {}
    for part in string.gmatch(input, "[^" .. delimiter .. "]+") do
        table.insert(result, part)
    end
    return result
end

-- 1. 移除订单号
local function rollbackOrderNo()
    local orderGoodKey = KEYS[1]
    local hasOrderNo = redis.call('GET', orderGoodKey)
    if hasOrderNo then
        redis.call('DEL', orderGoodKey)
    end
    return 1
end

-- 2. 验证并回滚商品库存
local function rollbackProductStock()
    local values = ARGV
    local prefix = KEYS[2]

    for i, value in ipairs(values) do
        local pItem = split(value, ":")
        if #pItem < 2 then
            return 0, "无效的产品数据格式: " .. tostring(value)
        end

        local productId = pItem[1]
        local buyNum = tonumber(pItem[2])
        if not buyNum or buyNum <= 0 then
            return 0, "无效的购买数量: " .. tostring(pItem[2])
        end

        local productStockKey = prefix .. ":Product:{" .. productId .. "}"
        local freezeStock = tonumber(redis.call('HGET', productStockKey, 'FreezeStock') or "0")
        if freezeStock and freezeStock > 0 then
            local newFreezeStock = freezeStock - buyNum
            if newFreezeStock < 0 then
                return 0, "库存异常，冻结库存不能小于0，回滚失败: " .. productId
            end
            redis.call('HSET', productStockKey, 'FreezeStock', newFreezeStock);

            -- 将Key存到集合，用于后台同步到数据库
		    local goodStockDirtyKey = prefix .. ":StockDirtyKey"; -- Rex.GoodService:{0}:StockDirtyKey
		    redis.call('LPUSH', goodStockDirtyKey, productStockKey);
        end
    end

    return 1
end

--[[
    二、函数调用
]]--

-- 1. 回滚订单
local status = rollbackOrderNo()
if status ~= 1 then
    return '回滚订单异常'
end

-- 2. 回滚库存
local status, msg = rollbackProductStock()
if status ~= 1 then
    return msg or '回滚扣减库存异常'
end

-- 返回成功
return 1