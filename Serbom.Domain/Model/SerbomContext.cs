using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Serbom.Domain.Model;

public partial class SerbomContext : DbContext
{
    public SerbomContext()
    {
    }

    public SerbomContext(DbContextOptions<SerbomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amendment> Amendments { get; set; }

    public virtual DbSet<Annex> Annexes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractStatus> ContractStatuses { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var cnnString = configuration.GetConnectionString("Serbom");
            if(String.IsNullOrEmpty(cnnString))
            {
                throw new System.Exception("Connection string not found");
            }
            optionsBuilder.UseMySQL(cnnString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amendment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("amendment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Conditions)
                .HasColumnType("text")
                .HasColumnName("conditions");
            entity.Property(e => e.Contract).HasColumnName("contract");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("number");
            entity.Property(e => e.Value)
                .HasPrecision(10)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Annex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("annex");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amendment).HasColumnName("amendment");
            entity.Property(e => e.Content)
                .HasColumnType("mediumblob")
                .HasColumnName("content");
            entity.Property(e => e.Contract).HasColumnName("contract");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("address1");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("address2");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasColumnType("enum('AC','AL','AM','AP','BA','CE','DF','ES','GO','MA','MG','MS','MT','PA','PB','PE','PI','PR','RJ','RN','RO','RR','RS','SC','SE','SP','TO')")
                .HasColumnName("state");
            entity.Property(e => e.Type)
                .HasColumnType("enum('individual','corporate')")
                .HasColumnName("type");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("zipCode");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contract");

            entity.HasIndex(e => e.Number, "number").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.Conditions)
                .HasColumnType("text")
                .HasColumnName("conditions");
            entity.Property(e => e.End)
                .HasColumnType("date")
                .HasColumnName("end");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .HasColumnName("number");
            entity.Property(e => e.Start)
                .HasColumnType("date")
                .HasColumnName("start");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Subject)
                .HasMaxLength(500)
                .HasColumnName("subject");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Value)
                .HasPrecision(10)
                .HasColumnName("value");
        });

        modelBuilder.Entity<ContractStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contractStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
        });

        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contractType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("history");

            entity.HasIndex(e => e.Date, "date");

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "entityType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasColumnType("enum('create','update','delete')")
                .HasColumnName("action");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.EntityId).HasColumnName("entityId");
            entity.Property(e => e.EntityType)
                .HasColumnType("enum('user','client','contract','amendment','annex')")
                .HasColumnName("entityType");
            entity.Property(e => e.User).HasColumnName("user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Salt)
                .HasMaxLength(21)
                .IsFixedLength()
                .HasColumnName("salt");
            entity.Property(e => e.Secret)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("secret");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
