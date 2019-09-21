﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TwoDrive.DataAccess.Interface;

namespace TwoDrive.WebApi.Migrations
{
    [DbContext(typeof(TwoDriveContext))]
    partial class TwoDriveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TwoDrive.Domain.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<string>("Name");

                    b.Property<long>("OwnerId");

                    b.Property<long?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("TwoDrive.Domain.Folder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<long>("OwnerId");

                    b.Property<long?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("TwoDrive.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<long?>("FileId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<long?>("RootFolderId");

                    b.Property<string>("Token");

                    b.Property<long?>("UserId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("RootFolderId");

                    b.HasIndex("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "admin@admin.admin",
                            FirstName = "Admin",
                            LastName = "Istrador",
                            Password = "admin",
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("TwoDrive.Domain.File", b =>
                {
                    b.HasOne("TwoDrive.Domain.Folder", "Parent")
                        .WithMany("Files")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TwoDrive.Domain.Folder", b =>
                {
                    b.HasOne("TwoDrive.Domain.Folder", "Parent")
                        .WithMany("Folders")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TwoDrive.Domain.User", b =>
                {
                    b.HasOne("TwoDrive.Domain.File")
                        .WithMany("Readers")
                        .HasForeignKey("FileId");

                    b.HasOne("TwoDrive.Domain.Folder", "RootFolder")
                        .WithMany("Readers")
                        .HasForeignKey("RootFolderId");

                    b.HasOne("TwoDrive.Domain.User")
                        .WithMany("FriendList")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
