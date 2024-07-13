using Authentication.Entities;
using Authentication.Model;
using Authentication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IMission _missionRepository;

        public MissionController(IMission missionRepository)
        {
            _missionRepository = missionRepository;
           
        }
        [HttpPost("CreateMission")]
        public async Task<IActionResult> CreateMission([FromBody] MissionDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mission = await _missionRepository.CreateMission(model);

            return Ok(mission);
        }

        [HttpGet("GetMissions")]
        public async Task<IActionResult> GetMissionWithDetails()
        {
            var missionWithDetails = await _missionRepository.GetMissionsWithDetails();
            return Ok(missionWithDetails);
        }


        [HttpGet("GetMissionById/{MissionId}")]
        public async Task<IActionResult> GetMissionWithDetailsById(int MissionId)
        {
            var missionWithDetails = await _missionRepository.GetMissionDetailsById(MissionId);
            return Ok(missionWithDetails);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var result = await _missionRepository.DeleteMission(id);

            if (result == "Mission not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }


        [HttpPost("UpdateMission")]
        public async Task<IActionResult> UpdateMission([FromBody] MissionDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _missionRepository.UpdateMission(model);
        
            return Ok(result);
        }
    }
}
