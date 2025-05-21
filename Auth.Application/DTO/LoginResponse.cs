using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? AccessToken { get; set; }
        public DateTime TokenExpires { get; set; }
        public string? RefreshToken { get; set; }
        public object? ErorMessage { get; set; }
        public string? Description { get; set; }

    }
}
