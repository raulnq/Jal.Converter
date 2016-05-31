using System;
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
        private readonly Func<Assembly[]> _converterProvider;

        public ConverterInstaller(Func<Assembly[]> converterProvider)
        {
            _converterProvider = converterProvider;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblies = _converterProvider();

            if (assemblies != null)
            {
                foreach (var assembly in assemblies)
                {
                    var assemblyDescriptor = Classes.FromAssembly(assembly);
                    //registering all the converters inside the assembly marked with the attribute
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