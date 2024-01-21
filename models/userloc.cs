using Microsoft.EntityFrameworkCore;

namespace hiBuddy.Models
{
    [PrimaryKey(nameof(location_id),nameof(user_id))]
    public class userloc
    {
        public int location_id { get; set; }
        public int user_id { get; set; }
    }
}

