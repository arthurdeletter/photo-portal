using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PhotoPortal.Authentication;
using PhotoPortal.Models.Custom;
using Umbraco.Headless.Client.Net;

namespace PhotoPortal.Heartcore
{
	public class UmbracoAuthService
	{
        private readonly UmbracoManagementService _managementService;
		private readonly HttpClient _client;
        private readonly IJSRuntime _runtime;
        private readonly UmbracoAuthenticationStateProvider _customAuthProvider;

        const string _baseUrl = Constants.Urls.BaseCdnUrl;


        public UmbracoAuthService(UmbracoManagementService managementService, HttpClient client, AuthenticationStateProvider authState, IConfiguration config, IJSRuntime runtime)
        {
            _managementService = managementService;
            _client = client;
            _runtime = runtime;
            _customAuthProvider = (UmbracoAuthenticationStateProvider)authState;

            ConfigureClient(config);
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

                var body = GetFormData(login.Username, login.Password);

				var response = await _client.PostAsync($"{_baseUrl}/member/oauth/token", body);

                if (!response.IsSuccessStatusCode) return new AuthResponse
                {
                    ErrorMessage = "Invalid username or password.",
                    succes = false
                };

				var result = await response.Content.ReadFromJsonAsync<AuthResult>();

                await _runtime.InvokeVoidAsync("localStorage.setItem", result.TokenType, result.AccessToken);

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

        private static FormUrlEncodedContent GetFormData(string username, string password)
        {
            return new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password),
                });
        }

        private void ConfigureClient(IConfiguration config)
        {
            var projectAlias = config.GetValue<string>("Heartcore:ProjectAlias");
            var apiKey = config.GetValue<string>("Heartcore:APIKey");

            _client.DefaultRequestHeaders.Add("Umb-Project-Alias", projectAlias);
            _client.DefaultRequestHeaders.Add("api-key", apiKey);
        }
    }
}

