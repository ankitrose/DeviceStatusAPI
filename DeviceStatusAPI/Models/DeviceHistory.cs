using System;

namespace DeviceStatusAPI.Models
{
    public class DeviceHistory
    {
        public int HistoryID { get; set; }
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
