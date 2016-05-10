using Jal.Converter.Interface;
using Jal.Locator.Interface;

namespace Jal.Converter.Impl
{
    public class ModelConverter : IModelConverter
    {
        private readonly IConverterFactory _converterFactory;

        public static IModelConverter Current;

        public static void SetModelConverterProvider(IModelConverter provider)
        {
            Current = provider;
        }

        public static void SetDefaultModelConverterProvider(IServiceLocator serviceLocator)
        {
            var factory = new ConverterFactory(serviceLocator);
            Current = new ModelConverter(factory, new NullModelConverterLogger());
        }

        private readonly IModelConverterLogger _modelConverterLogger;

        public ModelConverter(IConverterFactory converterFactory, IModelConverterLogger modelConverterLogger)
        {
            _converterFactory = converterFactory;
            _modelConverterLogger = modelConverterLogger;
        }

        public TDestination Convert<TSource, TDestination>(TSource source)
        {
            _modelConverterLogger.Before<TSource, TDestination>(source);
            var converter = _converterFactory.Create<TSource, TDestination>();
            var result = converter.Convert(source);
            _modelConverterLogger.After(source, result);
            return result;
        }

        public TDestination Convert<TSource, TDestination>(TSource source, dynamic context)
        {
            _modelConverterLogger.Before<TSource, TDestination>(source);
            var converter = _converterFactory.Create<TSource, TDestination>();
            var result = converter.Convert(source, context);
            _modelConverterLogger.After(source, result);
            return result;
        }

        public TDestination Convert<TSource, TDestination>(TSource source, TDestination destination)
        {
            _modelConverterLogger.Before<TSource, TDestination>(source);
            var converter = _converterFactory.Create<TSource, TDestination>();
            var result = converter.Convert(source, destination);
            _modelConverterLogger.After(source, result);
            return result;
        }

        public TDestination Convert<TSource, TDestination>(TSource source, TDestination destination, dynamic context)
        {
            _modelConverterLogger.Before<TSource, TDestination>(source);
            var converter = _converterFactory.Create<TSource, TDestination>();
            var result = converter.Convert(source, destination, context);
            _modelConverterLogger.After(source, result);
            return result;
        }
    }
}
