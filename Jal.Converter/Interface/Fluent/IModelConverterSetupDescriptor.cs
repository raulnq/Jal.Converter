namespace Jal.Converter.Interface.Fluent
{
    public interface IModelConverterSetupDescriptor : IModelConverterEndSetupDescriptor
    {
        IModelConverterSetupDescriptor UseModelConverterInterceptor(IModelConverterInterceptor modelConverterInterceptor);
    }
}