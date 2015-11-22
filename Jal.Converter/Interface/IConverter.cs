namespace Jal.Converter.Interface
{
    public interface IConverter<in TSource, TDestination> : IConverter
    {
        TDestination Convert(TSource source);

        TDestination Convert(TSource source, TDestination destination);
    }

    public interface IConverter
    {

    }
}