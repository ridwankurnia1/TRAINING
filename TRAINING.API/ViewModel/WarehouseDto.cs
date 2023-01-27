using System;

namespace TRAINING.API.ViewModel
{
    public class WarehouseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public decimal CarryOutFlag { get; set; }
        public decimal StocktakingFlag { get; set; }
        public string DepartmentCode { get; set; }
        public string ProfitCode { get; set; }
        public string CostCenter { get; set; }
        public string DocumentCode { get; set; }
        public decimal PoliceNumber { get; set; }
        public decimal FifoFlag { get; set; }
        public decimal FifoDays { get; set; }
        public decimal TransferModelFlag { get; set; }
        public string PalletGroup { get; set; }
        public decimal QualityFlag { get; set; }
        public string Remark { get; set; }
        public string System { get; set; }
        public string Status { get; set; }
        public decimal RecordStatus { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ChangedTime { get; set; }
        public string ChangedUser { get; set; }
    }
}