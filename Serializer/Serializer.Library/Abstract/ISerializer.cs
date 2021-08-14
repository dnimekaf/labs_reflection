using System;
using System.Collections.Generic;
using JetBrains.Annotations;


namespace Serializer.Library.Abstract
{
    /// <summary>
    /// CSV serializator
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes source object to CSV string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        string Serialize([NotNull] object source);
        
        T Deserialize<T>([NotNull] string line);
        
        IEnumerable<T> Deserialize<T>([NotNull] string[] lines);
    }
}