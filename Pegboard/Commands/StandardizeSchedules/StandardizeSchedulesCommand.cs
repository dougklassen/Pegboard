using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Text.RegularExpressions;

namespace DougKlassen.Pegboard.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class StandardizeSchedulesCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiDoc = commandData.Application.ActiveUIDocument;
            var dbDoc = uiDoc.Document;
            //the standards column heading formats used to label units. If headings don't match these, columns will be relabelled.
            //Regex unitsLabelRegex = new Regex(@"\([a-zA-Z]+\)$");
            Regex lengthRegex = new Regex(@"\(ft\)$|\(lf\)$");
            Regex volumeRegex = new Regex(@"\(cy\)$");
            Regex areaRegex = new Regex(@"\(sf\)$|\(ssf\)$|\(sfca\)$");

            //filter the selection for schedules only
            var schedules = uiDoc.Selection
                .GetElementIds()
                .Select(id => dbDoc.GetElement(id) as ViewSchedule)
                .Where(v => null != v);

            using (Transaction t = new Transaction(dbDoc, "Standardize schedules"))
            {
                t.Start();

                foreach (var schedule in schedules)
                {
                    ScheduleDefinition definition = schedule.Definition;
                    //step through each field in the schedule
                    ScheduleField field;
                    for (int f = 0; f < definition.GetFieldCount(); f++)
                    {
                        field = definition.GetField(f);

                        FormatOptions formatOptions = field.GetFormatOptions();

                        ForgeTypeId specType = field.GetSpecTypeId();
                        if (specType == SpecTypeId.Length)
                        {
                            if (!lengthRegex.IsMatch(field.ColumnHeading))
                            {
                                field.ColumnHeading += " (lf)";
                            }
                            formatOptions.UseDefault = false;
                            formatOptions.SetUnitTypeId(UnitTypeId.Feet); //UnitTypeId.Feet represents decimal feet
                            formatOptions.Accuracy = 0.001;
                        }
                        else if (specType == SpecTypeId.Area)
                        {
                            if (!areaRegex.IsMatch(field.ColumnHeading))
                            {
                                field.ColumnHeading += " (sf)";
                            }
                            formatOptions.UseDefault = false;
                            formatOptions.SetUnitTypeId(UnitTypeId.SquareFeet);
                            formatOptions.Accuracy = 0.1;
                        }
                        else if (specType == SpecTypeId.Volume)
                        {
                            if (!volumeRegex.IsMatch(field.ColumnHeading))
                            {
                                field.ColumnHeading += " (cy)";
                            }
                            formatOptions.UseDefault = false;
                            formatOptions.SetUnitTypeId(UnitTypeId.CubicYards);
                            formatOptions.Accuracy = 0.1;
                        }
                        else if (specType == SpecTypeId.Currency)
                        {
                            formatOptions.UseDefault = false;
                            formatOptions.SetUnitTypeId(UnitTypeId.Currency);
                            formatOptions.Accuracy = 0.01;
                        }

                        //standardize field format
                        if (!formatOptions.UseDefault)
                        {
                            formatOptions.SetSymbolTypeId(new ForgeTypeId()); //set to empty ForgeType to specify no units symbol
                            if (formatOptions.CanSuppressTrailingZeros())
                            {
                                formatOptions.SuppressTrailingZeros = false;
                            }
                            if (formatOptions.CanSuppressLeadingZeros())
                            {
                                formatOptions.SuppressLeadingZeros = false;
                            }
                            if (formatOptions.CanUsePlusPrefix())
                            {
                                formatOptions.UsePlusPrefix = false;
                            }
                            formatOptions.UseDigitGrouping = false;
                            if (formatOptions.CanSuppressSpaces())
                            {
                                formatOptions.SuppressSpaces = true;
                            }
                            field.SetFormatOptions(formatOptions);
                        }
                    }
                }

                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
