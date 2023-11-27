using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using project.Models.DBObjects;

namespace project.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Checkout> Checkouts { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<Publisher> Publishers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");
            //
            //    entity.Property(e => e.Name).HasMaxLength(256);
            //
            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});
            //
            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");
            //
            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});
            //
            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");
            //
            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");
            //
            //    entity.Property(e => e.Email).HasMaxLength(256);
            //
            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            //
            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            //
            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //
            //    entity.HasMany(d => d.Roles)
            //        .WithMany(p => p.Users)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "AspNetUserRole",
            //            l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
            //            r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
            //            j =>
            //            {
            //                j.HasKey("UserId", "RoleId");
            //
            //                j.ToTable("AspNetUserRoles");
            //
            //                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
            //            });
            //});
            //
            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");
            //
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});
            //
            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            //
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");
            //
            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);
            //
            //    entity.Property(e => e.ProviderKey).HasMaxLength(128);
            //
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});
            //
            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            //
            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);
            //
            //    entity.Property(e => e.Name).HasMaxLength(128);
            //
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId)
                    .ValueGeneratedNever()
                    .HasColumnName("Book_ID");

                entity.Property(e => e.BookAuthor).HasColumnName("Book_Author");

                entity.Property(e => e.BookCoverType)
                    .HasMaxLength(10)
                    .HasColumnName("Book_CoverType")
                    .IsFixedLength();

                entity.Property(e => e.BookGenreID).HasColumnName("Book_GenreID");

                entity.Property(e => e.BookName)
                    .HasMaxLength(30)
                    .HasColumnName("Book_Name")
                    .IsFixedLength();

                entity.Property(e => e.BookNumberOfUnits).HasColumnName("Book_NumberOfUnits");

                entity.Property(e => e.BookNumberOfUnitsAvailable).HasColumnName("Book_NumberOfUnits_Available");

                entity.Property(e => e.BookPublishYear).HasColumnName("Book_PublishYear");

                entity.Property(e => e.BookPublisherID).ValueGeneratedNever().HasColumnName("Book_PublisherID");

                entity.HasOne(d => d.BookGenre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookGenreID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_Genre");

                entity.HasOne(d => d.BookPublisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookPublisherID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Books_Publisher");
            });

            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.ToTable("Checkout");

                entity.Property(e => e.CheckoutId)
                    .ValueGeneratedNever()
                    .HasColumnName("Checkout_ID");

                entity.Property(e => e.CheckoutBookId).HasColumnName("Checkout_BookID");

                entity.Property(e => e.CheckoutCheckoutDate)
                    .HasColumnType("date")
                    .HasColumnName("Checkout_CheckoutDate");

                entity.Property(e => e.CheckoutDueDate)
                    .HasColumnType("date")
                    .HasColumnName("Checkout_DueDate");

                entity.Property(e => e.CheckoutEmployeeName)
                    .HasMaxLength(30)
                    .HasColumnName("Checkout_EmployeeName")
                    .IsFixedLength();

                entity.Property(e => e.CheckoutMemberId).HasColumnName("Checkout_MemberID");

                entity.Property(e => e.CheckoutReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("Checkout_ReturnDate");

                entity.HasOne(d => d.CheckoutBook)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.CheckoutBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_Book");

                entity.HasOne(d => d.CheckoutMember)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.CheckoutMemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_Member");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId)
                    .ValueGeneratedNever()
                    .HasColumnName("Genre_ID");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(15)
                    .HasColumnName("Genre_Name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("Member_ID");

                entity.Property(e => e.MemberAdress)
                    .HasMaxLength(30)
                    .HasColumnName("Member_Adress")
                    .IsFixedLength();

                entity.Property(e => e.MemberEmail)
                    .HasMaxLength(30)
                    .HasColumnName("Member_Email")
                    .IsFixedLength();

                entity.Property(e => e.MemberMembershipExpirationDate)
                    .HasColumnType("date")
                    .HasColumnName("Member_MembershipExpirationDate");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(30)
                    .HasColumnName("Member_Name")
                    .IsFixedLength();

                entity.Property(e => e.MemberPhoneNumber)
                    .HasMaxLength(12)
                    .HasColumnName("Member_PhoneNumber")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.PublisherId)
                    .ValueGeneratedNever()
                    .HasColumnName("Publisher_ID");

                entity.Property(e => e.PublisherName)
                    .HasMaxLength(30)
                    .HasColumnName("Publisher_Name")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
