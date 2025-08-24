using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Rex.GoodService.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rex.GoodService.Stores
{
    /// <summary>
    /// 门店仓储
    /// </summary>
    public class StoreRepository : EfCoreRepository<GoodServiceDbContext, Store, Guid>, IStoreRepository
    {
        public GoodServiceDbContext gServiceDbContext { get; set; }

        public StoreRepository(IDbContextProvider<GoodServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 根据坐标获取门店数量
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">精度</param>
        /// <returns></returns>
        public async Task<long> GetStoreByCoordinateCountAsync(string? storeName, decimal latitude = 0, decimal longitude = 0, CancellationToken cancellationToken = default)
        {
            long totalCount = 0;
            if (latitude > 0 && longitude > 0)
            {
                string sqrt = "SQRT(power(SIN((" + latitude + "*PI()/180-(Gd_Stores.Latitude)*PI()/180)/2),2)+COS(" + latitude + "*PI()/180)*COS((Gd_Stores.Latitude)*PI()/180)*power(SIN((" + longitude + "*PI()/180-(Gd_Stores.Longitude)*PI()/180)/2),2))";
                string sql = "SELECT Id, StoreName, Mobile, LinkMan, LogoImage, AreaId, Address, Coordinate, Latitude, Longitude, IsDefault, CreationTime, LastModificationTime, ROUND(6378.138*2*ASIN(" + sqrt + ")*1000,2)  AS Distance FROM Gd_Stores WHERE IsDeleted = 0;";
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                if (!storeName.IsNullOrWhiteSpace())
                {
                    sql += " AND StoreName LIKE CONCAT('%',@StoreName,'%') ";
                    parameters.Add(new MySqlParameter("@StoreName", storeName));
                }
                totalCount = await Task.Run(() =>
                {
                    var totalCount = gServiceDbContext.ExecuteScalar($"SELECT COUNT(*) FROM ({sql})", CommandType.Text, parameters.ToArray());
                    if (totalCount != null)
                    {
                        return Convert.ToInt64(totalCount);
                    }
                    return 0;
                });
            }
            else
            {
                totalCount = await (await GetDbSetAsync())
                .WhereIf(!storeName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(storeName))
                .LongCountAsync(GetCancellationToken(cancellationToken));
            }
            return totalCount;
        }

        /// <summary>
        /// 根据坐标获取门店
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">精度</param>
        /// <returns></returns>
        public async Task<List<Store>> GetStoreByCoordinateListAsync(string? storeName, int skipCount, int maxResultCount, string sorting, decimal latitude = 0, decimal longitude = 0, CancellationToken cancellationToken = default)
        {
            List<Store> storeList = new List<Store>();
            if (latitude > 0 && longitude > 0)
            {
                string sqrt = "SQRT(power(SIN((" + latitude + " * PI()/180-(Gd_Stores.Latitude) * PI()/180)/2),2)+COS(" + latitude + " * PI()/180) * COS((Gd_Stores.Latitude) * PI()/180) * power(SIN((" + longitude + " * PI()/180-(Gd_Stores.Longitude) * PI()/180)/2),2))";
                string sql = "SELECT Id, StoreName, Mobile, LinkMan, LogoImage, AreaId, Address, Coordinate, Latitude, Longitude, IsDefault, CreationTime, LastModificationTime, ROUND(6378.138 * 2 * ASIN(" + sqrt + ") * 1000,2)  AS Distance FROM Gd_Stores WHERE IsDeleted = 0;";
                List<MySqlParameter> parameters = new List<MySqlParameter>()
                {
                    new MySqlParameter("@Limit", maxResultCount),
                    new MySqlParameter("@Offset", skipCount)
                };
                if (!storeName.IsNullOrWhiteSpace())
                {
                    sql += " AND StoreName LIKE CONCAT('%',@StoreName,'%') ";
                    parameters.Add(new MySqlParameter("@StoreName", storeName));
                }
                if (!sorting.IsNullOrWhiteSpace())
                {
                    sql += $" ORDER BY {sorting} ";
                }
                sql += " LIMIT @Offset,@Limit ";
                storeList = await Task.Run(() =>
                {
                    DataTable dataTable = gServiceDbContext.ExecuteQuery(sql, CommandType.Text, parameters.ToArray());
                    List<DataRow> dataRowList = dataTable.AsEnumerable().ToList();
                    foreach (DataRow dataRow in dataRowList)
                    {
                        Store store = new Store(dataRow.Field<Guid>("Id"));
                        store.StoreName = dataRow.Field<string>("StoreName");
                        store.Mobile = dataRow.Field<string>("Mobile");
                        store.LinkMan = dataRow.Field<string>("LinkMan");
                        store.LogoImage = dataRow.Field<string>("LogoImage");
                        store.AreaId = dataRow.Field<long>("AreaId");
                        store.Address = dataRow.Field<string>("Address");
                        store.Coordinate = dataRow.Field<string>("Coordinate");
                        store.Latitude = dataRow.Field<string>("Latitude");
                        store.Longitude = dataRow.Field<string>("Longitude");
                        store.IsDefault = dataRow.Field<bool>("IsDefault");
                        store.Distance = dataRow.Field<decimal>("Distance");
                        storeList.Add(store);
                    }
                    return storeList;
                });
            }
            else
            {
                IQueryable<Store> queryable = (await GetDbSetAsync())
                .WhereIf(!storeName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(storeName))
                .OrderByIf<Store, IQueryable<Store>>(!sorting.IsNullOrWhiteSpace(), sorting);
                storeList = await queryable.PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken));
            }
            return storeList;
        }
    }
}