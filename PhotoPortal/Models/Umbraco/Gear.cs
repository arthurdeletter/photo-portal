using System;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace BachVisuals.Models;

public class Gear : Content
{
	public string Heading { get; set; } = default!;
	public string SubHeading { get; set; } = default!;
	public MediaWithCrops Banner { get; set; } = default!;

    public string AvailableGearTitle { get; set; }
    public List<GearItem> AvailableGear { get; set; }
}

