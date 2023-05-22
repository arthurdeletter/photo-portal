using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PhotoPortal.Authentication;
using PhotoPortal.Models.Custom;
using Umbraco.Headless.Client.Net;
using Umbraco.Headless.Client.Net.Security;

namespace PhotoPortal.Heartcore
{
	public class UmbracoAuthService
	{
        private readonly AuthenticationService _authenticationService;
        private readonly UmbracoManagementService _managementService;
		private readonly HttpClient _client;
        private readonly IJSRuntime _runtime;
        private readonly UmbracoAuthenticationStateProvider _customAuthProvider;

        const string _baseUrl = Constants.Urls.BaseCdnUrl;


        public UmbracoAuthService(AuthenticationService authenticationService, UmbracoManagementService managementService, AuthenticationStateProvider authState, IJSRuntime runtime)
        {
            _managementService = managementService;
            _runtime = runtime;
            _customAuthProvider = (UmbracoAuthenticationStateProvider)authState;
        }

        public async Task<AuthResponse> Login(MemberLogin login)
		{
			try
			{
                var member = await _managementService.GetMemberByUsername(login.Username);

                if (member is null)
                {
                    return new AuthResponse
                    {
                        ErrorMessage = $"No member with username {login.Username} was found.",
                        succes = false
                    };
                };

				var response = await _authenticationService.AuthenticateMember(login.Username, login.Password);

                if (!string.IsNullOrEmpty(response.Error)) return new AuthResponse
                {
                    ErrorMessage = response.Error,
                    succes = false
                };

                await _runtime.InvokeVoidAsync("localStorage.setItem", response.TokenType, response.AccessToken);

                await _customAuthProvider.UpdateAuthenticationState(new UserSession
                {
                    Username = member.Username,
                    Role = member.MemberTypeAlias
                });

                return new AuthResponse
                {
                    succes = true
                };
            }
			catch (Exception ex)
			{
				Console.WriteLine($"Login error: {ex.Message}");
                return new AuthResponse
                {
                    ErrorMessage = ex.Message,
                    succes = false
                };
			}
		}
    }
}

