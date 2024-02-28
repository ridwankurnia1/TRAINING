using System;

namespace TRAINING.API.ViewModel
{
    public class TruckDto
    {
          public int TruckId { get; set; }
        public string TruckName { get; set; }
        public string Merk { get; set; }
        public string Driver { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public decimal RecordStatus { get; set; }

    }
}