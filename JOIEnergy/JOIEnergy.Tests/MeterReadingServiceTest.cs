using System;
using System.Collections.Generic;
using JOIEnergy.Services;
using JOIEnergy.Domain;
using Xunit;

namespace JOIEnergy.Tests
{
    public class MeterReadingServiceTest
    {
        private static string SMART_METER_ID = "smart-meter-id";

        private MeterReadingService meterReadingService;

        public MeterReadingServiceTest()
        {
            meterReadingService = new MeterReadingService(new Dictionary<string, List<ElectricityReading>>());

            meterReadingService.StoreReadings(SMART_METER_ID, new List<ElectricityReading>() {
                new ElectricityReading() { Time = DateTime.Now.AddMinutes(-30), Reading = 35m },
                new ElectricityReading() { Time = DateTime.Now.AddMinutes(-15), Reading = 30m }
            });

            meterReadingService.StoreReadings("smart_meter_lastweek", new List<ElectricityReading>() {
                new ElectricityReading() { Time = DateTime.Now.AddMinutes(-30), Reading = 35m },
                new ElectricityReading() { Time = DateTime.Now.AddMinutes(-15), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-50), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-50), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-50), Reading = 30m }
            });
        }
        

        [Fact]
        public void GivenMeterIdThatDoesNotExistShouldReturnNull() {

            Assert.Empty(meterReadingService.GetReadings("unknown-id"));
        }

        [Fact]
        public void GivenMeterIdThatReturnLastWeeksData()
        {

            var expected = new List<ElectricityReading>() {
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m },
                new ElectricityReading() { Time = DateTime.Now.AddDays(-7), Reading = 30m }
            };

            var  records =  meterReadingService.GetReadingsForLastWeek("smart_meter_lastweek");
            Assert.Equal(4,records.Count);

        }

        [Fact]
        public void GivenDateSholdReturnTrueIfDateIsLastWeek()
        {

            Assert.True(meterReadingService.IsLastWeek(new DateTime(2021,6,10)));
            Assert.False(meterReadingService.IsLastWeek(new DateTime(2021, 6,1)));
            Assert.False(meterReadingService.IsLastWeek(new DateTime(2021, 6,17)));

        }

        [Fact]
        public void GivenDateSholdReturnTrueIfDateIsLastWeek2()
        {

            Assert.True(meterReadingService.IsLastWeek(new DateTime(2021, 6, 6)));
            Assert.True(meterReadingService.IsLastWeek(new DateTime(2021, 6, 12)));

        }


        [Fact]
        public void GivenMeterReadingThatExistsShouldReturnMeterReadings()
        {
            meterReadingService.StoreReadings(SMART_METER_ID, new List<ElectricityReading>() {
                new ElectricityReading() { Time = DateTime.Now, Reading = 25m }
            });

            var electricityReadings = meterReadingService.GetReadings(SMART_METER_ID);

            Assert.Equal(3, electricityReadings.Count);
        }

    }
}
