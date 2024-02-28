namespace TRAINING.API.Helper
{
    public class TruckParams : BaseParams
    {
          public string? SearchString { get; set; }
          
        public string? filter { get; set; }
          public string? srt { get; set; } // column sorting
    }
}