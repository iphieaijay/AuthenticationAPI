using Application.DTO;
using Application.Interfaces;
using Auth.Application.Helper;
using Auth.Application.Interfaces;
using Auth.Domain.Contracts;
using Auth.Domain.Entities;
using Auth.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, 
            ILogger<UserService> logger, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterUserRequest request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists." });
            }
            
            var user = _mapper.Map<ApplicationUser>(request);
            user.CreatedOn = DateTime.Now;
            user.PasswordHash = _passwordHasher.HashPassword(user,request.Password);
            
            return await _userRepository.CreateAsync(user);
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse userResponse = new LoginResponse();
            ApplicationUser user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is  null )
            {
                return new LoginResponse();
            }
            bool passwordValid = await _userRepository.CheckPassword(user, request.Password);
            if (!passwordValid)
            {
                userResponse.Description = "Invalid credentials";
                return userResponse;
            }

                //Generate access Token
                var(token, expiresAt) = _tokenService.GenerateToken(user);
            
                //Geneate refresh token
                var refreshToken = _tokenService.GenerateRefreshToken();

                userResponse = _mapper.Map<LoginResponse>(user);
                userResponse.RefreshToken = refreshToken;
                userResponse.AccessToken = token;
                 userResponse.TokenExpires = expiresAt;
                return userResponse;

          
        }

        
    }
    
}
