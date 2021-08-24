using FanoutMessageLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FanoutMessageConsumer2.Contexts
{
    public class Consumer2DbContext : DbContext
    {
        public Consumer2DbContext(DbContextOptions<Consumer2DbContext> options) : base(options)
        {
        }

        public DbSet<ProducerDetails> ProducerDetails_Table { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        { }
    }
}
