using System;
using Jal.Converter.Interface;

namespace Jal.Converter.Impl
{
    public abstract class AbstractConverterInterceptor : IModelConverterInterceptor
    {
        public static IModelConverterInterceptor Instance = new NullModelConverterInterceptor();

        public virtual void OnEnter<TSource, TDestination>(TSource source, TDestination destination)
        {

        }

        public virtual void OnSuccess<TSource, TDestination>(TSource source, TDestination destination)
        {

        }

        public virtual void OnError<TSource, TDestination>(TSource source, TDestination destination, Exception exception)
        {

        }

        public virtual void OnExit<TSource, TDestination>(TSource source, TDestination destination)
        {

        }
    }
}
