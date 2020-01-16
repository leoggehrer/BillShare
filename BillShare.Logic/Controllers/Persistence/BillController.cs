using BillShare.Logic.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillShare.Logic.Controllers.Persistence
{
	internal sealed partial class BillController : BillShareController<Entities.Persistence.Bill, Contracts.Persistence.IBill>
	{
		protected override IEnumerable<Entities.Persistence.Bill> Set => BillShareContext.Bills;

		public BillController(IContext context)
			: base(context)
		{
		}
		public BillController(ControllerObject controller)
			: base(controller)
		{
		}
	}
}
