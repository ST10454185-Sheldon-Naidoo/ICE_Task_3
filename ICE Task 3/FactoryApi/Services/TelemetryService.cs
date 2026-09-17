using FactoryApi.Logic;
using FactoryApi.Models;

namespace FactoryApi.Services
{
    public class TelemetryService
    {
        // 2D Array storing Machine ID, Name, and Location (Point 5)
        private readonly string[,] _machines = new string[,]
        {
        { "M-101", "CNC Lathe", "Sector A" },
        { "M-102", "Robotic Arm", "Sector B" },
        { "M-103", "Hydraulic Press", "Sector C" },
        { "M-104", "Conveyor Belt", "Sector A" }
        };

        private readonly Random _rand = new();

        // Generates readings stored in a List (Point 2 and 6)
        public List<MachineReading> GenerateReadings()
        {
            List<MachineReading> readings = new();
            int machineCount = _machines.GetLength(0);

            for (int i = 0; i < machineCount; i++)
            {
                readings.Add(new MachineReading
                {
                    MachineId = _machines[i, 0],
                    MachineName = _machines[i, 1],
                    Location = _machines[i, 2],
                    Temperature = new TelemetryReading<double>("Temperature", Math.Round(60.0 + _rand.NextDouble() * 35.0, 2)),
                    RotationSpeed = new TelemetryReading<int>("RotationSpeed", _rand.Next(2000, 5500)),
                    IsOperational = new TelemetryReading<bool>("IsOperational", _rand.Next(0, 10) > 1),
                    Timestamp = DateTime.Now
                });
            }

            return readings;
        }

        // Recursive method counting all anomalies (Point 8)
        public int CountAnomaliesRecursive(List<MachineReading> readings, int index = 0)
        {
            if (index >= readings.Count)
                return 0;

            int currentCount = Anomaly.IsAnomaly(readings[index]) ? 1 : 0;
            return currentCount + CountAnomaliesRecursive(readings, index + 1);
        }

        // Recursive method to find highest temperature using overloaded operator (Point 10)
        public MachineReading? FindHottestMachineRecursive(List<MachineReading> readings, int index = 0)
        {
            if (readings == null || readings.Count == 0)
                return null;

            if (index == readings.Count - 1)
                return readings[index];

            MachineReading? maxInRest = FindHottestMachineRecursive(readings, index + 1);

            if (maxInRest == null || readings[index] > maxInRest)
                return readings[index];

            return maxInRest;
        }
    }
}
