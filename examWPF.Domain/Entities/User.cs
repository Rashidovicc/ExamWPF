using System;
using System.Net.Mail;
using examWPF.Domain.Commons;
using Newtonsoft.Json;

namespace examWPF.Domain.Entities
{
    public class User : Auditable
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("faculty")]
        public string Faculty { get; set; }

        [JsonProperty("passportId")]
        public string PassportId { get; set; }

        [JsonProperty("passport")]
        public Attachments Passport { get; set; }

        [JsonProperty("imageId")]
        public string ImageId { get; set; }

        [JsonProperty("image")]
        public Attachments Image { get; set; }
        
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}