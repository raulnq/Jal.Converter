﻿using System;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.Interface.Fluent;
using Jal.Locator.Interface;

namespace Jal.Converter.Fluent
{
    public class ModelConverterFluentBuilder : IModelConverterFluentBuilder, IModelConverterStartFluentBuilder
    {
        private IConverterFactory _converterFactory;

        private IModelConverterInterceptor _modelConverterInterceptor;

        private IModelConverter _modelConverter;

        public IModelConverterFluentBuilder UseFactory(IConverterFactory converterFactory)
        {
            if (converterFactory == null)
            {
                throw new ArgumentNullException("converterFactory");
            }
            _converterFactory = converterFactory;
            return this;
        }

        public IModelConverterFluentBuilder UseServiceLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _converterFactory = new ConverterFactory(serviceLocator);
            return this;
        }

        public IModelConverterEndFluentBuilder UseModelConverter(IModelConverter modelConverter)
        {
            if (modelConverter == null)
            {
                throw new ArgumentNullException("modelConverter");
            }
            _modelConverter = modelConverter;
            return this;
        }

        public IModelConverterFluentBuilder UseInterceptor(IModelConverterInterceptor modelConverterInterceptor)
        {
            _modelConverterInterceptor = modelConverterInterceptor;
            return this;
        }

        public IModelConverter Create
        {
            get
            {
                if (_modelConverter != null)
                {
                    return _modelConverter;
                }

                var modelconverter = new ModelConverter(_converterFactory);

                if (_modelConverterInterceptor != null)
                {
                    modelconverter.Interceptor = _modelConverterInterceptor;
                }

                return modelconverter;
            }
        }
    }
}