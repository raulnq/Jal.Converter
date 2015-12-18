using System;
using Jal.Converter.Interface;

namespace Jal.Converter.Impl
{
    public abstract class AbstractConverter<TSource, TDestination> : IConverter<TSource, TDestination>
    {
        public virtual TDestination Convert(TSource source)
        {
            throw new NotImplementedException();
        }

        public virtual TDestination Convert(TSource source, TDestination destination)
        {
            return Convert(source);
        }

        public virtual TDestination Convert(TSource source, dynamic context)
        {
            return Convert(source);
        }

        public virtual TDestination Convert(TSource source, TDestination destination, dynamic context)
        {
            return Convert(source);
        }
    }
}
