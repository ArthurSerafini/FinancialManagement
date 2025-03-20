using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConstructor
{
    public class InstallmentsTable
    {
        [Key]
        public int InstallmentId { get; set; }
        public string? Date { get; set; }
        public float PaidValue { get; set; }
        public bool Paid { get; set; }
        public int PaymentId { get; set; }
        public PaymentInfo paymentInfo { get; set; }
    }
}