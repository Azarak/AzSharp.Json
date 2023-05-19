﻿using AzSharp.Json.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AzSharp.Json.Serialization.TypeSerializers;

public class ListSerializer<TObject, TSerializer> : ITypeSerializer
    where TSerializer : ITypeSerializer
{
    public object? Deserialize(JsonNode node, object? obj, Type type, int version)
    {
        if (obj == null)
        {
            obj = new List<TObject>();
        }
        List<TObject> cast = (List<TObject>)obj;
        foreach (var item in node.AsList())
        {
            cast.Add((TObject)JsonSerializer.Deserialize(null, item, typeof(TObject), typeof(TSerializer)));
        }
        return obj;
    }

    public JsonNode Serialize(object obj, Type type)
    {
        JsonNode node = new JsonNode(JsonNodeType.LIST);
        IEnumerable enumerable_obj = (IEnumerable)obj;
        foreach (var item in enumerable_obj)
        {
            node.AsList().Add(JsonSerializer.Serialize(item, typeof(TObject), typeof(TSerializer)));
        }
        return node;
    }

    public void VersionDataTreatment(object? obj, JsonNode node, Type type, int version)
    {
        return;
    }
}
