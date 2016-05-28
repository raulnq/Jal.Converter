using System;
using Jal.Converter.Interface;

namespace Jal.Converter.Impl
{
    public class NullModelConverterInterceptor : IModelConverterInterceptor
    {
        public void OnEnter<TSource, TDestination>(TSource source, TDestination destination)
        {

        }

        public void OnSuccess<TSource, TDestination>(TSource source, TDestination destination)
        {

        }

        public void OnError<TSource, TDestination>(TSource source, TDestination destination, Exception exception)
        {

        }

        public void OnExit<TSource, TDestination>(TSource source, TDestination destination)
        {

        }
    }
}
