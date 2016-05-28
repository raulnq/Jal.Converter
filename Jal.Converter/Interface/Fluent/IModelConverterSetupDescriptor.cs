namespace Jal.Converter.Interface.Fluent
{
    public interface IModelConverterSetupDescriptor : IModelConverterEndSetupDescriptor
    {
        IModelConverterSetupDescriptor UseInterceptor(IModelConverterInterceptor modelConverterInterceptor);
    }
}