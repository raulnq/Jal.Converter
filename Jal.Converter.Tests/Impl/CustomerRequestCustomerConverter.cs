using Jal.Converter.Impl;
using Jal.Converter.Tests.Model;

namespace Jal.Converter.Tests.Impl
{
    public class CustomerRequestCustomerConverter : AbstractConverter<CustomerRequest, Customer>
    {
        public override Customer Convert(CustomerRequest source)
        {
            return new Customer()
                   {
                       Name = source.Name,
                       Age = source.Age,
                       Category = "None"
                   };
        }
    }
}
