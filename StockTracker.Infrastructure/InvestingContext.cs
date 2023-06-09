﻿using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StockTracker.Core.Entities;

#nullable disable

namespace StockTracker.Infrastructure.Investing
{
    public partial class InvestingContext : DbContext
    {
        public InvestingContext()
        {
        }

        public InvestingContext(DbContextOptions<InvestingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Averages> Averages { get; set; }
        public virtual DbSet<Rsi> Rsis { get; set; }
        public virtual DbSet<Ticker> Tickers { get; set; }
        public virtual DbSet<IndustrySector> IndustrySectors { get; set; }
        public virtual DbSet<JobStatus> JobStatuses { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<PriceDirection> PriceDirections { get; set; }
        public DbSet<EmaResult> EmaResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(ConfigurationManager.ConnectionStrings["InvestingDatabase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("activity");

                entity.HasComment("		");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("activity_date");

                entity.Property(e => e.Close)
                    .HasColumnType("decimal(9,4)")
                    .HasColumnName("close");

                entity.Property(e => e.High)
                    .HasColumnType("decimal(9,4)")
                    .HasColumnName("high");

                entity.Property(e => e.Low)
                    .HasColumnType("decimal(9,4)")
                    .HasColumnName("low");

                entity.Property(e => e.Open)
                    .HasColumnType("decimal(9,4)")
                    .HasColumnName("open");

                entity.Property(e => e.TickerId).HasColumnName("ticker_id");

                entity.Property(e => e.Updown)
                    .HasMaxLength(10)
                    .HasColumnName("updown");

                entity.Property(e => e.CandlePattern)
                    .HasMaxLength(50)
                    .HasColumnName("candle_pattern");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<PriceDirection>(entity =>
            {
                entity.ToTable("price_direction");

                entity.HasIndex(e => e.TickerId, "id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("date_added");

                entity.Property(e => e.Direction)
                    .HasMaxLength(10)
                    .HasColumnName("direction");

                entity.Property(e => e.TickerId).HasColumnName("ticker_id");
            });

            modelBuilder.Entity<Averages>(entity =>
            {
                entity.ToTable("averages");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("value");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("activity_date");

                entity.Property(e => e.AverageType)
                    .HasMaxLength(50)
                    .HasColumnName("average_type");

                entity.Property(e => e.TickerId).HasColumnName("ticker_id");
            });

            modelBuilder.Entity<Rsi>(entity =>
            {
                entity.ToTable("rsi");

                entity.HasIndex(e => e.TickerId, "id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivityDate)
                    .HasColumnType("date")
                    .HasColumnName("activity_date");

                entity.Property(e => e.AvgGain)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("avg_gain");

                entity.Property(e => e.AvgLoss)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("avg_loss");

                entity.Property(e => e.Rs)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("rs");

                entity.Property(e => e.Rsi1)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("rsi");

                entity.Property(e => e.TickerId).HasColumnName("ticker_id");

                entity.HasOne(d => d.Ticker)
                    .WithMany(p => p.Rsis)
                    .HasForeignKey(d => d.TickerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id");
            });

            modelBuilder.Entity<Ticker>(entity =>
            {
                entity.ToTable("tickers");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Industry)
                    .HasMaxLength(100)
                    .HasColumnName("industry");

                entity.Property(e => e.Sector)
                    .HasMaxLength(100)
                    .HasColumnName("sector");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(10)
                    .HasColumnName("ticker");

                entity.Property(e => e.TickerName)
                    .HasMaxLength(45)
                    .HasColumnName("ticker_name");
                
                entity.Property(e => e.Active)
                    .HasColumnName("active");
            });

            modelBuilder.Entity<IndustrySector>(entity =>
            {
                entity.ToTable("industry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Industry)
                    .HasMaxLength(100)
                    .HasColumnName("industry");

                entity.Property(e => e.Sector)
                    .HasMaxLength(100)
                    .HasColumnName("sector");

            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.ToTable("portfolio");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TickerId).HasColumnName("ticker_id");

                entity.Property(e => e.Active)
                    .HasColumnName("active");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("date")
                    .HasColumnName("date_added");
            });

            modelBuilder.Entity<EmaResult>(builder =>
            {
                builder.HasNoKey();
                builder.ToView(null);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
