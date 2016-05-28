using System;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.Interface.Fluent;
using Jal.Locator.Interface;

namespace Jal.Converter.Fluent
{
    public class ModelConverterSetupDescriptor : IModelConverterSetupDescriptor, IModelConverterStartSetupDescriptor
    {
        private IConverterFactory _converterFactory;

        private IModelConverterInterceptor _modelConverterInterceptor;

        private IModelConverter _modelConverter;

        public IModelConverterSetupDescriptor UseFactory(IConverterFactory converterFactory)
        {
            if (converterFactory == null)
            {
                throw new ArgumentNullException("converterFactory");
            }
            _converterFactory = converterFactory;
            return this;
        }

        public IModelConverterSetupDescriptor UseServiceLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _converterFactory = new ConverterFactory(serviceLocator);
            return this;
        }

        public IModelConverterEndSetupDescriptor UseModelConverter(IModelConverter modelConverter)
        {
            _modelConverter = modelConverter;
            return this;
        }

        public IModelConverterSetupDescriptor UseInterceptor(IModelConverterInterceptor modelConverterInterceptor)
        {
            _modelConverterInterceptor = modelConverterInterceptor;
            return this;
        }

        public IModelConverter Create()
        {
            if (_modelConverter != null)
            {
                return _modelConverter;
            }

            IModelConverterInterceptor modelConverterInterceptor = new NullModelConverterInterceptor();

            if (_modelConverterInterceptor != null)
            {
                modelConverterInterceptor = _modelConverterInterceptor;
            }

            return new ModelConverter(_converterFactory, modelConverterInterceptor);
        }
    }
}