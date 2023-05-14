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

    public async Task<Content> GetContentById(Guid id)
    {
        var content = await _contentDelivery.Content.GetById<Content>(id);
        return content;
    }

    public async Task<Content> GetContentByRoute(string route)
    {
        var content = await _contentDelivery.Content.GetByUrl(route, null, depth: 2);

        return content;
    }

    public async Task<PagedContent<Content>> GetContentByType(string type, int page = 1, int pageSize = 10)
    {
        var content = await _contentDelivery.Content.GetByType(type, page: page, pageSize: pageSize);

        return content;
    }

    public async Task<PagedContent<Content>> GetChildrenById(Guid guid, int page = 1, int pageSize = 10)
    {
        var content = await _contentDelivery.Content.GetChildren(guid);

        return content;
    }
}

