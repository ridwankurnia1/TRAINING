using System;

namespace TRAINING.API.Helper
{
    public class Params
    {
        private const int MaxPageSize = 1000;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        
        public string nik { get; set; }
        public string rfid { get; set; }
        public string name { get; set; }
        public string brno { get; set; }
        public string dept { get; set; }
        public string grade { get; set; }
        public string filter { get; set; }
        public string Filled { get; set; }
        public string Unfilled { get; set; }
        public string MustCheck { get; set; }
        public string NoNeedCheck { get; set; }
        public string AlreadyCheck { get; set; }
        public string NotYetCheck { get; set; }
    }
}