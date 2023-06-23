namespace WebGateway.Services.BookingService.Requests;

public record GetAllInfoAboutBooking
{
    public required string PassportNumber { get; set; }
    public required int HotelId { get; set; }
    public required int RoomId { get; set; }
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
    public required DateTime BookingStartDate { get; set; }
    public required DateTime BookingEndDate { get; set; }
}