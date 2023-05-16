using System;
using BachVisuals.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PhotoPortal.Heartcore;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;

namespace PhotoPortal.Shared
{
	public partial class WorkshopRegistrationDialog
	{
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
        [Parameter] public Form Form { get; set; }
        [Parameter] public Workshop Workshop { get; set; }

        [Inject] public ISnackbar Snackbar { get; set; } = default!;
        [Inject] public UmbracoManagementService ManagementService { get; set; } = default!;

        public IEnumerable<FormField> FormFields => Form.Pages.First().Fieldsets.First().Columns.First().Fields;

        public WorkshopRegistration Model { get; set; } = new();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private async void OnValidSubmit()
        {
            // Fetch correct content for management
            var contentItem = await ManagementService.GetById(Workshop.Id);
            int registrations = contentItem.Properties["registrations"]["$invariant"] != string.Empty ? Convert.ToInt32(contentItem.Properties["registrations"]["$invariant"]) : 0;
            int maxRegistrations = Convert.ToInt32(contentItem.Properties["maxRegistrations"]["$invariant"]);

            if (registrations == maxRegistrations) {
                Snackbar.Add("Sorry, Workshop is full", Severity.Error);
                MudDialog.Cancel();
                return;
            }

            Model.WorkshopToRegister = Workshop;

            var formValues = GenerateFormValues(Model);

            var succes = await ManagementService.SubmitFormEntry(Form.Id, formValues);

            if (!succes)
            {
                Snackbar.Add("Something went wrong, Please try again later.", Severity.Error);
                return;
            }

            // Set the value
            int updatedRegistrationCount = registrations += 1;
            contentItem.SetValue("registrations", updatedRegistrationCount);

            // Update the content item in Umbraco
            var updatedItem = await ManagementService.Update(contentItem);

            // Publish the content item
            var publishedItem = await ManagementService.Publish(updatedItem.Id);

            Snackbar.Add("Succesfully registered to " + Workshop.WorkshopTitle, Severity.Success);
            MudDialog.Close(DialogResult.Ok(Convert.ToInt32(publishedItem.Properties["registrations"]["$invariant"])));
        }

        private static Dictionary<string, object> GenerateFormValues(WorkshopRegistration registration)
        {
            var formValues = new Dictionary<string, object>();

            formValues.Add("firstName", registration.FirstName);
            formValues.Add("lastName", registration.LastName);
            formValues.Add("email", registration.Email);
            formValues.Add("dataConsent", registration.DataConsent);
            formValues.Add("workshopName", registration.WorkshopToRegister.Name);

            return formValues;
        }
    }
}

