using System;
using Microsoft.AspNetCore.Components;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Shared
{
	public partial class StandardBanner
	{
        [Parameter, EditorRequired] public string Headline { get; set; } = default!;
        [Parameter, EditorRequired] public string SubHeadline { get; set; } = default!;
        [Parameter, EditorRequired] public MediaWithCrops BannerImage { get; set; }
    }
}

