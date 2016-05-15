using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Locator.Fluent;
using Jal.Locator.Interface;

namespace Jal.Converter.Fluent
{
    public class ModelConverterSetupDescriptor
    {
        private IConverterFactory _converterFactory;

        private IServiceLocator _serviceLocator;

        private IModelConverterLogger _modelConverterLogger;


        public ModelConverterSetupDescriptor UseServiceLocator(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            return this;
        }

        public ModelConverterSetupDescriptor UseConverterFactory(IConverterFactory converterFactory)
        {
            _converterFactory = converterFactory;
            return this;
        }

        public ModelConverterSetupDescriptor UseModelConverterLogger(IModelConverterLogger modelConverterLogger)
        {
            _modelConverterLogger = modelConverterLogger;
            return this;
        }

        public IModelConverter Create()
        {

            if (_converterFactory != null)
            {
                if (_modelConverterLogger != null)
                {
                    return new ModelConverter(_converterFactory, _modelConverterLogger);
                }
                else
                {
                    return new ModelConverter(_converterFactory, new NullModelConverterLogger());
                }
                      
            }
            else
            {
                if (_serviceLocator != null)
                {
                    var factory = new ConverterFactory(_serviceLocator);
                    if (_modelConverterLogger != null)
                    {
                        return new ModelConverter(factory, _modelConverterLogger);
                    }
                    else
                    {
                        return new ModelConverter(factory, new NullModelConverterLogger());
                    }
                }
                else
                {
                    var servicelocator = new ServiceLocatorSetupDescriptor().Create();
                    var factory = new ConverterFactory(servicelocator);
                    if (_modelConverterLogger != null)
                    {
                        return new ModelConverter(factory, _modelConverterLogger);
                    }
                    else
                    {
                        return new ModelConverter(factory, new NullModelConverterLogger());
                    }
                }
            }
        }
    }
}