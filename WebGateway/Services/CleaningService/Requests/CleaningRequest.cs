namespace WebGateway.Services.CleaningService.Requests;

public record CleaningRequest
{
    public required float latitude { get; set; }
    public required float longitude { get; set; }
    public required int room_number { get; set; }
}