using System;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace BachVisuals.Models;

public class HomePage : Content
{
    public string HeroHeading { get; set; } = default!;
    public string HeroSubheading { get; set; } = default!;
    public MediaWithCrops HeroBanner { get; set; } = default!;

    # region Showcase Gear
    public string ShowcaseGearTitle { get; set; }
    public List<GearItem> ShowcaseGear { get; set; }
    # endregion

    # region Showcase Workshops
    public string ShowcaseWorkshopsTitle { get; set; }
    public List<Workshop> ShowcaseWorkshops { get; set; }
    #endregion
}


