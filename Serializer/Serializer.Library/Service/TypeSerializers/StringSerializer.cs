using System;
using System.Collections.Generic;
using Serializer.Library.Abstract;

namespace Serializer.Library.Service.TypeSerializers
{
    internal class StringSerializer : ITypeSerializer
    {
        public string Serialize(object source)
        {
            return source as string;
        }

        public object Deserialize(string line, ref int currentPosition, Type type)
        {
            var token = TokenReader.GetNextToken(line, ref currentPosition);
            // Quoted string
            if (string.IsNullOrEmpty(token) == false)
            {
                if (token[0] == '"' && token[^1] == '"')
                {
                    token = token.Substring(1, token.Length - 2).Replace("\"\"", "\"");   
                }
            }
            return token;
        }
    }
}