using Autodesk.Revit.DB;

namespace DougKlassen.Pegboard.Models
{
    internal class ScheduleModel
    {
        public String name;
        public long id;
        public List<long> parameters;

        public ScheduleModel(ViewSchedule sched)
        {
            name = sched.Name;
            id = sched.Id.Value;

            parameters = new List<long>();
            IList<ScheduleFieldId> fieldIds = sched.Definition.GetFieldOrder();
            foreach (ScheduleFieldId id in fieldIds)
            {
                ScheduleField field = sched.Definition.GetField(id);

                parameters.Add(field.ParameterId.Value);
            }
        }
    }
}
