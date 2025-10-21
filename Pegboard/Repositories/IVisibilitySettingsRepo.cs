using DougKlassen.Pegboard.Models;

namespace DougKlassen.Pegboard.Repositories
{
    internal interface IVisibilitySettingsRepo
    {
        VisibilitySettings LoadSettings();

        void WriteSettings(VisibilitySettings settings);
    }
}
