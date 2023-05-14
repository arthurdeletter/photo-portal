using System;
using BachVisuals.Models;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;

namespace PhotoPortal.Pages.PhotographyWorkshops
{
	public partial class WorkshopDetails
	{
        [Inject] public UmbracoService UmbracoService { get; set; } = default!;

        [Parameter] public string Route { get; set; }

		public Workshop? WorkshopPage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            WorkshopPage = (Workshop)await UmbracoService.GetContentByRoute("/workshops/" + Route);
        }
    }
}

