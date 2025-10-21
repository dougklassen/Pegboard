using Autodesk.Revit.DB;
using DougKlassen.Pegboard.Helpers;

namespace DougKlassen.Pegboard.Models
{
    internal class VisibilityOverrides
    {
        //public int DetailLevel { get; set; }
        public bool Halftone { get; set; }
        public long ProjectionLinePatternId { get; set; }
        public VisibilityColor ProjectionLineColor { get; set; }
        public int ProjectionLineWeight { get; set; }
        public bool IsSurfaceForegroundPatternVisible { get; set; }
        public long SurfaceForegroundPatternId { get; set; }
        public VisibilityColor SurfaceForegroundPatternColor { get; set; }
        public bool IsSurfaceBackgroundPatternVisible { get; set; }
        public long SurfaceBackgroundPatternId { get; set; }
        public VisibilityColor SurfaceBackgroundPatternColor { get; set; }
        public int SurfaceTransparency { get; set; }
        public long CutLinePatternId { get; set; }
        public VisibilityColor CutLineColor { get; set; }
        public int CutLineWeight { get; set; }
        public bool IsCutForegroundPatternVisible { get; set; }
        public long CutForegroundPatternId { get; set; }
        public VisibilityColor CutForegroundPatternColor { get; set; }
        public bool IsCutBackgroundPatternVisible { get; set; }
        public long CutBackgroundPatternId { get; set; }
        public VisibilityColor CutBackgroundPatternColor { get; set; }

        public VisibilityOverrides()
        {
            OverrideGraphicSettings settings = new OverrideGraphicSettings();
            AssignValues(settings);
        }

        public VisibilityOverrides(OverrideGraphicSettings settings)
        {
            AssignValues(settings);
        }

        private void AssignValues(OverrideGraphicSettings settings)
        {
            //DetailLevel = (int)settings.DetailLevel;
            Halftone = settings.Halftone;
            ProjectionLinePatternId = settings.ProjectionLinePatternId.Value;
            ProjectionLineColor = settings.ProjectionLineColor.GetVisibilityModel();
            ProjectionLineWeight = settings.ProjectionLineWeight;
            IsSurfaceForegroundPatternVisible = settings.IsSurfaceForegroundPatternVisible;
            SurfaceForegroundPatternId = settings.SurfaceForegroundPatternId.Value;
            SurfaceForegroundPatternColor = settings.SurfaceForegroundPatternColor.GetVisibilityModel();
            IsSurfaceBackgroundPatternVisible = settings.IsSurfaceBackgroundPatternVisible;
            SurfaceBackgroundPatternId = settings.SurfaceBackgroundPatternId.Value;
            SurfaceBackgroundPatternColor = settings.SurfaceBackgroundPatternColor.GetVisibilityModel();
            SurfaceTransparency = settings.Transparency;
            CutLinePatternId = settings.CutLinePatternId.Value;
            CutLineColor = settings.CutLineColor.GetVisibilityModel();
            CutLineWeight = settings.CutLineWeight;
            IsCutForegroundPatternVisible = settings.IsCutForegroundPatternVisible;
            CutForegroundPatternId = settings.CutForegroundPatternId.Value;
            CutForegroundPatternColor = settings.CutForegroundPatternColor.GetVisibilityModel();
            IsCutBackgroundPatternVisible = settings.IsCutBackgroundPatternVisible;
            CutBackgroundPatternId = settings.CutBackgroundPatternId.Value;
            CutBackgroundPatternColor = settings.CutBackgroundPatternColor.GetVisibilityModel();
        }

        public OverrideGraphicSettings GetOverride()
        {
            OverrideGraphicSettings settings = new OverrideGraphicSettings();

            //settings.SetDetailLevel((ViewDetailLevel)Enum.Parse(typeof(ViewDetailLevel), DetailLevel.ToString()));
            settings.SetHalftone(Halftone);
            settings.SetProjectionLinePatternId(new ElementId(ProjectionLinePatternId));
            if (ProjectionLineColor != null)
            {
                settings.SetProjectionLineColor(ProjectionLineColor.GetColor());
            }
            settings.SetProjectionLineWeight(ProjectionLineWeight);
            settings.SetSurfaceForegroundPatternVisible(IsSurfaceForegroundPatternVisible);
            settings.SetSurfaceForegroundPatternId(new ElementId(SurfaceForegroundPatternId));
            if (SurfaceForegroundPatternColor != null)
            {
                settings.SetSurfaceForegroundPatternColor(SurfaceForegroundPatternColor.GetColor());
            }
            settings.SetSurfaceBackgroundPatternVisible(IsSurfaceBackgroundPatternVisible);
            settings.SetSurfaceBackgroundPatternId(new ElementId(SurfaceBackgroundPatternId));
            if (SurfaceBackgroundPatternColor != null)
            {
                settings.SetSurfaceBackgroundPatternColor(SurfaceBackgroundPatternColor.GetColor());
            }
            settings.SetSurfaceTransparency(SurfaceTransparency);
            settings.SetCutLinePatternId(new ElementId(CutLinePatternId));
            if (CutLineColor != null)
            {
                settings.SetCutLineColor(CutLineColor.GetColor());
            }
            settings.SetCutLineWeight(CutLineWeight);
            settings.SetCutForegroundPatternVisible(IsCutForegroundPatternVisible);
            settings.SetCutForegroundPatternId(new ElementId(CutForegroundPatternId));
            if (CutForegroundPatternColor != null)
            {
                settings.SetCutForegroundPatternColor(CutForegroundPatternColor.GetColor());
            }
            settings.SetCutBackgroundPatternVisible(IsCutBackgroundPatternVisible);
            settings.SetCutBackgroundPatternId(new ElementId(CutBackgroundPatternId));
            if (CutBackgroundPatternColor != null)
            {
                settings.SetCutBackgroundPatternColor(CutBackgroundPatternColor.GetColor());
            }
            return settings;
        }
    }
}
