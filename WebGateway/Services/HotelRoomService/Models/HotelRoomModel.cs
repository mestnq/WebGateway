namespace WebGateway.Services.HotelRoomService.Models;

public class HotelRoomModel
{
    public required float Latitude { get; set; }
    public required float Longitude { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public required Room[] Rooms { get; set; }
}