using System;
using System.Collections.Generic;
using System.Text;

namespace BillShare.Logic.Entities.Business
{
    internal class Balance : Contracts.Business.IBalance
    {
        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }
    }
}
