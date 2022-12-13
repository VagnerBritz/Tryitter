using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tryitter.Models;

    public class TryitterContext : DbContext
    {
        public TryitterContext (DbContextOptions<TryitterContext> options)
            : base(options)
        {
        }

        public DbSet<User>? User { get; set; } = default!;
        public DbSet<Post>? Post { get; set; }
    }
