using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalPayment.Integration.Api.V1.Models.dto
{
    public class MerchantSummaryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Status { get; set; }
    }
}
