namespace Cryptocop.Software.API.Models.Dtos;

public class ExchangeResponseDto
{
    public string id { get; set; }
    public string exchange_name { get; set; }
    public string exchange_slug { get; set; }
    public string quote_asset_symbol { get; set; }
    public float? price_usd { get; set; }
    public DateTime? last_trade_at { get; set; }
}