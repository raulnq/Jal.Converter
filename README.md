# Jal.Converter [![Build status](https://ci.appveyor.com/api/projects/status/c63jmwrdr2iussdm?svg=true)](https://ci.appveyor.com/project/raulnq/jal-converter) [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.svg)](https://www.nuget.org/packages/Jal.Converter) [![Coverage Status](https://coveralls.io/repos/github/raulnq/Jal.Converter/badge.svg?branch=master)](https://coveralls.io/github/raulnq/Jal.Converter?branch=master) [![BCH compliance](https://bettercodehub.com/edge/badge/raulnq/Jal.Converter?branch=master)](https://bettercodehub.com/)
Just another library to convert classes

## How to use?

### Default service locator implementation

Create an instance of the locator
```c++
var locator = new ServiceLocator();
```   
Create your converter class
```c++
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
```
Register your converter
```c++
serviceLocator.Register(typeof(IConverter<CustomerRequest, Customer>), new CustomerRequestCustomerConverter());
```
Create a instance of the IModelConverter interface
```c++
modelConverter = ModelConverter.Create(servicelocator);
```    
Use the IModelConverter interface
```c++
var customerRequest = new CustomerRequest
{
	Name = name,
	Age = age
};

var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);  
```
### Castle Windsor as service locator implementation [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.Installer.svg)](https://www.nuget.org/packages/Jal.Converter.Installer)

Note: The [Jal.Locator.CastleWindsor](https://www.nuget.org/packages/Jal.Locator.CastleWindsor/) library is needed.

Setup the IoC container
```c++
var container = new WindsorContainer();
```
Install the Jal.Locator.CastleWindsor library
```c++
container.Install(new ServiceLocatorInstaller());
```
Install the Jal.Converter library
```c++
container.Install(new ConverterInstaller());
```
Create your converter class
```c++
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
```	
Register your converter class in the IoC container
```c++
container.Register(Component.For<IConverter<CustomerRequest, Customer>>().ImplementedBy<CustomerRequestCustomerConverter>());
```
Resolve and use an instance of IModelConverter
```c++
var modelConverter = container.Resolve<IModelConverter>();

var customerRequest = new CustomerRequest
{
	Name = name,
	Age = age
};

var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);
```
### LightInject as service locator implementation [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.LightInject.Installer.svg)](https://www.nuget.org/packages/Jal.Converter.LightInject.Installer)

Note: The [Jal.Locator.LightInject](https://www.nuget.org/packages/Jal.Locator.LightInject/) library is needed. 

Setup the LightInject container
```c++
var container = new ServiceContainer();
```
Install the Jal.Locator.LightInject library
```c++
container.RegisterFrom<ServiceLocatorCompositionRoot>();
```
Install the Jal.Converter library
```c++
container.RegisterConverter();
```
Create your converter class
```c++
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
```	
Register your converter class in the IoC container
```c++
container.Register<IConverter<CustomerRequest, Customer>, CustomerRequestCustomerConverter>();
```
Resolve and use an instance of IModelConverter
```c++
var modelConverter = container.GetInstance<IModelConverter>();

var customerRequest = new CustomerRequest
{
	Name = name,
	Age = age
};

var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);
```
## AutoMapper as converter implementation [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.AutoMapper.svg)](https://www.nuget.org/packages/Jal.Converter.AutoMapper)

Note: [The Jal.Converter.AutoMapper](https://www.nuget.org/packages/Jal.Converter.AutoMapper/) library is needed.

Setup AutoMapper library instead of create your own converter
```c++
Mapper.Initialize(a =>
{
	a.CreateMap<CustomerRequest, Customer>();
});
```