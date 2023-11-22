﻿using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Entities;
using ShortUrl.Services;

namespace ShortUrl
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberOfCharsInShortLink);

                builder.HasIndex(s => s.Code).IsUnique();
            });
        }
    }
}

