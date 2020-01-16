using BillShare.Logic.DataContext;
using BillShare.Logic.DataContext.Db;

namespace BillShare.Logic.Controllers.Persistence
{
    internal abstract partial class BillShareController<E, I> : GenericController<E, I>
       where E : Entities.IdentityObject, I, Contracts.ICopyable<I>, new()
       where I : Contracts.IIdentifiable
    {
        protected DbBillShareContext BillShareContext => (DbBillShareContext)Context;

        protected BillShareController(IContext context)
            : base(context)
        {
        }
        protected BillShareController(ControllerObject controller)
            : base(controller)
        {

        }
    }
}
