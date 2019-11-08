using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace YAB.Models
{
    public partial class Project1Context : DbContext
    {
        public Project1Context()
        {
        }

        public Project1Context(DbContextOptions<Project1Context> options)
            : base(options)
        {
        }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomersToAccounts> CustomersToAccounts { get; set; }
        public virtual DbSet<DebtAccounts> DebtAccounts { get; set; }
        public virtual DbSet<InterestRates> InterestRates { get; set; }
        public virtual DbSet<TermAccounts> TermAccounts { get; set; }
        public virtual DbSet<AccountTypes> AccountTypes { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("DbContext not configured.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnType("decimal(20, 6)").HasDefaultValue(0);

                entity.Property(e => e.Created).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.InterestId).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime").HasDefaultValueSql("getdate()");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.InterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_InterestRates");
            });

            //modelBuilder.Entity<Customers>(entity =>
            //{
            //    entity.Property(e => e.PasswordHash)
            //        .IsRequired()
            //        .HasMaxLength(64)
            //        .IsFixedLength();

            //    entity.Property(e => e.Salt)
            //        .IsRequired()
            //        .HasMaxLength(36)
            //        .IsFixedLength();

            //    entity.Property(e => e.Username)
            //        .IsRequired()
            //        .HasMaxLength(10)
            //        .IsFixedLength();
            //});

            modelBuilder.Entity<CustomersToAccounts>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CustomersToAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CustomersToAccounts_Accounts");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomersToAccounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CustomersToAccounts_Customers");
            });

            modelBuilder.Entity<DebtAccounts>(entity =>
            {
                entity.Property(e => e.NextPaymentDue).HasColumnType("datetime");

                entity.Property(e => e.PaymentAmount).HasColumnType("decimal(20, 6)");

                entity.Property(e => e.PaymentsBehind).HasDefaultValue(0);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.DebtAccounts) // WithMany here allows 0 or 1
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DebtAccounts_Accounts");
            });

            modelBuilder.Entity<InterestRates>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rate).HasColumnType("decimal(20, 6)");
            });

            modelBuilder.Entity<TermAccounts>(entity =>
            {
                entity.Property(e => e.MaturationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TermAccounts) // WithMany here allows 0 or 1
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TermAccounts_Accounts");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasOne(t => t.FromAccount)
                    .WithMany(a => a.OutgoingTransactions)
                    .HasForeignKey(a => a.FromAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_FromAccounts");

                entity.HasOne(t => t.ToAccount)
                    .WithMany(a => a.IncomingTransactions)
                    .HasForeignKey(a => a.ToAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_ToAccounts");

                entity.HasOne(t => t.Type)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(t => t.TypeId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Transactions_TransactionType");

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
