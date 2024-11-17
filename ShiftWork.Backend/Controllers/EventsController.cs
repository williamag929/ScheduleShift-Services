using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftWork.Backend.Services;
using ShiftWork.Backend.Models;


[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly WebhookEmitterService _webhookEmitterService;

    public EventsController(WebhookEmitterService webhookEmitterService)
    {
        _webhookEmitterService = webhookEmitterService;
    }

    [HttpPost("trigger")]
    public async Task<IActionResult> TriggerEvent()
    {
        var webhookEvent = new WebhookEvent
        {
            EventType = "SampleEvent",
            Payload = "Sample payload data"
        };

        await _webhookEmitterService.EmitWebhookAsync("https://example.com/webhook-endpoint", webhookEvent);

        return Ok("Event triggered and webhook emitted");
    }
}