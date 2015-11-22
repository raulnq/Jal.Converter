using System;

namespace Jal.Converter.Model
{
    public class ConverterException : Exception
    {
        public ConverterException()
        {
        }   

        public ConverterException(string message)
            : base(message)
        {
        }

        public ConverterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
