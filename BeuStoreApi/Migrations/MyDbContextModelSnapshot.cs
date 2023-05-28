﻿// <auto-generated />
using System;
using BeuStoreApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeuStoreApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AttrbutesProducts", b =>
                {
                    b.Property<Guid>("Attrbutesid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("productsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Attrbutesid", "productsId");

                    b.HasIndex("productsId");

                    b.ToTable("AttrbutesProducts");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Attrbutes", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("atrribute_name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<DateTime>("create_at")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("attributes");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.AttrbuteValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Attrbutesid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("attribute_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("attribute_value")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("color")
                        .HasColumnType("NVARCHAR(50)");

                    b.HasKey("Id");

                    b.HasIndex("Attrbutesid");

                    b.ToTable("Attribute_Value");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Cart_Items", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Cartsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<int>("stauts")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Cartsid");

                    b.HasIndex("ProductsId");

                    b.ToTable("cart_items");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Carts", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("customersid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("customersid");

                    b.ToTable("carts");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Categories", b =>
                {
                    b.Property<Guid>("categoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("category_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("category_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("parent_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("categoryId");

                    b.HasIndex("parent_id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Customers", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Gallerles", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("product_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("productsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("urlImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.HasIndex("productsId");

                    b.ToTable("gallerles");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.OrderItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Ordersid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("price")
                        .HasColumnType("NUMERIC(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Ordersid");

                    b.HasIndex("ProductsId");

                    b.ToTable("orderItems");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Orders", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("create_at")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("customersid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("update_at")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("customersid");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Products", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("createed_by")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("discount_price")
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("product_description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("product_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("regular_price")
                        .HasColumnType("numeric(18,2)");

                    b.Property<DateTime?>("update_at")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("updated_by")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("product_name");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RefreshTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("refreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("refreshTokens");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Tags", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("tag_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

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

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("profileImage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("CategoriesProducts", b =>
                {
                    b.Property<Guid>("CategoriescategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriescategoryId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CategoriesProducts");
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProductsTags", b =>
                {
                    b.Property<Guid>("Tagsid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("productsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Tagsid", "productsId");

                    b.HasIndex("productsId");

                    b.ToTable("ProductsTags");
                });

            modelBuilder.Entity("AttrbutesProducts", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Attrbutes", null)
                        .WithMany()
                        .HasForeignKey("Attrbutesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeuStoreApi.Entities.Products", null)
                        .WithMany()
                        .HasForeignKey("productsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeuStoreApi.Entities.AttrbuteValue", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Attrbutes", "Attrbutes")
                        .WithMany("attrbutes")
                        .HasForeignKey("Attrbutesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attrbutes");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Cart_Items", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Carts", "Carts")
                        .WithMany("items")
                        .HasForeignKey("Cartsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeuStoreApi.Entities.Products", "Products")
                        .WithMany("Cart_Items")
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carts");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Carts", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Customers", "customers")
                        .WithMany("carts")
                        .HasForeignKey("customersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customers");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Categories", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Categories", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("parent_id");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Gallerles", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Products", "products")
                        .WithMany("Gallerles")
                        .HasForeignKey("productsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("products");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.OrderItems", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Orders", "Orders")
                        .WithMany("orders")
                        .HasForeignKey("Ordersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeuStoreApi.Entities.Products", "Products")
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Orders", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Customers", "customers")
                        .WithMany("orders")
                        .HasForeignKey("customersid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customers");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.RefreshToken", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("CategoriesProducts", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Categories", null)
                        .WithMany()
                        .HasForeignKey("CategoriescategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeuStoreApi.Entities.Products", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("BeuStoreApi.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.User", null)
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

                    b.HasOne("BeuStoreApi.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductsTags", b =>
                {
                    b.HasOne("BeuStoreApi.Entities.Tags", null)
                        .WithMany()
                        .HasForeignKey("Tagsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeuStoreApi.Entities.Products", null)
                        .WithMany()
                        .HasForeignKey("productsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Attrbutes", b =>
                {
                    b.Navigation("attrbutes");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Carts", b =>
                {
                    b.Navigation("items");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Categories", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Customers", b =>
                {
                    b.Navigation("carts");

                    b.Navigation("orders");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Orders", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("BeuStoreApi.Entities.Products", b =>
                {
                    b.Navigation("Cart_Items");

                    b.Navigation("Gallerles");
                });
#pragma warning restore 612, 618
        }
    }
}
