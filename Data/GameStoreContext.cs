using System;
using GameStore.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options): DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Genre>()
            .HasData(
                new  { Id = 1, Name = "MOBA"},
                new  { Id = 2, Name = "Rore play"},
                new  { Id = 3, Name = "shooting"},
                new  { Id = 4, Name = "racing"}  
            );
    }
}
