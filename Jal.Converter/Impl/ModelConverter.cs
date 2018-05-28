using System;
using System.Linq;
using Jal.Converter.Interface;
using Jal.Locator.Interface;

namespace Jal.Converter.Impl
{
    public class ModelConverter : IModelConverter
    {
        public IConverterFactory Factory { get; }

        public IModelConverterInterceptor Interceptor { get; set; }

        public static IModelConverter Current;

        public static IModelConverter Create(IServiceLocator locator)
        {
            return new ModelConverter(new ConverterFactory(locator));
        }

        public ModelConverter(IConverterFactory converterFactory)
        {
            Factory = converterFactory;

            Interceptor = AbstractConverterInterceptor.Instance;
        }

        TDestination Try<TSource, TDestination>(TSource source, TDestination destination, Func<IConverter<TSource, TDestination>, TDestination> converterfunc)
        {
            Interceptor.OnEnter(source, destination);
            try
            {
                var converter = Factory.Create<TSource, TDestination>();

                var result = converterfunc(converter);

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

        public object Convert(Type sourcetype, Type destinationtype, object source)
        {
            var method = typeof(ModelConverter).GetMethods().First(x => x.Name == nameof(ModelConverter.Convert) && x.GetParameters().Count() == 1);

            var genericmethod = method?.MakeGenericMethod(sourcetype, destinationtype);

            return genericmethod?.Invoke(this, new[] { source });
        }

        public object Convert(Type sourcetype, Type destinationtype, object source, object destination)
        {
            var method = typeof(ModelConverter).GetMethods().First(x => x.Name == nameof(ModelConverter.Convert) && x.GetParameters().Count() == 2);

            var genericmethod = method?.MakeGenericMethod(sourcetype, destinationtype);

            return genericmethod?.Invoke(this, new[] { source, destination });
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
