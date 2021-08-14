using System;
using Serializer.Library.Abstract;
using Serializer.Library.Service.TypeSerializers;

namespace Serializer.Library.Service
{
    internal class TypeSerializerFactory
    {
        private readonly Lazy<StringSerializer> _stringSerializer = new(new StringSerializer());
        private readonly Lazy<PrimitiveSerializer> _primitiveSerializer = new(new PrimitiveSerializer());
        private readonly Lazy<ObjectSerializer> _objectSerializer;

        public TypeSerializerFactory()
        {
            _objectSerializer = new Lazy<ObjectSerializer>(new ObjectSerializer(this));
        }
        
        public ITypeSerializer GetSerializer(Type type)
        {
            if (type == typeof(string))
            {
                return _stringSerializer.Value;
            }

            if (type.IsPrimitive)
            {
                return _primitiveSerializer.Value;
            }

            return _objectSerializer.Value;
        }
    }
}