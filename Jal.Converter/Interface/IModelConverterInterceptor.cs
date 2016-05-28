using System;

namespace Jal.Converter.Interface
{
    public interface IModelConverterInterceptor
    {
        void OnEnter<TSource, TDestination>(TSource source, TDestination destination);

        void OnSuccess<TSource, TDestination>(TSource source, TDestination destination);

        void OnError<TSource, TDestination>(TSource source, TDestination destination, Exception exception);

        void OnExit<TSource, TDestination>(TSource source, TDestination destination);
    }
}
