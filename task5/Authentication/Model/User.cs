using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Authentication.Model
{
    public class LoginRequest
    {

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class MissionViewModel
    {
        public int MissionId { get; set; }
        public string MissionTitle { get; set; }
        public string MissionDescription { get; set; }
        public string Challenge { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Deadline { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string ThemeName { get; set; }
        public string OrganizationName { get; set; }
        public int? Rating { get; set; }
        public int? SeatsLeft { get; set; }
        public string ImageURL { get; set; }
        public int? MissionType { get; set; }
        public string? MissionObject { get; set; }
        public int? MissionAchieved { get; set; }
        public int? Availability { get; set; }
    }

    public class ThemeViewModel
    {
        [Key]
        public int ThemeId { get; set; }
        public string? ThemeName { get; set; }
    }
}

