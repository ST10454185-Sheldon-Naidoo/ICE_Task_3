using FactoryApi.Models;

namespace FactoryApi.Logic
{
    // Anomaly class applying specified rules (Point 7)
    public class Anomaly
    {
        public static bool IsAnomaly(MachineReading reading)
        {
            return reading.Temperature.Value > 85.0 ||
                   reading.RotationSpeed.Value > 4500 ||
                   !reading.IsOperational.Value;
        }
    }
}
