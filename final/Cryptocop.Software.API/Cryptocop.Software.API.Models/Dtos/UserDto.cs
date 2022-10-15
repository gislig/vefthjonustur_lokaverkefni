namespace Cryptocop.Software.API.Models.Dtos
{
    public class UserDto
    {
        public string Identifier { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        [JsonIgnore]
        public string TokenId { get; set; }
    }
}