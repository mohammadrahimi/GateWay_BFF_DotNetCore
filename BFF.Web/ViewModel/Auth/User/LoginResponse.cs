namespace BFF.Web.ViewModel.Auth.User;


public record LoginResponse(string token, string? state, string? message);

