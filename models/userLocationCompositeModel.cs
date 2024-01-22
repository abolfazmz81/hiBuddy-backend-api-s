using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.SqlServer.Types;


using hiBuddy.Models;

namespace hiBuddy.models
{
    public class userLocationCompositeModel
    {
        public UserManagement User { get; set; }
        public locations? Locations { get; set; }
        public ArrayList? OtherLocation { get; set; }
    }
}

