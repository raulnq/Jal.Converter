using System;

namespace Jal.Converter.Interface
{
    public interface IModelConverter
    {
        object Convert(Type sourcetype, Type destinationtype, object source, object destination);
        object Convert(Type sourcetype, Type destinationtype, object source);
        TDestination Convert<TSource, TDestination>(TSource source);

        TDestination Convert<TSource, TDestination>(TSource source, dynamic context);

        TDestination Convert<TSource, TDestination>(TSource source, TDestination destination);

        TDestination Convert<TSource, TDestination>(TSource source, TDestination destination, dynamic context);

        IConverterFactory Factory { get; }

        IModelConverterInterceptor Interceptor { get; set; }
    }
}
