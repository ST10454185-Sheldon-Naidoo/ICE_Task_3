namespace FactoryApi.Models
{
    public class ApiResponse
    {
        public List<MachineReading> Readings { get; set; } = new();
        public int TotalAnomalies { get; set; }
        public string HottestMachineName { get; set; } = string.Empty;
    }
}
