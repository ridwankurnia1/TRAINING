using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TRAINING.API.ViewModel
{
    public class WarehouseGroupDto
    {
        public string Company { get; set; } = "AMG";
        public string Code { get; set; } = "CKP";
        public string Name { get; set; }
        public string Branch { get; set; }
        public string Remark { get; set; }
        public string System { get; set; }
        public string Status { get; set; }
        public decimal RecordStatus { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? ChangedTime { get; set; } = DateTime.Now;
        public string ChangedUser { get; set; }
    }
}