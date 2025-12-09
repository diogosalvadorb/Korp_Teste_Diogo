using KorpBilling.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KorpBilling.Infrastructure.Persistence
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoice>(builder =>
            {
                builder.HasKey(i => i.Id);

                modelBuilder.Entity<Invoice>()
                    .HasIndex(i => i.Number)
                    .IsUnique();

                builder.Property(i => i.Status)
                    .IsRequired()
                    .HasConversion<int>();

                builder.Property(i => i.CreatedAt)
                    .IsRequired();

                builder.HasMany(i => i.Items)
                    .WithOne(it => it.Invoice)
                    .HasForeignKey(it => it.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<InvoiceItem>(builder =>
            {
                builder.ToTable("InvoiceItems");

                builder.HasKey(it => it.Id);

                builder.Property(it => it.ProductId)
                    .IsRequired();

                builder.Property(it => it.Quantity)
                    .IsRequired();

                builder.Property(it => it.UnitPrice)
                    .HasPrecision(18, 2)
                    .IsRequired();

                builder.Property(it => it.TotalPrice)
                    .HasPrecision(18, 2)
                    .IsRequired();
            });
        }
    }
}
