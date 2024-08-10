namespace BFF.Web.ViewModel.Auth.User;


public record CreateUserResponse(string state, string message, int? statusCode, string? title);
