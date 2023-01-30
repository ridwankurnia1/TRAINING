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

        public string status {get; set;}
        public string remark {get; set; }

        public int rcst { get; set; }
    }
}