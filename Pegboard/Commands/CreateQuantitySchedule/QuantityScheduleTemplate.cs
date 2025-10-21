using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Commands
{
    /// <summary>
    /// A model of a standard quantity schedule template containing information to generate a new schedule
    /// </summary>
    public class QuantityScheduleTemplate
    {
        /// <summary>
        /// The category of Elements that will be scheduled. This value is taken from the name of the category in the Revit API BuiltInParams enumeration
        /// </summary>
        public String ElementCategory { get; set; }
        /// <summary>
        /// The name of the Parameter that will be used to filter the schedule. A single filter will be added to the schedule, with Filter By set to this parameter
        /// </summary>
        public String FilterParameterName { get; set; }
        /// <summary>
        /// This value will be used in the schedule filter. Only elements who's filter parameter value matches this value
        /// will be included in the schedule
        /// </summary>
        public String FilterParameterValue { get; set; }
        /// <summary>
        /// Further description of the meaning of the value of FilterParameterValue. Combined with FilterParameterValue to name the schedule
        /// </summary>
        public String FilterParameterValueLabel { get; set; }
        /// <summary>
        /// All of the fields included in the schedule
        /// </summary>
        public List<QuantityScheduleField> Fields { get; set; }
        /// <summary>
        /// Initialize a new QuantityScheduleTemplate
        /// </summary>
        public QuantityScheduleTemplate()
        {
            Fields = new List<QuantityScheduleField>();
        }

        /// <summary>
        /// Get description for schedule including its filter parameter and how many fields it contains
        /// </summary>
        /// <returns>A text summary of the schedule</returns>
        public String GetDescription()
        {
            String catLabel;
            try
            {
                BuiltInCategory cat = (BuiltInCategory)Enum.Parse(typeof(BuiltInCategory), ElementCategory);
                catLabel = LabelUtils.GetLabelFor(cat);
            }
            catch (Exception)
            {
                catLabel = ElementCategory;
            }
            String desc = String.Format("{0}: {1} ({2}) - {3} fields", FilterParameterValue, FilterParameterValueLabel, catLabel, Fields.Count);
            return desc;
        }
    }
}
