using System;
using Jal.Converter.Extension;
using Jal.Converter.Model;
using Jal.Converter.Tests.Impl;
using Jal.Converter.Tests.Model;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests
{
    public class ConverterExtensionTests
    {
        [Test]
        public void ShouldMatch_With_ShouldNotThrowException()
        {
            var converter = new CustomerRequestCustomerConverter();

            var request = new CustomerRequest {Age = 15};

            converter.ShouldMatch(request, new Func<Customer, bool>[] {x => x.Age == request.Age});
        }

        [Test]
        public void ShouldMatch_With_ShouldThrowException()
        {
            var converter = new CustomerRequestCustomerConverter();

            var request = new CustomerRequest { Age = 15 };

            Should.Throw<ConverterException>( ()=>converter.ShouldMatch(request, new Func<Customer, bool>[] { x => x.Age == 0 }));
        }

        [Test]
        public void ShouldMatch_WithDestination_ShouldNotThrowException()
        {
            var converter = new CustomerRequestCustomerConverter();

            var request = new CustomerRequest { Age = 15 };

            converter.ShouldMatch(request, new Customer(), new Func<Customer, bool>[] { x => x.Age == request.Age });
        }

        [Test]
        public void ShouldMatch_WithDestination_ShouldThrowException()
        {
            var converter = new CustomerRequestCustomerConverter();

            var request = new CustomerRequest { Age = 15 };

            Should.Throw<ConverterException>(() => converter.ShouldMatch(request, new Customer(), new Func<Customer, bool>[] { x => x.Age == 0 }));
        }

        [Test]
        public void ShouldMatch_WithDestinationAndDynamic_ShouldNotThrowException()
        {
            var converter = new CustomerRequestCustomerConverter();

            var request = new CustomerRequest { Age = 15 };

            converter.ShouldMatch(request, new Customer(), new {}, new Func<Customer, bool>[] { x => x.Age == request.Age });
        }

        [Test]
        public void ShouldMatch_WithDestinationAndDynamic_ShouldThrowException()
        {
            var converter = new CustomerRequestCustomerConverter();

            var request = new CustomerRequest { Age = 15 };

            Should.Throw<ConverterException>(() => converter.ShouldMatch(request, new Customer(), new {}, new Func<Customer, bool>[] { x => x.Age == 0 }));
        }
    }
}