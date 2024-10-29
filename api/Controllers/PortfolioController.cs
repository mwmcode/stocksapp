using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepo,
            IPortfolioRepository portfolioRepo
        )
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            // User comes from the context of ControllerBase
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("Stock not found!");
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);

            if (userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Stock already exists in user portfolio");
            }

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = user.Id,
            };

            var portfolio = await _portfolioRepo.CreateAsync(portfolioModel);
            if (portfolio == null)
            {
                return StatusCode(500, "Failed to create portfolio");
            }
            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("Stock not found!");
            }

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(user);
            var stockReq = userPortfolio.Where(p => p.Symbol.ToLower() == symbol.ToLower());

            if (stockReq.Count() != 1)
            {
                return BadRequest("Stock not found in user portfolio");
            }

            await _portfolioRepo.DeletePortfolio(user, symbol);

            return Ok();
        }
    }
}