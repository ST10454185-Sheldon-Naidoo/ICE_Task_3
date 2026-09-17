using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryClient.Models
{
    public class TelemetryValue<T>
    {
        public string SensorName { get; set; } = string.Empty;
        public T Value { get; set; } = default!;
    }
}
