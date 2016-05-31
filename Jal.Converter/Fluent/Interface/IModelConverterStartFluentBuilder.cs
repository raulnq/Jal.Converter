using Jal.Converter.Interface;
using Jal.Locator.Interface;

namespace Jal.Converter.Fluent.Interface
{
    public interface IModelConverterStartFluentBuilder
    {
        IModelConverterFluentBuilder UseFactory(IServiceLocator serviceLocator);

        IModelConverterFluentBuilder UseFactory(IConverterFactory converterFactory);

        IModelConverterEndFluentBuilder UseModelConverter(IModelConverter modelConverter);
    }
}