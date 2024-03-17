using System.Text.Json.Serialization;
using ArtHubBO.Enum;

namespace InventoryManagementGUI.Model;

public class PostResult
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Result Result { get; set; }

    public string Data { get; set; }
}