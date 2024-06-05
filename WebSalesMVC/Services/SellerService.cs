using Microsoft.EntityFrameworkCore;
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
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }
        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }
        public Seller FindById(int id)
        {
            return _context.Seller.Include(y => y.Department).FirstOrDefault(x => x.Id == id);
        }
        public void Remove(int id)
        {
            var x = _context.Seller.Find(id);
            _context.Seller.Remove(x);
            _context.SaveChanges();
        }

        public void Update(Seller seller) 
        {
            if (!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbConcurrencyException ex) 
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
