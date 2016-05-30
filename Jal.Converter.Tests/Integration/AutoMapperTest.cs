using System.Collections.Generic;
using System.Data;
using AutoMapper;
using AutoMapper.Data;
using AutoMapper.Mappers;
using Jal.Converter.AutoMapper;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Converter.Tests.Model;
using Jal.Locator.Impl;
using NUnit.Framework;

namespace Jal.Converter.Tests.Integration
{
    [TestFixture]
    public class AutoMapperTest
    {
        private IModelConverter _modelConverter;

        [SetUp]
        public void SetUp()
        {
            Mapper.Initialize(a =>
            {
                MapperRegistry.Mappers.Add(new DataReaderMapper { YieldReturnEnabled = true });
                a.CreateMap<CustomerRequest, Customer>();
                a.CreateMap<IDataReader, Customer>();
            });

            var servicelocator = ServiceLocator.Builder.Create as ServiceLocator;

            servicelocator.Register(typeof(IConverter<CustomerRequest, Customer>), new AutoMapperConverter<CustomerRequest, Customer>());

            servicelocator.Register(typeof(IConverter<IDataReader, Customer>), new AutoMapperConverter<IDataReader, Customer>());

            servicelocator.Register(typeof(IConverter<IDataReader, IList<Customer>>), new AutoMapperConverter<IDataReader, IList<Customer>>());

            servicelocator.Register(typeof(IConverter<IDataReader, List<Customer>>), new AutoMapperConverter<IDataReader, List<Customer>>());

            servicelocator.Register(typeof(IConverter<IDataReader, IEnumerable<Customer>>), new AutoMapperConverter<IDataReader, IEnumerable<Customer>>());

            _modelConverter = ModelConverter.Builder.UseServiceLocator(servicelocator).Create;
        }

        [Test]
        public void TestSuccessConvert()
        {
            var request = new CustomerRequest
                          {
                              Age = 18,
                              Name = "Name"
                          };
            var customer = _modelConverter.Convert<CustomerRequest, Customer>(request);
            Assert.AreEqual(request.Age,customer.Age);
            Assert.AreEqual(request.Name, customer.Name);
        }

        [Test]
        public void TestSuccessConvertWithPreviousObject()
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
            Assert.AreEqual(request.Age, sameCustomer.Age);
            Assert.AreEqual(request.Name, sameCustomer.Name);
            Assert.AreEqual("Category", sameCustomer.Category);
        }

        [Test]
        public void TestSuccessDataTableToObjectConverter()
        {


            var dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("Age", typeof(int)));
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            var row = dt.NewRow();
            row["Name"] = "Name";
            row["Category"] = "Category";
            row["Age"] = 15;
            dt.Rows.Add(row);

            var customer = _modelConverter.Convert<IDataReader, Customer>(dt.CreateDataReader());
            Assert.AreEqual(15, customer.Age);
            Assert.AreEqual("Name", customer.Name);
            Assert.AreEqual("Category", customer.Category);
        }

        [Test]
        public void TestSuccessDataTableToIListConverter()
        {
            var dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("Age", typeof(int)));
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            var row = dt.NewRow();
            row["Name"] = "Name";
            row["Category"] = "Category";
            row["Age"] = 15;
            dt.Rows.Add(row);

            var customers = _modelConverter.Convert<IDataReader, IList<Customer>>(dt.CreateDataReader());
            Assert.AreEqual(15, customers[0].Age);
            Assert.AreEqual("Name", customers[0].Name);
            Assert.AreEqual("Category", customers[0].Category);
        }

        [Test]
        public void TestSuccessDataTableToListConverter()
        {
            var dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("Age", typeof(int)));
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            var row = dt.NewRow();
            row["Name"] = "Name";
            row["Category"] = "Category";
            row["Age"] = 15;
            dt.Rows.Add(row);

            var customers = _modelConverter.Convert<IDataReader, List<Customer>>(dt.CreateDataReader());
            Assert.AreEqual(15, customers[0].Age);
            Assert.AreEqual("Name", customers[0].Name);
            Assert.AreEqual("Category", customers[0].Category);
        }

        [Test]
        public void TestSuccessDataTableToEnumerableConverter()
        {
            var dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(new DataColumn("Age", typeof(int)));
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            var row = dt.NewRow();
            row["Name"] = "Name";
            row["Category"] = "Category";
            row["Age"] = 15;
            dt.Rows.Add(row);

            var customers = _modelConverter.Convert<IDataReader, IEnumerable<Customer>>(dt.CreateDataReader());
        }
    }
}
