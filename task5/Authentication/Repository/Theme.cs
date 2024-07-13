using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Entities;
using Authentication.Model;
using Microsoft.EntityFrameworkCore;
using Authentication.Repository;

namespace Authentication.Repository
{
    public class Theme : ITheme
    {
        private readonly AuthContext _authContext;

        public Theme(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<List<ThemeViewModel>> GetThemes()
        {
            try
            {
                var themes = await _authContext.Themes.Select(theme => new ThemeViewModel
                {
                    ThemeId = theme.ThemeId,
                    ThemeName = theme.ThemeName,
                    // Add other properties as needed
                }).ToListAsync();

                return themes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> CreateTheme(ThemeDto model)
        {
            var theme = new ThemeDto
            {
                // ThemeId= model.ThemeId,
                ThemeName = model.ThemeName,
               
            };

            _authContext.Themes.Add(theme);
            await _authContext.SaveChangesAsync();

            return "Theme Created Successfully";
        }

        public async Task<ThemeViewModel> GetThemeById(int themeId)
        {
            var theme = await _authContext.Themes.FindAsync(themeId);
            if (theme == null)
            {
                return new ThemeViewModel{}; // or handle not found case appropriately
            }

            var themeViewModel = new ThemeViewModel
            {
                ThemeId = theme.ThemeId,
                ThemeName = theme.ThemeName,
                // Add other properties as needed
            };  

            return themeViewModel;
        }

        public async Task<string> DeleteTheme(int id)
        {
            var theme = await _authContext.Themes.FindAsync(id);
            if (theme == null)
            {
                return "Theme not found";
            }

            _authContext.Themes.Remove(theme);
            await _authContext.SaveChangesAsync();

            return "Theme Deleted Successfully";
        }

        public async Task<string> UpdateTheme(int id, ThemeDto model)
        {
            var theme = await _authContext.Themes.FindAsync(id);
            if (theme == null)
            {
                return "Theme not found";
            }

            theme.ThemeName = model.ThemeName;
            // Update other properties as needed

            await _authContext.SaveChangesAsync();

            return "Theme Updated Successfully";
        }

        public async Task<ThemeViewModel> GetThemeDetailsById(int themeId)
        {
            try
            {
                var themeWithDetails = await _authContext.Themes
                    .Where(theme => theme.ThemeId == themeId)
                    .Select(theme => new ThemeViewModel
                    {
                        ThemeId = theme.ThemeId,
                        ThemeName = theme.ThemeName,
                       
                    })
                    .FirstOrDefaultAsync();

                return themeWithDetails;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

    }
}
