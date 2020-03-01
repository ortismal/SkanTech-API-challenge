using System;
using Microsoft.EntityFrameworkCore;

namespace SkanTech.API.Models
{

    // Class needed to coordinate with the Entity Framework 
    public class ContactsContext : DbContext
    {
        
        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options) { }
            public DbSet<Contact> Contacts { get; set; } 
            
        }
    }
