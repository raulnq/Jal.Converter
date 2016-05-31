using Jal.Converter.Interface;

namespace Jal.Converter.Fluent.Interface
{
    public interface IModelConverterFluentBuilder : IModelConverterEndFluentBuilder
    {
        IModelConverterFluentBuilder UseInterceptor(IModelConverterInterceptor modelConverterInterceptor);
    }
}