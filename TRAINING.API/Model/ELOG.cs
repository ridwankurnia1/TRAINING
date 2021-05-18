using System;

namespace TRAINING.API.Model
{
    public class ELOG
    {
        public int ELTRID { get; set; }
        public int ELRCID { get; set; }
        public string ELEMNO { get; set; }
        public string ELEMNA { get; set; }
        public string ELBRNO { get; set; }
        public string ELRFID { get; set; }
        public string ELDENO { get; set; }
        public string ELDENA { get; set; }
        public DateTime? ELSCDT { get; set; }
        public DateTime? ELTRDT { get; set; }

        public virtual ELOH ELOH { get; set; }
    }
}