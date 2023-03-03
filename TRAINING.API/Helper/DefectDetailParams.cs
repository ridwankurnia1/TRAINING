namespace TRAINING.API.Helper
{
    public class DefectDetailParams : BaseParams
    {
        public string filter { get; set; }
        public string status {get; set;}
        public string remark {get; set; }
        public string defName { get; set; }
        public string defType {get; set;}
    }
}