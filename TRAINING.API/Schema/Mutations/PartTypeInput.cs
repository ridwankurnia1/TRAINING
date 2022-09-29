using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRAINING.API.Schema.Mutations
{
    public class PartTypeInput
    {
        public string CustomerId { get; set; }
        public string ModelName { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string Remarks { get; set; }
        public string EoNumber { get; set; }
        public string AlcNumber { get; set; }
    }
}