using Application.DTO;
using Auth.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterUserRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
     }
}
