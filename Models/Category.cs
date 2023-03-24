using System.ComponentModel.DataAnnotations;
public class Category{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set;}
    public ICollection<Book>? Books { get; set; }
}