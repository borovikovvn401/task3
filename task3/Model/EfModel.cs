using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace task3.Model
{
    public partial class EfModel : DbContext
    {

        public static EfModel instance;

        public static EfModel init()
        {
            if (instance == null)
                instance = new EfModel();
            return instance;
        }

        public EfModel()
        {
        }

        public EfModel(DbContextOptions<EfModel> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Tour> Tours { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=cfif31.ru;database=ISPr23-47_BorovikovVN_task3;user id=ISPr23-47_BorovikovVN;password=ISPr23-47_BorovikovVN", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PRIMARY");

                entity.ToTable("Country");

                entity.Property(e => e.Code)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.HasIndex(e => e.CountryCode, "fk_Tour_Country1_idx");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .HasColumnName("Country_Code")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasPrecision(19, 4);

                entity.HasOne(d => d.CountryCodeNavigation)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tour_Country1");

                entity.HasMany(d => d.Types)
                    .WithMany(p => p.Tours)
                    .UsingEntity<Dictionary<string, object>>(
                        "TypeOfTour",
                        l => l.HasOne<Type>().WithMany().HasForeignKey("TypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TypeOfTour_Type"),
                        r => r.HasOne<Tour>().WithMany().HasForeignKey("TourId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TypeOfTour_Tour"),
                        j =>
                        {
                            j.HasKey("TourId", "TypeId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("TypeOfTour");

                            j.HasIndex(new[] { "TypeId" }, "FK_TypeOfTour_Type");
                        });
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
