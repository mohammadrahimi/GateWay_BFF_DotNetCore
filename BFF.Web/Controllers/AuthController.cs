
using BFF.Web.Authentication;
using BFF.Web.ViewModel.Auth.Role;
using BFF.Web.ViewModel.Auth.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BFF.Web.Controllers;

[Route("api/Auth")]
[ApiController]
public class AuthController : ControllerBase
{

    [HttpPost(nameof(Register))]
    public async Task<IActionResult> Register(CreateUserViewModel request)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7068");

        // var token = "";
        //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("User/Register", content);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseContent = await response.Content.ReadAsStringAsync();
            var postResponse = JsonSerializer.Deserialize<CreateUserResponse>(responseContent, options);
            return Ok(postResponse);
        }
        return Ok(response);
    }


    [HttpPost(nameof(Login))]
    public async Task<IActionResult> Login(LoginViewModel request)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7068");

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("User/Login", content);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseContent = await response.Content.ReadAsStringAsync();
            var postResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent, options);
            return Ok(postResponse);
        }
        return Ok(response.StatusCode);
    }


    [HasPermission(Permission.User, Permission.Admin)]
    [HttpPost(nameof(CreateRole))]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel request)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7068");

        // var token = "";
        //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("Role/CreateRole", content);

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var responseContent = await response.Content.ReadAsStringAsync();
            var postResponse = JsonSerializer.Deserialize<CreateRoleResponse>(responseContent, options);
            return Ok(postResponse);
        }
        return Ok(response.StatusCode);
    }
}
