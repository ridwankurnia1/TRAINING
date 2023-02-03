namespace TRAINING.API.Helper
{
    public class ProductionParams : BaseParams
    {
        // params for global search
        public string SearchString { get; set; }
        public string status {get; set;}
        public string remark {get; set; }
        public string filter { get; set; }
        public string linePro {get; set; } //untuk mengfilter line process
        public string defT { get; set; } // untuk mengfilter defect type
        public decimal recordStatus { get; set; }
        
    }
}