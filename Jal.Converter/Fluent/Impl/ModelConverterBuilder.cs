using System;
using Jal.Converter.Fluent.Interface;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Locator.Interface;

namespace Jal.Converter.Fluent.Impl
{
    public class ModelConverterBuilder : IModelConverterInterceptorBuilder, IModelConverterLocatorBuilder
    {
        public IConverterFactory ConverterFactory;

        public IModelConverterInterceptor ModelConverterInterceptor;

        public IModelConverterInterceptorBuilder UseLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException(nameof(serviceLocator));
            }

            ConverterFactory = new ConverterFactory(serviceLocator);

            return this;
        }

        public IModelConverterCreateBuilder UseInterceptor(IModelConverterInterceptor modelConverterInterceptor)
        {
            if (modelConverterInterceptor == null)
            {
                throw new ArgumentNullException(nameof(modelConverterInterceptor));
            }
            ModelConverterInterceptor = modelConverterInterceptor;

            return this;
        }

        public IModelConverter Create
        {
            get
            {
                var modelconverter = new ModelConverter(ConverterFactory);

                if (ModelConverterInterceptor != null)
                {
                    modelconverter.Interceptor = ModelConverterInterceptor;
                }

                return modelconverter;
            }
        }
    }
}