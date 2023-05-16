using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;
using PhotoPortal.Models.Umbraco;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Pages.Home;

public partial class Index
{
	[Inject] public UmbracoService UmbracoService { get; set; } = default!;
    public HomePage? HomePage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        HomePage = (HomePage)await UmbracoService.GetContentByRoute("/home");
    }
}

