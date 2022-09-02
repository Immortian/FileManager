using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FileManager.Data
{
    public partial class historydbContext : DbContext
    {
        public historydbContext()
        {
        }

        public historydbContext(DbContextOptions<historydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<History> Histories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DateVisited)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_visited");

                entity.Property(e => e.Filename)
                    .HasMaxLength(150)
                    .HasColumnName("filename");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
