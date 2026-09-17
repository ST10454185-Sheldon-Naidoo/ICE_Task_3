using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryClient.Models
{
    public class ApiResponseDto
    {
        public List<MachineReadingDto> Readings { get; set; } = new();
        public int TotalAnomalies { get; set; }
        public string HottestMachineName { get; set; } = string.Empty;
    }
}
