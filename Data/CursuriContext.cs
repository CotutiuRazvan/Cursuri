using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cursuri.Models;

namespace Cursuri.Data
{
    public class CursuriContext : DbContext
    {
        public CursuriContext (DbContextOptions<CursuriContext> options)
            : base(options)
        {
        }

        public DbSet<Cursuri.Models.Course> Course { get; set; } = default!;

        public DbSet<Cursuri.Models.City>? City { get; set; }

        public DbSet<Cursuri.Models.Professor>? Professor { get; set; }

        public DbSet<Cursuri.Models.Grade>? Grade { get; set; }

        public DbSet<Cursuri.Models.Member>? Member { get; set; }

        public DbSet<Cursuri.Models.Enrolling>? Enrolling { get; set; }
    }
}
