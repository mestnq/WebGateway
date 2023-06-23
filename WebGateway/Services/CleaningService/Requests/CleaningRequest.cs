namespace WebGateway.Services.CleaningService.Requests;

public record CleaningRequest
{
    public required float HotelLatitude { get; set; }
    public required float HotelLongitude { get; set; }
    public required int RoomNumber { get; set; }
}