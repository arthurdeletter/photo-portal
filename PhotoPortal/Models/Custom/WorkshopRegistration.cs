using PhotoPortal.Models.Umbraco;

namespace PhotoPortal.Models.Custom
{
    public class WorkshopRegistration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool DataConsent { get; set; }
        public Workshop WorkshopToRegister { get; set; }
    }
}

