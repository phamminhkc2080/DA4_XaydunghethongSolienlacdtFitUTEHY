using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace SoLLDienTu.Models
{
    public partial class SoLLDienTuContext : DbContext
    {
        private string StrConnection;
        public SoLLDienTuContext(IConfiguration configuration)
        {
            StrConnection = configuration["SQLServer:ConnectionString"];
        }

        public SoLLDienTuContext(DbContextOptions<SoLLDienTuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GiaoVien> GiaoViens { get; set; }
        public virtual DbSet<Gvmh> Gvmhs { get; set; }
        public virtual DbSet<KetQua> KetQuas { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=HIHI-HAHA\\SQLEXPRESS;Database=SoLLDienTu;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(StrConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<GiaoVien>(entity =>
            {
                entity.HasKey(e => e.MaGv)
                    .HasName("PK__GiaoVien__2725AEF3E36058DF");

                entity.ToTable("GiaoVien");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(50)
                    .HasColumnName("MaGV");

                entity.Property(e => e.Anh).HasMaxLength(200);

                entity.Property(e => e.NgaySinh).HasMaxLength(200);

                entity.Property(e => e.QueQuan).HasMaxLength(200);

                entity.Property(e => e.TenGv)
                    .HasMaxLength(200)
                    .HasColumnName("TenGV");
            });

            modelBuilder.Entity<Gvmh>(entity =>
            {
                entity.HasKey(e => new { e.MaGv, e.MaMh })
                    .HasName("PK__GVMH__A557F30EAF389A1E");

                entity.ToTable("GVMH");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(50)
                    .HasColumnName("MaGV");

                entity.Property(e => e.MaMh)
                    .HasMaxLength(50)
                    .HasColumnName("MaMH");

                entity.HasOne(d => d.MaGvNavigation)
                    .WithMany(p => p.Gvmhs)
                    .HasForeignKey(d => d.MaGv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GVMH__MaGV__540C7B00");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.Gvmhs)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GVMH__MaMH__55009F39");
            });

            modelBuilder.Entity<KetQua>(entity =>
            {
                entity.HasKey(e => new { e.MaSv, e.MaMh })
                    .HasName("PK__KetQua__A55755E7EDDBDD00");

                entity.ToTable("KetQua");

                entity.Property(e => e.MaSv)
                    .HasMaxLength(50)
                    .HasColumnName("MaSV");

                entity.Property(e => e.MaMh)
                    .HasMaxLength(50)
                    .HasColumnName("MaMH");

                entity.Property(e => e.DiemLd).HasColumnName("DiemLD");

                entity.Property(e => e.DiemTl).HasColumnName("DiemTL");

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.KetQuas)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KetQua__MaMH__5BAD9CC8");

                entity.HasOne(d => d.MaSvNavigation)
                    .WithMany(p => p.KetQuas)
                    .HasForeignKey(d => d.MaSv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KetQua__MaSV__5AB9788F");
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.HasKey(e => e.MaLop)
                    .HasName("PK__Lop__3B98D273B65013F0");

                entity.ToTable("Lop");

                entity.Property(e => e.MaLop).HasMaxLength(50);

                entity.Property(e => e.MaGv)
                    .HasMaxLength(50)
                    .HasColumnName("MaGV");

                entity.Property(e => e.TenLop).HasMaxLength(200);

                entity.HasOne(d => d.MaGvNavigation)
                    .WithMany(p => p.Lops)
                    .HasForeignKey(d => d.MaGv)
                    .HasConstraintName("FK__Lop__MaGV__4F47C5E3");
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMh)
                    .HasName("PK__MonHoc__2725DFD9ED066A9B");

                entity.ToTable("MonHoc");

                entity.Property(e => e.MaMh)
                    .HasMaxLength(50)
                    .HasColumnName("MaMH");

                entity.Property(e => e.SoTc).HasColumnName("SoTC");

                entity.Property(e => e.TenMh)
                    .HasMaxLength(200)
                    .HasColumnName("TenMH");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSv)
                    .HasName("PK__SinhVien__2725081A0E914AE3");

                entity.ToTable("SinhVien");

                entity.Property(e => e.MaSv)
                    .HasMaxLength(50)
                    .HasColumnName("MaSV");

                entity.Property(e => e.Anh).HasMaxLength(200);

                entity.Property(e => e.MaLop).HasMaxLength(50);

                entity.Property(e => e.NgaySinh).HasMaxLength(200);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sdt");

                entity.Property(e => e.TamTru).HasMaxLength(200);

                entity.Property(e => e.TenSv)
                    .HasMaxLength(20)
                    .HasColumnName("TenSV");

                entity.Property(e => e.ThuongTru).HasMaxLength(200);

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.MaLop)
                    .HasConstraintName("FK__SinhVien__MaLop__57DD0BE4");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.LoaiNg).HasColumnName("LoaiNG");

                entity.Property(e => e.Ma).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.TaiKhoan1)
                    .HasMaxLength(100)
                    .HasColumnName("TaiKhoan");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
