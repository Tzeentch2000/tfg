
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
[NotMapped]
public class Jwt{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Subject { get; set; }

    public static dynamic TokenValidation(ClaimsIdentity identity){
        try{
            if(identity.Claims.Count() == 0){
                return new{
                    success=false,
                    message="Incorrect Token",
                    result=""
                };
            }

            var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
            var role = identity.Claims.FirstOrDefault(c => c.Type == "userRole").Value;

            var userObjectResponse = new UserJWTDTO(Int32.Parse(id),Convert.ToBoolean(role));

            return new{
                    success=true,
                    message="Exit",
                    result=userObjectResponse
                };
        } catch(Exception e){
            return new{
                success=false,
                message="Catch:"+e.Message,
                result=""
            };
        }
    }
}