using BillShare.Logic.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillShare.Logic.Controllers.Persistence
{
	internal sealed partial class ExpenseController : BillShareController<Entities.Persistence.Expense, Contracts.Persistence.IExpense	>
	{
		protected override IEnumerable<Entities.Persistence.Expense> Set => BillShareContext.Expenses;

		public ExpenseController(IContext context)
			: base(context)
		{
		}
		public ExpenseController(ControllerObject controller)
			: base(controller)
		{
		}
	}
}
