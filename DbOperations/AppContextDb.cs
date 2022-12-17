using Kontaklar.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kontaklar.DbOperations
{
    public class AppContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\DEVBEYT;Database=MyLocalDb;Trusted_Connection=true");
        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
