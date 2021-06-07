using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ASM_ADIDAS.Models
{
    public partial class QlBanHangContext : DbContext
    {
        //public QlBanHangContext()
        //{
        //}

        public QlBanHangContext(DbContextOptions<QlBanHangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHd> ChiTietHd { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<NhomSp> NhomSp { get; set; }
        public virtual DbSet<Sanpham> Sanpham { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHd>(entity =>
            {
                entity.ToTable("ChiTietHD");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdhoaDon).HasColumnName("IDHoaDon");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<NhomSp>(entity =>
            {
                entity.HasKey(e => e.MaNhom)
                    .HasName("PK__NhomSP__234F91CD83EA7BF2");

                entity.ToTable("NhomSP");

                entity.Property(e => e.MaNhom).ValueGeneratedNever();

                entity.Property(e => e.TenNhom).IsRequired();
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SANPHAM__2725081CA484523A");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.HinhAnh).IsUnicode(false);

                entity.Property(e => e.MoTaSp)
                    .HasColumnName("MoTaSP")
                    .HasMaxLength(150);

                entity.Property(e => e.NhomSp).HasColumnName("NhomSP");

                entity.Property(e => e.TenSp)
                    .IsRequired()
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.NhomSpNavigation)
                    .WithMany(p => p.Sanpham)
                    .HasForeignKey(d => d.NhomSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SANPHAM__NhomSP__276EDEB3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
