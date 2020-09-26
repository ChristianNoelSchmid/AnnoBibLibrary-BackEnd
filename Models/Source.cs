using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Models
{
    public class Source
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required, JsonProperty("type")]
        public string Type { get; set; }

        [Required, JsonProperty("fields")]
        public string Fields { get; set; }

        // One-to-many relationship between Source and Annotation
        [Required, JsonProperty("annotations")]
        public List<Annotation> Annotations { get; set; }
    }
}