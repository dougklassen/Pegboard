using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.IO;

namespace DougKlassen.Pegboard
{
    public static class FileLocations
    {
        public static string AddInDirectory;
        public static string AssemblyName;
        public static string AssemblyPath;
        public static readonly String ImperialTemplateDirectory = @"C:\ProgramData\Autodesk\RVT 2026\Family Templates\English-Imperial\";
        public static readonly String ResourceNameSpace = "DougKlassen.Pegboard.Resources";
        public static readonly String CommandNameSpace = "DougKlassen.Pegboard.Commands";
    }

    public class StartUpApp : IExternalApplication
    {
        BitmapImage largeIcon;
        BitmapImage smallIcon;

        /// <summary>
        /// Run by Revit on start up. Loads the Pegboard tab of the ribbon.
        /// </summary>
        /// <param name="application">A reference to the Revit UI</param>
        /// <returns>Whether the application sucessfully started up</returns>
        public Result OnStartup(UIControlledApplication application)
        {
            FileLocations.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            FileLocations.AddInDirectory = application.ControlledApplication.AllUsersAddinsLocation + @"\" + FileLocations.AssemblyName + @"\";
            FileLocations.AssemblyPath = FileLocations.AddInDirectory + FileLocations.AssemblyName + ".dll";

            largeIcon = GetEmbeddedImageResource("iconLarge.png");
            smallIcon = GetEmbeddedImageResource("iconSmall.png");

            String tabName = "Pegboard";
            try
            {
                application.CreateRibbonTab(tabName);
            }
            //an exception will be thrown if the tab already exists
            catch (Autodesk.Revit.Exceptions.ArgumentException) { }
            RibbonPanel pegboardRibbonPanel = application.CreateRibbonPanel(tabName, "Pegboard Tools");

            #region Create column one: Naming, Clean Up, Export
            PulldownButtonData namingStandardsPulldownButtonData = new PulldownButtonData(
                name: "AuditNamesToolsPulldown",
                text: "Name Auditing");
            PulldownButtonData cleanUpToolsPullDownButtonData = new PulldownButtonData(
                name: "CleanUpToolsPulldown",
                text: "Clean Up");
            PulldownButtonData exportDataPullDownButttonData = new PulldownButtonData(
                name: "ExportDataDownButton",
                text: "Export/Import Data");
            IList<RibbonItem> stackOne = pegboardRibbonPanel.AddStackedItems(
                namingStandardsPulldownButtonData,
                cleanUpToolsPullDownButtonData,
                exportDataPullDownButttonData);

            #region Column One - Naming Standards Pulldown
            PulldownButton namingStandardsPulldownButton = (PulldownButton) stackOne[0];
            addButtonToPulldown(
                pulldown: namingStandardsPulldownButton,
                commandClass: "AuditViewNamesCommand",
                buttonText: "Audit View Names",
                buttonToolTip: "Check view names against the view naming standard");
            addButtonToPulldown(
                pulldown: namingStandardsPulldownButton,
                commandClass: "SetViewTitlesCommand",
                buttonText: "Set empty view titles",
                buttonToolTip: "Set the Title on Sheet parameter for views that don't have it set yet");
            addButtonToPulldown(
                pulldown: namingStandardsPulldownButton,
                commandClass: "FixFamilyTypeNamesCommand",
                buttonText: "Fix Family Type Names",
                buttonToolTip: "Set family types in families with only one type to match the family name");
            #endregion

            #region Column One - Clean Up Pulldown
            PulldownButton cleanUpPulldownButton = (PulldownButton) stackOne[1];
            addButtonToPulldown(
                pulldown: cleanUpPulldownButton,
                commandClass: "PurgeLinePatternsCommand",
                buttonText: "Purge Line Patterns",
                buttonToolTip: "Purge line patterns using regular expression matches");
            addButtonToPulldown(
                pulldown: cleanUpPulldownButton,
                commandClass: "PurgeRefPlanesCommand",
                buttonText: "Purge Reference Planes",
                buttonToolTip: "Purge unlabelled reference planes");
            addButtonToPulldown(
                pulldown: cleanUpPulldownButton,
                commandClass: "PurgeViewsCommand",
                buttonText: "Purge Views",
                buttonToolTip: "Purge unamed views");
            #endregion

            #region Column One - Export Pulldown
            PulldownButton exportPulldownButton = (PulldownButton) stackOne[2];
            #endregion
            #endregion

            #region Create column two: Geometry, Elements, Schedules
            PulldownButtonData geometryPulldownButtonData = new PulldownButtonData(
                name: "GeometryPulldownButton",
                text: "Fix Geometry");
            PulldownButtonData elementsPulldownButtonData = new PulldownButtonData(
                name: "ElementsPulldownButton",
                text: "Element Properties");
            PulldownButtonData schedulesPulldownButtonData = new PulldownButtonData(
                name: "SchedulesPulldownButton",
                text: "Schedules");
            IList<RibbonItem> stackTwo = pegboardRibbonPanel.AddStackedItems(
                geometryPulldownButtonData,
                elementsPulldownButtonData,
                schedulesPulldownButtonData);

            #region Column Two - Geometry Pulldown
            PulldownButton geometryPulldownButton = (PulldownButton) stackTwo[0];
            addButtonToPulldown(
               pulldown: geometryPulldownButton,
               commandClass: "UnflipCommand",
               buttonText: "Unflip Elements",
               buttonToolTip: "Unflip elements that have been flipped on both axes",
               commandAvailability: "UnflipCommandAvailability");
            addButtonToPulldown(
                pulldown: geometryPulldownButton,
                commandClass: "DisallowWallJoinsCommand",
                buttonText: "Disallow Wall Joins",
                buttonToolTip: "Turn off wall joins for all selected walls",
                commandAvailability: "DisallowWallJoinsCommandAvailability");
            addButtonToPulldown(
               pulldown: geometryPulldownButton,
               commandClass: "SplitWallByLevelCommand",
               buttonText: "Split Wall by Level",
               buttonToolTip: "Split wall by intervening levels");
            #endregion

            #region Column Two - Elements Pulldown
            PulldownButton elementsPulldownButton = (PulldownButton)stackTwo[1];
            addButtonToPulldown(
                pulldown: elementsPulldownButton,
                commandClass: "CommentAddCommand",
                buttonText: "Add comment tag",
                buttonToolTip: "Add space delimited tags to the comment parameter of selected elements");
            addButtonToPulldown(
                pulldown: elementsPulldownButton,
                commandClass: "CommentRemoveCommand",
                buttonText: "Remove comment tags",
                buttonToolTip: "Remove space delimited tags from the comment parameter of selected elements");
            #endregion

            #region Column Two - Schedules Pulldown
            PulldownButton schedulesPulldownButton = (PulldownButton)stackTwo[2];
            addButtonToPulldown(
                pulldown: schedulesPulldownButton,
                commandClass: "StandardizeSchedulesCommand",
                buttonText: "Standardize Schedule Formatting",
                buttonToolTip: "Implement schedule formatting standards for export to Excel",
                commandAvailability: "StandardizeSchedulesCommandAvailability");
            addButtonToPulldown(
                pulldown: schedulesPulldownButton,
                commandClass: "CreateQuantityScheduleCommand",
                buttonText: "Create a Quantity Schedule",
                buttonToolTip: "Create a standardize quantity schedule using a configuration template",
                commandAvailability: "NeverAvailableCommandAvailability");
            #endregion
            #endregion

            #region Create slide out panel: About
            pegboardRibbonPanel.AddSlideOut();
            PushButtonData aboutCommandPushButtonData = new PushButtonData(
                name: "AboutCommandButton",
                text: "About",
                assemblyName: FileLocations.AssemblyPath,
                className: FileLocations.CommandNameSpace + '.' + "AboutCommand")
            {
                LargeImage = largeIcon,
                Image = smallIcon,
                AvailabilityClassName = FileLocations.CommandNameSpace + '.' + "AlwaysAvailableCommandAvailability"
            };
            pegboardRibbonPanel.AddItem(aboutCommandPushButtonData);
            #endregion

            #region Create panel: Reset View Overrides
            #endregion

            #region Create panel: Apply Override Styles
            #endregion

            #region Create panel: Manage View Callouts
            #endregion

            return (Result.Succeeded);
        }

        /// <summary>
        /// Run by Revit on shutdown.
        /// </summary>
        /// <param name="application">A reference to the Revit UI</param>
        /// <returns>Whether the application successfully unloaded</returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return(Result.Succeeded);
        }

        /// <summary>
        /// Helper method to add a button to a pulldown
        /// </summary>
        /// <param name="pulldown">The pulldown button to which the command will be added</param>
        /// <param name="buttonText">The text of the button</param>
        /// <param name="buttonToolTip">The button tooltip</param>
        /// <param name="commandClass">The command that will be executed</param>
        /// <param name="commandAvailability">The command availability class for the command</param>
        /// <param name="largeImage">The large icon image</param>
        /// <param name="smallImage">The small icon image</param>
        /// <returns>A reference to the added button</returns>
        private PushButton addButtonToPulldown(
            PulldownButton pulldown,
            string buttonText,
            string buttonToolTip,
            string commandClass,
            string commandAvailability = null,
            BitmapImage largeImage = null,
            BitmapImage smallImage = null)
        {
            if (largeImage == null)
            {
                largeImage = largeIcon;
            }
            if (smallImage == null)
            {
                smallImage = smallIcon;
            }

            PushButtonData buttonData = new PushButtonData(
                name: commandClass + "Button",
                text: buttonText,
                assemblyName: FileLocations.AssemblyPath,
                className: FileLocations.CommandNameSpace + '.' + commandClass)
            {
                LargeImage = largeImage,
                Image = smallImage
            };
            var button = pulldown.AddPushButton(buttonData);
            button.ToolTip = buttonToolTip;

            if (commandAvailability != null)
            {
                button.AvailabilityClassName = FileLocations.CommandNameSpace + '.' + commandAvailability;
            }

            return button;
        }

        /// <summary>
        /// Utility method to retrieve an embedded image resource from the assembly
        /// </summary>
        /// <param name="resourceName">The name of the image, corresponding to the filename of the image embedded in the assembly</param>
        /// <returns>The loaded image represented as a BitmapImage</returns>
        private BitmapImage GetEmbeddedImageResource(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream str = asm.GetManifestResourceStream(FileLocations.ResourceNameSpace + "." + resourceName);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = str;
            bmp.EndInit();

            return bmp;
        }
    }
}
