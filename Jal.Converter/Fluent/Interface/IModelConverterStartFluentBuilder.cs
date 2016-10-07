using Jal.Locator.Interface;

namespace Jal.Converter.Fluent.Interface
{
    public interface IModelConverterStartFluentBuilder
    {
        IModelConverterFluentBuilder UseLocator(IServiceLocator serviceLocator);
    }
}