using System;
using Jal.Converter.Fluent.Impl;
using Jal.Converter.Fluent.Interface;
using Jal.Converter.Impl;
using Jal.Converter.Interface;
using Jal.Locator.Interface;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Jal.Converter.Tests
{
    public class ModelConverterFluentBuilderTests
    {
        [Test]
        public void UseLocator_WithNotNull_ShouldNotBeNull()
        {
            var sut = new ModelConverterFluentBuilder();

            var locator = new Mock<IServiceLocator>();

            var chain = sut.UseLocator(locator.Object);

            sut.ConverterFactory.ShouldNotBeNull();

            sut.ConverterFactory.ShouldBeOfType<ConverterFactory>();

            chain.ShouldNotBeNull();

            chain.ShouldBeAssignableTo<IModelConverterFluentBuilder>();
        }

        [Test]
        public void UseLocator_WithNull_ShouldThrowException()
        {
            var sut = new ModelConverterFluentBuilder();

            Should.Throw<ArgumentNullException>(() => { var chain = sut.UseLocator(null); });
        }

        [Test]
        public void UseInterceptor_WithNotNull_ShouldNotBeNull()
        {
            var sut = new ModelConverterFluentBuilder();

            var interceptor = new Mock<IModelConverterInterceptor>();

            var chain = sut.UseInterceptor(interceptor.Object);

            sut.ModelConverterInterceptor.ShouldNotBeNull();

            chain.ShouldNotBeNull();

            chain.ShouldBeAssignableTo<IModelConverterEndFluentBuilder>();
        }

        [Test]
        public void UseInterceptor_WithNull_ShouldThrowException()
        {
            var sut = new ModelConverterFluentBuilder();

            Should.Throw<ArgumentNullException>(() => { var chain = sut.UseInterceptor(null); });
        }

        [Test]
        public void Create_WithLocator_ShouldNotBeNull()
        {
            var sut = new ModelConverterFluentBuilder();

            var locator = new Mock<IServiceLocator>();

            var instance = sut.UseLocator(locator.Object).Create;

            instance.ShouldNotBeNull();

            instance.ShouldBeOfType<ModelConverter>();
        }

        [Test]
        public void Create_WithLocatorAndInterceptor_ShouldNotBeNull()
        {
            var sut = new ModelConverterFluentBuilder();

            var locator = new Mock<IServiceLocator>();

            var interceptor = new Mock<IModelConverterInterceptor>();

            var instance = sut.UseLocator(locator.Object).UseInterceptor(interceptor.Object).Create;

            instance.ShouldNotBeNull();

            instance.ShouldBeOfType<ModelConverter>();
        }
    }
}