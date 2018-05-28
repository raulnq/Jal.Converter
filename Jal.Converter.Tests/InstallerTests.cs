using System;
using Castle.Windsor;
using Jal.Converter.Impl;
using Jal.Converter.Installer;
using Jal.Converter.Interface;
using Jal.Finder.Impl;
using Jal.Locator.CastleWindsor.Installer;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests
{
    [TestFixture]
    public class ConverterInstallerTests
    {
        [Test]
        public void Install_With_ShouldNotBeNull()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            var assemblies = finder.GetAssemblies(x => x.FullName.Contains("Jal.Converter.Tests"));

            container.Install(new ConverterInstaller(assemblies));

            var modelConverter = container.Resolve<IModelConverter>();

            modelConverter.Factory.ShouldNotBeNull();

            modelConverter.Factory.ShouldBeOfType<ConverterFactory>();
        }
    }

}

