using Refit;
using WebGateway.Services.HotelRoomService.Requests;
using WebGateway.Services.HotelRoomService.Responses;

namespace WebGateway.Services.HotelRoomService;

public interface IHotelRoomApi
{
    [Get("/Hotels")]
    Task<GetHotelResponse> GetInfoHotel(HotelRoomInfoRequest hotelRoomInfoRequest);
}