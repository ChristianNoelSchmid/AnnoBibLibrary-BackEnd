using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnnoBibLibrary.RouteModels
{
    public class NewUser
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public override string ToString() => Email + ' ' + Password;
    }
}