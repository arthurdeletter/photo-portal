using System;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Models.Umbraco
{
    public class Workshop : Content
    {
        public string WorkshopTitle { get; set; } = default!;
        public string Excerpt { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Registrations { get; set; }
        public int MaxRegistrations { get; set; }

        public MediaWithCrops WorkshopBanner { get; set; } = default!;
    }
}

