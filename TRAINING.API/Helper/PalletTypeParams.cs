
#nullable enable
namespace TRAINING.API.Helper
{
    public class PalletTypeParams : BaseParams
    {
        // global search query
        public string? SearchString { get; set; }

        // individual search queries
        public string? ptp { get; set; } // pallet type query
        public string? atp { get; set; } // pallet app query
        public string? mtp { get; set; } // pallet material type query
        public string? col { get; set; } // pallet color query
        public decimal? plt { get; set; } // pallet length query
        public string? pltd { get; set; } // pallet length direction/terms query
        public decimal? pwt { get; set; } // pallet width query
        public string? pwtd { get; set; } // pallet width direction/terms query
        public decimal? stp { get; set; } // pallet status query
        public string? srt { get; set; } // column sorting
    }
}