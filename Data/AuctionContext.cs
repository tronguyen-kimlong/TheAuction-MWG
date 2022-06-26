﻿using System;
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
        public virtual DbSet<AutoAuction> AutoAuctions { get; set; }
        public virtual DbSet<BlockAccount> BlockAccounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<FavouriteItem> FavouriteItems { get; set; }
        public virtual DbSet<HistoryBuy> HistoryBuys { get; set; }
        public virtual DbSet<HistorySearch> HistorySearches { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PaidItem> PaidItems { get; set; }
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
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;initial catalog=Auction; trusted_connection=yes;");
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
                    .HasConstraintName("FK__Approval___id_it__40F9A68C");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.ApprovalItems)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Approval___id_ma__40058253");
            });

            modelBuilder.Entity<AutoAuction>(entity =>
            {
                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsStillAuction).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.AutoAuctions)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AutoAucti__id_it__30C33EC3");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.AutoAuctions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AutoAucti__id_us__31B762FC");
            });

            modelBuilder.Entity<BlockAccount>(entity =>
            {
                entity.Property(e => e.Reason).HasDefaultValueSql("('You was violate the terms of us, so your account be block')");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.BlockAccounts)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Block_Acc__id_ma__44CA3770");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.BlockAccounts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Block_Acc__id_us__45BE5BA9");
            });

            modelBuilder.Entity<FavouriteItem>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.FavouriteItems)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__id_it__1DB06A4F");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.FavouriteItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__id_us__1CBC4616");
            });

            modelBuilder.Entity<HistoryBuy>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.HistoryBuys)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History_B__id_it__2180FB33");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.HistoryBuys)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History_B__id_us__208CD6FA");
            });

            modelBuilder.Entity<HistorySearch>(entity =>
            {
                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.HistorySearches)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__History_S__id_us__282DF8C2");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Auction).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Discount).HasDefaultValueSql("((2))");

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
                    .HasName("PK__Manager__F3DBC573C0D26AF1");

                entity.Property(e => e.Roles).HasDefaultValueSql("('mod')");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.CategoryNum).HasDefaultValueSql("((5))");

                entity.Property(e => e.Price).HasDefaultValueSql("((10000))");

                entity.Property(e => e.TimesPost).HasDefaultValueSql("((10))");
            });

            modelBuilder.Entity<PaidItem>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.PaidItems)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PaidItems__id_it__46E78A0C");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PaidItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PaidItems__id_us__47DBAE45");
            });

            modelBuilder.Entity<PaymentsAuction>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdUserBuyerNavigation)
                    .WithMany(p => p.PaymentsAuctionIdUserBuyerNavigations)
                    .HasForeignKey(d => d.IdUserBuyer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_us__37703C52");

                entity.HasOne(d => d.IdUserSellerNavigation)
                    .WithMany(p => p.PaymentsAuctionIdUserSellerNavigations)
                    .HasForeignKey(d => d.IdUserSeller)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_us__3864608B");
            });

            modelBuilder.Entity<PaymentsPostItem>(entity =>
            {
                entity.HasOne(d => d.IdPackageNavigation)
                    .WithMany(p => p.PaymentsPostItems)
                    .HasForeignKey(d => d.IdPackage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_pa__3D2915A8");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PaymentsPostItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Payments___id_us__3C34F16F");
            });

            modelBuilder.Entity<PostItem>(entity =>
            {
                entity.Property(e => e.CategoryNum).HasDefaultValueSql("((1))");

                entity.Property(e => e.TimesPost).HasDefaultValueSql("((5))");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.PostItems)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostItems__id_us__2B0A656D");
            });

            modelBuilder.Entity<ReportAccount>(entity =>
            {
                entity.HasOne(d => d.IdItemsNavigation)
                    .WithMany(p => p.ReportAccounts)
                    .HasForeignKey(d => d.IdItems)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report_Ac__id_it__25518C17");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.ReportAccounts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report_Ac__id_us__245D67DE");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__F3DBC573FB0E622F");

                entity.Property(e => e.Lockuser).HasDefaultValueSql("('active')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
