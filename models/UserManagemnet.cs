using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace hiBuddy.Models
{
    public class UserManagement
    {
        [Key]
        public int user_id { get; set; }
        public String username { get; set; }
        public String? name { get; set; }        
        public String? password { get; set; }
        public String? job { get; set; }
        public String? education { get; set; }        
        public String? pic { get; set; }        
        public String email { get; set; }        
        public String? hobbies { get; set; }
        public String? favorites { get; set; }        
        public String? Date { get; set; }
        public String? gender { get; set; }

    }
}