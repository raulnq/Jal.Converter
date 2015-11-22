# Jal.Converter
Just another library to convert classes

## How to use?

Note: The Jal.Locator.CastleWindsor and Jal.AssemblyFinder library are needed.

Setup the Jal.AssemblyFinder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;
	AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(directory);
	
Setup the Castle Windsor container

	var container = new WindsorContainer();

Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());

Install the Jal.Factory library, use the ConverterInstaller class included

	container.Install(new ConverterInstaller());

Create your converter class

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
	
Tag the assembly container of the converter classes in order to be read by the library

	[assembly: AssemblyTag("ConverterSource")]

Resolve a instance of the interface IModelConverter

	var modelConverter = container.Resolve<IModelConverter>();

Use the Converter class

	var customerRequest = new CustomerRequest
	{
		Name = name,
		Age = age
	};
    var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);
	
## AutoMapper Integration

Note: The Jal.Converter.AutoMapper library is needed.

Setup AutoMapper library instead of create your own converter

	Mapper.Initialize(a =>
	{
		a.CreateMap<CustomerRequest, Customer>();
	});
