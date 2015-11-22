namespace Jal.Converter.Interface
{
    public interface IModelConverterLogger
    {
        void Before<TSource, TDestination>(TSource source);

        void After<TSource, TDestination>(TSource source, TDestination destination);
    }
}
