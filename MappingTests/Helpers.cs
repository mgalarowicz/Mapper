using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace MappingTests
{
    public static class Helpers
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<One, OneDto>()
                    .EqualityComparison((r1, r2) => r1.Id == r2.Id);
            });
           
            return config.CreateMapper();
        }

        public static IEnumerable<OneDto> InitializeDefaultDtoList()    //DESTINATION
        {
            return new List<OneDto>
            {
                new OneDto {Id = 1, Name = "Name100", City = "City100"},
                new OneDto {Id = 3, Name = "Name300", City = "City300"},
                new OneDto {Id = 3, Name = "Name600", City = "City600"},
                new OneDto {Id = 5, Name = "Name500", City = "City500"},
            };
        }
        
        public static IEnumerable<One> InitializeDefaultList()   //SOURCE
        {
            return new List<One>
            {
                new One {Id = 1, Name = "Name1", City = "City1", Country = "Country1"},
                new One {Id = 2, Name = "Name2", City = "City2", Country = "Country2"},
                new One {Id = 3, Name = "Name3", City = "City3", Country = "Country3"},
                new One {Id = 1, Name = "Name4", City = "City4", Country = "Country4"},
            };
        }

        public static void WriteLineDtoObjects(IEnumerable<OneDto> dtos)
        {
            foreach (var dto in dtos)
            {
                Console.WriteLine("DestID: " + dto.Id);
                Console.WriteLine("DestName: " + dto.Name);
                Console.WriteLine("DestCity: " + dto.City);
                Console.WriteLine();
            }
        }
    }
}