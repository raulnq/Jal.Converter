using Jal.Locator.Interface;

namespace Jal.Converter.Interface.Fluent
{
    public interface IModelConverterStartFluentBuilder
    {
        IModelConverterFluentBuilder UseServiceLocator(IServiceLocator serviceLocator);

        IModelConverterFluentBuilder UseFactory(IConverterFactory converterFactory);

        IModelConverterEndFluentBuilder UseModelConverter(IModelConverter modelConverter);
    }
}