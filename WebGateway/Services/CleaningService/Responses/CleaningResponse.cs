namespace WebGateway.Services.CleaningService.Responses;

public record CleaningResponse
{
    public required float HotelLatitude { get; set; }
    public required float HotelLongitude { get; set; }
    public required int roomNumber { get; set; }
    public required string state { get; set; }
    public required bool isCleaningRequested { get; set; }
}