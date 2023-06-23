using Microsoft.AspNetCore.Mvc;
using Refit;
using WebGateway.Services.CleaningService.Requests;
using WebGateway.Services.CleaningService.Responses;

namespace WebGateway.Services.CleaningService;

public interface ICleaningApi
{
    [Get("/gatewayOrder")]
    Task<CleaningResponse> CallCleaningToRoom(CleaningRequest cleaningRequest);
}