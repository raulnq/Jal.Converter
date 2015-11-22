using Jal.Converter.Interface;
using Jal.Locator.Interface;

namespace Jal.Converter.Impl
{
    public class ConverterFactory : IConverterFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public ConverterFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IConverter<TSource, TDestination> Create<TSource, TDestination>()
        {
            return _serviceLocator.Resolve<IConverter<TSource, TDestination>>();
        }
    }
}
