using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftWork.Backend.Services;
using ShiftWork.Backend.Models;

namespace ShiftWork.Backend.Controllers;

[ApiController]
[Route("api/webhooks")]

//https://dev.to/damikun/integrate-webhook-under-net-c-backend-4f7
//https://www.c-sharpcorner.com/article/webhooks-in-net/

public class WebhooksController : ControllerBase
{
    [HttpPost]
    public IActionResult ReceiveWebhook(WebhookData webhookData)
    {
        // Handle the incoming webhook data here
        // You can perform actions based on the event and payload

        // For demonstration purposes, we'll just return a success response
        return Ok("Webhook received successfully");
    }
}