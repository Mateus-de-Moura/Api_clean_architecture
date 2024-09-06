using api_clean_architecture.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_clean_architecture.Infra.Data.Data
{
    public class TasksDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspace { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<ListCard> ListCard { get; set; }
    }
}
