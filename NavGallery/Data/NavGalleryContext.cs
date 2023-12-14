using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NavGallery.Models;

namespace NavGallery.Data
{
    public class NavGalleryContext : DbContext
    {
        public NavGalleryContext (DbContextOptions<NavGalleryContext> options)
            : base(options)
        {
        }

        public DbSet<Marca> Marca { get; set; } = default!;
        public DbSet<Car> Carros { get; set; }

    }
}
