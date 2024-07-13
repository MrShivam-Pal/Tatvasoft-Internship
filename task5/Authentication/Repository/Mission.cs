using Authentication.Entities;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repository
{
    public class Mission:IMission

    {
        private readonly AuthContext _authContext;

        public Mission(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<List<MissionViewModel>> GetMissionsWithDetails()
        {
            try
            {
              
                var missionsWithDetails = await _authContext.Missions.Select(mission => new MissionViewModel
                {
                    MissionId = mission.MissionId,
                    MissionTitle = mission.Title,
                    MissionDescription = mission.Description,
                    Challenge = mission.Challenge,
                    // CityName = _authContext.Cities.FirstOrDefault(c => c.CityId == mission.CityId).CityName,
                    //CountryName = _authContext.Countries.FirstOrDefault(c => c.CountryId == mission.CountryId).CountryName,
                    StartDate = mission.StartDate.ToString(),
                    EndDate = mission.EndDate.ToString(),
                    Deadline = mission.Deadline.ToString()
                }).ToListAsync();

                return missionsWithDetails;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<string> CreateMission(MissionDto model)
        {
            var mission = new MissionDto
            {
                Title = model.Title,
                Introduction = model.Introduction,
                Challenge = model.Challenge,
                Description = model.Description,
                CityId = model.CityId,
                CountryId = model.CountryId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Deadline = model.Deadline
            };

            _authContext.Missions.Add(mission);
            await _authContext.SaveChangesAsync();

            return "Mission Created Succesfully";
        }

        public async Task<MissionViewModel> GetMissionDetailsById(int missionId)
        {
            var missionWithDetails = await _authContext.Missions
                .Where(mission => mission.MissionId == missionId)
                .Select(mission => new MissionViewModel
                {
                    MissionId = mission.MissionId,
                    MissionTitle = mission.Title,
                    MissionDescription = mission.Description,
                   // CityName = _authContext.Cities.FirstOrDefault(c => c.CityId == mission.CityId).CityName,
                   // CountryName = _authContext.Countries.FirstOrDefault(c => c.CountryId == mission.CountryId).CountryName,
                    StartDate = mission.StartDate.ToString(),
                    EndDate = mission.EndDate.ToString(),
                    Deadline = mission.Deadline.ToString(),
                    SeatsLeft = mission.SeatsLeft,
                 //   OrganizationName = _authContext.Organizations.FirstOrDefault(r => r.OrganizationId == mission.OrganizationId).OrganizationName,
                 ////   Rating = _authContext.Organizations.FirstOrDefault(r => r.OrganizationId == mission.OrganizationId).Rating,
                  //  ImageURL = _authContext.MissionImage.FirstOrDefault(i => i.MissionId == mission.MissionId).ImageURL,
                 //   ThemeName = _authContext.MissionThemes.FirstOrDefault(t => t.ThemeId == mission.ThemeId).ThemeName,
                    Challenge = mission.Challenge,
                    MissionType = mission.MissionType,
                    MissionObject = mission.MissionObject,
                    MissionAchieved = mission.MissionAchieved,
                    Availability = mission.Availability,

            
                })
                .FirstOrDefaultAsync();

            return missionWithDetails;
        }

        public async Task<string> DeleteMission(int id)
       {
            var mission = await _authContext.Missions.FindAsync(id);
            if (mission == null)
            {
                return "Mission not found";
            }

            _authContext.Missions.Remove(mission);
           await _authContext.SaveChangesAsync();

           return "Mission Deleted Successfully";
        }


        public async Task<string> UpdateMission(MissionDto model){
            var result = "";
            // var existingUser = await _authContext.Missions.FirstOrDefault(x => x.MissionId == model.missionId);
            var mission = await _authContext.Missions.FindAsync(model.MissionId);

            if (mission != null)
            {
                if(model.Title != "string")mission.Title = model.Title;
                if(model.Description != "string")mission.Description = model.Description;
                if(model.Introduction != "string")mission.Introduction = model.Introduction;
                if(model.Challenge != "string")mission.Challenge = model.Challenge;

                // Save changes to the User entity
                await _authContext.SaveChangesAsync();
                result = "Mission updated";
            }
            else
            {
                result = "Mission not found.";
            }

            return result;
        }

    }
}
