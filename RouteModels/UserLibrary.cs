using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnnoBibLibrary.RouteModels
{
    public class UserLibrary
    {
        [Required, JsonProperty("userId")]
        public string UserId { get; set; }

        [Required, JsonProperty("libraryId")]
        public int LibraryId { get; set; }
    }
}