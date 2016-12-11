# Jal.Converter [![Build status](https://ci.appveyor.com/api/projects/status/c63jmwrdr2iussdm?svg=true)](https://ci.appveyor.com/project/raulnq/jal-converter) [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.svg)](https://www.nuget.org/packages/Jal.Converter) [![Coverage Status](https://coveralls.io/repos/github/raulnq/Jal.Converter/badge.svg?branch=master)](https://coveralls.io/github/raulnq/Jal.Converter?branch=master)
Just another library to convert classes

## How to use?

### Default implementation

I only suggest to use this implementation on simple apps.

Create an instance of the locator
```c++
var locator = ServiceLocator.Builder.Create as ServiceLocator;
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
modelConverter = ModelConverter.Builder.UseLocator(serviceLocator).Create;
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
### Castle Windsor Integration [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.Installer.svg)](https://www.nuget.org/packages/Jal.Converter.Installer)

Note: The [Jal.Locator.CastleWindsor](https://www.nuget.org/packages/Jal.Locator.CastleWindsor/) and [Jal.Finder library](https://www.nuget.org/packages/Jal.Finder/) are needed.

Setup the Jal.Finder library
```c++
var directory = AppDomain.CurrentDomain.BaseDirectory;

var finder = AssemblyFinder.Builder.UsePath(directory).Create;
	
var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();
```
Setup the Castle Windsor container
```c++
var container = new WindsorContainer();
```
Install the Jal.Locator.CastleWindsor library
```c++
container.Install(new ServiceLocatorInstaller());
```
Install the Jal.Converter library, use the ConverterInstaller class included
```c++
container.Install(new ConverterInstaller(assemblies));
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
Tag the assembly container of the converter classes in order to be read by the library
```c++
[assembly: AssemblyTag()]
```
Resolve a instance of the interface IModelConverter
```c++
var modelConverter = container.Resolve<IModelConverter>();
```
Use the Converter class
```c++
var customerRequest = new CustomerRequest
{
	Name = name,
	Age = age
};
var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);
```
### LightInject Integration [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.LightInject.Installer.svg)](https://www.nuget.org/packages/Jal.Converter.LightInject.Installer)

Note: The [Jal.Locator.LightInject](https://www.nuget.org/packages/Jal.Locator.LightInject/) and [Jal.Finder](https://www.nuget.org/packages/Jal.Finder/) library are needed. 

Setup the Jal.Finder library
```c++
var directory = AppDomain.CurrentDomain.BaseDirectory;

var finder = AssemblyFinder.Builder.UsePath(directory).Create;
	
var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();
```
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
container.RegisterConverter(assemblies);
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
Tag the assembly container of the converter classes in order to be read by the library
```c++
[assembly: AssemblyTag()]
```
Resolve a instance of the interface IModelConverter
```c++
var modelConverter = container.GetInstance<IModelConverter>();
```
Use the Converter class
```c++
var customerRequest = new CustomerRequest
{
	Name = name,
	Age = age
};
var customer = modelConverter.Convert<CustomerRequest, Customer>(customerRequest);
```
## AutoMapper Integration [![NuGet](https://img.shields.io/nuget/v/Jal.Converter.AutoMapper.svg)](https://www.nuget.org/packages/Jal.Converter.AutoMapper)

Note: [The Jal.Converter.AutoMapper](https://www.nuget.org/packages/Jal.Converter.AutoMapper/) library is needed.

Setup AutoMapper library instead of create your own converter
```c++
Mapper.Initialize(a =>
{
	a.CreateMap<CustomerRequest, Customer>();
});
```