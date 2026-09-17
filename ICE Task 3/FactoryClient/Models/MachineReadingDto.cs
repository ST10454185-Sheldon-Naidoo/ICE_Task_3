using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryClient.Models
{
    public class MachineReadingDto
    {
        public string MachineId { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public TelemetryValue<double> Temperature { get; set; } = new();
        public TelemetryValue<int> RotationSpeed { get; set; } = new();
        public TelemetryValue<bool> IsOperational { get; set; } = new();
        public DateTime Timestamp { get; set; }

        public double TempValue => Temperature.Value;
        public int SpeedValue => RotationSpeed.Value;
        public bool OperationalValue => IsOperational.Value;
        public bool HasAnomaly => TempValue > 85.0 || SpeedValue > 4500 || !OperationalValue;
    }
}
