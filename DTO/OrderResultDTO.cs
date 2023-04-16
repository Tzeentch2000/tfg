
public class OrderForResultDTO{
    public int Id { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
    public int BookId { get; set; }
    public BookForInsertDTO Book { get; set; }
    public int UserId {get;set;}
}