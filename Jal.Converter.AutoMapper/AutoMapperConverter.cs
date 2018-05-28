using AutoMapper;
using Jal.Converter.Interface;

namespace Jal.Converter.AutoMapper
{
    public class AutoMapperConverter<TSource, TDestination> : IConverter<TSource, TDestination>
    {
        public TDestination Convert(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Convert(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
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