﻿using InRealTime.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InRealTime.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
