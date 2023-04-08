
public class BookDTO{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public double Price { get; set; }
    public string? Image { get; set; }
    public ICollection<CategoryDTO>? Categories { get; set; }
    public State State { get; set; }
    public bool IsActive { get; set;}
}