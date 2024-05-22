using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PajoPhone.Models;

namespace PajoPhone.Data
{
    public class PajoPhoneContext : DbContext
    {
        public PajoPhoneContext (DbContextOptions<PajoPhoneContext> options)
            : base(options)
        {
        }

        public DbSet<PajoPhone.Models.Phone> Phone { get; set; } = default!;
    }
}
