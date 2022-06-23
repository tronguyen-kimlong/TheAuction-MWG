using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Auction.Models;

#nullable disable

namespace Auction.Data
{
    public partial class AuctionContext : DbContext
    {
        public AuctionContext()
        {
        }

        public AuctionContext(DbContextOptions<AuctionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApprovalItem> ApprovalItems { get; set; }
        public virtual DbSet<BlockAccount> BlockAccounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<FavouriteItem> FavouriteItems { get; set; }
        public virtual DbSet<HistoryBuy> HistoryBuys { get; set; }
        public virtual DbSet<HistorySearch> HistorySearches { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PaymentsAuction> PaymentsAuctions { get; set; }
        public virtual DbSet<PaymentsPostItem> PaymentsPostItems { get; set; }
        public virtual DbSet<PostItem> PostItems { get; set; }
        public virtual DbSet<ReportAccount> ReportAccounts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=TRONGUYEN-DESKT\\SQLEXPRESS;initial catalog=Auction; trusted_connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ApprovalItem>(entity =>
            {
                entity.Property(e => e.Reason).HasDefaultValueSql("('This items was approved')");

                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.ApprovalItems)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Approval___id_it__24E777C3");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.ApprovalItems)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Approval___id_ma__23F3538A");
            });

            modelBuilder.Entity<BlockAccount>(entity =>
            {
                entity.Property(e => e.Reason).HasDefaultValueSql("('You was violate the terms of us, so your account be block')");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.BlockAccounts)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Block_Acc__id_ma__28B808A7");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.BlockAccounts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Block_Acc__id_us__29AC2CE0");
            });

            modelBuilder.Entity<FavouriteItem>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.FavouriteItems)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__id_it__084B3915");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.FavouriteItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__id_us__075714DC");
            });

            modelBuilder.Entity<HistoryBuy>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.HistoryBuys)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History_B__id_it__0C1BC9F9");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.HistoryBuys)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History_B__id_us__0B27A5C0");
            });

            modelBuilder.Entity<HistorySearch>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.HistorySearches)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History_S__id_us__12C8C788");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Items__id_catego__2A4B4B5E");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Items__id_user__2B3F6F97");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Manager__F3DBC5732F7BFC7A");

                entity.Property(e => e.Roles).HasDefaultValueSql("('mod')");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.CategoryNum).HasDefaultValueSql("((5))");

                entity.Property(e => e.Price).HasDefaultValueSql("((10000))");

                entity.Property(e => e.TimesPost).HasDefaultValueSql("((10))");
            });

            modelBuilder.Entity<PaymentsAuction>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdUserBuyerNavigation)
                    .WithMany(p => p.PaymentsAuctionIdUserBuyerNavigations)
                    .HasForeignKey(d => d.IdUserBuyer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_us__1B5E0D89");

                entity.HasOne(d => d.IdUserSellerNavigation)
                    .WithMany(p => p.PaymentsAuctionIdUserSellerNavigations)
                    .HasForeignKey(d => d.IdUserSeller)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_us__1C5231C2");
            });

            modelBuilder.Entity<PaymentsPostItem>(entity =>
            {
                entity.HasOne(d => d.IdPackageNavigation)
                    .WithMany(p => p.PaymentsPostItems)
                    .HasForeignKey(d => d.IdPackage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_pa__2116E6DF");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PaymentsPostItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_us__2022C2A6");
            });

            modelBuilder.Entity<PostItem>(entity =>
            {
                entity.Property(e => e.CategoryNum).HasDefaultValueSql("((1))");

                entity.Property(e => e.TimesPost).HasDefaultValueSql("((5))");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PostItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostItems__id_us__15A53433");
            });

            modelBuilder.Entity<ReportAccount>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.ReportAccounts)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report_Ac__id_it__0FEC5ADD");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ReportAccounts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report_Ac__id_us__0EF836A4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__F3DBC573E6BC7616");

                entity.Property(e => e.Lockuser).HasDefaultValueSql("('active')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
