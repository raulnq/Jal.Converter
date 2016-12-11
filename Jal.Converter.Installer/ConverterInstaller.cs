using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.Converter.Impl;
using Jal.Converter.Interface;

namespace Jal.Converter.Installer
{
    public class ConverterInstaller : IWindsorInstaller
    {
        private readonly Assembly[] _converterSourceAssemblies;

        public ConverterInstaller(Assembly[] converterSourceAssemblies)
        {
            _converterSourceAssemblies = converterSourceAssemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_converterSourceAssemblies != null)
            {
                foreach (var assemblyDescriptor in _converterSourceAssemblies.Select(Classes.FromAssembly))
                {
                    container.Register(assemblyDescriptor.BasedOn(typeof(IConverter<,>)).WithServiceAllInterfaces());
                }
            }
            
            container.Register(
                Component.For<IConverterFactory>().ImplementedBy<ConverterFactory>().LifestyleSingleton(),
                Component.For<IModelConverter>().ImplementedBy<ModelConverter>().LifestyleSingleton()
                );
        }
    }
}