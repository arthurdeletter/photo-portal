using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Pages.Gear;

public partial class Index
{
    [Inject] public UmbracoService UmbracoService { get; set; } = default!;
    IEnumerable<Content>? content;

    protected override async Task OnInitializedAsync()
    {
        base.OnInitializedAsync();
        content = await UmbracoService.GetCameras();
    }
}

