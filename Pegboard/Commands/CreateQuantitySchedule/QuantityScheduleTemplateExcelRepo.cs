using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace DougKlassen.Pegboard.Commands
{
    internal class QuantityScheduleTemplateExcelRepo : IQuantityScheduleTemplateRepo
    {
        private string filePath;
        private string filterParamColumnName;
        private string labelColumnName;
        private string unitsColumnName;
        private string elementCategoryColumnName;
        private string projectParamColumnName;
        private string builtInParamColumnName;
        private string calculationColumnName;

        private int filterParamColumn = -1;
        private int labelColumn = -1;
        private int unitsColumn = -1;
        private int elementCategoryColumn = -1;
        private int projectParamColumn = -1;
        private int builtInParamColumn = -1;
        private int calculationColumn = -1;

        public QuantityScheduleTemplateExcelRepo(
            string repoFilePath,
            string repoFilterParamColumn,
            string repoLabelColumn,
            string repoUnitsColumn,
            string repoElementCategoryColumn,
            string repoProjectParamColumn,
            string repoBuiltInParamColumn,
            string repoCalculationColumn)
        {
            filePath = repoFilePath;
            filterParamColumnName = repoFilterParamColumn;
            labelColumnName = repoLabelColumn;
            unitsColumnName = repoUnitsColumn;
            elementCategoryColumnName = repoElementCategoryColumn;
            projectParamColumnName = repoProjectParamColumn;
            builtInParamColumnName = repoBuiltInParamColumn;
            calculationColumnName = repoCalculationColumn;
        }

        public List<QuantityScheduleTemplate> LoadTemplates()
        {
            List<QuantityScheduleTemplate> templates = new List<QuantityScheduleTemplate>();
            Dictionary<int, string> parseErrors = new Dictionary<int, string>();

            IWorkbook workBook;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workBook = new XSSFWorkbook(fs);
            }

            ISheet sheet = workBook.GetSheetAt(0);
            //Find the columns specified by column names. Columns are identified by their first row header titles
            IRow headerRow = sheet.GetRow(0);
            for (int i = 0; i < headerRow.LastCellNum; i++)
            {
                string val = ParseCellToString(headerRow.GetCell(i));

                if (val == filterParamColumnName)
                {
                    filterParamColumn = i;
                }
                else if (val == labelColumnName)
                {
                    labelColumn = i;
                }
                else if (val == unitsColumnName)
                {
                    unitsColumn = i;
                }
                else if (val == elementCategoryColumnName)
                {
                    elementCategoryColumn = i;
                }
                else if (val == projectParamColumnName)
                {
                    projectParamColumn = i;
                }
                else if (val == builtInParamColumnName)
                {
                    builtInParamColumn = i;
                }
                else if (val == calculationColumnName)
                {
                    calculationColumn = i;
                }
            }
            if (
                filterParamColumn == -1 ||
                labelColumn == -1 ||
                unitsColumn == -1 ||
                elementCategoryColumn == -1 ||
                projectParamColumn == -1 ||
                builtInParamColumn == -1 ||
                calculationColumn == -1)
            {
                throw new QuantityScheduleParseException("A required column was not found in the spreadsheet");
            }
            //The filterParamName is defined in the header of the filterParamColumn
            string filterParamName = headerRow.GetCell(filterParamColumn).StringCellValue.Trim();

            //iterate through all rows after the heading row, starting a new template each time a new filter parameter value is encountered
            IRow row;
            int rowLength = sheet.GetRow(1).Cells.Count;
            QuantityScheduleTemplate currentTemplate = null;
            for (int r = 1; r < sheet.LastRowNum; r++)
            {
                row = sheet.GetRow(r);

                //skip empty rows
                if (row == null)
                {
                    continue;
                }

                #region row analysis
                //if (row.Cells == null || row.Cells.Count < rowLength)
                ////if (true)
                //{
                //    //show a diagnostic message the row
                //    String msg = "Row: " + (r + 1);
                //    msg += "\n physical number of cells: " + row.PhysicalNumberOfCells;
                //    if (row.Collapsed.HasValue)
                //    {
                //        msg += "\n collapsed?: " + row.Collapsed.Value;
                //    }
                //    else
                //    {
                //        msg += "\n collapsed?: no value";
                //    }
                //    msg += "\n first cell num: " + row.FirstCellNum;
                //    msg += "\n last cell num: " + row.LastCellNum;
                //    if (row.Hidden.HasValue)
                //    {
                //        msg += "\n hidden?: " + row.Hidden.Value;
                //    }
                //    else
                //    {
                //        msg += "\n hidden?: no value";
                //    }
                //    msg += "\n outline level: " + row.OutlineLevel;
                //    msg += "\n";
                //    if (row != null)
                //    {
                //        for (int c = 0; c < row.Cells.Count; c++)
                //        {
                //            ICell cell = row.GetCell(c);
                //            String cellVal;
                //            if (cell != null)
                //            {
                //                if (cell.CellType == CellType.String)
                //                {
                //                    cellVal = cell.StringCellValue.Trim();
                //                    if (cellVal == String.Empty)
                //                    {
                //                        cellVal = "Empty String";
                //                    }
                //                }
                //                else if (cell.CellType == CellType.Numeric)
                //                {
                //                    cellVal = cell.NumericCellValue.ToString();
                //                }
                //                else
                //                {
                //                    cellVal = "*" + Enum.GetName(typeof(CellType), cell.CellType) + "*";
                //                }
                //            }
                //            else
                //            {
                //                cellVal = "*NULL*";
                //            }
                //            msg += String.Format("\n   Cell {0}: {1}", c, cellVal);
                //        }
                //    }
                //    else
                //    {
                //        msg += "\n   *NULL ROW*";
                //    }
                //    Autodesk.Revit.UI.TaskDialog.Show("Corrupt Row", msg);
                //    continue;
                //}
                #endregion row analysis

                string filterParamVal = ParseCellToString(row.GetCell(filterParamColumn));
                //if an entry is found in the filterParamColumn start a new template
                if (filterParamVal != string.Empty)
                {
                    //add the last template found to the dictionary. currentTemplate is null when no template has been parsed yet.
                    if (currentTemplate != null)
                    {
                        //skip instances where ElementCategory is undefined
                        if (currentTemplate.ElementCategory != null)
                        {
                            //skip instances where no fields are defined
                            if (currentTemplate.Fields.Count > 0)
                            {
                                templates.Add(currentTemplate);
                            }
                        }
                    }
                    currentTemplate = new QuantityScheduleTemplate();
                    currentTemplate.FilterParameterName = filterParamName;
                    currentTemplate.FilterParameterValue = filterParamVal;
                    string elementCategoryValue = ParseCellToString(row.GetCell(elementCategoryColumn));
                    if (elementCategoryValue != string.Empty)
                    {
                        currentTemplate.ElementCategory = elementCategoryValue;
                    }
                    else
                    {
                        currentTemplate.ElementCategory = null;
                    }
                    string filterParamValueLabel = ParseCellToString(row.GetCell(labelColumn));
                    currentTemplate.FilterParameterValueLabel = filterParamValueLabel;
                }
                //if no new filterParam value is found, parse the row into a new schedule field to be added to the template
                else
                {
                    QuantityScheduleField currentField = new QuantityScheduleField();
                    //parse the label
                    string labelValue = ParseCellToString(row.GetCell(labelColumn));
                    if (labelValue != string.Empty)
                    {
                        currentField.Label = labelValue;
                        string unitsValue = ParseCellToString(row.GetCell(unitsColumn));
                        //only parse units if the field has a label
                        if (unitsValue != string.Empty)
                        {
                            currentField.Units = unitsValue;
                        }
                    }
                    //parse the field 
                    string calculationValue = ParseCellToString(row.GetCell(calculationColumn));
                    string builtInParamValue = ParseCellToString(row.GetCell(builtInParamColumn));
                    string projectParamValue = ParseCellToString(row.GetCell(projectParamColumn));
                    int sourcesInRow = 0; //track how many values are present
                    if (projectParamValue != string.Empty)
                    {
                        sourcesInRow++;
                        currentField.FieldValue = projectParamValue;
                        currentField.Type = FieldType.ProjectParameter;
                    }
                    if (builtInParamValue != string.Empty)
                    {
                        sourcesInRow++;
                        currentField.FieldValue = builtInParamValue;
                        currentField.Type = FieldType.BuiltInParameter;
                    }
                    if (calculationValue != string.Empty)
                    {
                        sourcesInRow++;
                        currentField.FieldValue = calculationValue;
                        currentField.Type = FieldType.Calculation;
                    }

                    //check how many values are present. There should be no more than one
                    if (sourcesInRow == 1)
                    {
                        currentTemplate.Fields.Add(currentField);
                    }
                    else if (sourcesInRow > 1)
                    {
                        parseErrors.Add(r, "Only one source can be specified for a field");
                    }
                    //if sourcesInRow == 0, don't add the row
                }
            }

            //if no parse errors occured, return the list of templates
            if (parseErrors.Count == 0)
            {
                return templates;
            }
            //if parse errors occured, generate an error file and throw an exception
            else
            {
                string msg = string.Empty;
                foreach (int rowNum in parseErrors.Keys)
                {
                    msg += string.Format("  Row {0}: {1}\n", rowNum, parseErrors[rowNum]);
                }

                throw new QuantityScheduleParseException(msg);
            }
        }

        public void WriteTemplates(List<QuantityScheduleTemplate> templates)
        {
            throw new NotImplementedException();
        }

        public static string ParseCellToString(ICell cell)
        {
            string val;

            if (cell == null)
            {
                val = string.Empty;
            }
            else if (cell.CellType == CellType.String)
            {
                val = cell.StringCellValue.Trim();
            }
            else if (cell.CellType == CellType.Numeric)
            {
                val = cell.NumericCellValue.ToString();
            }
            else
            {
                val = string.Empty;
            }

            return val;
        }
    }
}
