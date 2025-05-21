using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Contracts
{
    public interface IUserRepository
    {

        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<bool> CheckPassword(ApplicationUser user,string password);
        
    }
}
