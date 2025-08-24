using Microsoft.AspNetCore.Authorization;
using Rex.GoodService.Products;
using Rex.Service.Core.Configurations;
using Rex.Service.Core.Helper;
using Rex.Service.Permission.GoodServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Rex.GoodService.Goods
{
    /// <summary>
    /// 商品类型规格服务
    /// </summary>
    [RemoteService]
    [Authorize(GoodServicePermissions.GoodTypeSpecs.Default)]
    public class GoodTypeSpecAppService : CrudAppService<GoodTypeSpec, GoodTypeSpecDto, Guid, PagedAndSortedResultRequestDto, GoodTypeSpecCreateDto, GoodTypeSpecUpdateDto>, IGoodTypeSpecAppService
    {
        private readonly IGoodTypeSpecRepository _goodTypeSpecRepository;
        public IRepository<GoodTypeSpecValue, Guid> GoodTypeSpecValueRepository { get; set; }

        public GoodTypeSpecAppService(IGoodTypeSpecRepository repository) : base(repository)
        {
            _goodTypeSpecRepository = repository;
        }

        /// <summary>
        /// 获取(分页)商品类型规格列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PagedResultDto<GoodTypeSpecDto>> GetPageListAsync(GetGoodTypeSpecInput input)
        {
            // 查询数量
            long totalCount = await _goodTypeSpecRepository.GetPageCountAsync(input.Name);
            // 查询数据列表
            if (totalCount < 1)
            {
                return new PagedResultDto<GoodTypeSpecDto>();
            }
            List<GoodTypeSpec> goodTypeSpecList = await _goodTypeSpecRepository.GetPageListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Name);
            return new PagedResultDto<GoodTypeSpecDto>(
                totalCount,
                ObjectMapper.Map<List<GoodTypeSpec>, List<GoodTypeSpecDto>>(goodTypeSpecList)
            );
        }

        /// <summary>
        /// 新增商品类型规格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.GoodTypeSpecs.Create)]
        public override async Task<GoodTypeSpecDto> CreateAsync(GoodTypeSpecCreateDto input)
        {
            GoodTypeSpec gTypeSpec = await _goodTypeSpecRepository.FindAsync(p => p.Name.Equals(input.Name));
            if (gTypeSpec != null)
            {
                throw new UserFriendlyException($"SKU模型[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "SKU模型唯一，不允许重复。").WithData("Name", input.Name);
            }

            //GoodTypeSpecDto goodTypeSpecDto = await base.CreateAsync(input);
            GoodTypeSpec goodTypeSpecCreate = ObjectMapper.Map<GoodTypeSpecCreateDto, GoodTypeSpec>(input);
            if (goodTypeSpecCreate.Id == Guid.Empty)
            {
                Type goodTypeSpecType = goodTypeSpecCreate.GetType();
                goodTypeSpecType?.GetProperty("Id")?.SetValue(goodTypeSpecCreate, GuidGenerator.Create());
                foreach (var specValue in goodTypeSpecCreate.SpecValues)
                {
                    Type goodTypeSpecValueType = specValue.GetType();
                    goodTypeSpecValueType?.GetProperty("Id")?.SetValue(specValue, GuidGenerator.Create());
                    goodTypeSpecValueType?.GetProperty("SpecId")?.SetValue(specValue, goodTypeSpecCreate.Id);
                }
            }
            goodTypeSpecCreate = await _goodTypeSpecRepository.InsertAsync(goodTypeSpecCreate);
            return ObjectMapper.Map<GoodTypeSpec, GoodTypeSpecDto>(goodTypeSpecCreate);
        }

        /// <summary>
        /// 修改商品类型规格
        /// </summary>
        /// <returns></returns>
        [Authorize(GoodServicePermissions.GoodTypeSpecs.Update)]
        public override async Task<GoodTypeSpecDto> UpdateAsync(Guid id, GoodTypeSpecUpdateDto input)
        {
            GoodTypeSpec gTypeSpec = await _goodTypeSpecRepository.FindAsync(p => p.Name.Equals(input.Name) && p.Id != id);
            if (gTypeSpec != null)
            {
                throw new UserFriendlyException($"SKU模型[{input.Name}]已存在，请重新输入！", SystemStatusCode.Fail.ToGoodServicePrefix(), "SKU模型唯一，不允许重复。").WithData("Name", input.Name);
            }

            //GoodTypeSpecDto goodTypeSpecDto = await base.UpdateAsync(id, input);
            GoodTypeSpec goodTypeSpecUpdate = ObjectMapper.Map<GoodTypeSpecUpdateDto, GoodTypeSpec>(input);

            #region 筛选需要修改的数据

            Type goodTypeSpecType = goodTypeSpecUpdate.GetType();
            goodTypeSpecType?.GetProperty("Id")?.SetValue(goodTypeSpecUpdate, id);
            goodTypeSpecUpdate.SpecValues = goodTypeSpecUpdate.SpecValues.Where(sv => sv.Id != Guid.Empty).ToList();
            await _goodTypeSpecRepository.UpdateAsync(goodTypeSpecUpdate);

            #endregion 筛选需要修改的数据

            if (input.SpecValues.Count > 0)
            {
                List<GoodTypeSpecValue> goodTypeSpecValueList = goodTypeSpecUpdate.SpecValues;
                Guid[] goodTypeSpecValueIds = goodTypeSpecValueList.Select(gv => gv.Id).ToArray();

                #region 得到删除的数据

                List<GoodTypeSpecValue> deleteGoodTypeSpecValues = await GoodTypeSpecValueRepository.GetListAsync(p => p.SpecId == id);
                deleteGoodTypeSpecValues = deleteGoodTypeSpecValues.Where(gv => !goodTypeSpecValueIds.Contains(gv.Id) && gv.Id != Guid.Empty).ToList();
                if (deleteGoodTypeSpecValues.Count > 0)
                {
                    Guid[] delIds = deleteGoodTypeSpecValues.Select(p => p.Id).ToArray();
                    await GoodTypeSpecValueRepository.DeleteAsync(p => delIds.Contains(p.Id));
                }

                #endregion 得到删除的数据

                #region 得到新增的数据

                List<GoodTypeSpecValueUpdateDto> addGoodTypeSpecValueDtos = input.SpecValues.Where(sv => sv.Id == Guid.Empty).ToList();
                if (addGoodTypeSpecValueDtos.Count > 0)
                {
                    List<GoodTypeSpecValue> addGoodTypeSpecValues = ObjectMapper.Map<List<GoodTypeSpecValueUpdateDto>, List<GoodTypeSpecValue>>(addGoodTypeSpecValueDtos);
                    foreach (var specValue in addGoodTypeSpecValues)
                    {
                        Type goodTypeSpecValueType = specValue.GetType();
                        goodTypeSpecValueType?.GetProperty("Id")?.SetValue(specValue, GuidGenerator.Create());
                        goodTypeSpecValueType?.GetProperty("SpecId")?.SetValue(specValue, id);
                    }
                    await GoodTypeSpecValueRepository.InsertManyAsync(addGoodTypeSpecValues);
                }

                #endregion 得到新增的数据
            }
            return ObjectMapper.Map<GoodTypeSpec, GoodTypeSpecDto>(await _goodTypeSpecRepository.GetAsync(id));
        }

        /// <summary>
        /// 根据ID获取商品类型规格信息
        /// </summary>
        /// <param name="ids">商品类型规格ID</param>
        /// <returns></returns>
        public async Task<List<GoodTypeSpecDto>> GetManyByIdsAsync(List<Guid> ids)
        {
            List<GoodTypeSpec> goodTypeSpecs = await _goodTypeSpecRepository.GetListByIdAsync(ids);
            return ObjectMapper.Map<List<GoodTypeSpec>, List<GoodTypeSpecDto>>(goodTypeSpecs);
        }

        /// <summary>
        /// 生成商品类型规格(SKU)
        /// </summary>
        /// <param name="input">生成商品类型规格Dto</param>
        /// <returns></returns>
        public async Task<List<ProductDto>> CreateGenerateSpecAsync(List<GoodTypeSpecGenerateDto> input)
        {
            return await Task.Run(() =>
            {
                // 得到商品类型规格
                List<GoodTypeSpecValueModel> goodTypeSpecValues = new List<GoodTypeSpecValueModel>();
                foreach (var gTypeSpecGenerate in input)
                {
                    foreach (var specValue in gTypeSpecGenerate.SpecValues)
                    {
                        goodTypeSpecValues.Add(new GoodTypeSpecValueModel()
                        {
                            Name = gTypeSpecGenerate.Name,
                            Value = specValue.Value
                        });
                    }
                }

                // 生成商品规格SKU
                var skuArrList = goodTypeSpecValues.Select((x, i) => new { Index = i, Value = x })
                        .GroupBy(x => x.Value.Name)
                        .Select(x => x.Select(v => v.Value).ToList())
                        .ToList();
                var sku = new string[skuArrList.Count][];
                var arrListIndex = 0;
                skuArrList.ForEach(p =>
                {
                    var arr = new string[p.Count];
                    for (var index = 0; index < p.Count; index++) arr[index] = p[index].Name + ":" + p[index].Value;
                    sku[arrListIndex] = arr;
                    arrListIndex++;
                });
                var skuArray = SkuHelper.Process(sku);

                // 生成货品列表
                List<ProductDto> productList = new List<ProductDto>();
                for (int i = 0; i < skuArray.Length; i++)
                {
                    productList.Add(new ProductDto()
                    {
                        Id = Guid.Empty,
                        GoodId = Guid.Empty,
                        BarCode = string.Empty,
                        Sn = CommonHelper.GetRandomCode("SN"),
                        Price = 0,
                        CostPrice = 0,
                        MktPrice = 0,
                        Weight = 0,
                        Stock = 0,
                        FreezeStock = 0,
                        SpesDesc = skuArray[i],
                        IsDefault = i == 0,
                        Images = string.Empty
                    });
                }
                return productList;
            });
        }
    }
}