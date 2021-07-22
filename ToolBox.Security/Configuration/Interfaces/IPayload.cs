using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Security.Configuration.Interfaces
{
    public interface IPayload
    {
        string Email { get; }
        
        string Role { get; }
        
        string Identifier { get; }


    }
}
