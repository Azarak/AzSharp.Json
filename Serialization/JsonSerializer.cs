using AzSharp.Json.Parsing;
using AzSharp.Json.Serialization.TypeSerializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzSharp.Json.Serialization;

public static class JsonSerializer
{
    private static Dictionary<Type, ITypeSerializer> typeDictionary = new();
    public  static ITypeSerializer GetSerializer(Type serializer_type)
    {
        if (!typeDictionary.ContainsKey(serializer_type))
        {
            typeDictionary[serializer_type] = (ITypeSerializer)Activator.CreateInstance(serializer_type);
        }
        return typeDictionary[serializer_type];
    }
    public static JsonNode Serialize(object obj, Type object_type, Type serializer_type)
    {
        ITypeSerializer serializer = GetSerializer(serializer_type);
        return serializer.Serialize(obj, object_type);
    }
    public static object? Deserialize(object? obj, JsonNode node, Type object_type, Type serializer_type, int version = 0)
    {
        ITypeSerializer serializer = GetSerializer(serializer_type);
        serializer.VersionDataTreatment(obj, node, object_type, version);
        return serializer.Deserialize(node, obj, object_type, version);
    }
}
