using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mapper
{
    public class MapperNumberOne : IConcreteMapper
    {
        public TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest : class
        {
            return (source, destination) switch
            {
                (SomeOne src, SomeOneDto dest) => (TDest)(object)MapToSomeOne(src, dest),
                
                _ => throw new Exception("No mapping was found between the specified types")
            };
        }

        private SomeOneDto MapToSomeOne(SomeOne src, SomeOneDto dest)
        {
            throw new NotImplementedException();
        }
    }
}
