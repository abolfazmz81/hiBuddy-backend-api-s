using System.ComponentModel.DataAnnotations;

namespace User.Domain;

public class User
{
    [Key]
    public int user_id { get; set; }
    public String username { get; set; }
    public long phone_number { get; set; }
    public String? name { get; set; }        
    public String? password { get; set; }
    public String? job { get; set; }
    public String? education { get; set; }        
    public String? pic { get; set; }        
    public String? email { get; set; }        
    public String? hobbies { get; set; }
    public String? favorites { get; set; }        
    public String? Date { get; set; }
    public String? gender { get; set; }
    public bool isverified { get; set; }
}