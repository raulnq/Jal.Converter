using Jal.Locator.Interface;

namespace Jal.Converter.Interface
{
    public interface IModelConverterServiceLocatorSetupDescriptor
    {
        IModelConverterSetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);
    }
}