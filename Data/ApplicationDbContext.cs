using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudEF.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        public DbSet<Contacto> Contactos { get; set;}
    }
}