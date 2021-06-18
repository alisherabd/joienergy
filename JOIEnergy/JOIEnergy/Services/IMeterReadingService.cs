using System;
using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);
        List<ElectricityReading> GetReadingsForLastWeek(string smartMeterId);
        bool IsLastWeek(DateTime date);
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
                    
    }
}