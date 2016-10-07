using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using LightInject;

namespace Jal.Converter.LightInject.Installer
{
    public static class ServiceContainerExtension
    {
        public static void RegisterConverter(this IServiceContainer container, Assembly[] assemblies)
        {
            container.Register<IConverterFactory, ConverterFactory>(new PerContainerLifetime());

            container.Register<IModelConverter, ModelConverter>(new PerContainerLifetime());

            if (assemblies != null)
            {
                foreach (var assembly in assemblies)
                {
                    foreach (var exportedType in assembly.ExportedTypes)
                    {
                        if (typeof (IConverter).IsAssignableFrom(exportedType))
                        {
                            var type1 = exportedType.BaseType.GetGenericArguments()[0];

                            var type2 = exportedType.BaseType.GetGenericArguments()[1];

                            var type = typeof (IConverter<,>);

                            Type[] typeArgs = {type1, type2};

                            var constructed = type.MakeGenericType(typeArgs);

                            container.Register(constructed, exportedType, exportedType.FullName, new PerContainerLifetime());
                        }
                    }
                }
            }
        }
    }
}
