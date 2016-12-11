using Jal.Locator.Interface;

namespace Jal.Converter.Fluent.Interface
{
    public interface IModelConverterLocatorBuilder
    {
        IModelConverterInterceptorBuilder UseLocator(IServiceLocator serviceLocator);
    }
}