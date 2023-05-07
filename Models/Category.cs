using System.ComponentModel.DataAnnotations;
public class Category{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set;}
    //public DateTime Date { get; set; } = DateTime.UtcNow;
    //public string? ColorCode { get; set;}
    public ICollection<Book>? Books { get; set; }
}