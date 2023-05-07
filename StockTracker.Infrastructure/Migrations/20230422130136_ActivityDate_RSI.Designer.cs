﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockTracker.Infrastructure.Investing;

#nullable disable

namespace StockTracker.Infrastructure.Migrations
{
    [DbContext(typeof(InvestingContext))]
    [Migration("20230422130136_ActivityDate_RSI")]
    partial class ActivityDate_RSI
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StockTracker.Core.Entities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("ActivityDate")
                        .HasColumnType("date")
                        .HasColumnName("activity_date");

                    b.Property<decimal>("Close")
                        .HasColumnType("decimal(9,4)")
                        .HasColumnName("close");

                    b.Property<decimal?>("High")
                        .HasColumnType("decimal(9,4)")
                        .HasColumnName("high");

                    b.Property<decimal?>("Low")
                        .HasColumnType("decimal(9,4)")
                        .HasColumnName("low");

                    b.Property<decimal>("Open")
                        .HasColumnType("decimal(9,4)")
                        .HasColumnName("open");

                    b.Property<int>("TickerId")
                        .HasColumnType("int")
                        .HasColumnName("ticker_id");

                    b.Property<string>("Updown")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("updown");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.ToTable("activity", (string)null);

                    b.HasComment("		");
                });

            modelBuilder.Entity("StockTracker.Core.Entities.Averages", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("ActivityDate")
                        .HasColumnType("date")
                        .HasColumnName("activity_date");

                    b.Property<string>("AverageType")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("average_type");

                    b.Property<int>("TickerId")
                        .HasColumnType("int")
                        .HasColumnName("ticker_id");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("averages", (string)null);
                });

            modelBuilder.Entity("StockTracker.Core.Entities.JobStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActivityDescription")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("ActivityTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("JobStatuses");
                });

            modelBuilder.Entity("StockTracker.Core.Entities.MovingAverages", b =>
                {
                    b.Property<DateTime>("ActivityDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Activity_Date");

                    b.Property<decimal>("EMA12")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("EMA12");

                    b.Property<decimal?>("EMA26")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("EMA26");

                    b.Property<decimal?>("MA9")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("MA9");

                    b.Property<decimal?>("MACD")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("MACD");

                    b.Property<decimal?>("Signal")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("Signal");

                    b.Property<string>("Symbol")
                        .HasColumnType("longtext")
                        .HasColumnName("Symbol");

                    b.Property<int>("TickerId")
                        .HasColumnType("int")
                        .HasColumnName("Ticker_Id");

                    b.Property<string>("TickerName")
                        .HasColumnType("longtext")
                        .HasColumnName("Ticker_Name");

                    b.Property<decimal?>("Vol90")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("Vol90");

                    b.ToView("moving_averages");
                });

            modelBuilder.Entity("StockTracker.Core.Entities.Rsi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("ActivityDate")
                        .HasColumnType("date")
                        .HasColumnName("activity_date");

                    b.Property<decimal>("AvgGain")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("avg_gain");

                    b.Property<decimal>("AvgLoss")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("avg_loss");

                    b.Property<decimal>("Rs")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("rs");

                    b.Property<decimal>("Rsi1")
                        .HasColumnType("decimal(9,2)")
                        .HasColumnName("rsi");

                    b.Property<int>("TickerId")
                        .HasColumnType("int")
                        .HasColumnName("ticker_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TickerId" }, "id_idx");

                    b.ToTable("rsi", (string)null);
                });

            modelBuilder.Entity("StockTracker.Core.Entities.Ticker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Industry")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("industry");

                    b.Property<string>("Sector")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("sector");

                    b.Property<string>("Symbol")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("ticker");

                    b.Property<string>("TickerName")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("ticker_name");

                    b.HasKey("Id");

                    b.ToTable("tickers", (string)null);
                });

            modelBuilder.Entity("StockTracker.Core.Entities.Rsi", b =>
                {
                    b.HasOne("StockTracker.Core.Entities.Ticker", "Ticker")
                        .WithMany("Rsis")
                        .HasForeignKey("TickerId")
                        .IsRequired()
                        .HasConstraintName("id");

                    b.Navigation("Ticker");
                });

            modelBuilder.Entity("StockTracker.Core.Entities.Ticker", b =>
                {
                    b.Navigation("Rsis");
                });
#pragma warning restore 612, 618
        }
    }
}
