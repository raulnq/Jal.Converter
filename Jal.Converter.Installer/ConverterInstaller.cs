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
        private readonly Assembly[] _assemblies;

        public ConverterInstaller(Assembly[] assemblies = null)
        {
            _assemblies = assemblies;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_assemblies != null)
            {
                foreach (var assemblyDescriptor in _assemblies.Select(Classes.FromAssembly))
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