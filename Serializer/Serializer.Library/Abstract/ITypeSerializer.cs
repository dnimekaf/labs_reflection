using System;
using JetBrains.Annotations;

namespace Serializer.Library.Abstract
{
    internal interface ITypeSerializer
    {
        string Serialize(object source);
        
        object Deserialize(string line, ref int currentPosition, Type type);
    }
}