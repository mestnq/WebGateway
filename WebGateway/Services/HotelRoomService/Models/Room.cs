namespace WebGateway.Services.HotelRoomService.Models;

public record Room
{
    public required string Class { get; set; }
    public required decimal Price { get; set; }
    public required int Number { get; set; }
    public required string State { get; set; }
    public required string[] ImageUrls { get; set; }
    public required int Floor { get; set; }
    public required string[] Modifiers { get; set; }
}