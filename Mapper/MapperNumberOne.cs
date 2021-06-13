using System;

namespace Mapper
{
    internal class MapperNumberOne : IMapper
    {
        public TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest : class
        {
            return (source, destination) switch
            {
                (SomeOne src, SomeOneDto dest) => (TDest)(object)MapToSomeOneDto(src, dest),
                
                _ => throw new Exception("No mapping was found between the specified types")
            };
        }

        private SomeOneDto MapToSomeOneDto(SomeOne src, SomeOneDto dest)
        {
            throw new NotImplementedException();
        }

        //private IImmutableDictionary<(Type, Type), Func<object, object, object>> InitializeMappings()
        //{
        //    var mappings = new Dictionary<(Type, Type), Func<object, object, object>>
        //    {
        //        { (typeof(SomeOne), typeof(SomeOneDto)), (x,y) => (SomeOneDto)(object)MapToSomeOne((SomeOne)x, (SomeOneDto)y)}
        //    };

        //    return mappings.ToImmutableDictionary();
        //}
    }
}
