using System;
using Jal.Converter.Fluent;
using Jal.Converter.Fluent.Impl;
using Jal.Converter.Fluent.Interface;
using Jal.Converter.Interface;

namespace Jal.Converter.Impl
{
    public class ModelConverter : IModelConverter
    {
        public IConverterFactory Factory { get; set; }

        public IModelConverterInterceptor Interceptor { get; set; }

        public static IModelConverter Current;

        public static IModelConverterStartFluentBuilder Builder
        {
            get
            {
                return new ModelConverterFluentBuilder();
            }
        }

        public ModelConverter(IConverterFactory converterFactory)
        {
            Factory = converterFactory;

            Interceptor =AbstractConverterInterceptor.Instance;
        }

        TDestination Try<TSource, TDestination>(TSource source, TDestination destination, Func<IConverter<TSource, TDestination>, TDestination> convertion)
        {
            Interceptor.OnEnter(source, destination);
            try
            {
                var converter = Factory.Create<TSource, TDestination>();
                var result = convertion(converter);
                Interceptor.OnSuccess(source, result);
                return result;
            }
            catch (Exception ex)
            {
                Interceptor.OnError(source, destination, ex);
                throw;
            }
            finally
            {
                Interceptor.OnExit(source, destination);
            }
        }

        public TDestination Convert<TSource, TDestination>(TSource source)
        {
            return Try(source, default(TDestination), converter => converter.Convert(source));
        }

        public TDestination Convert<TSource, TDestination>(TSource source, dynamic context)
        {
            return Try(source, default(TDestination), converter => converter.Convert(source, context));
        }

        public TDestination Convert<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Try(source, destination, converter => converter.Convert(source, destination));
        }

        public TDestination Convert<TSource, TDestination>(TSource source, TDestination destination, dynamic context)
        {
            return Try(source, destination, converter => converter.Convert(source, destination, context));
        }
    }
}
