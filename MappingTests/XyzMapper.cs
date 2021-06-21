using System;

namespace MappingTests
{
    internal interface IXyzMapper : IBothWaysMapper<One, OneDto>{}
    
    internal sealed class XyzMapper : IXyzMapper
    {
        private XyzMapper(){}
        
        private static readonly Lazy<XyzMapper> _lazy = new Lazy<XyzMapper>(() => new XyzMapper());
        
        internal static XyzMapper Instance => _lazy.Value;
        
        public OneDto Map(One source, OneDto destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.City = source.City;
            
            Console.WriteLine("SourceID: " + source.Id);
            Console.WriteLine("DestID: " + destination.Id);
            Console.WriteLine("DestName: " + destination.Name);
            Console.WriteLine("SourceCity: " + destination.City);
            Console.WriteLine("---------------");

            return destination;
        }

        public One Map(OneDto source, One destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.City = source.City;
            
            Console.WriteLine("SourceDtoID: " + source.Id);
            Console.WriteLine("DestID: " + destination.Id);
            Console.WriteLine("DestName: " + destination.Name);
            Console.WriteLine("SourceCity: " + destination.City);
            Console.WriteLine("---------------");

            return destination;
        }
    }
}