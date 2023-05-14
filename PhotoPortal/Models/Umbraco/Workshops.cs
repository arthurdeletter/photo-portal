using System;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace BachVisuals.Models;

public class Workshops : Content
{
	public string Heading { get; set; } = default!;
    public string SubHeading { get; set; } = default!;
    public MediaWithCrops Banner { get; set; } = default!;

    public string AvailableWorkshopsTitle { get; set; }
    public List<Workshop> AvailableWorkshops { get; set; }
}

