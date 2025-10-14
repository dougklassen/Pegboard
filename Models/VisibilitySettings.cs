using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Models
{
    internal class VisibilitySettings
    {
        public IEnumerable<BuiltInCategory> ModelCategories { get; set; }
        public IEnumerable<BuiltInCategory> AnnotationCategories { get; set; }
        public IEnumerable<BuiltInCategory> ViewBugCategories { get; set; }
        public IEnumerable<BuiltInCategory> AnalyticalCategories { get; set; }

        public VisibilityOverrides CurrentOverrideStyle { get; set; }

        public VisibilitySettings()
        {
            ModelCategories = new List<BuiltInCategory>();
            AnnotationCategories = new List<BuiltInCategory>();
            ViewBugCategories = new List<BuiltInCategory>();
            AnalyticalCategories = new List<BuiltInCategory>();

            CurrentOverrideStyle = new VisibilityOverrides();
        }
    }
}
