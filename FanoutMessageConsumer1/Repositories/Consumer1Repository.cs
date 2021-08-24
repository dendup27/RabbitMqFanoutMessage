using FanoutMessageConsumer1.Contexts;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageConsumer1.Repositories
{
    public class Consumer1Repository : IRepository<ProducerDetails>
    {
        private readonly Consumer1DbContext _context;

        public Consumer1Repository(Consumer1DbContext context)
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
