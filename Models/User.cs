using System.ComponentModel.DataAnnotations;
public class User{
    [Key]
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Phone { get; set; }
    public bool IsAdmin { get; set;}
    public string? Direction { get; set; }
    public ICollection<Order>? Orders{ get; set; }
}