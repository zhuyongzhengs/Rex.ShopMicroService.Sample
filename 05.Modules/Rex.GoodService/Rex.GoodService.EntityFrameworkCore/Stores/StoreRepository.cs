using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
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
        /// <param name="longitude">经度</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>门店数量</returns>
        public async Task<long> GetStoreByCoordinateCountAsync(string? storeName,
            decimal latitude = 0,
            decimal longitude = 0,
            CancellationToken cancellationToken = default)
        {
            var ct = GetCancellationToken(cancellationToken);
            if (latitude > 0 && longitude > 0)
            {
                var sqrtPart = @"SQRT(POWER(SIN( (@Lat * PI()/180 - ""Gd_Stores"".""Latitude"" * PI()/180)/2 ),2) " +
                               @"+ COS(@Lat * PI()/180) * COS(""Gd_Stores"".""Latitude"" * PI()/180) " +
                               @"* POWER(SIN( (@Lng * PI()/180 - ""Gd_Stores"".""Longitude"" * PI()/180)/2 ),2))";

                var sql = $@"SELECT ""Id"", ""StoreName"", ""Mobile"", ""LinkMan"", ""LogoImage"", ""AreaId"", ""Address"", ""Coordinate"",
                     ""Latitude"", ""Longitude"", ""IsDefault"", ""CreationTime"", ""LastModificationTime"",
                     ROUND(6378.138 * 2 * ASIN({sqrtPart}) * 1000, 2) AS Distance
                     FROM ""Gd_Stores""
                     WHERE ""IsDeleted"" = '0'";

                var parameters = new List<NpgsqlParameter>
                {
                    new NpgsqlParameter("@Lat", NpgsqlDbType.Numeric) { Value = latitude },
                    new NpgsqlParameter("@Lng", NpgsqlDbType.Numeric) { Value = longitude }
                };

                if (!storeName.IsNullOrWhiteSpace())
                {
                    sql += @" AND ""StoreName"" LIKE '%' || @StoreName || '%'";
                    parameters.Add(new NpgsqlParameter("@StoreName", NpgsqlDbType.Varchar) { Value = storeName });
                }
                var countSql = $"SELECT COUNT(*) FROM ({sql}) t";

                var totalCountObj = gServiceDbContext.ExecuteScalar(countSql, CommandType.Text, parameters.ToArray());
                return totalCountObj is not null ? Convert.ToInt64(totalCountObj) : 0;
            }
            else
            {
                return await (await GetDbSetAsync())
                    .WhereIf(!storeName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(storeName))
                    .LongCountAsync(ct);
            }
        }

        /// <summary>
        /// 根据坐标获取门店（分页）
        /// </summary>
        /// <param name="storeName">门店名称</param>
        /// <param name="skipCount">跳过数</param>
        /// <param name="maxResultCount">最大结果数</param>
        /// <param name="sorting">排序（如Distance ASC/Id DESC）</param>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>门店列表（含距离Distance字段）</returns>
        public async Task<List<Store>> GetStoreByCoordinateListAsync(string? storeName,
            int skipCount,
            int maxResultCount,
            string sorting,
            decimal latitude = 0,
            decimal longitude = 0,
            CancellationToken cancellationToken = default)
        {
            var ct = GetCancellationToken(cancellationToken);
            var storeList = new List<Store>();
            if (latitude > 0 && longitude > 0)
            {
                var sqrtPart = @"SQRT(POWER(SIN( (@Lat * PI()/180 - ""Gd_Stores"".""Latitude"" * PI()/180)/2 ),2) " +
                               @"+ COS(@Lat * PI()/180) * COS(""Gd_Stores"".""Latitude"" * PI()/180) " +
                               @"* POWER(SIN( (@Lng * PI()/180 - ""Gd_Stores"".""Longitude"" * PI()/180)/2 ),2))";

                var sql = $@"SELECT ""Id"", ""StoreName"", ""Mobile"", ""LinkMan"", ""LogoImage"", ""AreaId"", ""Address"", ""Coordinate"",
                     ""Latitude"", ""Longitude"", ""IsDefault"", ""CreationTime"", ""LastModificationTime"",
                     ROUND(6378.138 * 2 * ASIN({sqrtPart}) * 1000, 2) AS Distance
                     FROM ""Gd_Stores""
                     WHERE ""IsDeleted"" = '0'";

                var parameters = new List<NpgsqlParameter>
                {
                    new NpgsqlParameter("@Lat", NpgsqlDbType.Numeric) { Value = latitude },
                    new NpgsqlParameter("@Lng", NpgsqlDbType.Numeric) { Value = longitude },
                    new NpgsqlParameter("@Limit", NpgsqlDbType.Integer) { Value = maxResultCount },
                    new NpgsqlParameter("@Offset", NpgsqlDbType.Integer) { Value = skipCount }
                };

                if (!storeName.IsNullOrWhiteSpace())
                {
                    sql += @" AND ""StoreName"" LIKE '%' || @StoreName || '%'";
                    parameters.Add(new NpgsqlParameter("@StoreName", NpgsqlDbType.Varchar) { Value = storeName });
                }
                if (!sorting.IsNullOrWhiteSpace())
                {
                    sql += $" ORDER BY {sorting} ";
                }
                sql += " LIMIT @Limit OFFSET @Offset";

                DataTable dataTable = gServiceDbContext.ExecuteQuery(sql, CommandType.Text, parameters.ToArray());
                foreach (DataRow row in dataTable.Rows)
                {
                    var store = new Store(row.Field<Guid>("Id"))
                    {
                        StoreName = row.Field<string?>("StoreName"),
                        Mobile = row.Field<string?>("Mobile"),
                        LinkMan = row.Field<string?>("LinkMan"),
                        LogoImage = row.Field<string?>("LogoImage"),
                        AreaId = row.Field<long>("AreaId"),
                        Address = row.Field<string?>("Address"),
                        Coordinate = row.Field<string?>("Coordinate"),
                        Latitude = row.Field<string?>("Latitude"),
                        Longitude = row.Field<string?>("Longitude"),
                        IsDefault = row.Field<bool>("IsDefault"),
                        CreationTime = row.Field<DateTime>("CreationTime"),
                        LastModificationTime = row.Field<DateTime?>("LastModificationTime"),
                        Distance = row.Field<decimal>("Distance") // 地理距离（米，保留2位）
                    };
                    storeList.Add(store);
                }
            }
            else
            {
                // 无坐标时，使用原有EF Linq分页排序逻辑，保持不变
                IQueryable<Store> queryable = (await GetDbSetAsync())
                    .WhereIf(!storeName.IsNullOrWhiteSpace(), p => p.StoreName.Contains(storeName))
                    .OrderByIf<Store, IQueryable<Store>>(!sorting.IsNullOrWhiteSpace(), sorting);

                storeList = await queryable
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(ct);
            }

            return storeList;
        }
    }
}