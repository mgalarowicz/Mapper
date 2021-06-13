using System;
using System.Collections.Concurrent;

namespace Mapper
{
    public class SingletonMapper : IMapper
    {
        private static SingletonMapper instance;
        private ConcurrentDictionary<(Type, Type), Func<object, object, object>> _mappings;
        
        private SingletonMapper() 
        {
            _mappings = InitializeMappings();
        }

        public static SingletonMapper GetInstance() 
        {   
            if (instance == null)
                instance = new SingletonMapper();

            return instance;
        }

        private ConcurrentDictionary<(Type, Type), Func<object, object, object>> InitializeMappings()
        {
            var mappings = new ConcurrentDictionary<(Type, Type), Func<object, object, object>>();
                mappings.Register<SomeOneDto, SomeOne>(MapToSomeOne);

            return mappings;
        }

        public TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest : class
        {
            return _mappings.TryGetValue((typeof(TSource), typeof(TDest)), out var mapping)
                ? (TDest)mapping(source, destination)
                : throw new Exception("Bla bla bla no mapper found");
        }

        private SomeOne MapToSomeOne(SomeOneDto src, SomeOne dest)
        {
            throw new NotImplementedException();
        }

    }
}
