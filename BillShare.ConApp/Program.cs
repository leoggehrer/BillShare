using System;
using System.Threading.Tasks;

namespace BillShare.ConApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var ctrlBill = Logic.Factory.CreateBillController();
            var bill = await ctrlBill.CreateAsync();
            bill.Date = DateTime.Now;
            bill.Title = "Manchester 2020";
            bill.Description = "Demo";
            bill.Friends = "Gerhard,Robert";
            bill.Currency = "EUR";
            bill = await ctrlBill.InsertAsync(bill);
            await ctrlBill.SaveChangesAsync();

        }
    }
}
