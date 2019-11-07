﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YAB.Models;

namespace YAB.Migrations
{
    [DbContext(typeof(Project1Context))]
    [Migration("20191105174755_AddTypeToInterestRate")]
    partial class AddTypeToInterestRate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YAB.Models.AccountTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountTypes");
                });

            modelBuilder.Entity("YAB.Models.Accounts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20, 6)")
                        .HasDefaultValue(0m);

                    b.Property<bool>("Business")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("InterestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.HasIndex("TypeId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("YAB.Models.Customers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("YAB.Models.CustomersToAccounts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomersToAccounts");
                });

            modelBuilder.Entity("YAB.Models.DebtAccounts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("NextPaymentDue")
                        .HasColumnType("datetime");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("decimal(20, 6)");

                    b.Property<int>("PaymentsBehind")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("DebtAccounts");
                });

            modelBuilder.Entity("YAB.Models.InterestRates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(20, 6)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("InterestRates");
                });

            modelBuilder.Entity("YAB.Models.TermAccounts", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("MaturationDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TermAccounts");
                });

            modelBuilder.Entity("YAB.Models.Accounts", b =>
                {
                    b.HasOne("YAB.Models.InterestRates", "Interest")
                        .WithMany("Accounts")
                        .HasForeignKey("InterestId")
                        .HasConstraintName("FK_Accounts_InterestRates")
                        .IsRequired();

                    b.HasOne("YAB.Models.AccountTypes", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YAB.Models.CustomersToAccounts", b =>
                {
                    b.HasOne("YAB.Models.Accounts", "Account")
                        .WithMany("CustomersToAccounts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_CustomersToAccounts_Accounts")
                        .IsRequired();

                    b.HasOne("YAB.Models.Customers", "Customer")
                        .WithMany("CustomersToAccounts")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_CustomersToAccounts_Customers")
                        .IsRequired();
                });

            modelBuilder.Entity("YAB.Models.DebtAccounts", b =>
                {
                    b.HasOne("YAB.Models.Accounts", "Account")
                        .WithMany("DebtAccounts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_DebtAccounts_Accounts")
                        .IsRequired();
                });

            modelBuilder.Entity("YAB.Models.InterestRates", b =>
                {
                    b.HasOne("YAB.Models.AccountTypes", "Type")
                        .WithMany("InterestRates")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YAB.Models.TermAccounts", b =>
                {
                    b.HasOne("YAB.Models.Accounts", "Account")
                        .WithMany("TermAccounts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_TermAccounts_Accounts")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
