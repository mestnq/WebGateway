using Gateway.Services.HotelBaseService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Refit;
using WebGateway.Services.BookingService;
using WebGateway.Services.BookingService.Requests;
using WebGateway.Services.CleaningService;
using WebGateway.Services.CleaningService.Requests;
using WebGateway.Services.CleaningService.Responses;
using WebGateway.Services.HotelBaseService;
using WebGateway.Services.HotelRoomService;
using WebGateway.Services.HotelRoomService.Requests;
using WebGateway.Services.HotelRoomService.Responses;

namespace WebGateway.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GatewayController : ControllerBase
{
    private string realHostUrlHotelBase = "http://un1ver5e.keenetic.link:5001/";
    private string realHostUrlHotelRoom = "http://un1ver5e.keenetic.link:5000/";
    private string realHostUrlBooking = "http://195.133.49.239:3500/index.html/";
    private string realHostUrlCleaning = "https://cleaningservice-production.up.railway.app/";

    private StringValues HostUrlHotelBase => ChangedHost(KeyForHost.HotelBase, realHostUrlHotelBase);
    private StringValues HostUrlHotelRoom => ChangedHost(KeyForHost.HotelRoom, realHostUrlHotelRoom);
    private StringValues HostUrlBooking => ChangedHost(KeyForHost.Booking, realHostUrlBooking);
    private StringValues HostUrlCleaning => ChangedHost(KeyForHost.Cleaning, realHostUrlCleaning);

    private StringValues ChangedHost(KeyForHost key, string realHost)
    {
        StringValues host;
        bool canGet;
        try
        {
            canGet = HttpContext.Request.Headers.TryGetValue(key.ToString(), out host);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return !canGet ? realHost : host;
    }

    #region HotemBaseService

    /// <summary>
    /// Просит список отелей
    /// </summary>
    /// <param name="city">Город</param>
    /// <param name="order">Сортировка (0 - или 1 - )</param>
    /// <returns></returns>
    [HttpGet("all-hotels/{city}-orderBy={order}")]
    public async Task<IEnumerable<HotelBaseModel>> GetListHotels(string city, int order)
    {
        try
        {
            var hotelBaseApi = RestService.For<IHotelBaseApi>(HostUrlHotelBase!);
            return await hotelBaseApi.GetAllHotels(city, order);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    /// <summary>
    /// Просит информацию об отеле
    /// </summary>
    /// <param name="hotelId">Id отеля который пришел в GetListHotels</param>
    /// <returns></returns>
    [HttpGet("all-hotels/{hotelId}")]
    public async Task<HotelBaseModel> GetHotel(int hotelId)
    {
        try
        {
            var hotelBaseApi = RestService.For<IHotelBaseApi>(HostUrlHotelBase!);
            return await hotelBaseApi.GetHotel(hotelId);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    #endregion

    #region HotelRoomService

    /// <summary>
    /// Просит фотографии  номера, его расположение в отеле и прочую дополнительную информацию.
    /// </summary>
    /// <param name="hotelRoomInfoRequest">Координаты</param>
    /// <returns></returns>
    [HttpGet("all-hotels/more")]
    public async Task<GetHotelResponse> GetInfoHotel([FromQuery] HotelRoomInfoRequest hotelRoomInfoRequest)
    {
        try
        {
            var hotelRoomApi = RestService.For<IHotelRoomApi>(HostUrlHotelRoom!);
            return await hotelRoomApi.GetInfoHotel(hotelRoomInfoRequest);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    #endregion

    #region CleaningService

    /// <summary>
    /// Просит клининг (вызовется техническая уборка)
    /// </summary>
    /// <param name="cleaningRequest">Координаты отеля и номер комнаты</param>
    /// <returns></returns>
    [HttpGet("rooms/current/clean")]
    public async Task<CleaningResponse> GetInfoHotel([FromQuery] CleaningRequest cleaningRequest)
    {
        try
        {
            var cleaningApi = RestService.For<ICleaningApi>(HostUrlCleaning!);
            return await cleaningApi.CallCleaningToRoom(cleaningRequest);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    #endregion

    #region BookingService

    /// <summary>
    /// Просит историю брони
    /// </summary>
    /// <param name="getPassportRequest">Паспорт в виде строчки</param>
    /// <returns></returns>
    [HttpGet("booking/history")]
    public async Task<IEnumerable<IActionResult>> GetHistoryBooking([FromQuery] GetPassportRequest getPassportRequest)
    {
        try
        {
            var bookingApi = RestService.For<IBookingApi>(HostUrlBooking!);
            return await bookingApi.GetHistoryBooking(getPassportRequest.PassportNumber);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    /// <summary>
    /// Просит текущие бронирования
    /// </summary>
    /// <param name="getPassportRequest">Паспорт в виде строчки</param>
    /// <returns></returns>
    [HttpGet("booking/current")]
    public async Task<IActionResult> GetCurrentBooking([FromQuery] GetPassportRequest getPassportRequest)
    {
        try
        {
            var bookingApi = RestService.For<IBookingApi>(HostUrlBooking!);
            return await bookingApi.GetCurrentBooking(getPassportRequest.PassportNumber);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    /// <summary>
    /// Просит забронировать конкретный номер для проживания
    /// </summary>
    /// <param name="getPassportRequest">Паспорт в виде строчки</param>
    /// <returns></returns>
    [HttpGet("booking/add")]
    public async Task<IActionResult> AddBooking([FromQuery] GetAllInfoAboutBooking getAllInfoAboutBooking)
    {
        try
        {
            var bookingApi = RestService.For<IBookingApi>(HostUrlBooking!);
            return await bookingApi.AddBooking(getAllInfoAboutBooking);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    [HttpGet("booking/cancel")]
    public async Task<IActionResult> CancelBooking([FromQuery] GetPassportRequest getPassportRequest)
    {
        try
        {
            var bookingApi = RestService.For<IBookingApi>(HostUrlBooking!);
            return await bookingApi.CancelBooking(getPassportRequest);
        }
        catch (ApiException exception) //todo: polly
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    #endregion
}

public enum KeyForHost
{
    HotelBase,
    HotelRoom,
    Booking,
    Cleaning
}