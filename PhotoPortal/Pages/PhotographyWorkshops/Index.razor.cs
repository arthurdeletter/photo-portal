using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Heartcore;
using PhotoPortal.Models.Umbraco;

namespace PhotoPortal.Pages.PhotographyWorkshops
{
	public partial class Index
	{
        [Inject] public UmbracoService UmbracoService { get; set; } = default!;
        public Workshops? WorkshopsPage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            WorkshopsPage = (Workshops)await UmbracoService.GetContentByRoute("/workshops");
        }
    }
}

