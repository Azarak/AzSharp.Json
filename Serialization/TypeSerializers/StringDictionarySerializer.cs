using AzSharp.Json.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzSharp.Json.Serialization.TypeSerializers;

public class StringDictionarySerializer<TValue, TValueSerializer> : ITypeSerializer
    where TValueSerializer : ITypeSerializer
{
    public object? Deserialize(JsonNode node, object? obj, Type type, int version)
    {
        Dictionary<string, TValue> dictionary = new();
        foreach (var pair in node.AsDict())
        {
            TValue value = (TValue)JsonSerializer.Deserialize(null, pair.Value, typeof(TValue), typeof(TValueSerializer), version);
            dictionary.Add(pair.Key, value);
        }
        return dictionary;
    }

    public JsonNode Serialize(object obj, Type type)
    {
        Dictionary<string, TValue> cast = (Dictionary<string, TValue>)obj;
        JsonNode node = new JsonNode(JsonNodeType.DICTIONARY);
        var node_dict = node.AsDict();
        foreach (var pair in cast)
        {
            node_dict[pair.Key] = JsonSerializer.Serialize(pair.Value, typeof(TValue), typeof(TValueSerializer));
        }
        return node;
    }

    public void VersionDataTreatment(object? obj, JsonNode node, Type type, int version)
    {
        return;
    }
}
