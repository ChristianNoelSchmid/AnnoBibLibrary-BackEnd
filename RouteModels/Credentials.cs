using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnnoBibLibrary.RouteModels
{
    public class Credentials
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        public override string ToString() => Email + ' ' + Password;
    }
}