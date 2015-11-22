using Jal.Converter.Interface;

namespace Jal.Converter.Impl
{
    public class NullModelConverterLogger : IModelConverterLogger
    {
        public void Before<TSource, TDestination>(TSource source)
        {

        }

        public void After<TSource, TDestination>(TSource source, TDestination destination)
        {

        }
    }
}
