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

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistSong> PlaylistSongs { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongRequest> SongRequests { get; set; }

    // Added while generating using EF Tools, but moved to Program/StartUp for dependency injection (DI)
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

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC078B26D9F2");
        });

        modelBuilder.Entity<PlaylistSong>(entity =>
        {
            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistSongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaylistSongs_Playlists");

            entity.HasOne(d => d.Song).WithMany(p => p.PlaylistSongs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlaylistSongs_Songs");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Songs__3214EC07BCD86634");

            entity.HasOne(d => d.AlbumNavigation).WithMany(p => p.Songs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Songs");
        });

        modelBuilder.Entity<SongRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SongRequ__3214EC0717FA529E");

            entity.HasOne(d => d.Song).WithMany(p => p.SongRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SongRequests_Songs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
