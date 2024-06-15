using Microsoft.EntityFrameworkCore;
using WebSalesMVC.Data;
using WebSalesMVC.Models;

namespace WebSalesMVC.Services
{
    public class SalesRecordService
    {
        private readonly WebSalesMVCContext _context;

        public SalesRecordService(WebSalesMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var list = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                list = list.Where(x => x.Date >= minDate.Value);
            }
            
            if (maxDate.HasValue)
            {
                list = list.Where(x => x.Date <= maxDate.Value);
            }

            return await list.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }
        
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var list = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                list = list.Where(x => x.Date >= minDate.Value);
            }
            
            if (maxDate.HasValue)
            {
                list = list.Where(x => x.Date <= maxDate.Value);
            }

            return await list.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).GroupBy(x => x.Seller.Department).ToListAsync();
        }
    }
}
