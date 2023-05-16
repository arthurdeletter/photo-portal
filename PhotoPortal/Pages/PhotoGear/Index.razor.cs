using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;
using PhotoPortal.Models.Umbraco;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Pages.PhotoGear;

public partial class Index
{
    [Inject] public UmbracoService UmbracoService { get; set; } = default!;
    public Gear? GearPage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        GearPage = (Gear) await UmbracoService.GetContentByRoute("/gear");
    }
}

