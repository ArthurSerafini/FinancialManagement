using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConstructor
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalInstallments { get; set; }
        public int InstallmentsPaid { get; set; }
        public double TotalPrice { get; set; }
        public double PricePaid { get; set; }
    }
}
