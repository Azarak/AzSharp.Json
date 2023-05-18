using AzSharp.Json.Parsing;

namespace AzSharp.Json.Serialization;

public interface IJsonSerializable
{
    public virtual void Serialize(JsonNode node)
    {

    }
    public virtual void Deserialize(JsonNode node)
    {

    }
}
