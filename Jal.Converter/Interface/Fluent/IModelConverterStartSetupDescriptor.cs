using Jal.Locator.Interface;

namespace Jal.Converter.Interface.Fluent
{
    public interface IModelConverterStartSetupDescriptor
    {
        IModelConverterSetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);

        IModelConverterSetupDescriptor UseConverterFactory(IConverterFactory converterFactory);

        IModelConverterEndSetupDescriptor UseModelConverter(IModelConverter modelConverter);
    }
}