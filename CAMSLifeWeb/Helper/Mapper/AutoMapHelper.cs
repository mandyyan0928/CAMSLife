using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Helper.Mapper
{
    public class AutoMapHelper
    {
        public AutoMapHelper()
        {

        }
        public static TDestnation Map<TSource, TDestnation>(TSource source)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestnation>(); });
            IMapper iMapper = config.CreateMapper();
            var result = iMapper.Map<TSource, TDestnation>(source);
            return result;
        }


        public static List<TDestnation> MapList<TSource, TDestnation>(List<TSource> source)
        {

            if (source == null || source.Count == 0)
                return new  List<TDestnation>();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestnation>(); });
            IMapper iMapper = config.CreateMapper();
            var resul = iMapper.Map<List<TSource>, List<TDestnation>>(source);
            return resul;
        }
    }
}