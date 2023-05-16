using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using MudBlazor;
using PhotoPortal.Heartcore;
using PhotoPortal.Models.Umbraco;
using PhotoPortal.Shared;

namespace PhotoPortal.Pages.PhotographyWorkshops
{
	public partial class WorkshopDetails
	{
        [Inject] public UmbracoService UmbracoService { get; set; } = default!;
        [Inject] public UmbracoManagementService ManagementService { get; set; } = default!;
        [Inject] public IDialogService DialogService { get; set; } = default!;

        [Parameter] public string Route { get; set; }

		public Workshop? WorkshopPage { get; set; }
        private int _currentRegistrations;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            await FetchWorkshopDetails();
        }

        private async Task GetWorkshopForm()
        {
            var allForms = await ManagementService.GetAllForms();

            var workshopForm = allForms.First(f => f.Name.Equals("Workshop.Registration"));

            var parameters = new DialogParameters { ["Workshop"] = WorkshopPage, ["Form"] = workshopForm };

            var dialog = await DialogService.ShowAsync<WorkshopRegistrationDialog>(WorkshopPage.WorkshopTitle, parameters, new DialogOptions { FullWidth = true });

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                _currentRegistrations = Convert.ToInt32(result.Data);   
            }
        }

        private async Task FetchWorkshopDetails()
        {
            WorkshopPage = (Workshop)await UmbracoService.GetContentByRoute("/workshops/" + Route);
            _currentRegistrations  = WorkshopPage.Registrations;
        }
    }
}

