using BillShare.Contracts.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillShare.Logic
{
    public static class Factory
    {
        public enum PersistenceType
        {
            Db,
            //Csv,
            //Ser,
        }
        /// <summary>
        /// Get and sets the persistence type.
        /// </summary>
        public static PersistenceType Persistence { get; set; } = Factory.PersistenceType.Db;
        /// <summary>
        /// This method creates the 'DataContext' depending on the persist type.
        /// </summary>
        /// <returns>An instance of the 'DataContext' type.</returns>
        private static DataContext.IContext CreateContext()
        {
            DataContext.IContext result = null;

            if (Persistence == PersistenceType.Db)
            {
                result = new DataContext.Db.DbBillShareContext();
            }
            return result;
        }
        /// <summary>
        /// This method creates a controller object for the genre entity type.
        /// </summary>
        /// <returns>The controller's interface.</returns>
        public static IControllerAccess<Contracts.Persistence.IBill> CreateBillController()
        {
            return new Controllers.Persistence.BillController(CreateContext());
        }
        /// <summary>
        /// This method creates a controller object for the genre entity type.
        /// </summary>
        /// <returns>The controller's interface.</returns>
        public static IControllerAccess<Contracts.Persistence.IExpense> CreateExpenseController()
        {
            return new Controllers.Persistence.ExpenseController(CreateContext());
        }

        public static IControllerAccess<Contracts.Business.IBillExpense> CreateBillExpenseController()
        {
            return new Controllers.Business.BillExpenseController(CreateContext());
        }
        public static IControllerAccess<Contracts.Business.IBillExpense> CreateBillExpenseController(object sharedController)
        {
            if (sharedController == null)
                throw new ArgumentNullException(nameof(sharedController));

            Controllers.ControllerObject controller = (Controllers.ControllerObject)sharedController;

            return new Controllers.Business.BillExpenseController(controller);
        }

    }
}
