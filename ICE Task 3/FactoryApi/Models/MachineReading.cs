namespace FactoryApi.Models
{
    // Machine Reading model (Point 3)
    public class MachineReading
    {
        public string MachineId { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public TelemetryReading<double> Temperature { get; set; } = null!;
        public TelemetryReading<int> RotationSpeed { get; set; } = null!;
        public TelemetryReading<bool> IsOperational { get; set; } = null!;
        public DateTime Timestamp { get; set; }

        // Overloads > and < operators for temperature comparison (Point 9)
        public static bool operator >(MachineReading a, MachineReading b)
        {
            return a.Temperature.Value > b.Temperature.Value;
        }

        public static bool operator <(MachineReading a, MachineReading b)
        {
            return a.Temperature.Value < b.Temperature.Value;
        }
    }
}
