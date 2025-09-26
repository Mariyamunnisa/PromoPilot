using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PromoPilot.Core.Entities;
using PromoPilot.Infrastructure.Data;

namespace PromoPilot.Infrastructure.Data
{
    public partial class PromoPilotDbContext : DbContext
    {
        public PromoPilotDbContext()
        {
        }

        public PromoPilotDbContext(DbContextOptions<PromoPilotDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; }

        public virtual DbSet<Campaign> Campaigns { get; set; }

        public virtual DbSet<CampaignReport> CampaignReports { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Engagement> Engagements { get; set; }

        public virtual DbSet<ExecutionStatus> ExecutionStatuses { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=LTIN678811\\SQLEXPRESS;Initial Catalog=PromoPilotDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budget");

                entity.HasIndex(e => e.CampaignId, "IX_Budget_CampaignID");

                entity.Property(e => e.BudgetId).HasColumnName("BudgetID");
                entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
                entity.Property(e => e.SpentAmount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Campaign).WithMany(p => p.Budgets).HasForeignKey(d => d.CampaignId);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign");

                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
            });

            modelBuilder.Entity<CampaignReport>(entity =>
            {
                entity.HasKey(e => e.ReportId);

                entity.ToTable("CampaignReport");

                entity.HasIndex(e => e.CampaignId, "IX_CampaignReport_CampaignID");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");
                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
                entity.Property(e => e.ConversionRate).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Roi)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ROI");

                entity.HasOne(d => d.Campaign).WithMany(p => p.CampaignReports).HasForeignKey(d => d.CampaignId);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            });

            modelBuilder.Entity<Engagement>(entity =>
            {
                entity.ToTable("Engagement");

                entity.HasIndex(e => e.CampaignId, "IX_Engagement_CampaignID");

                entity.Property(e => e.EngagementId).HasColumnName("EngagementID");
                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.PurchaseValue).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Campaign).WithMany(p => p.Engagements).HasForeignKey(d => d.CampaignId);
            });

            modelBuilder.Entity<ExecutionStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("ExecutionStatus");

                entity.HasIndex(e => e.CampaignId, "IX_ExecutionStatus_CampaignID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Campaign).WithMany(p => p.ExecutionStatuses).HasForeignKey(d => d.CampaignId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasIndex(e => e.CampaignId, "IX_Sales_CampaignID");

                entity.HasIndex(e => e.CustomerId, "IX_Sales_CustomerID");

                entity.HasIndex(e => e.ProductId, "IX_Sales_ProductID");

                entity.Property(e => e.SaleId).HasColumnName("SaleID");
                entity.Property(e => e.CampaignId).HasColumnName("CampaignID");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.StoreId).HasColumnName("StoreID");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Campaign).WithMany(p => p.Sales).HasForeignKey(d => d.CampaignId);

                entity.HasOne(d => d.Customer).WithMany(p => p.Sales).HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Product).WithMany(p => p.Sales).HasForeignKey(d => d.ProductId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
