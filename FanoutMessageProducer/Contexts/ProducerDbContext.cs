using FanoutMessageLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FanoutMessageProducer.Contexts
{
    public class ProducerDbContext : DbContext
    {
        public ProducerDbContext(DbContextOptions<ProducerDbContext> options) : base(options)
        {
        }

        public DbSet<ProducerDetails> ProducerDetails_Table { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        { }
    }
}
