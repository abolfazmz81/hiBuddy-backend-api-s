using Microsoft.SqlServer.Server;

namespace hiBuddy.Models
{
    public class UserManagement
    {
        public int user_id { get; set; }
        public String username { get; set; }
        public String profilename { get; set; }
        public String job { get; set; }
        public String education { get; set; }
        
        //public String geography { get; set; }
        
        //public String x { get; set; }
        
        //public String y { get; set; }

    }
}