using Newtonsoft.Json;

namespace Vacations.Models.Typeahead
{
    public class ItemTypeahead
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
