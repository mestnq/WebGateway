using Gateway.Services.HotelBaseService.Model;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace WebGateway.Services.HotelBaseService;

public interface IHotelBaseApi
{
    [Get("/Hotel/all?order={order}&city={city}")]
    Task<IEnumerable<HotelBaseModel>> GetAllHotels([AliasAs("city")] string city, [AliasAs("order")] int order);

    [Get("/Hotel/{hotelId}")]
    Task<HotelBaseModel> GetHotel([AliasAs("hotelId")] int hotelId);
}