using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using RestaurantOrderingSystem.Models;
using System.Security.Claims;

namespace RestaurantOrderingSystem.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            // Try to get the session from the browser's local storage
            var result = await _localStorage.GetAsync<UserSession>("UserSession");
            var userSession = result.Success ? result.Value : null;

            if (userSession == null)
                return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimsPrincipal = CreateClaimsPrincipal(userSession);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession != null)
        {
            await _localStorage.SetAsync("UserSession", userSession);
            claimsPrincipal = CreateClaimsPrincipal(userSession);
        }
        else
        {
            await _localStorage.DeleteAsync("UserSession");
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    private ClaimsPrincipal CreateClaimsPrincipal(UserSession userSession)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userSession.Id),
            new Claim(ClaimTypes.Name, userSession.Name),
            new Claim(ClaimTypes.Email, userSession.Email),
            new Claim(ClaimTypes.Role, userSession.Role)
        };
        return new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
    }
}