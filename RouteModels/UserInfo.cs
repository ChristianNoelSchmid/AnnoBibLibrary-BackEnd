using AnnoBibLibrary.Models;
using Newtonsoft.Json;

namespace AnnoBibLibrary.RouteModels
{
    public class UserInfo
    {
        public UserInfo(ApplicationUser fromAppUser)
        {
            Id = fromAppUser.Id;
            Email = fromAppUser.Email;
            FirstName = fromAppUser.FirstName;
            LastName = fromAppUser.LastName;
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonIgnore]
        public string FirstName { get; set; }
        [JsonIgnore]
        public string LastName { get; set; }
        [JsonProperty("name")]
        public string FullName => $"{FirstName} {LastName}";

        [JsonProperty("token")]
        public string Token { get; set; }

    }
}