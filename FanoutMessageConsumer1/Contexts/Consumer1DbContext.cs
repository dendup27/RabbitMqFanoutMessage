using FanoutMessageLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FanoutMessageConsumer1.Contexts
{
    public class Consumer1DbContext : DbContext
    {
        public Consumer1DbContext(DbContextOptions<Consumer1DbContext> options) : base(options)
        {
        }

        public DbSet<ProducerDetails> ProducerDetails_Table { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        { }
    }
}
