using Authentication.Entities;
using Authentication.Model;
using Authentication.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ThemeController : ControllerBase
    {
        private readonly ITheme _themeRepository;

        public ThemeController(ITheme themeRepository)
        {
            _themeRepository = themeRepository;

        }
        [HttpPost("CreateTheme")]
        public async Task<IActionResult> CreateMission([FromBody] ThemeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var theme = await _themeRepository.CreateTheme(model);

            return Ok(theme);
        }

        [HttpGet("GetThemeDetailsById/{themeId}")]
        public async Task<IActionResult> GetThemeById(int themeId)
        {
            var themeDetails = await _themeRepository.GetThemeById(themeId);
            if (themeDetails == null)
            {
                return NotFound("Theme not found");
            }

            return Ok(themeDetails);
        }



        [HttpDelete("DeleteTheme/{id}")]
        public async Task<IActionResult> DeleteTheme(int id)
        {
            var result = await _themeRepository.DeleteTheme(id);
            if (result == "Theme not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }


        [HttpPut("UpdateTheme/{id}")]
       
        public async Task<IActionResult> UpdateTheme(int id, [FromBody] ThemeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _themeRepository.UpdateTheme(id, model);
            if (result == "Theme not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }



    }

}