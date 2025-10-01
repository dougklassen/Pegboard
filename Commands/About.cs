using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class AboutCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            AssemblyName name = asm.GetName();

            TaskDialog.Show(
                name.Version.ToString(),
                asm.FullName + '\n' +
                asm.Location + '\n' +
                name.Version);

            return Result.Succeeded;
        }
    }
}
