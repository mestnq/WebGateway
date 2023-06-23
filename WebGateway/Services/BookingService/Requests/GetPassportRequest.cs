namespace WebGateway.Services.BookingService.Requests;

public record GetPassportRequest
{
    public required string PassportNumber { get; set; }
}