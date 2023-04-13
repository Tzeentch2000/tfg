using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order{
    [Key]
    public int Id { get; set; }
    public int Amount { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [ForeignKey("Book")]
    public int BookId { get; set; }
    public Book Book { get; set; }

    [ForeignKey("Usuario")]
    public int UserId {get;set;}
    public User? User {get;set;}
}