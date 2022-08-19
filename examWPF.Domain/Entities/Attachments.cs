using System;
using Newtonsoft.Json;

namespace examWPF.Domain.Entities
{
    public class Attachments
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

    }
}