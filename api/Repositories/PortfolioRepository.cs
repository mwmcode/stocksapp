using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
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