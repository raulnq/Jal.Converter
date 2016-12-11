using Jal.Converter.Interface;

namespace Jal.Converter.Fluent.Interface
{
    public interface IModelConverterInterceptorBuilder : IModelConverterCreateBuilder
    {
        IModelConverterCreateBuilder UseInterceptor(IModelConverterInterceptor modelConverterInterceptor);
    }
}