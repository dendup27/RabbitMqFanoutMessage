using FanoutMessageProducer.Contexts;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageProducer.Repositories
{
    public class ProducerRepository : IRepository<ProducerDetails>
    {
        private readonly ProducerDbContext _context;

        public ProducerRepository(ProducerDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProducerDetails>> GetAsync()
        {
            return await _context.ProducerDetails_Table.ToListAsync();
        }

        public async Task<bool> CreateAsync(ProducerDetails producerDetails)
        {
            if (!IsIDExistsAsync(producerDetails.Id).Result)
            {
                _context.Add(producerDetails);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> IsIDExistsAsync(Guid id)
        {
            return await _context.ProducerDetails_Table.AnyAsync(e => e.Id == id);
        }
    }
}
