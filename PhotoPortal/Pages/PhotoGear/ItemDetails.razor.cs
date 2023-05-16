using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;
using PhotoPortal.Models.Umbraco;

namespace PhotoPortal.Pages.PhotoGear
{
	public partial class ItemDetails
	{
        [Inject] public UmbracoService UmbracoService { get; set; } = default!;

        [Parameter] public string Route { get; set; }

		public GearItem? ItemPage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            ItemPage = (GearItem)await UmbracoService.GetContentByRoute("/gear/" + Route);
        }
    }
}

