using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAINING.API.ViewModel
{
    public class MdmpDto
    {
        public string Company { get; set; }
        public string Branch { get; set; }
        public string DefectType { get; set; }
        public string LineProcessGroup { get; set; }
        public string DefectCode { get; set; }
        public string DefectPosition { get; set; }
        public string DefectGroup { get; set; }
        public int DefectPostX { get; set; }
        public int DefectPostY { get; set; }
        public string Remark { get; set; }
        public int RecordStatus { get; set; }
        public int CreateDate { get; set; }
        public int CreateTime { get; set; }
        public string CreateUser { get; set; }
        public int ChangeDate { get; set; }
        public int ChangeTime { get; set; }
        public string ChangeUser { get; set; }
        public string RecordStatusText {get; set; }
    }
}