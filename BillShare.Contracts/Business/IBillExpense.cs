using BillShare.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillShare.Contracts.Business
{
    public interface IBillExpense : IIdentifiable, ICopyable<IBillExpense>
    {
        IBill Bill { get; }
        IEnumerable<IExpense> Expenses { get; }

        new int Id { get; }
        double TotalExpense { get; }
        double FriendPortion { get; }
        int NumberOfFriends { get; }
        string[] Friends { get; }
        double[] FriendAmounts { get; }
        IEnumerable<IBalance> Balances { get; }
        IExpense CreateExpense();
        void Add(IExpense expense);
        void Remove(IExpense expense);
    }
}
