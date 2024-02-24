using Flipster.Shared.Contracts.Catalog.Dtos;

namespace Flipster.Modules.Catalog.Dtos;

public class CategoryDto : ICategoryDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Icon { get; set; }
}
