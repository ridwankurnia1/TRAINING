using System;

namespace TRAINING.API.ViewModel
{
    public class EmployeeDto
    {
        public string Nik { get; set; }
        public string Nama { get; set; }
        public string RFID { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string Grade { get; set; }
        public string Photo { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? FillDate { get; set; }
        public DateTime? AttendDate { get; set; }
        public string Status { get; set; }
        public string Address {get; set;}
        public string Branch {get; set;}
    }
}