using System;
using System.Collections.Generic;
using System.Reflection;
using Serializer.Library.Abstract;

namespace Serializer.Library.Service.TypeSerializers
{
    internal class ObjectSerializer : ITypeSerializer
    {
        private readonly TypeSerializerFactory _typeSerializerFactory;
        public ObjectSerializer(TypeSerializerFactory typeSerializerFactory)
        {
            _typeSerializerFactory = typeSerializerFactory;
        }
        
        public string Serialize(object source)
        {
            var properties = new List<string>();
            var type = source.GetType();

            var fields = type.GetFields( BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var fieldSerializer = _typeSerializerFactory.GetSerializer(field.FieldType);
                properties.Add(fieldSerializer.Serialize(field.GetValue(source)));
            }
            
            // var members = type.GetMembers();
            // var p = type.GetProperties();

            return string.Join(",", properties);
        }
        
        public object Deserialize(string line, ref int currentPosition, Type type)
        {
            var result = Activator.CreateInstance(type);
            
            var fields = type.GetFields( BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                var fieldSerializer = _typeSerializerFactory.GetSerializer(field.FieldType);
                var ftType = field.FieldType;
                var value = fieldSerializer.Deserialize(line, ref currentPosition, ftType);
                field.SetValue(result, value);
            }
            return result;
        }
    }
}