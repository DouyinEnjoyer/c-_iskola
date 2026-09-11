using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using RoverTelemetry;

namespace RoverTelemetry.Tests
{
    public class TelemetryServiceTests
    {
        private readonly TelemetryService _service = new TelemetryService();

        #region Sorelemzési tesztek

        [Fact]
        public void ParseLine_ValidLine_ReturnsCorrectObject()
        {
            string line = "M2024;Opportunity;2023.11.01;220;Igaz";

            var result = TelemetryParser.ParseLine(line);

            Assert.Equal("M2024", result.Id);
            Assert.Equal("Opportunity", result.RoverName);
            Assert.Equal(new DateTime(2023, 11, 1), result.Date);
            Assert.Equal(220, result.Distance);
            Assert.True(result.IsSuccessful);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("M2024;Opportunity;2023.11.01")]
        public void ParseLine_InvalidLine_ThrowsArgumentException(string invalidLine)
        {
            Assert.Throws<ArgumentException>(() => TelemetryParser.ParseLine(invalidLine));
        }

        #endregion

        #region Fájlkezelési tesztek (I/O)

        [Fact]
        public void ReadAllRecords_ValidFile_ReturnsParsedRecords()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                string content = "Azonosito;RoverNeve;MeresIdeje;MegtettUt_Meter;Sikeres\n" +
                                 "M2024;Opportunity;2023.11.01;220;Igaz\n" +
                                 "M2025;Curiosity;2023.11.02;0;Hamis";
                File.WriteAllText(tempFile, content);

                var records = TelemetryFileReader.ReadAllRecords(tempFile);

                Assert.Equal(2, records.Count);
                Assert.Equal("M2024", records[0].Id);
                Assert.Equal("Curiosity", records[1].RoverName);
                Assert.False(records[1].IsSuccessful);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
            }
        }

        [Fact]
        public void ReadAllRecords_NonExistingFile_ThrowsFileNotFoundException()
        {
            string nonExistingPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

            Assert.Throws<FileNotFoundException>(() => TelemetryFileReader.ReadAllRecords(nonExistingPath));
        }

        [Fact]
        public void WriteSuccessfulRecords_WritesCorrectHeaderAndData()
        {
            string tempFile = Path.GetTempFileName();
            try
            {
                var records = new List<TelemetryRecord>
                {
                    new TelemetryRecord
                    {
                        Id = "M2024",
                        RoverName = "Opportunity",
                        Date = new DateTime(2023, 11, 1),
                        Distance = 220,
                        IsSuccessful = true
                    }
                };

                TelemetryFileReader.WriteSuccessfulRecords(tempFile, records);

                var lines = File.ReadAllLines(tempFile);
                Assert.Equal(2, lines.Length);
                Assert.Equal("Azonosito;RoverNeve;MeresIdeje;MegtettUt_Meter;Sikeres", lines[0]);
                Assert.Equal("M2024;Opportunity;2023.11.01;220;Igaz", lines[1]);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
            }
        }

        #endregion

        #region Összesítési tesztek

        [Fact]
        public void GetTotalMeasurementCount_ValidList_ReturnsCorrectCount()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { Id = "M1" },
                new TelemetryRecord { Id = "M2" }
            };

            Assert.Equal(2, _service.GetTotalMeasurementCount(records));
        }

        [Fact]
        public void GetTotalDistance_ValidList_ReturnsSummedMeters()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { Distance = 100 },
                new TelemetryRecord { Distance = 250 },
                new TelemetryRecord { Distance = 50 }
            };

            Assert.Equal(400, _service.GetTotalDistance(records));
        }

        [Fact]
        public void GetLongestDailyDistanceRecord_FindsRecordWithMaxDistance()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { Id = "M1", Distance = 100, RoverName = "Curiosity" },
                new TelemetryRecord { Id = "M2", Distance = 340, RoverName = "Perseverance" },
                new TelemetryRecord { Id = "M3", Distance = 200, RoverName = "Opportunity" }
            };

            var maxRecord = _service.GetLongestDailyDistanceRecord(records);

            Assert.NotNull(maxRecord);
            Assert.Equal("M2", maxRecord.Id);
            Assert.Equal("Perseverance", maxRecord.RoverName);
            Assert.Equal(340, maxRecord.Distance);
        }

        #endregion

        #region Keresési és szûrési tesztek

        [Fact]
        public void GetSuccessfulMissionCountByRover_ExistingRover_ReturnsSuccessfulCount()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { RoverName = "Curiosity", IsSuccessful = true },
                new TelemetryRecord { RoverName = "Curiosity", IsSuccessful = false },
                new TelemetryRecord { RoverName = "Curiosity", IsSuccessful = true },
                new TelemetryRecord { RoverName = "Opportunity", IsSuccessful = true }
            };

            int count = _service.GetSuccessfulMissionCountByRover(records, "Curiosity");

            Assert.Equal(2, count);
        }

        [Fact]
        public void GetSuccessfulMissionCountByRover_NonExistingRover_ThrowsKeyNotFoundException()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { RoverName = "Opportunity", IsSuccessful = true }
            };

            Assert.Throws<KeyNotFoundException>(() =>
                _service.GetSuccessfulMissionCountByRover(records, "Spirit"));
        }

        [Fact]
        public void GetMeasurementCountPerRover_GroupsCorrectly()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { RoverName = "Curiosity" },
                new TelemetryRecord { RoverName = "Opportunity" },
                new TelemetryRecord { RoverName = "Curiosity" }
            };

            var stats = _service.GetMeasurementCountPerRover(records);

            Assert.Equal(2, stats["Curiosity"]);
            Assert.Equal(1, stats["Opportunity"]);
        }

        [Fact]
        public void GetSuccessfulRecords_FiltersOnlySuccessfulMissions()
        {
            var records = new List<TelemetryRecord>
            {
                new TelemetryRecord { Id = "M1", IsSuccessful = true },
                new TelemetryRecord { Id = "M2", IsSuccessful = false },
                new TelemetryRecord { Id = "M3", IsSuccessful = true }
            };

            var successful = _service.GetSuccessfulRecords(records);

            Assert.Equal(2, successful.Count);
            Assert.All(successful, r => Assert.True(r.IsSuccessful));
        }

        #endregion
    }
}