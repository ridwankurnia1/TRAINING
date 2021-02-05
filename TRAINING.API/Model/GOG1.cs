using System.Collections.Generic;

namespace TRAINING.API.Model
{
    public class GOG1
    {
        public string GOCONO { get; set; }
        public string GOBRNO { get; set; }
        public string GOOGNO { get; set; }
        public string GOOGNA { get; set; }
        public string GONICK { get; set; }
        public string GOSENO { get; set; }
        public decimal GOOGLV { get; set; }
        public string GOOGUP { get; set; }
        public string GOOG01 { get; set; }
        public string GOOG02 { get; set; }
        public string GOOG03 { get; set; }
        public string GOOG04 { get; set; }
        public string GOOG05 { get; set; }
        public string GOCCNO { get; set; }
        public decimal GOAVQT { get; set; }
        public string GOREMA { get; set; }
        public decimal GORCST { get; set; }
        public decimal GOCRDT { get; set; }
        public decimal GOCRTM { get; set; }
        public string GOCRUS { get; set; }
        public decimal GOCHDT { get; set; }
        public decimal GOCHTM { get; set; }
        public string GOCHUS { get; set; }

        public virtual ICollection<MEMP> MEMP { get; set; }
    }
}