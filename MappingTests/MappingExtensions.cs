using System;
using System.Collections.Generic;
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
        public static IEnumerable<TDest> Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper, IEnumerable<TSrc> source)
            where TDest : class, new()
            where TSrc : class 
            => source is null ? Enumerable.Empty<TDest>() : source.Select(src => mapper.Map<TSrc, TDest>(src, new TDest()));

        /// <summary>
        /// Extension method for IOneWayMapper. Used to specify explicitly types of mapping inputs.
        /// Current mapping behavior for each element in mapping input collections:
        ///     - If comparator expression returns true, mapper will map source object to destination object
        ///     - If source object exists and destination object doesn't, mapper will add a new destination object
        ///         mapped from source object to the destination collection
        ///     - If destination object exists and source object doesn't, mapper will remove destination object
        ///         from destination collection
        ///     - If there are any duplicates in destination collection, mapper will map to the first one
        ///         and remove the rest
        ///     - If there are any duplicates in source collection, mapper will map the first one and ignore the the rest
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Collection of objects to map from</param>
        /// <param name="destination">Existing collection of objects to map to</param>
        /// <param name="comparator">Mapping expression used to identify which objects from destination collection
        /// should be updated with which elements from source collection</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>Updated collection of destination objects with mapped values</returns>
        public static IEnumerable<TDest> Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper,
            IEnumerable<TSrc> source, IEnumerable<TDest> destination, Func<TSrc, TDest, bool> comparator)
            where TDest : class, new()
            where TSrc : class
        {
            var destinationForUpdate = destination is null ? new List<TDest>() : destination.ToList();

            if (source is null)
                return destinationForUpdate;
            
            var destToMap = new Dictionary<TDest, TSrc>();
            var destToAdd = new HashSet<TDest>();

            foreach (var src in source)
            {
                var destObj = destinationForUpdate.FirstOrDefault(dest => comparator(src, dest));

                if (destObj is null)
                    destToAdd.Add(mapper.Map<TSrc, TDest>(src, new TDest()));
                else if (destToMap.TryAdd(destObj, src))
                    mapper.Map<TSrc, TDest>(src, destObj);
            }
            
            var destToRemove = destinationForUpdate.Except(destToMap.Keys).ToHashSet();

            destinationForUpdate.RemoveAll(d => destToRemove.Contains(d));
            
            destinationForUpdate.AddRange(destToAdd);

            return destinationForUpdate;
        }
        
        /// <summary>
        /// Extension method for IOneWayMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Object to map from</param>
        /// <param name="destination">Object to map to</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New or updated destination object with mapped values</returns>
        public static TDest Map<TSrc, TDest>(this IOneWayMapper<TSrc, TDest> mapper, TSrc source, TDest destination = null)
            where TDest : class, new()
            where TSrc : class
        {
            var destToReturn = destination ?? new TDest();
            
            return source is null ? destToReturn : mapper.Map(source, destToReturn);
        }


        /// <summary>
        /// Extension method for IBothWaysMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Collection of objects to map from</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New collection of destination objects with mapped values</returns>
        public static IEnumerable<TSrc> Map<TDest, TSrc>(this IBothWaysMapper<TSrc, TDest> mapper, IEnumerable<TDest> source)
            where TDest : class
            where TSrc : class, new()
            => source is null ? Enumerable.Empty<TSrc>() : source.Select(src => mapper.Map<TDest, TSrc>(src, new TSrc()));
        
        /// <summary>
        /// Extension method for IBothWaysMapper. Used to specify explicitly types of mapping inputs.
        /// Current mapping behavior for each element in mapping input collections:
        ///     - If comparator expression returns true, mapper will map source object to destination object
        ///     - If source object exists and destination object doesn't, mapper will add a new destination object
        ///         mapped from source object to the destination collection
        ///     - If destination object exists and source object doesn't, mapper will remove destination object
        ///         from destination collection
        ///     - If there are any duplicates in destination collection, mapper will map to the first one
        ///         and remove the rest
        ///     - If there are any duplicates in source collection, mapper will map the first one and ignore the the rest
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Collection of objects to map from</param>
        /// <param name="destination">Existing collection of objects to map to</param>
        /// <param name="comparator">Mapping expression used to identify which objects from destination collection
        /// should be updated with which elements from source collection</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>Updated collection of destination objects with mapped values</returns>
        public static IEnumerable<TSrc> Map<TDest, TSrc>(this IBothWaysMapper<TSrc, TDest> mapper,
            IEnumerable<TDest> source, IEnumerable<TSrc> destination, Func<TDest, TSrc, bool> comparator)
            where TDest : class
            where TSrc : class, new() 
        {
            var destinationForUpdate = destination is null ? new List<TSrc>() : destination.ToList();

            if (source is null)
                return destinationForUpdate;
            
            var destToMap = new Dictionary<TSrc, TDest>();
            var destToAdd = new HashSet<TSrc>();
            
            foreach (var src in source)
            {
                var destObj = destinationForUpdate.FirstOrDefault(dest => comparator(src, dest));
            
                if (destObj is null)
                    destToAdd.Add(mapper.Map<TDest, TSrc>(src, new TSrc()));
                else if (destToMap.TryAdd(destObj, src))
                    mapper.Map<TDest, TSrc>(src, destObj);
            }
            
            var destToRemove = destinationForUpdate.Except(destToMap.Keys).ToHashSet();
            
            destinationForUpdate.RemoveAll(d => destToRemove.Contains(d));
            
            destinationForUpdate.AddRange(destToAdd);
            
            return destinationForUpdate;
        }
        
        /// <summary>
        /// Extension method for IBothWaysMapper. Used to specify explicitly types of mapping inputs.
        /// </summary>
        /// <param name="mapper">Concrete instance of the mapper with mappings implementation</param>
        /// <param name="source">Object to map from</param>
        /// <param name="destination">Object to map to</param>
        /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
        /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
        /// <returns>New or updated destination object with mapped values</returns>
        public static TSrc Map<TDest, TSrc>(this IBothWaysMapper<TSrc, TDest> mapper, TDest source, TSrc destination = null)
            where TDest : class
            where TSrc : class, new()
        {
            var destToReturn = destination ?? new TSrc();
            
            return source is null ? destToReturn : mapper.Map(source, destToReturn);
        }
    }
}