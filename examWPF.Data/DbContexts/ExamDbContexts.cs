using examWPF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace examWPF.Data.DbContexts
{
    public class ExamDbContexts : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ExamDbSettings.CONNECTION_STRING);
        }
        
        DbSet<User> Users { get; set; }
    }
}