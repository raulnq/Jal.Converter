namespace Jal.Converter.Interface
{
    public interface IModelConverterSetupDescriptor
    {
        IModelConverterSetupDescriptor UseModelConverter(IModelConverter modelConverter);

        IModelConverterSetupDescriptor UseConverterFactory(IConverterFactory converterFactory);

        IModelConverterSetupDescriptor UseModelConverterLogger(IModelConverterLogger modelConverterLogger);

        IModelConverter Create();
    }
}