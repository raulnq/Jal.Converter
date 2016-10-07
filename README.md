# Jal.Converter
Just another library to convert classes

## How to use?

### Default implementation

I only suggest to use this implementation on simple apps.

Create an instance of the locator

    var locator = ServiceLocator.Builder.Create as ServiceLocator;
    
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

Register your converter

    serviceLocator.Register(typeof(IConverter<CustomerRequest, Customer>), new CustomerRequestCustomerConverter());

Create a instance of the IModelConverter interface

    modelConverter = ModelConverter.Builder.UseLocator(serviceLocator).Create;
    
Use the IModelConverter interface

	var customerRequest = new CustomerRequest
	{
		Name = name,
		Age = age
	};
	var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);  

### Castle Windsor Integration

Note: The Jal.Locator.CastleWindsor and Jal.Finder library are needed.

Setup the Jal.Finder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;

	var finder = AssemblyFinder.Builder.UsePath(directory).Create;
	
	var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

Setup the Castle Windsor container

	var container = new WindsorContainer();

Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());

Install the Jal.Converter library, use the ConverterInstaller class included

	container.Install(new ConverterInstaller(assemblies));

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

	[assembly: AssemblyTag("Converter")]

Resolve a instance of the interface IModelConverter

	var modelConverter = container.Resolve<IModelConverter>();

Use the Converter class

	var customerRequest = new CustomerRequest
	{
		Name = name,
		Age = age
	};
    var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);
	
### LightInject Integration

Note: The Jal.Locator.LightInject and Jal.Finder library are needed.

Setup the Jal.Finder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;

	var finder = AssemblyFinder.Builder.UsePath(directory).Create;
	
	var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

Setup the LightInject container

	var container = new ServiceContainer();

Install the Jal.Locator.LightInject library

	container.RegisterFrom<ServiceLocatorCompositionRoot>();

Install the Jal.Converter library

	container.RegisterConverter(assemblies);

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

	[assembly: AssemblyTag("Converter")]

Resolve a instance of the interface IModelConverter

	var modelConverter = container.GetInstance<IModelConverter>();

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
