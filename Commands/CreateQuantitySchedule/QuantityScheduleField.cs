namespace DougKlassen.Pegboard.Commands
{
    /// <summary>
    /// The definition of a field in the schedule. The column heading for the field will be in the format Label (Units)
    /// </summary>
    public class QuantityScheduleField
    {
        /// <summary>
        /// The description label for the field
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// The unit label for the field
        /// </summary>
        public string Units { get; set; }
        /// <summary>
        /// The units of value displayed by the field. Either a user defined project parameter, a built in parameter, or a calculation based on the 
        /// </summary>
        public FieldType Type { get; set; }
        /// <summary>
        /// The source for the fields value. This will be either the name of a parameter value or a calculation, as specified by Type
        /// </summary>
        public string FieldValue { get; set; }
    }

    public enum FieldType
    {
        ProjectParameter,
        BuiltInParameter,
        Calculation
    }
}
