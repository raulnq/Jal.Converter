using System;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.LightInject.Installer;
using Jal.Finder.Atrribute;
using Jal.Finder.Impl;
using Jal.Locator.LightInject.Installer;
using LightInject;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests
{
    [TestFixture]
    public class ServiceContainerExtensionTests
    {
        [Test]
        public void RegisterConverter_WithCompositionRoot_ShouldBeNotNull()
        {
            var container = new ServiceContainer();

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            container.RegisterConverter(assemblies);

            var instance = container.GetInstance<IModelConverter>();

            instance.ShouldNotBeNull();

            instance.Factory.ShouldNotBeNull();

            instance.ShouldBeAssignableTo<IModelConverter>();

            instance.ShouldBeOfType<ModelConverter>();
        }
    }
}