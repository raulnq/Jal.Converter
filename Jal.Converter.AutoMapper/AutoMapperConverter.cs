using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoMapper;
using Jal.Converter.Interface;

namespace Jal.Converter.AutoMapper
{
    public class AutoMapperConverter<TSource, TDestination> : IConverter<TSource, TDestination>
    {
        public TDestination Convert(TSource source)
        {
            if (typeof (TSource) == typeof (IDataReader))
            {
                var data = source as IDataReader;

                var type = typeof (TDestination);

                if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IList<>) || type.GetGenericTypeDefinition() == typeof(List<>) || type.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                {
                    var genericType = type.GetGenericTypeDefinition();

                    var parameter = type.GetGenericArguments()[0];
  
                    var enumerableType = typeof(IEnumerable<>);

                    var constructedEnumerableType = enumerableType.MakeGenericType(parameter);

                    var enumerable = Mapper.Map(data, typeof(IDataReader), constructedEnumerableType);

                    if (genericType == typeof(IList<>) || genericType == typeof(List<>))
                    {
                        var listType = typeof(List<>);

                        var constructedListType = listType.MakeGenericType(parameter);

                        var list = Activator.CreateInstance(constructedListType, new[] { enumerable });

                        return (TDestination)list;
                    }
                    else
                    {
                        if (genericType == typeof(IEnumerable<>))
                        {
                            return (TDestination)enumerable;
                        }                      
                    }
                }
                else
                {
                    if (!typeof (TDestination).IsGenericType)
                    {
                        var results = Mapper.Map<IDataReader, IEnumerable<TDestination>>(data);
                        return results.FirstOrDefault();
                    }
                }
            }
            else
            {
                var results = Mapper.Map<TSource, TDestination>(source);
                return results;
            }

            return default(TDestination);
        }

        public TDestination Convert(TSource source, TDestination destination)
        {
            var results = Mapper.Map(source, destination);
            return results;
        }

        public TDestination Convert(TSource source, dynamic context)
        {
            return Convert(source);
        }

        public TDestination Convert(TSource source, TDestination destination, dynamic context)
        {
            return Convert(source, destination);
        }
    }
}