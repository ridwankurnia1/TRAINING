using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;

namespace TRAINING.API.Schema.Queries
{
    [Authorize]
    public class PartNumberType
    {
        public string CustomerId { get; set; }
        public string ModelName { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public string Remarks { get; set; }
        public string EoNumber { get; set; }
        public string AlcNumber { get; set; }
        public decimal OnHandStock { get; set; }
        public string Currency { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Status { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}