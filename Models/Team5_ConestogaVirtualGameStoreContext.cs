using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class Team5_ConestogaVirtualGameStoreContext : DbContext
    {
        public Team5_ConestogaVirtualGameStoreContext()
        {
        }

        public Team5_ConestogaVirtualGameStoreContext(DbContextOptions<Team5_ConestogaVirtualGameStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Team5_ConestogaVirtualGameStore;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId)
                    .HasColumnName("addressId")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasColumnName("addressLine1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("addressLine2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AddressTypeId).HasColumnName("addressTypeId");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Provice)
                    .IsRequired()
                    .HasColumnName("provice")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AddressTypeId).HasColumnName("addressTypeId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId)
                    .HasColumnName("gameId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountPercent).HasColumnName("discountPercent");

                entity.Property(e => e.Inventory).HasColumnName("inventory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PlatformId).HasColumnName("platformId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("releaseDate")
                    .HasColumnType("date");

                entity.Property(e => e.ReviewListId).HasColumnName("reviewListId");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Platform");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId)
                    .HasColumnName("genreId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.Property(e => e.PlatformId)
                    .HasColumnName("platformId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .HasColumnName("reviewId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.IsPending)
                    .IsRequired()
                    .HasColumnName("isPending")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewListId).HasColumnName("reviewListId");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
