using System.ComponentModel.DataAnnotations.Schema;

namespace TRAINING.API.Model
{
    [Table("VMAIN_MASTER_EMPLOYEE", Schema = "AHAPS")]
    public class EMPLOYEE
    {
        public string NIK { get; set; }
        public string NAME { get; set; }
        public string SEX { get; set; }
        public string BIRTHPLACE { get; set; }
        public string ORGANIZATIONSTRUCTURE { get; set; }
        public string GRADECODE { get; set; }
        public string MOBILEPHONE { get; set; }
    }
}