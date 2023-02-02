namespace TRAINING.API.Helper
{
    public class ProductionParams : BaseParams
    {
        // params for global search
        public string SearchString { get; set; }
        public string status {get; set;}
        public string remark {get; set; }
        public string filter { get; set; }

        // public string company { get; set; }
        // public string branch { get; set; }
        public decimal recordStatus { get; set; }
        
    }
}