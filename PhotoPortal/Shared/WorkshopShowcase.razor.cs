using System;
using BachVisuals.Models;
using Microsoft.AspNetCore.Components;

namespace PhotoPortal.Shared
{
	public partial class WorkshopShowcase
	{
		[Parameter, EditorRequired] public string SectionTitle { get; set; }
		[Parameter, EditorRequired] public List<Workshop>? Workshops { get; set; }
	}
}

