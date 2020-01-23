using BillShare.Contracts;
using BillShare.Logic.Entities;
using BillShare.Logic.Entities.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BillShare.Logic.DataContext.Db
{
    internal class DbBillShareContext : DbContext, IContext//, IMusicStoreContext
    {
        private static string ConnectionString { get; set; } = "Data Source=(localdb)\\MSSQLLocalDb;Database=BillShareDb;Integrated Security=True;";

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        #region Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bill>()
                .ToTable(nameof(Bill))
                .HasKey(p => p.Id);
            modelBuilder.Entity<Bill>()
                .HasIndex(p => p.Title)
                .IsUnique();
            modelBuilder.Entity<Bill>()
                .Property(p => p.Title)
                .HasMaxLength(256);
            modelBuilder.Entity<Bill>()
                .Property(p => p.Description)
                .HasMaxLength(256);
            modelBuilder.Entity<Bill>()
                .Property(p => p.Friends)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<Bill>()
                .Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<Expense>()
                .ToTable(nameof(Expense))
                .HasKey(p => p.Id);
            modelBuilder.Entity<Expense>()
                .Property(p => p.Designation)
                .IsRequired()
                .HasMaxLength(256);
            modelBuilder.Entity<Expense>()
                .Property(p => p.Friend)
                .IsRequired()
                .HasMaxLength(50);
        }
        #endregion Configuration

        #region IContext
        #region Async-Methods
        public Task<int> CountAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Set<E>().CountAsync();
        }
        public Task<E> CreateAsync<I, E>()
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() => new E());
        }

        public Task<E> InsertAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() =>
            {
                Set<E>().Add(entity);
                return entity;
            });
        }
        public Task<E> UpdateAsync<I, E>(E entity)
            where I : IIdentifiable
            where E : IdentityObject, ICopyable<I>, I, new()
        {
            return Task.Run(() =>
            {
                Set<E>().Update(entity);
                return entity;
            });
        }
        public Task<E> DeleteAsync<I, E>(int id)
            where I : IIdentifiable
            where E : IdentityObject, I
        {
            return Task.Run(() =>
            {
                E result = Set<E>().SingleOrDefault(i => i.Id == id);

                if (result != null)
                {
                    Set<E>().Remove(result);
                }
                return result;
            });
        }

        public Task SaveAsync()
        {
            return SaveChangesAsync();
        }
        #endregion Async-Methods
        #endregion IContext
    }
}

