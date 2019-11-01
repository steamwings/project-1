using Microsoft.EntityFrameworkCore;

namespace YAB.Models
{
    public class P1Context : DbContext
    {
        public P1Context(DbContextOptions<P1Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Account>().HasBaseType<IAccount>();
            //modelBuilder.Entity<TermDeposit>().HasBaseType<ITerm>();
            //modelBuilder.Entity<BusinessAccount>().HasBaseType<IDebt>();
            //modelBuilder.Entity<Loan>().HasBaseType<IDebt>();
            //modelBuilder.Entity<CheckingAccount>().HasBaseType<IChecking>();
            modelBuilder.Entity<CheckingAccount>().HasBaseType<Account>();
            modelBuilder.Entity<BusinessAccount>().HasBaseType<CheckingAccount>();
            modelBuilder.Entity<Loan>().HasBaseType<Account>();
            modelBuilder.Entity<TermDeposit>().HasBaseType<Account>();
            modelBuilder.Entity<CustomerAccount>().HasKey(x => new { x.CustomerId, x.AccountId });


            //modelBuilder.Entity<Customer>()
            //.HasMany(c => c.Accounts).WithMany(i => i.Customer)
            //.Map(t => t.MapLeftKey("Id")
            //.MapRightKey("Id")
            //.ToTable("CustomerAccount"));
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<InterestRate> InterestRates { get; set; }
    }

    public class CustomerAccount
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}