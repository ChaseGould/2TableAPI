using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TwoTableAPI.Models
{
    public partial class TwoTablesContext : DbContext
    {
        public TwoTablesContext()
        {
        }

        public TwoTablesContext(DbContextOptions<TwoTablesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PaymentDetails> PaymentDetails { get; set; }
        public virtual DbSet<PaymentHistory> PaymentHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-VCAF5P42;Initial Catalog=2Tables;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentDetails>(entity =>
            {
                entity.Property(e => e.Pmid).ValueGeneratedOnAdd();

                entity.Property(e => e.CardNumber).IsUnicode(false);

                entity.Property(e => e.CardOwnerName).IsUnicode(false);

                entity.Property(e => e.Cvv).IsUnicode(false);

                entity.Property(e => e.ExpirationDate).IsUnicode(false);

                entity.HasOne(d => d.Pm)
                    .WithOne(p => p.InversePm)
                    .HasForeignKey<PaymentDetails>(d => d.Pmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentDetails_PaymentDetails");
            });

            modelBuilder.Entity<PaymentHistory>(entity =>
            {
                entity.Property(e => e.Date).IsUnicode(false);

                entity.HasOne(d => d.PaymentDetailIdForeignNavigation)
                    .WithMany(p => p.PaymentHistory)
                    .HasForeignKey(d => d.PaymentDetailIdForeign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentHistory_PaymentDetails");
            });
        }
    }
}
