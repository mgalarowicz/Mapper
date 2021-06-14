using System;
using System.Collections.Concurrent;

namespace Mapper
{
    public sealed class SingletonMapper : IMapper
    {
        private static readonly Lazy<SingletonMapper> lazy = new Lazy<SingletonMapper>(() => new SingletonMapper());

        private static readonly ConcurrentDictionary<(Type, Type), Func<object, object, object>> _mappings = new ConcurrentDictionary<(Type, Type), Func<object, object, object>>();
        
        private SingletonMapper() 
        {
            InitializeMappings(_mappings);
        }

        private ConcurrentDictionary<(Type, Type), Func<object, object, object>> InitializeMappings(ConcurrentDictionary<(Type, Type), Func<object, object, object>> mappings)
        {
            mappings.Register<SomeOneDto, SomeOne>(MapToSomeOne);

            return mappings;
        }

        public static SingletonMapper Instance => lazy.Value;

        public TDest Map<TSource, TDest>(TSource source, TDest destination = null) where TDest : class
        {
            return _mappings.TryGetValue((typeof(TSource), typeof(TDest)), out var mapping)
                ? (TDest)mapping(source, destination)
                : throw new Exception("Bla bla bla no mapper found");
        }

        private SomeOne MapToSomeOne(SomeOneDto src, SomeOne dest)
        {
            return new SomeOne { Id = 10 };
        }
    }



    //Other thread-safe singleton solution
    public sealed class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton()
        {
        }

        private Singleton()
        {
        }

        public static Singleton Instance => instance;
    }
}
