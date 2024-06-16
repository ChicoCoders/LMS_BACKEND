using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LMS.Data
{  
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        
        public DbSet<User> Users { get; set; }
      
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Cupboard> Cupboard { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RequestResource> Requests { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<NotificationUser> NotificationUser { get; set; }
        public DbSet<FirebaseConnection> FirebaseConnections { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=tcp:easylibro.database.windows.net,1433;Initial Catalog=easylibro;Persist Security Info=False;User ID=easylibro;Password=kavidil2001#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Reservation>()
             .HasOne(a => a.IssuedBy)
             .WithMany()
             .HasForeignKey(a => a.IssuedByID);
        }



    }

}
