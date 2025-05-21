using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Models
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public double Expires { get; set; }
    }
}
