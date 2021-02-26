using System;

namespace TRAINING.API.ViewModel
{
    public class EmployeeDto
    {
        public string Nik { get; set; }
        public string Nama { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string Grade { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}