using System.ComponentModel.DataAnnotations;
public class State{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set;}
}