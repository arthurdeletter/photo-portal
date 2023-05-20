using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private readonly UmbracoManagementService _managementService;
		private readonly HttpClient _client;
        private readonly IJSRuntime _runtime;

        private UmbracoAuthenticationStateProvider _customAuthProvider;
		private string? _projectAlias;
		private string? _apiKey;

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
                }

                var body = GetFormData(login.Username, login.Password);

				var response = await _client.PostAsync($"{_baseUrl}/member/oauth/token", body);

                if (!response.IsSuccessStatusCode) return new AuthResponse
                {
                    ErrorMessage = "Username and password combination incorrect.",
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
            _projectAlias = config.GetValue<string>("Heartcore:ProjectAlias");
            _apiKey = config.GetValue<string>("Heartcore:APIKey");

            _client.DefaultRequestHeaders.Add("Umb-Project-Alias", _projectAlias);
            _client.DefaultRequestHeaders.Add("api-key", _apiKey);
        }
    }
}

