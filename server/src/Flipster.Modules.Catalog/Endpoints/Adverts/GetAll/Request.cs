namespace Flipster.Modules.Catalog.Endpoints.Adverts.GetAll;

public class Request
{
    public string? Query { get; set; } = null;
    public string? UserId { get; set; } = null;
    public string? CategoryId { get; set; } = null;
    public int? Min { get; set; } = null;
    public int? Max { get; set; } = null;
    public string? Location { get; set; } = null;
}