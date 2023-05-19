using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PhotoPortal.Authentication;

namespace PhotoPortal.Shared
{
	public partial class AppBar
	{
        [Inject] public AuthenticationStateProvider AuthState { get; set; } = default!;
        [Inject] public NavigationManager NavManager { get; set; } = default!;

        [Parameter]
        public EventCallback OnSidebarToggled { get; set; }

        private async Task Logout()
        {
            var customAuthState = (UmbracoAuthenticationStateProvider)AuthState;
            await customAuthState.UpdateAuthenticationState(null);
            NavManager.NavigateTo("/", true);
        }
    }
}

