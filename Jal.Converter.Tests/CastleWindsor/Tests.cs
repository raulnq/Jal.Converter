using System;
using Castle.Windsor;
using Jal.Converter.Installer;
using Jal.Converter.Interface;
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
        private IModelConverter _modelConverter;

        [SetUp]
        public void SetUp()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            var assemblies = finder.GetAssemblies(x => x.FullName.Contains("Jal.Converter.Tests"));

            container.Install(new ConverterInstaller(assemblies));

            _modelConverter = container.Resolve<IModelConverter>();
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_WithCustomerRequestToCustomer_ShouldBe(string name, int age)
        {
            var customerRequest = new CustomerRequest
            {
                Name = name,
                Age = age
            };
            var customer = _modelConverter.Convert<CustomerRequest, Customer>(customerRequest);

            customer.Name.ShouldBe(customerRequest.Name);

            customer.Age.ShouldBe(customerRequest.Age);

            customer.Category.ShouldBe("None");
        }
    }
}

