using FactoryClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactoryClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly TelemetryApiClient _apiClient;
        private DataGridView dgvReadings = null!;
        private Button btnRefresh = null!;
        private Label lblAnomalies = null!;
        private Label lblHottest = null!;

        public MainForm()
        {
            _apiClient = new TelemetryApiClient("http://localhost:5069");
            InitializeComponent();
            InitializeComponentsCustom();
        }
        private void InitializeComponentsCustom()
        {
            Text = "Factory Machine Telemetry Monitor";
            Width = 800;
            Height = 500;

            btnRefresh = new Button
            {
                Text = "Request New Readings",
                Top = 15,
                Left = 15,
                Width = 180,
                Height = 35
            };
            // Button triggers fresh API fetch (Point 14)
            btnRefresh.Click += async (s, e) => await LoadTelemetryDataAsync();

            lblAnomalies = new Label
            {
                Text = "Total Anomalies: -",
                Top = 22,
                Left = 210,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            lblHottest = new Label
            {
                Text = "Hottest Machine: -",
                Top = 22,
                Left = 400,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };

            // DataGridView setup (Point 12)
            dgvReadings = new DataGridView
            {
                Top = 60,
                Left = 15,
                Width = 750,
                Height = 370,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            Controls.Add(btnRefresh);
            Controls.Add(lblAnomalies);
            Controls.Add(lblHottest);
            Controls.Add(dgvReadings);

            Load += async (s, e) => await LoadTelemetryDataAsync();
        }

        private async Task LoadTelemetryDataAsync()
        {
            btnRefresh.Enabled = false;
            try
            {
                var result = await _apiClient.GetTelemetryDataAsync();

                if (result != null)
                {
                    // Binds data to DataGridView (Point 12)
                    dgvReadings.DataSource = result.Readings.Select(r => new
                    {
                        r.MachineId,
                        r.MachineName,
                        r.Location,
                        Temperature = $"{r.TempValue} °C",
                        RotationSpeed = $"{r.SpeedValue} RPM",
                        IsOperational = r.OperationalValue,
                        r.Timestamp,
                        IsAnomaly = r.HasAnomaly
                    }).ToList();

                    // Displays anomaly count and hottest machine name (Point 13)
                    lblAnomalies.Text = $"Total Anomalies: {result.TotalAnomalies}";
                    lblHottest.Text = $"Hottest Machine: {result.HottestMachineName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching data from API: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }
    }
}
