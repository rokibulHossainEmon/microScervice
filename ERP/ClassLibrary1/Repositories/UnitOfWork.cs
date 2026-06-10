using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Persistence_Layer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence_Layer.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;
        private IGenericRepository<Product>? _products;
        private IGenericRepository<Order>? _orders;

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);//it will chack database exists the table if exist then take the table otherwishe make a new table..
        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();// (Dispose) database connection get off and free the memory
    }
}
