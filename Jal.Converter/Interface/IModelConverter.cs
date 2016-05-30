namespace Jal.Converter.Interface
{
    public interface IModelConverter
    {
        TDestination Convert<TSource, TDestination>(TSource source);

        TDestination Convert<TSource, TDestination>(TSource source, dynamic context);

        TDestination Convert<TSource, TDestination>(TSource source, TDestination destination);

        TDestination Convert<TSource, TDestination>(TSource source, TDestination destination, dynamic context);

        IConverterFactory Factory { get; }

        IModelConverterInterceptor Interceptor { get; set; }
    }
}
