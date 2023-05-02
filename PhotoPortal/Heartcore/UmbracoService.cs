using System;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace PhotoPortal.Heartcore;

public class UmbracoService
{
	private readonly ContentDeliveryService _contentDelivery;

	public UmbracoService(ContentDeliveryService contentDelivery)
	{
		this._contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
	}

	public async Task<Content> GetRoot()
	{
		var rootContentItems = await _contentDelivery.Content.GetRoot();
		return rootContentItems.FirstOrDefault();
	}

	public async Task<IEnumerable<Content>> GetCameras()
	{
		var cameraContentItems = await _contentDelivery.Content.GetByType("camera", null);
		return cameraContentItems.Content.Items;
	}
}

