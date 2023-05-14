using System;
using BachVisuals.Models;
using Microsoft.AspNetCore.Components;

namespace PhotoPortal.Shared
{
	public partial class GearShowcase
	{
        [Parameter, EditorRequired] public string SectionTitle { get; set; }
        [Parameter, EditorRequired] public List<GearItem> Items { get; set; }
    }
}

