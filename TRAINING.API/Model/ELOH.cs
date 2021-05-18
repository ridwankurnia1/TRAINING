using System;
using System.Collections.Generic;

namespace TRAINING.API.Model
{
    public class ELOH
    {
        public int EHRCID { get; set; }
        public string EHDESC { get; set; }
        public decimal EHRCST { get; set; }
        public DateTime? EHCRDT { get; set; }
        public string EHCRUS { get; set; }
        public DateTime? EHCHDT { get; set; }
        public string EHCHUS { get; set; }

        public virtual ICollection<ELOG> ELOG { get; set; }
    }
}