namespace TRAINING.API.ViewModel
{
    public class LebaranSummaryDto
    {
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public int Filled { get; set; }
        public int Unfilled { get; set; }
        public int MustCheck { get; set; }
        public int NoNeedCheck { get; set; }
        public int AlreadyCheck { get; set; }
        public int NotYetCheck { get; set; }
    }
}