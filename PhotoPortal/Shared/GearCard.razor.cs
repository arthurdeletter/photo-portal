using System;
using Microsoft.AspNetCore.Components;

namespace PhotoPortal.Shared
{
    public partial class GearCard
    {
        [Parameter, EditorRequired] public Guid Id { get; set; }
        [Parameter, EditorRequired] public string DisplayName { get; set; } = default!;
        [Parameter, EditorRequired] public string Description { get; set; } = default!;
        [Parameter, EditorRequired] public string Owners { get; set; } = default!;
        [Parameter] public object? DisplayImage { get; set; }

        protected override Task OnParametersSetAsync()
        {
            Console.WriteLine(DisplayImage);
            return base.OnParametersSetAsync();
        }
    }
}

