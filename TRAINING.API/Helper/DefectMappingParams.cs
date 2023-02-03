using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAINING.API.Helper
{
    public class DefectMappingParams : BaseParams
    {
        public string defT { get; set; } //filter defect Type
        public string lineP { get; set; } // filter line process  
    }
}