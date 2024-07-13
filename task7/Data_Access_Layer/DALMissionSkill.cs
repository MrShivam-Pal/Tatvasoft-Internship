using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class DALMissionSkill
    {
        private readonly AppDbContext _dbContext;

        public DALMissionSkill(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<MissionSkill>> GetMissionSkillListAsync()
        {
            return await _dbContext.MissionSkill
                                   .Where(ms => !ms.IsDeleted)
                                   .ToListAsync();
        }

        public async Task<MissionSkill> GetMissionSkillByIdAsync(int id)
        {
            return await _dbContext.MissionSkill
                                   .Where(ms => ms.Id == id && !ms.IsDeleted)
                                   .FirstOrDefaultAsync();
        }

        public async Task<string> AddMissionSkillAsync(MissionSkill missionSkill)
        {
            try
            {
                int maxId = await _dbContext.MissionSkill.MaxAsync(ud => (int?)ud.Id) ?? 0;
                var newMissionSkill = new MissionSkill
                {
                    Id = maxId + 1,
                    SkillName = missionSkill.SkillName,
                    Status = missionSkill.Status,
                    IsDeleted = false
                };

                _dbContext.MissionSkill.Add(newMissionSkill);
                await _dbContext.SaveChangesAsync();
                return "Save Skill Successfully..";
            }
            catch (Exception ex)
            {
                throw new Exception("Error in adding skill.", ex);
            }
        }

        public async Task<string> UpdateMissionSkillAsync(MissionSkill missionSkill)
        {
            try
            {
                var existingSkill = await _dbContext.MissionSkill.FirstOrDefaultAsync(ms => ms.Id == missionSkill.Id && !ms.IsDeleted);
                if (existingSkill != null)
                {
                    if(missionSkill.SkillName != "string")existingSkill.SkillName = missionSkill.SkillName;
                    if(missionSkill.Status != "string")existingSkill.Status = missionSkill.Status;
                    existingSkill.ModifiedDate = DateTime.UtcNow;

                    await _dbContext.SaveChangesAsync();
                    return "Update Mission Skill Successfully..";
                }
                else
                {
                    throw new Exception("Mission Skill not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in updating mission skill.", ex);
            }
        }

        public async Task<string> DeleteMissionSkillAsync(int id)
        {
            try
            {
                var existingSkill = await _dbContext.MissionSkill.FirstOrDefaultAsync(ms => ms.Id == id && !ms.IsDeleted);
                if (existingSkill != null)
                {
                    existingSkill.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                    return "Delete Mission Skill Successfully..";
                }
                else
                {
                    throw new Exception("Mission Skill not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in deleting mission skill.", ex);
            }
        }
    }
}
