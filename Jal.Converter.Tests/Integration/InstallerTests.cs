using System;
using Castle.Windsor;
using Cignium.Framework.Infrastructure.Converter.Extension;
using Jal.Converter.Impl;
using Jal.Converter.Installer;
using Jal.Converter.Interface;
using Jal.Converter.Tests.Impl;
using Jal.Converter.Tests.Model;
using Jal.Locator.CastleWindsor.Installer;
using Jal.Locator.Impl;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests.Integration
{
    [TestFixture]
    public class InstallerTests
    {
        private IModelConverter _modelConverter;

        [SetUp]
        public void SetUp()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(directory);

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new ConverterInstaller());

            _modelConverter = container.Resolve<IModelConverter>();
        }

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void Convert_CustomerRequestToCustomer_Successfully(string name, int age)
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

        [Test]
        [TestCase("Name", 19)]
        [TestCase("A", 10000)]
        [TestCase("_", 999)]
        public void ShouldMatch_CustomerRequestToCustomer_Successfully(string name, int age)
        {
            var customerRequest = new CustomerRequest
            {
                Name = name,
                Age = age
            };
            new CustomerRequestCustomerConverter().ShouldMatch(customerRequest, new Func<Customer, bool> []
                                                                                {
                                                                                    x => x.Name == customerRequest.Name,
                                                                                    x => x.Age == customerRequest.Age,
                                                                                    x => x.Category == "None"
                                                                                });
        }
    }
}

