using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PhotoPortal.Authentication;
using PhotoPortal.Heartcore;
using PhotoPortal.Models.Custom;

namespace PhotoPortal.Pages.Login
{
	public partial class Index
	{
		[Inject] public AuthenticationStateProvider AuthState { get; set; } = default!;
		[Inject] public UmbracoAuthService AuthenticationService { get; set; } = default!;
		[Inject] public NavigationManager NavManager { get; set; } = default!;

		public MemberLogin LoginModel { get; set; } = new();
		private string error = "";

		private async Task Login()
		{
			if (string.IsNullOrEmpty(LoginModel.Username) || string.IsNullOrEmpty(LoginModel.Password))
			{
				error = "Username or password cannot be empty.";
				return;
			}

			var response = await AuthenticationService.Login(LoginModel);

			if (!response.succes)
			{
				error = response?.ErrorMessage;
				return;
			}

			error = string.Empty;

			NavManager.NavigateTo("/", true);
		}
	}
}

