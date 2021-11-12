using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class CVGS_Context : DbContext
    {
        public CVGS_Context()
        {
        }

        public CVGS_Context(DbContextOptions<CVGS_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressList> AddressList { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventGameItem> EventGameItem { get; set; }
        public virtual DbSet<EventGamesList> EventGamesList { get; set; }
        public virtual DbSet<FriendItem> FriendItem { get; set; }
        public virtual DbSet<FriendList> FriendList { get; set; }
        public virtual DbSet<FriendType> FriendType { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<JoinedEvent> JoinedEvent { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<ReviewList> ReviewList { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }
        public virtual DbSet<WishlistItem> WishlistItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Team5_ConestogaVirtualGameStore;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddressType).HasColumnName("addressType");

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

                entity.Property(e => e.Line1)
                    .IsRequired()
                    .HasColumnName("line1")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Line2)
                    .HasColumnName("line2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .IsRequired()
                    .HasColumnName("province")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressTypeNavigation)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Address__address__38996AB5");
            });

            modelBuilder.Entity<AddressList>(entity =>
            {
                entity.HasKey(e => e.ListId)
                    .HasName("PK__AddressL__7D4CA69B2B08720C");

                entity.Property(e => e.ListId)
                    .HasColumnName("listID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__AddressT__F04DF11AA8415D27");

                entity.Property(e => e.TypeId)
                    .HasColumnName("typeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId)
                    .HasColumnName("cartID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.Property(e => e.TaxPercent).HasColumnName("taxPercent");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__CartItem__56A1284A8CB38421");

                entity.Property(e => e.ItemId)
                    .HasColumnName("itemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CartId).HasColumnName("cartID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItem)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CartItem__cartID__33D4B598");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId)
                    .HasColumnName("eventID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GameListId).HasColumnName("gameListID");

                entity.HasOne(d => d.GameList)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.GameListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Event__gameListI__4222D4EF");
            });

            modelBuilder.Entity<EventGameItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__EventGam__56A1284AB9C140B9");

                entity.Property(e => e.ItemId)
                    .HasColumnName("itemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ListId).HasColumnName("listID");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.EventGameItem)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EventGame__listI__3F466844");
            });

            modelBuilder.Entity<EventGamesList>(entity =>
            {
                entity.HasKey(e => e.ListId)
                    .HasName("PK__EventGam__7D4CA69B7425D7BD");

                entity.Property(e => e.ListId)
                    .HasColumnName("listID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<FriendItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__FriendIt__56A1284A835D16FB");

                entity.Property(e => e.ItemId)
                    .HasColumnName("itemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.FriendType).HasColumnName("friendType");

                entity.Property(e => e.FriendUserId)
                    .IsRequired()
                    .HasColumnName("friendUserID")
                    .HasMaxLength(450);

                entity.Property(e => e.ListId).HasColumnName("listID");

                entity.HasOne(d => d.List)
                    .WithMany(p => p.FriendItem)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FriendIte__listI__5535A963");
            });

            modelBuilder.Entity<FriendList>(entity =>
            {
                entity.HasKey(e => e.ListId)
                    .HasName("PK__FriendLi__7D4CA69B0209EDB1");

                entity.Property(e => e.ListId)
                    .HasColumnName("listID")
                    .ValueGeneratedNever();

                entity.Property(e => e.HostUserId)
                    .IsRequired()
                    .HasColumnName("hostUserID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<FriendType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__FriendTy__F04DF11A3AC3ECF6");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId)
                    .HasColumnName("gameID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.DiscountPercent).HasColumnName("discountPercent");

                entity.Property(e => e.GenreId).HasColumnName("genreID");

                entity.Property(e => e.Inventory).HasColumnName("inventory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlatformId).HasColumnName("platformID");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("releaseDate")
                    .HasColumnType("date");

                entity.Property(e => e.ReviewListId).HasColumnName("reviewListID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Game__genreID__2D27B809");

                entity.HasOne(d => d.Platform)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Game__platformID__2E1BDC42");

                entity.HasOne(d => d.ReviewList)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.ReviewListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Game__reviewList__2F10007B");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId)
                    .HasColumnName("genreID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JoinedEvent>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.EventId).HasColumnName("eventID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.JoinedEvent)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JoinedEve__event__44FF419A");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__OrderIte__56A1284A43D5ADA9");

                entity.Property(e => e.ItemId)
                    .HasColumnName("itemID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GameId).HasColumnName("gameID");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__gameI__5070F446");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderItem__order__4F7CD00D");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.Property(e => e.PlatformId)
                    .HasColumnName("platformID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Purchase__0809337D36E08C0B");

                entity.Property(e => e.OrderId)
                    .HasColumnName("orderID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOrdered)
                    .HasColumnName("dateOrdered")
                    .HasColumnType("date");

                entity.Property(e => e.SubTotal).HasColumnName("subTotal");

                entity.Property(e => e.Tax).HasColumnName("tax");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .HasColumnName("reviewID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Pending).HasColumnName("pending");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReviewListId).HasColumnName("reviewListID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.ReviewList)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.ReviewListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__reviewLi__2A4B4B5E");
            });

            modelBuilder.Entity<ReviewList>(entity =>
            {
                entity.HasKey(e => e.ListId)
                    .HasName("PK__ReviewLi__7D4CA69B930E8D3A");

                entity.Property(e => e.ListId)
                    .HasColumnName("listID")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("userID")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GameId).HasColumnName("gameID");

                entity.Property(e => e.WishlistId).HasColumnName("wishlistID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.WishlistItem)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WishlistI__gameI__4AB81AF0");

                entity.HasOne(d => d.Wishlist)
                    .WithMany(p => p.WishlistItem)
                    .HasForeignKey(d => d.WishlistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WishlistI__wishl__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
