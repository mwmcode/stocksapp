using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PortfolioRepository(ApplicationDbContext context) : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolio(AppUser user, string symbol)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.AppUserId == user.Id && p.Stock.Symbol == symbol);

            if (portfolio == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            // return await _context.Portfolios.Where(p => p.AppUserId == user.Id).Select(stock => stock.ToStockDto());
            return await _context.Portfolios
                .Where(p => p.AppUserId == user.Id)
                .Select(p => p.Stock.ToPortfolioStockDto())
                .ToListAsync();

        }
    }
}