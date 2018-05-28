using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Jal.Converter.Installer;
using Jal.Converter.Interface;
using Jal.Converter.Tests.Impl;
using Jal.Converter.Tests.Model;
using Jal.Finder.Impl;
using Jal.Locator.CastleWindsor.Installer;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests.CastleWindsor
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomerA_ShouldBe(string name, int age)
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            var assemblies = finder.GetAssemblies(x => x.FullName.Contains("Jal.Converter.Tests"));

            container.Install(new ConverterInstaller(assemblies));

            var sut = container.Resolve<IModelConverter>();

            var customerRequest = new CustomerRequest
            {
                Name = name,
                Age = age
            };
            var customer = sut.Convert<CustomerRequest, Customer>(customerRequest);

            customer.Name.ShouldBe(customerRequest.Name);

            customer.Age.ShouldBe(customerRequest.Age);

            customer.Category.ShouldBe("None");
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomerB_ShouldBe(string name, int age)
        {
            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new ConverterInstaller());

            container.Register(Component.For<IConverter<CustomerRequest, Customer>>().ImplementedBy<CustomerRequestCustomerConverter>());

            var sut = container.Resolve<IModelConverter>();

            var customerRequest = new CustomerRequest
            {
                Name = name,
                Age = age
            };
            var customer = sut.Convert<CustomerRequest, Customer>(customerRequest);

            customer.Name.ShouldBe(customerRequest.Name);

            customer.Age.ShouldBe(customerRequest.Age);

            customer.Category.ShouldBe("None");
        }
    }
}

