using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Pages.Home;

public partial class Index
{
	[Inject] public UmbracoService UmbracoService { get; set; } = default!;
	Content root = new Content();
	ContentCollection<Content> content = new ContentCollection<Content>();

    protected override async Task OnInitializedAsync()
    {
        root = await UmbracoService.GetRoot();
    }
}

