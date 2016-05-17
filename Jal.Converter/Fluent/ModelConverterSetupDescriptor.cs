using System;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Locator.Interface;

namespace Jal.Converter.Fluent
{
    public class ModelConverterSetupDescriptor : IModelConverterSetupDescriptor, IModelConverterServiceLocatorSetupDescriptor
    {
        private IConverterFactory _converterFactory;

        private IServiceLocator _serviceLocator;

        private IModelConverterLogger _modelConverterLogger;

        private IModelConverter _modelConverter;

        public IModelConverterSetupDescriptor UseServiceLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _serviceLocator = serviceLocator;
            return this;
        }

        public IModelConverterSetupDescriptor UseModelConverter(IModelConverter modelConverter)
        {
            _modelConverter = modelConverter;
            return this;
        }

        public IModelConverterSetupDescriptor UseConverterFactory(IConverterFactory converterFactory)
        {
            _converterFactory = converterFactory;
            return this;
        }

        public IModelConverterSetupDescriptor UseModelConverterLogger(IModelConverterLogger modelConverterLogger)
        {
            _modelConverterLogger = modelConverterLogger;
            return this;
        }

        public IModelConverter Create()
        {
            if (_modelConverter != null)
            {
                return _modelConverter;
            }

            IConverterFactory converterFactory=new ConverterFactory(_serviceLocator);

            if (_converterFactory != null)
            {
                converterFactory = _converterFactory;
            }

            IModelConverterLogger modelConverterLogger = new NullModelConverterLogger();

            if (_modelConverterLogger != null)
            {
                modelConverterLogger = _modelConverterLogger;
            }

            return new ModelConverter(converterFactory, modelConverterLogger);
        }
    }
}