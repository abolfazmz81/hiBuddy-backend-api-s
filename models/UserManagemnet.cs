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
        public String profile_name { get; set; }
        public String user_password { get; set; }
        
        public String? job { get; set; }
        public String? education { get; set; }
        
        public String? pic { get; set; }
        
        //public String geography { get; set; }
        
        //public String x { get; set; }
        
        //public String y { get; set; }

    }
}