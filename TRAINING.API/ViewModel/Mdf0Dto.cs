using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAINING.API.ViewModel
{
    public class Mdf0Dto
    {
        //buat step 2 disini
        public int TransactionId { get; set; }
        public string DefectGroup { get; set; }
        public string Remark { get; set; }
        public int RecordStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ChangeTime { get; set; }
        public string ChangeUser { get; set; }
        public string RecordStatusText {get; set; }
    }
}