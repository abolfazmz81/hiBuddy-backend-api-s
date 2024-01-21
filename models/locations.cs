using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.SqlServer.Types;


namespace hiBuddy.Models
{
    
    public class locations
    {
        [Key]
        public int location_id { get; set; }
        
        public int x { get; set; }
        public int y { get; set; }
        public string loc { get; set; }
        public string loc_description { get; set; }
    }
}