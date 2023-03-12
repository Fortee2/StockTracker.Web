using System;
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
        public virtual DbSet<MovingAverages> MovingAverages { get; set; }


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

                entity.Property(e => e.Volume).HasColumnName("volume");
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
            });

            modelBuilder.Entity<MovingAverages>(entity =>
            {
                entity.Property(e => e.ActivityDate)
                    .HasColumnName("Activity_Date");

                entity.Property(e => e.TickerId)
                    .HasColumnName("Ticker_Id");

                entity.Property(e => e.TickerName)
                    .HasColumnName("Ticker_Name");

                entity.Property(e => e.Symbol)
                    .HasColumnName("Symbol");

                entity.Property(e => e.EMA12)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("EMA12");

                entity.Property(e => e.EMA26)
                  .HasColumnType("decimal(9,2)")
                  .HasColumnName("EMA26");

                entity.Property(e => e.MA9)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("MA9");

                entity.Property(e => e.Vol90)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("Vol90");

                entity.Property(e => e.MACD)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("MACD");

                entity.Property(e => e.Signal)
                    .HasColumnType("decimal(9,2)")
                    .HasColumnName("Signal");

                entity.HasNoKey();
                entity.ToView("moving_averages");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
