namespace DeployService.Api.Contacts.Auth;

public sealed record AuthResponse(
    string AccessToken,
    DateTime ExpiresAtUtc);