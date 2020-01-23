using System;
using System.Linq;
using System.Threading.Tasks;

namespace BillShare.ConApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            Console.WriteLine("!BillShare!");

            using var ctrlBillExpense = Logic.Factory.CreateBillExpenseController();
            var travels = await ctrlBillExpense.GetAllAsync();

            // Delete all travles
            foreach (var item in travels)
            {
                await ctrlBillExpense.DeleteAsync(item.Id);
            }
            await ctrlBillExpense.SaveChangesAsync();

            // Create a Travel
            var friends = new string[] { "Gerhard", "Robert", "Tobias", "Herbert", "Walter" };

            for (int j = 0; j < 5; j++)
            {
                var travelExpense = await ctrlBillExpense.CreateAsync();

                travelExpense.Bill.Title = $"Gran Canaria 2019-{j + 1}";
                travelExpense.Bill.Description = "Reisen";
                travelExpense.Bill.Currency = "EUR";
                travelExpense.Bill.Friends = friends.Aggregate((s1, s2) => s1 + ";" + s2);

                for (int i = 0; i < 25; i++)
                {
                    var expense = travelExpense.CreateExpense();
                    expense.Designation = $"Essen-{i + 1}";
                    expense.Friend = friends[rnd.Next(0, friends.Length)];
                    expense.Amount = rnd.NextDouble() * 100.0;
                    travelExpense.Add(expense);
                }
                travelExpense = await ctrlBillExpense.InsertAsync(travelExpense);
                await ctrlBillExpense.SaveChangesAsync();
            }

            // Output
            foreach (var item in await ctrlBillExpense.GetAllAsync())
            {
                var balances = item.Balances;

                Console.WriteLine($"Bill: {item.Bill.Title}");
                Console.WriteLine($"\tTotal:   {item.TotalExpense:f}");
                Console.WriteLine($"\tPortion: {item.FriendPortion:f}");

                for (int i = 0; i < item.Friends.Length; i++)
                {
                    Console.WriteLine($"\t\tFriend: {item.Friends[i],-10} {item.FriendAmounts[i]:f}");
                }
                Console.WriteLine("\tBalance:");
                foreach (var balance in item.Balances)
                {
                    Console.WriteLine($"\t\t{balance.From,-10} -> {balance.To,-10}: {balance.Amount:f}");
                }
            }
        }
    }
}
