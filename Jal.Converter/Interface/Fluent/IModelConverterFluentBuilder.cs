namespace Jal.Converter.Interface.Fluent
{
    public interface IModelConverterFluentBuilder : IModelConverterEndFluentBuilder
    {
        IModelConverterFluentBuilder UseInterceptor(IModelConverterInterceptor modelConverterInterceptor);
    }
}