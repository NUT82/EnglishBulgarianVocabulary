using DataBaseVocabulary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseVocabulary.Data
{
    class VocabolaryDBContext: DbContext
    {
        public DbSet<EnglishWord> EnglishWords { get; set; }

        public DbSet<BulgarianWord> BulgarianWords { get; set; }

        public DbSet<BulgarianWordEnglishWord> BulgarianWordsEnglishWords { get; set; }

        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=VocabolaryDB;Integrated Security=True;");
                return;
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BulgarianWordEnglishWord>().HasKey(k => new { k.EnglishWordId, k.BulgarianWordId });

            modelBuilder.Entity<EnglishWord>().HasIndex(nameof(EnglishWord.Word)).IsUnique();
            modelBuilder.Entity <BulgarianWord>().HasIndex(nameof(BulgarianWord.Word)).IsUnique();
        }
    }
}
