using Auth.Application.Helper;
using Auth.Domain.Contracts;
using Auth.Domain.Entities;
using Auth.Infrastructure.Data;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            _logger.LogInformation("Getting user by email: {Email}", email);
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }
        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            _logger.LogInformation("Getting user by username: {Username}", username);
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> CheckPassword(ApplicationUser user,string password)
        {
            bool passwordValid = await _userManager.CheckPasswordAsync(user, password);
            return passwordValid;
        }
    }

}
