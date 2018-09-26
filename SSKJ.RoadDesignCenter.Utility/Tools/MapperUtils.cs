using AutoMapper;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public static class MapperUtils
    {
        /// <summary>
        /// 实体类对象映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            //配置对应关系
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>();
                });

            //创建对应关系
            IMapper mapper = config.CreateMapper();

            return mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// 实体类对象集合映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            //配置对应关系
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSource, TDestination>();
                });

            //创建对应关系
            IMapper mapper = config.CreateMapper();

            return mapper.Map<IEnumerable<TSource>, IList<TDestination>>(source);
        }
    }
}
