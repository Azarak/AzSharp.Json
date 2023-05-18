
namespace AzSharp.JsonNode;

public interface IJsonSchema
{
    public JsonNode Sanitize(JsonNode node)
    {
        if(!SanitizeNode(node))
        {
            return DefaultNode();
        }
        return node;
    }
    public JsonNode DefaultNode();
    public bool SanitizeNode(JsonNode node);
}
