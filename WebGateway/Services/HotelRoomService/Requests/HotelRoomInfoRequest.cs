namespace WebGateway.Services.HotelRoomService.Requests;

public record HotelRoomInfoRequest
{
    public required float HotelLatitude { get; set; }
    public required float HotelLongitude { get; set; }
}