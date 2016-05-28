using System;
using Jal.Converter.Fluent;
using Jal.Converter.Interface;
using Jal.Converter.Interface.Fluent;

namespace Jal.Converter.Impl
{
    public class ModelConverter : IModelConverter
    {
        public IConverterFactory Factory { get; set; }

        public IModelConverterInterceptor Interceptor { get; set; }

        public static IModelConverter Current;

        public static IModelConverterStartSetupDescriptor Setup
        {
            get
            {
                return new ModelConverterSetupDescriptor();
            }
        }

        private readonly IModelConverterInterceptor _modelConverterInterceptor;

        public ModelConverter(IConverterFactory converterFactory, IModelConverterInterceptor modelConverterInterceptor)
        {
            Factory = converterFactory;
            _modelConverterInterceptor = modelConverterInterceptor;
        }

        TDestination Try<TSource, TDestination>(TSource source, TDestination destination, Func<IConverter<TSource, TDestination>, TDestination> convertion)
        {
            _modelConverterInterceptor.OnEnter(source, destination);
            try
            {
                var converter = Factory.Create<TSource, TDestination>();
                var result = convertion(converter);
                _modelConverterInterceptor.OnSuccess(source, result);
                return result;
            }
            catch (Exception ex)
            {
                _modelConverterInterceptor.OnError(source, destination, ex);
                throw;
            }
            finally
            {
                _modelConverterInterceptor.OnExit(source, destination);
            }
        }

        public TDestination Convert<TSource, TDestination>(TSource source)
        {
            return Try(source, default(TDestination), (converter) => converter.Convert(source));
        }

        public TDestination Convert<TSource, TDestination>(TSource source, dynamic context)
        {
            return Try(source, default(TDestination), (converter) => converter.Convert(source, context));
        }

        public TDestination Convert<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Try(source, destination, (converter) => converter.Convert(source, destination));
        }

        public TDestination Convert<TSource, TDestination>(TSource source, TDestination destination, dynamic context)
        {
            return Try(source, destination, (converter) => converter.Convert(source, destination, context));
        }
    }
}
