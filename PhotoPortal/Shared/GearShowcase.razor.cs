using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Models.Umbraco;

namespace PhotoPortal.Shared
{
	public partial class GearShowcase
	{
        [Parameter, EditorRequired] public string SectionTitle { get; set; }
        [Parameter, EditorRequired] public List<GearItem> Items { get; set; }
    }
}

