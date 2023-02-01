namespace TRAINING.API.ViewModel
{
    public class PalletTypeExportDto
    {
        public string Company { get; set; }
        public string Branch { get; set; }
        public string PalletType { get; set; }
        public string PalletName { get; set; }
        public string PalletApp { get; set; }
        public decimal PalletLength { get; set; }
        public string LengthUm { get; set; }
        public decimal PalletWidth { get; set; }
        public string WidthUm { get; set; }
        public decimal PalletHeight { get; set; }
        public string HeightUm { get; set; }
        public decimal PalletWeight { get; set; }
        public string WeightUm { get; set; }
        public string PalletColor { get; set; }
        public string PalletCurrency { get; set; }
        public decimal PalletPrice { get; set; }
        public string CNCode { get; set; }
        public string Remark { get; set; }
        public decimal PalletCodification { get; set; }
        public string MaterialType { get; set; }
        public decimal Flag1 { get; set; }
        // public int PackingFlag { get; set; } //NONE
        public decimal CarryInFlag { get; set; }
        public decimal CarryOutFlag { get; set; }
        // public int RunOutFlag { get; set; } //NONE
        public string SystemFlag { get; set; }
        public string StatFlag { get; set; }
        public decimal RecordStatus { get; set; }
        public decimal CreatedDate { get; set; }
        public decimal CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public decimal ChangedDate { get; set; }
        public decimal ChangedTime { get; set; }
        public string ChangedUser { get; set; }
    }
}