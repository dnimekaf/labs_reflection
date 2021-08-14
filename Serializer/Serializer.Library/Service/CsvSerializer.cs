using System;
using System.Collections.Generic;
using Serializer.Library.Abstract;
using System.Diagnostics;

namespace Serializer.Library.Service
{
    public class CsvSerializer : ISerializer
    {
        private readonly TypeSerializerFactory _factory;

        public CsvSerializer()
        { 
            _factory = new TypeSerializerFactory();
        }
        public string Serialize(object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            var type = source.GetType();
            
            var serializer = _factory.GetSerializer(type);
            Debug.Assert(serializer != null);
            
            return serializer.Serialize(source);
        }

        public T Deserialize<T>(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new ArgumentNullException(nameof(line));
            }

            var type = typeof(T);
            var currentPosition = 0;
            var serializer = _factory.GetSerializer(type);
            return (T)serializer.Deserialize(line, ref currentPosition, type);
        }

        public IEnumerable<T> Deserialize<T>(string[] lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            var result = new List<T>();
            foreach (var line in lines)
            {
                result.Add(Deserialize<T>(line));
            }
            return result;
        }
    }
}