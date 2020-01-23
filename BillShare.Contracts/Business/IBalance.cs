using System;
using System.Collections.Generic;
using System.Text;

namespace BillShare.Contracts.Business
{
    public interface IBalance
    {
        string From { get; }
        string To { get; }
        double Amount { get; }
    }
}
