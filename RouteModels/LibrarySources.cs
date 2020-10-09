using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnnoBibLibrary.RouteModels
{
    public class LibrarySources
    {
        [JsonProperty("libraryId"), Required]
        public int LibraryId { get; set; }

        [JsonProperty("includeAnnotations")]
        public bool IncludeAnnotations { get; set; } = false;
    }
}