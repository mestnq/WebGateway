using WebGateway.Services.HotelRoomService.Models;

namespace WebGateway.Services.HotelRoomService.Responses;

public record GetHotelResponse
{
    public required HotelRoomModel Hotel { get; set; }
}