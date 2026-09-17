namespace FactoryApi.Models
{
    // Generic class for telemetry data (Point 4)
    public class TelemetryReading<T>
    {
        public string SensorName { get; set; } = string.Empty;
        public T Value { get; set; } = default!;

        public TelemetryReading(string sensorName, T value)
        {
            SensorName = sensorName;
            Value = value;
        }
    }
}
