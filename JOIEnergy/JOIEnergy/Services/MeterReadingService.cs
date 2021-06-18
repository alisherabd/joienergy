using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
        public MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
        {
            MeterAssociatedReadings = meterAssociatedReadings;
        }

        public List<ElectricityReading> GetReadings(string smartMeterId) {
            if (MeterAssociatedReadings.ContainsKey(smartMeterId)) {
                return MeterAssociatedReadings[smartMeterId];
            }
            return new List<ElectricityReading>();
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings) {
            if (!MeterAssociatedReadings.ContainsKey(smartMeterId)) {
                MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
            }

            electricityReadings.ForEach(electricityReading => MeterAssociatedReadings[smartMeterId].Add(electricityReading));
        }

        public List<ElectricityReading> GetReadingsForLastWeek(string smartMeterId)
        {
            if (MeterAssociatedReadings.ContainsKey(smartMeterId))
            {
                return MeterAssociatedReadings[smartMeterId].Where(x=>IsLastWeek(x.Time)).ToList();
            }
            return new List<ElectricityReading>();
        }

        public bool IsLastWeek(DateTime date)
        {
            var dayOfWeek = (int)DateTime.Now.DayOfWeek;
            var now = DateTime.Now;
            var daysOfWeek = 7;
            var firstDayOfLastWeek = now.AddDays(-dayOfWeek - daysOfWeek);
            var lastDayOfLastWeek = now.AddDays(-dayOfWeek - 1);

            return (date >= firstDayOfLastWeek && date <= lastDayOfLastWeek);
        }
    }
}
