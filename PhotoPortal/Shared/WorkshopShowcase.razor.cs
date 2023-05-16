using System;
using Microsoft.AspNetCore.Components;
using PhotoPortal.Models.Umbraco;

namespace PhotoPortal.Shared
{
	public partial class WorkshopShowcase
	{
		[Parameter, EditorRequired] public string SectionTitle { get; set; }
		[Parameter, EditorRequired] public List<Workshop>? Workshops { get; set; }
	}
}

