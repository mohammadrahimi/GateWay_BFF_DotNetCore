
using BFF.Web.Authentication;
using BFF.Web.ViewModel.Auth.Role;
using BFF.Web.ViewModel.Auth.User;
using BFF.Web.ViewModel.Catalog.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BFF.Web.Controllers;

[Route("api/Catalog")]
[ApiController]
public class CatalogController : ControllerBase
{
    [HasPermission(Permission.User, Permission.Admin)]
    [HttpPost(nameof(CreateProduct))]
    public async Task<IActionResult> CreateProduct(CreateProductVM request)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:8080");

        // var token = "";
        //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("Product/CreateProduct", content);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseContent = await response.Content.ReadAsStringAsync();
            var postResponse = JsonSerializer.Deserialize<CreateProductVMResponse>(responseContent, options);
            return Ok(postResponse);
        }
        return Ok(response.StatusCode);
    }

}
