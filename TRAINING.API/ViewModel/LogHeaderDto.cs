using System;

namespace TRAINING.API.ViewModel
{
    public class LogHeaderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal DuplicateFlag { get; set; }
        public decimal PredefinedFlag { get; set; }
        public decimal Status { get; set; }
        public int Count { get; set; }
    }
}