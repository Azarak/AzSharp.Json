using AzSharp.Json.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzSharp.Json.Serialization.TypeSerializers;

public class GenericDictionarySerializer<TKey, TKeySerializer, TValue, TValueSerializer> : ITypeSerializer
{
    public object? Deserialize(JsonNode node, object? obj, Type type, int version)
    {
        Dictionary<TKey, TValue> dict = new();
        var node_list = node.AsList();
        if (node_list.Count % 2 != 0)
        {
            return dict;
        }
        for (int i = 0; i < (node_list.Count - 1); i += 2)
        {
            var key_node = node_list[i];
            var value_node = node_list[i + 1];

            TKey key = (TKey)JsonSerializer.Deserialize(null, key_node, typeof(TKey), typeof(TKeySerializer), version);
            TValue value = (TValue)JsonSerializer.Deserialize(null, value_node, typeof(TValue), typeof(TValueSerializer), version);

            dict[key] = value;
        }
        return dict;
    }

    public JsonNode Serialize(object obj, Type type)
    {
        JsonNode node = new JsonNode(JsonNodeType.LIST);
        var node_list = node.AsList();
        Dictionary<TKey, TValue> cast = (Dictionary<TKey, TValue>)obj;
        foreach (var pair in cast)
        {
            JsonNode key_node = JsonSerializer.Serialize(pair.Key, typeof(TKey), typeof(TKeySerializer));
            JsonNode value_node = JsonSerializer.Serialize(pair.Value, typeof(TValue), typeof(TValueSerializer));
            node_list.Add(key_node);
            node_list.Add(value_node);
        }
        return node;
    }

    public void VersionDataTreatment(object? obj, JsonNode node, Type type, int version)
    {
        return;
    }
}
