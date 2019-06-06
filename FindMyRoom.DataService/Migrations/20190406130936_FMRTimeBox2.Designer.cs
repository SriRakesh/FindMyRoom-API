﻿// <auto-generated />
using System;
using FindMyRoom.DataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FindMyRoom.DataService.Migrations
{
    [DbContext(typeof(FindMyRoomDb))]
    [Migration("20190406130936_FMRTimeBox2")]
    partial class FMRTimeBox2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FindMyRoom.Entities.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RenterId");

                    b.Property<int>("RoomId");

                    b.Property<string>("Status");

                    b.HasKey("BookId");

                    b.HasIndex("RenterId");

                    b.HasIndex("RoomId");

                    b.ToTable("FMRBookingList");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.GeoLocation", b =>
                {
                    b.Property<int>("GeoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<int>("RoomId");

                    b.HasKey("GeoId");

                    b.HasIndex("RoomId");

                    b.ToTable("FMRGeolocation");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoomId");

                    b.Property<byte[]>("RoomImage");

                    b.HasKey("ImageId");

                    b.HasIndex("RoomId");

                    b.ToTable("FMRImages");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.Owner", b =>
                {
                    b.Property<int>("OwnerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PartnerId");

                    b.Property<int>("RoomId");

                    b.HasKey("OwnerID");

                    b.HasIndex("PartnerId");

                    b.HasIndex("RoomId");

                    b.ToTable("FMROwners");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Area")
                        .IsRequired();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int>("Cost");

                    b.Property<string>("Description");

                    b.Property<string>("Furniture");

                    b.Property<int>("NoOfBeds");

                    b.Property<int>("Pincode");

                    b.Property<string>("RoomType");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("RoomId");

                    b.ToTable("FMRRooms");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.SocialLogin", b =>
                {
                    b.Property<int>("SocialId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProviderId");

                    b.Property<string>("ProviderName");

                    b.Property<int>("UserId");

                    b.HasKey("SocialId");

                    b.HasIndex("UserId");

                    b.ToTable("FMRSociallogin");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserAddress")
                        .IsRequired();

                    b.Property<string>("UserEmail")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserPassword")
                        .IsRequired();

                    b.Property<string>("UserPhoneNumber")
                        .IsRequired();

                    b.Property<string>("UserStatus");

                    b.Property<string>("UserType");

                    b.HasKey("UserId");

                    b.ToTable("FMRUsers");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.WishList", b =>
                {
                    b.Property<int>("WishListId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RenterId");

                    b.Property<int>("RoomId");

                    b.HasKey("WishListId");

                    b.HasIndex("RenterId");

                    b.HasIndex("RoomId");

                    b.ToTable("FMRWishLists");
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.Book", b =>
                {
                    b.HasOne("FindMyRoom.Entities.Models.User", "FMRUsers")
                        .WithMany()
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FindMyRoom.Entities.Models.Room", "FMRRooms")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.GeoLocation", b =>
                {
                    b.HasOne("FindMyRoom.Entities.Models.Room", "FMRRooms")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.Image", b =>
                {
                    b.HasOne("FindMyRoom.Entities.Models.Room", "FMRRooms")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.Owner", b =>
                {
                    b.HasOne("FindMyRoom.Entities.Models.User", "FMRUsers")
                        .WithMany()
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FindMyRoom.Entities.Models.Room", "FMRRooms")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.SocialLogin", b =>
                {
                    b.HasOne("FindMyRoom.Entities.Models.User", "FMRUsers")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FindMyRoom.Entities.Models.WishList", b =>
                {
                    b.HasOne("FindMyRoom.Entities.Models.User", "FMRUsers")
                        .WithMany()
                        .HasForeignKey("RenterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FindMyRoom.Entities.Models.Room", "FMRRooms")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
