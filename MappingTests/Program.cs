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
            
            //oneCollection = null;
            // oneDtoCollection = null;
            
            Console.WriteLine("Starting point");
            
            //Helpers.WriteLineDtoObjects(oneDtoCollection);
            //Helpers.WriteLineObjects(oneCollection);
            
            Console.WriteLine();
            Console.WriteLine("Mapping:");
            Console.WriteLine();

            // oneCollection = autoMapper.Map(oneDtoCollection, oneCollection);
            var a1 = autoMapper.Map(oneDtoCollection, oneCollection);
           var a2 = autoMapper.Map<IEnumerable<OneDto>, IEnumerable<One>>(oneDtoCollection);
           
           Helpers.WriteLineObjects(a1);

            //oneDtoCollection = CEMapper.XyzMappings.Map<One, OneDto>(oneCollection, oneDtoCollection.ToList(), (x, y) => x.Id == y.Id).ToList();
            var smt = CEMapper.XyzMappings.Map<OneDto, One>(oneDtoCollection, oneCollection, (x, y) => x.Id == y.Id);
            var smt2 = CEMapper.XyzMappings.Map<OneDto, One>(oneDtoCollection);
            
            Helpers.WriteLineObjects(smt);
           
            Console.WriteLine("After Mapping");

            // Helpers.WriteLineDtoObjects(oneDtoCollection);
            Helpers.WriteLineObjects(oneCollection);
           
            Console.WriteLine();
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}