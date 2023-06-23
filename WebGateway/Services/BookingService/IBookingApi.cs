using Microsoft.AspNetCore.Mvc;
using Refit;
using WebGateway.Services.BookingService.Requests;

namespace WebGateway.Services.BookingService;

public interface IBookingApi
{
    [Get("/history/booking?passport={numberPassport}")]
    Task<IEnumerable<IActionResult>> GetHistoryBooking([AliasAs("numberPassport")] string numberPassport);

    [Get("/get/current/booking?passport={numberPassport}")]
    Task<IActionResult> GetCurrentBooking([AliasAs("numberPassport")] string numberPassport);

    [Get("/create/booking")]
    Task<IActionResult> AddBooking(GetAllInfoAboutBooking getAllInfoAboutBooking);

    [Get("/cancel/booking")]
    Task<IActionResult> CancelBooking(GetPassportRequest getPassportRequest);
}