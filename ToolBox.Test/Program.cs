using System;
using ToolBox.Security.Configuration;
using ToolBox.Security.Configuration.Interfaces;
using ToolBox.Security.Services;

namespace ToolBox.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenService service = new(new Config
            {
                Audience = "Mon App Console",
                Issuer = "Mon App Console",
                Signature = "W9$My#tZLC_2#+TmkC9cwsMPV7kAXN*_@FdRpTAsLFYZwY83^-a+7HzS@4FM",
                Duration = 86400,
                ValidateDate = true,
            });
            string token = service.CreateToken(
                new Payload
                {
                    Id= 42,
                    Email = "bastindieg@gmail.com",
                    Role = "Customer",
                    BirthDate = new DateTime(1995,3,22)
                }    
            );
            Console.WriteLine(token);
        }
    }
    class Payload : IPayload
    {
        public string Email { get; set; }

        public string Role { get; set; }

        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        public string Identifier { get { return Id.ToString(); } }
    }
}
