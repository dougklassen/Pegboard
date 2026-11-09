using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace DougKlassen.Pegboard.Models
{
    internal class ScheduleModel
    {
        public string name;
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
