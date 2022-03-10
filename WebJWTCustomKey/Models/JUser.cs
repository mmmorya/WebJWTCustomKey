using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebJWTCustomKey.Models
{
    public class JUser
    {
        public int Id { get; set; }
        public string UserToken { get; set; }
    }
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string UserToken { get; set; }
        public AuthenticateResponse(JUser user, string token)
        {
            Id = user.Id;
            UserToken = token;
        }
    }

    public class AuthenticateRequest
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string UserToken { get; set; }
    }
}
