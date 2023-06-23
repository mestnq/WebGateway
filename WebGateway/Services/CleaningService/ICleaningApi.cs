using Microsoft.AspNetCore.Mvc;
using Refit;
using WebGateway.Services.CleaningService.Requests;

namespace WebGateway.Services.CleaningService;

public interface ICleaningApi
{
    [Get("/gatewayOrder")]
    Task<IActionResult> CallCleaningToRoom(CleaningRequest cleaningRequest);
}