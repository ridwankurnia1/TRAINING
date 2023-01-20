namespace TRAINING.API.Helper
{
    public class InventoryParams : BaseParams
    {
        public string xls { get; set; }
        public string nik { get; set; }
        public string rfid { get; set; }
        public string name { get; set; }
        public string brno { get; set; }
        public string dept { get; set; }
        public string grade { get; set; }
        public string filter { get; set; }
        public string filled { get; set; }
        public string unfilled { get; set; }
        public string mustcheck { get; set; }
        public string noneedcheck { get; set; }
        public string alreadycheck { get; set; }
        public string notyetcheck { get; set; }
        public int attendance { get; set; }
        public int id { get; set; }

        public decimal status {get; set;}

        public string remark {get; set; }
    }
}