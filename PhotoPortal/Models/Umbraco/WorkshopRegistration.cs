using System;
using System.Runtime.CompilerServices;

namespace PhotoPortal.Models.Umbraco
{
    public class WorkshopRegistration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool DataConsent { get; set; }
        public Workshop WorkshopToRegister { get; set; }
    }
}

