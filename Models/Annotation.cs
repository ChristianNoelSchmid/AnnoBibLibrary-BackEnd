using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Models
{
    public class Annotation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required, JsonProperty("notes")]
        public string Notes { get; set; }

        // One-to-many relationship between Annotation and Quote
        [Required, JsonProperty("quoteData")]
        public string QuoteData { get; set; }

        // Many-to-one relationship between Annotation and Source
        [JsonProperty("sourceId")]
        public int SourceId { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }
    }
}