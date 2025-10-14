namespace DougKlassen.Pegboard.Models
{
    internal interface IVisibilitySettingsRepo
    {
        VisibilitySettings LoadSettings();

        void WriteSettings(VisibilitySettings settings);
    }
}
