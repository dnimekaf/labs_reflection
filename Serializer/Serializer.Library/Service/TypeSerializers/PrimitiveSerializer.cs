using System;
using Serializer.Library.Abstract;

namespace Serializer.Library.Service.TypeSerializers
{
    public class PrimitiveSerializer : ITypeSerializer
    {
        public string Serialize(object source)
        {
            return source.ToString();
        }

        public object Deserialize(string line, ref int currentPosition, Type type)
        {
            var token = TokenReader.GetNextToken(line, ref currentPosition);
            var result = Convert.ChangeType(token, type);
            return result;
        }
        
       
    }
}