using System.Data.Entity;

namespace WpfApplication1.Clients
{
    public class ClientContext : DbContext
    {
        public ClientContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Clients> Clients { get; set; }

    }
}
