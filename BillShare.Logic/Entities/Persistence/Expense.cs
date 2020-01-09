using BillShare.Contracts.Persistence;
using CommonBase.Extensions;

namespace BillShare.Logic.Entities.Persistence
{
    internal class Expense : IdentityObject, Contracts.Persistence.IExpense
    {
        public int BillId { get; set; }
        public string Designation { get; set; }
        public double Amount { get; set; }
        public string Friend { get; set; }

        public void CopyProperties(IExpense other)
        {
            other.CheckArgument(nameof(other));

            Id = other.Id;
            BillId = other.BillId;
            Designation = other.Designation;
            Amount = other.Amount;
            Friend = other.Friend;
        }
        // Navigation property
        public Bill Bill { get; set; }
    }
}
