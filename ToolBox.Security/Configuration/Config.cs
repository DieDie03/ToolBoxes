using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Security.Configuration
{
    public class Config
    {
        public string Signature { get; set; }

        public string Issuer { get; set; }
        
        public string Audience { get; set; }
        
        public int Duration { get; set; }
        
        public bool ValidateIssuer { get; set; }
        
        public bool ValidateAudience { get; set; }
        
        public bool ValidateDate { get; set; }

    }
}
