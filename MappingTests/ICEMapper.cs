namespace MappingTests
{
    /// <summary>
    /// Use it when you want to register one way mapping: x -> y.
    /// To register new mapping you should find or create suitable concrete mapper (for example UserMapper.cs),
    /// and implement new IOneWayMapper in mapper abstraction. 
    /// </summary>
    /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
    /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
    public interface IOneWayMapper<TSrc, TDest>
        where TDest : class
        where TSrc : class
    {
        /// <summary>
        /// CE Mapping method which is used to mapped objects one to another.
        /// To explicitly specify types of the object it is neccessary to use MappingExtenstion.cs methods from CE.Core project.
        /// </summary>
        /// <param name="source">Object to map from</param>
        /// <param name="destination">Object to map to</param>
        /// <returns>Updated or new mapped object</returns>
        public TDest Map(TSrc source, TDest destination);
    }


    /// <summary>
    /// Use it when you want to register two way mapping: x -> y, y -> x.
    /// To register new mapping you should find or create suitable concrete mapper (for example UserMapper.cs),
    /// and implement new IBothWaysMapper in mapper abstraction. 
    /// </summary>
    /// <typeparam name="TSrc">Type of source object (map from)</typeparam>
    /// <typeparam name="TDest">Type of destination object (map to)</typeparam>
    public interface IBothWaysMapper<TSrc, TDest> : IOneWayMapper<TSrc, TDest>
        where TSrc : class
        where TDest : class
    {
        /// <summary>
        /// CE Mapping method which is used to mapped objects one to another.
        /// To explicitly specify types of the object it is neccessary to use MappingExtenstion.cs methods from CE.Core project.
        /// </summary>
        /// <param name="source">Object to map from</param>
        /// <param name="destination">Object to map to</param>
        /// <returns>Updated or new mapped object</returns>
        public TSrc Map(TDest source, TSrc destination);
    }
}