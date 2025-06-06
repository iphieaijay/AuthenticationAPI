﻿using Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Interfaces
{
    public interface ITokenService
    {
        (string token, DateTime expiresAt) GenerateToken(ApplicationUser user);
        string GenerateRefreshToken();
    }
}
