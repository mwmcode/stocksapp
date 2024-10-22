using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
            };
        }

        public static Stock ToStockFromCreatedDTO(this CreateStockRequestDto stockReqDto)
        {
            return new Stock
            {
                Symbol = stockReqDto.Symbol,
                Company = stockReqDto.Company,
                Purchase = stockReqDto.Purchase,
                LastDiv = stockReqDto.LastDiv,
                Industry = stockReqDto.Industry,
                MarketCap = stockReqDto.MarketCap
            };
        }
        public static Stock ToPortfolioStockDto(this Stock stockModel)
        {
            return new Stock
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                Company = stockModel.Company,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }
    }
}
