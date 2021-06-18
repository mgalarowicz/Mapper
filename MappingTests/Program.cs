using System;
using System.Collections.Generic;
using System.Linq;

namespace MappingTests
{
    public static class Program
    {
        public static void Main()
        {
            var autoMapper = Helpers.InitializeMapper();

            IEnumerable<One> oneCollection = Helpers.InitializeDefaultList();

            IEnumerable<OneDto> oneDtoCollection = Helpers.InitializeDefaultDtoList();
            
            
            Console.WriteLine("Starting point");
            
            Helpers.WriteLineDtoObjects(oneDtoCollection);
            
            Console.WriteLine();
            Console.WriteLine("Mapping:");
            Console.WriteLine();

            //oneDtoCollection = autoMapper.Map(oneCollection, oneDtoCollection);

            oneDtoCollection = CEMapper.XyzMappings.Map<One, OneDto>(oneCollection, oneDtoCollection.ToList(), (x, y) => x.Id == y.Id).ToList();
           
            Console.WriteLine("After Mapping");

            Helpers.WriteLineDtoObjects(oneDtoCollection);
           
            Console.WriteLine();
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}