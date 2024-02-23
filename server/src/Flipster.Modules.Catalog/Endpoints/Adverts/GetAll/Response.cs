using Flipster.Modules.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Endpoints.Adverts.GetAll;

#pragma warning disable

internal class Response
{
    public IEnumerable<AdvertDto> Items {get;set;}
    public int PageCount { get; set; }
    public int ItemCount { get; set; }
};
