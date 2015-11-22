namespace Jal.Converter.Interface
{
    public interface IConverterFactory
    {
        IConverter<TSource, TDestination> Create<TSource, TDestination>();
    }
}
