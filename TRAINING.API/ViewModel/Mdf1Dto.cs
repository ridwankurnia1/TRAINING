using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAINING.API.ViewModel
{
    public class Mdf1Dto
    {
        public string Company { get; set; }
        public string Branch { get; set; }
        public string DefectCode { get; set; }
        public string DefectName { get; set; }
        public int IdGroup { get; set; }
        public string DefectType { get; set; }
        public string DefectGroup1 { get; set; }
        public string DefectGroup2 { get; set; }
        public string Remark { get; set; }
        public int RecordStatus { get; set; }
        public int CreateDate { get; set; }
        public int CreateTime { get; set; }
        public string CreateUser { get; set; }
        public int ChangeDate {get; set;}
        public int ChangeTime { get; set; }
        public string ChangeUser { get; set; }
        public string RecordStatusText {get; set; }

    }
}