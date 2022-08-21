using Autofac;
using MessagingServer.Dto;
using MessagingServer.Extensions;
using MessagingServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace MessagingServer.Controllers;

[ApiController]
[Route("message")]
public class MessagingController : ControllerBase
{

    private readonly ILogger<MessagingController> _logger;
    private readonly ILifetimeScope _scope;

    public MessagingController(ILogger<MessagingController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    [HttpPost]
    public async Task<IActionResult> Send(SendRequest request)
    {
        var messagingService = _scope.GetMessagingServiceByTag(request.ProviderTag);
        var result = await messagingService.SendAsync(request);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}