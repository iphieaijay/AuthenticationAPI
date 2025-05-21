﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.DTO
{
    public record CustomResponse(int responseCode, string message, object? data = null);
}
