using System.ComponentModel.DataAnnotations.Schema;

namespace TRAINING.API.Model
{
    [Table("KTPA02", Schema = "AKTLIBT")]
    public class KTPA02
    {
        public string RECTA02 { get; set; }
        public string DONOA02 { get; set; }
        public string DODTA02 { get; set; }
        public string DLNOA02 { get; set; }
    }
}