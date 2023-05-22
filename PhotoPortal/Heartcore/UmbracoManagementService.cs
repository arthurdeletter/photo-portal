using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Management.Models;

namespace PhotoPortal.Heartcore
{
    public class UmbracoManagementService
	{
        private readonly ContentManagementService _managementService;


        public UmbracoManagementService(ContentManagementService managementService)
		{
			_managementService = managementService ?? throw new ArgumentNullException(nameof(managementService));
		}

		public async Task<Content> GetById(Guid guid)
		{
			var content = await _managementService.Content.GetById(guid);

			return content;
		}

		public async Task<Content> Update(Content content)
		{
			var updatedContent = await _managementService.Content.Update(content);

			return updatedContent;
		}

		public async Task<Content> Publish(Guid guid)
		{
			var publishedContent = await _managementService.Content.Publish(guid);

			return publishedContent;
		}

		public async Task<IEnumerable<Form>> GetAllForms()
		{
            var allForms = await _managementService.Forms.GetAll();

            return allForms;
        }

		public async Task<bool> SubmitFormEntry(Guid formId, Dictionary<string, object> formValues)
		{
			try
			{
                await _managementService.Forms.SubmitEntry(formId, formValues);
				return true;
            }
			catch (Exception ex)
			{
				Console.WriteLine("Form submission error: " + ex.Message);
				return false;
			}
		}

		public async Task<Member?> GetMemberByUsername(string username)
		{
            try
            {
                var member = await _managementService.Member.GetByUsername(username);
                return member;
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

