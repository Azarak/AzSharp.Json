using AzSharp.Json.Parsing;
using AzSharp.Json.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace AzSharp.Json.Serialization.TypeSerializers;

public sealed class ObjectSerializer : ITypeSerializer
{
    public object? Deserialize(JsonNode node, object? obj, Type type, int version)
    {
        return GetObjectSerializer(type).Deserialize(node, obj, type, version);
    }

    public JsonNode Serialize(object obj, Type type)
    {
        return GetObjectSerializer(type).Serialize(obj, type);
    }

    public void VersionDataTreatment(object? obj, JsonNode node, Type type, int version)
    {
        GetObjectSerializer(type).VersionDataTreatment(obj, node, type, version);
    }
    private ITypeSerializer GetObjectSerializer(Type type)
    {
        JsonSerializableAttribute attribute = type.GetCustomAttribute<JsonSerializableAttribute>();
        ITypeSerializer serializer = JsonSerializer.GetSerializer(attribute.TypeSerializer);
        return serializer;
    }
}
