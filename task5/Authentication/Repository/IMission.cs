using Authentication.Entities;
using Authentication.Model;

namespace Authentication.Repository
{
    public interface IMission
    {
        Task<List<MissionViewModel>> GetMissionsWithDetails();
        Task<string> CreateMission(MissionDto model);
        Task<MissionViewModel> GetMissionDetailsById(int missionId);

        Task<string> DeleteMission(int id);

        Task<string> UpdateMission(MissionDto model);
    }

    public interface ITheme
    {
        Task<string> CreateTheme(ThemeDto model);

        Task<ThemeViewModel> GetThemeById(int themeid);

        Task<string> DeleteTheme(int id);

        Task<string> UpdateTheme(int id, ThemeDto model);
    }

}
