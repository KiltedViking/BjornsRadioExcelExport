using System;
using System.Collections.Generic;
using BjornsRadioExcelExport.Models;
using Microsoft.EntityFrameworkCore;

namespace BjornsRadioExcelExport.Data;

public partial class BjornsRadioContext : DbContext
{
    public BjornsRadioContext()
    {
    }

    public BjornsRadioContext(DbContextOptions<BjornsRadioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Name=ConnectionStrings:BjornsRadioConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Albums__3214EC07790AA6B9");

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Albums).HasConstraintName("FK_Albums_Genres");

            entity.HasOne(d => d.MediaNavigation).WithMany(p => p.Albums).HasConstraintName("FK_Albums_MediaTypes");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genres__3214EC0700A82894");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MediaTyp__3214EC07A7CBE4ED");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Songs__3214EC07BCD86634");

            entity.HasOne(d => d.AlbumNavigation).WithMany(p => p.Songs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Songs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
