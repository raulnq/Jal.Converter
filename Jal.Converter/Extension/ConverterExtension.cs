using System;
using System.Linq;
using Jal.Converter.Interface;
using Jal.Converter.Model;

namespace Jal.Converter.Extension
{
    public static class ConverterExtension
    {
        public static void ShouldMatch<TSource, TDestination>(this IConverter<TSource, TDestination> converter, TSource objectToConvert, Func<TDestination, bool>[] matches)
        {
            var destination = converter.Convert(objectToConvert);

            if (matches.Select(match => match(destination)).Any(result => !result))
            {
                throw new ConverterException();
            }
        }

        public static void ShouldMatch<TSource, TDestination>(this IConverter<TSource, TDestination> converter, TSource objectToConvert, dynamic context, Func<TDestination, bool>[] matches)
        {
            var destination = converter.Convert(objectToConvert, context);

            if (matches.Select(match => match(destination)).Any(result => !result))
            {
                throw new ConverterException();
            }
        }

        public static void ShouldMatch<TSource, TDestination>(this IConverter<TSource, TDestination> converter, TSource objectToConvert, TDestination objecDestination, dynamic context, Func<TDestination, bool>[] matches)
        {
            var destination = converter.Convert(objectToConvert, objecDestination, context);

            if (matches.Select(match => match(destination)).Any(result => !result))
            {
                throw new ConverterException();
            }
        }
    }
}
