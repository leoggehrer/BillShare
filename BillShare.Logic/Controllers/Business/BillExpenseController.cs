using BillShare.Contracts.Business;
using BillShare.Contracts.Client;
using BillShare.Logic.Controllers.Persistence;
using BillShare.Logic.DataContext;
using BillShare.Logic.Entities.Business;
using BillShare.Logic.Entities.Persistence;
using CommonBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillShare.Logic.Controllers.Business
{
    internal class BillExpenseController : ControllerObject, IControllerAccess<IBillExpense>
    {
        private BillController billController;
        private ExpenseController expenseController;

        public BillExpenseController(IContext context)
            : base(context)
        {
            billController = new BillController(this);
            expenseController = new ExpenseController(this);
        }
        public BillExpenseController(ControllerObject controller)
            : base(controller)
        {
            billController = new BillController(this);
            expenseController = new ExpenseController(this);
        }
        public Task<int> CountAsync()
        {
            return billController.CountAsync();
        }

        public Task<IBillExpense> CreateAsync()
        {
            return Task.Run<IBillExpense>(() => new BillExpense());
        }

        public async Task DeleteAsync(int id)
        {
            var deleteItem = await GetByIdAsync(id);

            if (deleteItem != null)
            {
                foreach (var item in deleteItem.Expenses)
                {
                    await expenseController.DeleteAsync(item.Id);
                }
                await billController.DeleteAsync(deleteItem.Id);
            }
            else
            {
                throw new Exception("Item not found!");
            }
        }

        public async Task<IEnumerable<IBillExpense>> GetAllAsync()
        {
            List<IBillExpense> result = new List<IBillExpense>();

            foreach (var item in (await billController.GetAllAsync()).ToList())
            {
                result.Add(await GetByIdAsync(item.Id));
            }
            return result;
        }

        public async Task<IBillExpense> GetByIdAsync(int id)
        {
            var result = default(BillExpense);
            var bill = await billController.GetByIdAsync(id);

            if (bill != null)
            {
                result = new BillExpense();
                result.Bill.CopyProperties(bill);
                foreach (var item in await expenseController.QueryAsync(e => e.BillId == id))
                {
                    result.ExpenseEntities.Add(item);
                }
            }
            else
            {
                throw new Exception("Entity can't find!");
            }
            return result;
        }

        public async Task<IBillExpense> InsertAsync(IBillExpense entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.Bill.CheckArgument(nameof(entity.Bill));
            entity.Expenses.CheckArgument(nameof(entity.Expenses));

            var result = new BillExpense();

            result.BillEntity.CopyProperties(entity.Bill);
            await billController.InsertAsync(result.BillEntity);

            foreach (var item in entity.Expenses)
            {
                var expense = new Expense();

                expense.CopyProperties(item);
                expense.Bill = result.BillEntity;

                await expenseController.InsertAsync(expense);
                result.ExpenseEntities.Add(expense);
            }
            return result;
        }

        public async Task<IBillExpense> UpdateAsync(IBillExpense entity)
        {
            entity.CheckArgument(nameof(entity));
            entity.Bill.CheckArgument(nameof(entity.Bill));
            entity.Expenses.CheckArgument(nameof(entity.Expenses));

            //Delete all costs that are no longer included in the list.
            foreach (var item in await expenseController.QueryAsync(e => e.BillId == entity.Bill.Id))
            {
                var tmpExpense = entity.Expenses.SingleOrDefault(i => i.Id == item.Id);

                if (tmpExpense == null)
                {
                    await expenseController.DeleteAsync(item.Id);
                }
            }

            var result = new BillExpense();
            var bill = await billController.UpdateAsync(entity.Bill);

            result.BillEntity.CopyProperties(bill);
            foreach (var item in entity.Expenses)
            {
                if (item.Id == 0)
                {
                    item.BillId = entity.Bill.Id;
                    var insEntity = await expenseController.InsertAsync(item);

                    item.CopyProperties(insEntity);
                }
                else
                {
                    var updEntity = await expenseController.UpdateAsync(item);

                    item.CopyProperties(updEntity);
                }
            }
            return result;
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveAsync();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            billController.Dispose();
            expenseController.Dispose();

            billController = null;
            expenseController = null;
        }
    }
}
