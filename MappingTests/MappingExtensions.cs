using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MappingTests
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Extension method for IOneWayMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Collection of objects to map from</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New collection of destination objects with mapped values</returns>
        public static IEnumerable<TDest> Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper, [NotNull] IEnumerable<TSrc> source)
            where TDest : class, new()
            where TSrc : class
            => source.Select(src => mapper.Map(src, new TDest()));

        
        
        public static IEnumerable<TDest> Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper,
            IEnumerable<TSrc> source, List<TDest> destination, Func<TSrc, TDest, bool> comparator)
            where TDest : class, new()
            where TSrc : class
        {
            Dictionary<TDest, TSrc> destToMap = new Dictionary<TDest, TSrc>();

            foreach (var src in source)
            {
                var destObj = destination.FirstOrDefault(dest => comparator(src, dest));

                if (destObj is null)
                    destination.Add(mapper.Map(src, new TDest()));
                else if (destToMap.TryAdd(destObj, src))
                    mapper.Map(src, destObj);
            }
            
            var destToRemove = destination.Except(destToMap.Keys).ToHashSet();

            destination.RemoveAll(d => destToRemove.Contains(d));

            return destination;
        }

        
        
        
        
        /// <summary>
        /// Extension method for IOneWayMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Object to map from</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New destination object with mapped values</returns>
        public static TDest Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper, [NotNull] TSrc source)
            where TDest : class, new()
            where TSrc : class
            => mapper.Map(source, new TDest());

        /// <summary>
        /// Extension method for IOneWayMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Object to map from</param>
        /// <param name="destination">Object to map to</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>Updated destination object with mapped values</returns>
        public static TDest Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper, [NotNull] TSrc source, [NotNull] TDest destination)
            where TDest : class
            where TSrc : class
        {
            ValidateMappingInputs(source, destination);
            return mapper.Map(source, destination);
        }


        /// <summary>
        /// Extension method for IBothWaysMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Collection of objects to map from</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New collection of destination objects with mapped values</returns>
        public static IEnumerable<TSrc> Map<TDest, TSrc>(this IBothWaysMapper<TSrc, TDest> mapper, [NotNull] IEnumerable<TDest> source)
            where TDest : class
            where TSrc : class, new()
            => source.Select(src => mapper.Map(src, new TSrc()));

        /// <summary>
        /// Extension method for IBothWaysMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Object to map from</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New destination object with mapped values</returns>
        public static TSrc Map<TDest, TSrc>(this IBothWaysMapper<TSrc, TDest> mapper, [NotNull] TDest source)
            where TDest : class
            where TSrc : class, new()
            => mapper.Map(source, new TSrc());

        /// <summary>
        /// Extension method for IBothWaysMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Object to map from</param>
        /// <param name="destination">Object to map to</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>Updated destination object with mapped values</returns>
        public static TSrc Map<TDest, TSrc>(this IBothWaysMapper<TSrc, TDest> mapper, [NotNull] TDest source, [NotNull] TSrc destination)
            where TDest : class
            where TSrc : class
        {
            ValidateMappingInputs(source, destination);
            return mapper.Map(source, destination);
        }
        
        private static void ValidateMappingInputs<TSrc, TDest>(TSrc sourceObj, TDest destinationObj)
            where TDest : class
            where TSrc : class
        {
            if (sourceObj is null)
                throw new ArgumentNullException(nameof(sourceObj));

            if (destinationObj is null)
                throw new ArgumentNullException(nameof(destinationObj));
        }
        
        private static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
                if (predicate(item))
                    yield return selector(item);
        }
    }
}