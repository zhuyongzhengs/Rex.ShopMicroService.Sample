using AutoMapper;

namespace Rex.FrontAggregationService.Core.Mappers
{
    /// <summary>
    /// 对象映射
    /// </summary>
    public class ObjectMapper
    {
        private static readonly IMapper _mapper;

        static ObjectMapper()
        {
            // 初始化Mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// 映射对象
        /// </summary>
        /// <typeparam name="TSource">来源</typeparam>
        /// <typeparam name="TDestination">目标</typeparam>
        /// <param name="source">来源对象</param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}