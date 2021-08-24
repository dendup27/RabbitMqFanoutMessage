using FanoutMessageConsumer2.Contexts;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageConsumer2.Repositories
{
    public class Consumer2Repository : IRepository<ProducerDetails>
    {
        private readonly Consumer2DbContext _context;

        public Consumer2Repository(Consumer2DbContext context)
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
