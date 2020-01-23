using BillShare.Contracts.Business;
using BillShare.Contracts.Persistence;
using BillShare.Logic.Entities.Persistence;
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillShare.Logic.Entities.Business
{
    internal class BillExpense : IdentityObject, Contracts.Business.IBillExpense
    {
        public Bill BillEntity { get; set; } = new Bill();
        public IBill Bill => BillEntity;

        public List<Expense> ExpenseEntities { get; set; } = new List<Expense>();
        public IEnumerable<IExpense> Expenses => ExpenseEntities;

        public new int Id => BillEntity.Id;

        public double TotalExpense
        {
            get
            {
                return ExpenseEntities.Sum(i => i.Amount);
            }
        }

        public double FriendPortion => NumberOfFriends != 0 ? TotalExpense / NumberOfFriends : 0;

        public int NumberOfFriends => BillEntity.Friends != null ? BillEntity.Friends.Split(";").Length : 0;

        public string[] Friends => BillEntity.Friends != null ? BillEntity.Friends.Split(";") : new string[0];

        public double[] FriendAmounts => Friends.Select(f => ExpenseEntities.Where(e => e.Friend.Equals(f)).Sum(j => j.Amount)).ToArray();

        public IEnumerable<IBalance> Balances => CreateBalance();

        private IEnumerable<IBalance> CreateBalance()
        {
            return new IBalance[0];
        }
        public IExpense CreateExpense()
        {
            return new Expense();
        }
        public void Add(IExpense expense)
        {
            expense.CheckArgument(nameof(expense));

            var entity = new Expense();

            entity.CopyProperties(expense);
            ExpenseEntities.Add(entity);
        }

        public void Remove(IExpense expense)
        {
            expense.CheckArgument(nameof(expense));

            var entity = ExpenseEntities.FirstOrDefault(i => (i.Id != 0 && i.Id == expense.Id)
                                                          || (i.Id == 0 && i.Designation != null && i.Designation.Equals(expense.Designation)));
            if (entity != null)
            {
                ExpenseEntities.Remove(entity);
            }
        }
        public void CopyProperties(IBillExpense other)
        {
            other.CheckArgument(nameof(other));
            other.Bill.CheckArgument(nameof(other.Bill));
            other.Expenses.CheckArgument(nameof(other.Expenses));

            BillEntity.CopyProperties(other.Bill);
            ExpenseEntities.Clear();
            foreach (var item in other.Expenses)
            {
                var expense = new Expense();

                expense.CopyProperties(item);
                ExpenseEntities.Add(expense);
            }
        }
    }
}
