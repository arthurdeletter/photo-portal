using System;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace BachVisuals.Models;

public class GearItem : Content
{
	public string DisplayName { get; set; }
	public MediaWithCrops DisplayImage { get; set; } = default!;
	public int OwnershipCounter { get; set; }
	public string Brand { get; set; }
	public string GearType { get; set; }
}

