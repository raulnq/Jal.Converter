using Jal.Converter.Interface;

namespace Jal.Converter.Fluent.Interface
{
    public interface IModelConverterFluentBuilder : IModelConverterEndFluentBuilder
    {
        IModelConverterEndFluentBuilder UseInterceptor(IModelConverterInterceptor modelConverterInterceptor);
    }
}