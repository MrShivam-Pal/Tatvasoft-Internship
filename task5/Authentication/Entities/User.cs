using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Authentication.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class MissionDto
    {
        [Key]
        public int MissionId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Introduction { get; set; }
        public string? Challenge { get; set; }
        public int? TotalSeats { get; set; }
        public int? SeatsLeft { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Deadline { get; set; }
        public int? ThemeId { get; set; }
        public int? OrganizationId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public string? MissionImage { get; set; }

        public int? MissionType { get; set; }
        public string? MissionObject { get; set; }
        public int? MissionAchieved { get; set; }
        public int? Availability { get; set; }
      

    }

    public class Theme
    {
        [Key]
        public int ThemeId { get; set; }


        public string? ThemeName { get; set; }

        // Add any other properties as needed
    }

    public class ThemeDto
    {
        [Key]
        public int ThemeId { get; set; }

        public string? ThemeName { get; set; }
    }
}
