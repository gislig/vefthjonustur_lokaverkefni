namespace Cryptocop.Software.API.Models.Dtos;

public class CryptoResponseDto
{
    public string id { get; set; }
    public string symbol { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public float price_usd { get; set; }
    public string project_details { get; set; }
}