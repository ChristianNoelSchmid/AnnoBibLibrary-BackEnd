using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnnoBibLibrary.Models
{
    public class Library
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required, MaxLength(255), JsonProperty("title")]
        public string Title { get; set; }

        [MaxLength(511), JsonProperty("description")]
        public string Description { get; set; }

        [Required, JsonProperty("keywordGroups")]
        public string KeywordGroups { get; set; }

        /* Many-to-one relationship with ApplicationUser */
        [JsonProperty("userId")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}