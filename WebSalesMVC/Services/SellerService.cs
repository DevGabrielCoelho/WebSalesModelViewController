using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebSalesMVC.Data;
using WebSalesMVC.Models;
using WebSalesMVC.Services.Exceptions;

namespace WebSalesMVC.Services
{
    public class SellerService
    {
        private readonly WebSalesMVCContext _context;

        public SellerService(WebSalesMVCContext context)
        {
            _context = context;
        }
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(y => y.Department).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var x = _context.Seller.Find(id);
                _context.Seller.Remove(x);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
