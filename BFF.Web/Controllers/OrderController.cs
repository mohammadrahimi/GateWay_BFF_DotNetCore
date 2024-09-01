
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using BFF.Web.Authentication;
using Grpc.Net.Client;
using BFF.Web.ViewModel.Order;

namespace BFF.Web.Controllers;

[Route("api/Order")]
[ApiController]
public class OrderController : ControllerBase
{
    [HasPermission(Permission.User, Permission.Admin)]
    [HttpPost(nameof(ByOrderIDQuery))]
    public async Task<IActionResult> ByOrderIDQuery(QueryOrderVM request)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5288");
        var client = new OrderQuery.OrderQueryClient(channel);
        var response = await client.ByOrderIDAsync(new OrderRequest { OrderId = request.orderId });

        return Ok(new { order = response });
    }

    [HasPermission(Permission.User, Permission.Admin)]
    [HttpGet(nameof(AllOrderQuery))]
    public async Task<IActionResult> AllOrderQuery()
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5288");
        var client = new OrderQuery.OrderQueryClient(channel);
        var response = await client.AllOrdersAsync(new Google.Protobuf.WellKnownTypes.Empty());

        return Ok(new { orders = response });
    }

}
