public class UserResultDTO{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Phone { get; set; }
    public bool IsAdmin { get; set;}
    public ICollection<OrderForResultDTO>? Orders{ get; set; }
}