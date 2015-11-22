namespace Jal.Converter.Interface
{
    public interface IModelConverter
    {
        TDestination Convert<TSource, TDestination>(TSource source);

        TDestination Convert<TSource, TDestination>(TSource source, TDestination destination);
    }
}
