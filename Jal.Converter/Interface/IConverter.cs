namespace Jal.Converter.Interface
{
    public interface IConverter<in TSource, TDestination> : IConverter
    {
        TDestination Convert(TSource source);

        TDestination Convert(TSource source, TDestination destination);

        TDestination Convert(TSource source, dynamic context);

        TDestination Convert(TSource source, TDestination destination, dynamic context);
    }

    public interface IConverter
    {

    }
}