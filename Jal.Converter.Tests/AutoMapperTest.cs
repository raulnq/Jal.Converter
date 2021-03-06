﻿using System.Collections.Generic;
using System.Data;
using AutoMapper;
using Jal.Converter.AutoMapper;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.Tests.Model;
using Jal.Locator.Impl;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests
{
    [TestFixture]
    public class AutoMapperTest
    {
        private IModelConverter _modelConverter;

        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper.Initialize(a =>
            {
                a.CreateMap<CustomerRequest, Customer>();
                a.CreateMap<IDataReader, Customer>();
            });

            var servicelocator = new ServiceLocator();

            servicelocator.Register(typeof(IConverter<CustomerRequest, Customer>), new AutoMapperConverter<CustomerRequest, Customer>());

            servicelocator.Register(typeof(IConverter<IDataReader, Customer>), new AutoMapperConverter<IDataReader, Customer>());

            servicelocator.Register(typeof(IConverter<IDataReader, IList<Customer>>), new AutoMapperConverter<IDataReader, IList<Customer>>());

            servicelocator.Register(typeof(IConverter<IDataReader, List<Customer>>), new AutoMapperConverter<IDataReader, List<Customer>>());

            servicelocator.Register(typeof(IConverter<IDataReader, IEnumerable<Customer>>), new AutoMapperConverter<IDataReader, IEnumerable<Customer>>());

            servicelocator.Register(typeof(IConverter<IDataReader, ICollection<Customer>>), new AutoMapperConverter<IDataReader, ICollection<Customer>>());

            _modelConverter = ModelConverter.Create(servicelocator);
        }

        [Test]
        public void Convert_With_ShouldBe()
        {
            var request = new CustomerRequest
                          {
                              Age = 18,
                              Name = "Name"
                          };
            var customer = _modelConverter.Convert<CustomerRequest, Customer>(request);

            customer.Age.ShouldBe(request.Age);

            customer.Name.ShouldBe(request.Name);
            
        }

        [Test]
        public void Convert_WithDestination_ShouldBe()
        {
            var request = new CustomerRequest
            {
                Age = 18,
                Name = "Name"
            };
            var customer = new Customer
                           {
                               Category = "Category"
                           };
            var sameCustomer = _modelConverter.Convert(request, customer);

            sameCustomer.Age.ShouldBe(request.Age);

            sameCustomer.Name.ShouldBe(request.Name);

            sameCustomer.Category.ShouldBe("Category");
            
        }

        [Test]
        public void Convert_WithDestinationAndDynamic_ShouldBe()
        {
            var request = new CustomerRequest
            {
                Age = 18,
                Name = "Name"
            };
            var customer = new Customer
            {
                Category = "Category"
            };
            var sameCustomer = _modelConverter.Convert(request, customer, new {});

            sameCustomer.Age.ShouldBe(request.Age);

            sameCustomer.Name.ShouldBe(request.Name);

            sameCustomer.Category.ShouldBe("Category");

        }

        [Test]
        public void Convert_WithDynamic_ShouldBe()
        {
            var request = new CustomerRequest
            {
                Age = 18,
                Name = "Name"
            };

            var customer = _modelConverter.Convert<CustomerRequest, Customer>(request, new { });

            customer.Age.ShouldBe(request.Age);

            customer.Name.ShouldBe(request.Name);

        }
    }
}
