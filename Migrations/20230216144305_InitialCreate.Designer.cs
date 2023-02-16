﻿// <auto-generated />
using System;
using MatchManagementApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MatchManagementApi.Migrations
{
    [DbContext(typeof(MatchManagementDataContext))]
    [Migration("20230216144305_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MatchManagementApi.Models.Match", b =>
                {
                    b.Property<int?>("MatchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("MatchID"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("Date");

                    b.Property<string>("MatchTime")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<byte>("Sport")
                        .HasColumnType("tinyint");

                    b.Property<string>("TeamA")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TeamB")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MatchID");

                    b.ToTable("Match", (string)null);
                });

            modelBuilder.Entity("MatchManagementApi.Models.MatchOdds", b =>
                {
                    b.Property<int?>("MatchOddsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("MatchOddsID"));

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<double>("Odd")
                        .HasColumnType("float");

                    b.Property<string>("Specifier")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("MatchOddsID");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchOdds", (string)null);
                });

            modelBuilder.Entity("MatchManagementApi.Models.MatchOdds", b =>
                {
                    b.HasOne("MatchManagementApi.Models.Match", null)
                        .WithMany("MatchOddss")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MatchManagementApi.Models.Match", b =>
                {
                    b.Navigation("MatchOddss");
                });
#pragma warning restore 612, 618
        }
    }
}
