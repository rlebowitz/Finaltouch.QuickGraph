using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Finaltouch.QuickGrid.Web.Shared.Models;
//https://www.c-sharpcorner.com/article/get-started-with-entity-framework-core-using-sqlite/
public partial class BabynamesContext : DbContext
{
    public BabynamesContext(DbContextOptions<BabynamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Babyname> Babynames { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Babyname>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BabyNames");

            entity.HasIndex(e => new { e.Name, e.Sex }, "name_sex_on_babynames");

            entity.HasIndex(e => new { e.Name, e.State }, "name_state_on_babynames");

            entity.HasIndex(e => new { e.RankWithinSex, e.State }, "rank_state_on_babynames");

            entity.Property(e => e.Count)
                .HasConversion<int>()
                .HasColumnType("NUMERIC")
                .HasColumnName("count");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Per100kWithinSex)
                .HasConversion<float>()
                .HasColumnType("FLOAT")
                .HasColumnName("per_100k_within_sex");
            entity.Property(e => e.RankWithinSex)
                .HasConversion<int>()
                .HasColumnType("NUMERIC")
                .HasColumnName("rank_within_sex");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Year)
                .HasConversion<int>()
                .HasColumnType("NUMERIC")
                .HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
