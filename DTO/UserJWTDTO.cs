public class UserJWTDTO{
    public int Id { get; set; }
    public bool IsAdmin { get; set;}

    public UserJWTDTO(int id, bool isAdmin){
        this.Id = id;
        this.IsAdmin = isAdmin;
    }
}

