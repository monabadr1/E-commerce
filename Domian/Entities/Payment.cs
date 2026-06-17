using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment
    {
        public int PaymentId {  get; set; }

        public int OrderId {  get; set; }

        public string? PaymentMethod { get; set; }

        public string? PaymentStatus { get; set; }

        public Order?Order { get; set; }

        public DateTime? PaidAt {  get; set; }

        public string ? TransactionId {  get; set; }
    }
}
