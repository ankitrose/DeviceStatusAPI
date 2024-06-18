using System;
using System.ComponentModel.DataAnnotations;

namespace DeviceStatusAPI.Models
{
    public class Device
    {
        public int DeviceID { get; set; }

        [Required]
        [StringLength(100)]
        public string DeviceName { get; set; }

        [Required]
        public string Status { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
