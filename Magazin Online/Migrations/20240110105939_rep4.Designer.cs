﻿// <auto-generated />
using System;
using Magazin_Online.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Magazin_Online.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240110105939_rep4")]
    partial class rep4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Magazin_Online.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Magazin_Online.Models.Basket", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BasketId"), 1L, 1);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BasketId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Magazin_Online.Models.BasketProduct", b =>
                {
                    b.Property<int>("BasketProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BasketProductId"), 1L, 1);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.HasKey("BasketProductId", "ProductId", "BasketId");

                    b.HasIndex("BasketId");

                    b.HasIndex("ProductId");

                    b.ToTable("BasketProducts");
                });

            modelBuilder.Entity("Magazin_Online.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Magazin_Online.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("data_ora")
                        .HasColumnType("datetime2");

                    b.HasKey("CommentId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Magazin_Online.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductIds")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductIds");

                    b.Property<int?>("ShippingPrice")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("BasketId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Magazin_Online.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("BasketId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float?>("rating")
                        .HasColumnType("real");

                    b.HasKey("ProductId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("BasketId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Magazin_Online.Models.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RequestId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Magazin_Online.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Magazin_Online.Models.Basket", b =>
                {
                    b.HasOne("Magazin_Online.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("Magazin_Online.Models.ApplicationUser", "User")
                        .WithOne("Basket")
                        .HasForeignKey("Magazin_Online.Models.Basket", "UserId");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Magazin_Online.Models.BasketProduct", b =>
                {
                    b.HasOne("Magazin_Online.Models.Basket", "Basket")
                        .WithMany("BasketProducts")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Magazin_Online.Models.Product", "Product")
                        .WithMany("BasketProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Magazin_Online.Models.Category", b =>
                {
                    b.HasOne("Magazin_Online.Models.ApplicationUser", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Magazin_Online.Models.Comment", b =>
                {
                    b.HasOne("Magazin_Online.Models.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId");

                    b.HasOne("Magazin_Online.Models.ApplicationUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Magazin_Online.Models.Order", b =>
                {
                    b.HasOne("Magazin_Online.Models.Basket", "Basket")
                        .WithMany()
                        .HasForeignKey("BasketId");

                    b.Navigation("Basket");
                });

            modelBuilder.Entity("Magazin_Online.Models.Product", b =>
                {
                    b.HasOne("Magazin_Online.Models.ApplicationUser", null)
                        .WithMany("Baskets")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Magazin_Online.Models.Basket", "Basket")
                        .WithMany()
                        .HasForeignKey("BasketId");

                    b.HasOne("Magazin_Online.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Magazin_Online.Models.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("Magazin_Online.Models.ApplicationUser", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId");

                    b.Navigation("Basket");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Magazin_Online.Models.Request", b =>
                {
                    b.HasOne("Magazin_Online.Models.Product", "Product")
                        .WithOne("Request")
                        .HasForeignKey("Magazin_Online.Models.Request", "RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Magazin_Online.Models.ApplicationUser", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Magazin_Online.Models.Review", b =>
                {
                    b.HasOne("Magazin_Online.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId");

                    b.HasOne("Magazin_Online.Models.ApplicationUser", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Magazin_Online.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Magazin_Online.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Magazin_Online.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Magazin_Online.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Magazin_Online.Models.ApplicationUser", b =>
                {
                    b.Navigation("Basket");

                    b.Navigation("Baskets");

                    b.Navigation("Categories");

                    b.Navigation("Comments");

                    b.Navigation("Products");

                    b.Navigation("Requests");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Magazin_Online.Models.Basket", b =>
                {
                    b.Navigation("BasketProducts");
                });

            modelBuilder.Entity("Magazin_Online.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Magazin_Online.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Magazin_Online.Models.Product", b =>
                {
                    b.Navigation("BasketProducts");

                    b.Navigation("Comments");

                    b.Navigation("Request");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
