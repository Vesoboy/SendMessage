﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SendMessage;

#nullable disable

namespace SendMessage.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240117104053_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SendMessage.Models.InformEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FailedMessage")
                        .HasColumnType("text");

                    b.Property<List<string>>("Recipients")
                        .HasColumnType("text[]");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("InformEmails");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d7d68390-65ac-465c-a6e1-02721847854e"),
                            Body = "root",
                            CreatedDate = new DateTime(2024, 1, 17, 10, 40, 53, 771, DateTimeKind.Utc).AddTicks(7290),
                            FailedMessage = "root",
                            Recipients = new List<string> { "root", "root1" },
                            Result = "root",
                            Subject = "root"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
