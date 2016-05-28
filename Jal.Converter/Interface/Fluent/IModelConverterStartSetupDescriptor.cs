using Jal.Locator.Interface;

namespace Jal.Converter.Interface.Fluent
{
    public interface IModelConverterStartSetupDescriptor
    {
        IModelConverterSetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);

        IModelConverterSetupDescriptor UseFactory(IConverterFactory converterFactory);

        IModelConverterEndSetupDescriptor UseModelConverter(IModelConverter modelConverter);
    }
}