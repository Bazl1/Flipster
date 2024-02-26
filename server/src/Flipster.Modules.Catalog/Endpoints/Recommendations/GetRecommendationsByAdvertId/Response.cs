using Flipster.Modules.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Endpoints.Recommendations.GetRecommendationsByAdvertId;

public class Response
{
    public IEnumerable<AdvertDto> Adverts { get; set; }
    public int PageCount { get; set; }
}