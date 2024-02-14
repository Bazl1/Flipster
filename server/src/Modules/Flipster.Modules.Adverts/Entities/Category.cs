namespace Flipster.Modules.Adverts.Entities;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Icon { get; set; } = null!;
    
    public Category(int id, string title, string icon)
    {
        Id = id;
        Title = title;
        Icon = icon;
    }
}