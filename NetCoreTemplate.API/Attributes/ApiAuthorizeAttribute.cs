﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreTemplate.API.Attributes;

//Identity Copy
public sealed class ApiAuthorizeAttribute : AuthorizeAttribute
{
    public ApiAuthorizeAttribute() : base()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }

    public ApiAuthorizeAttribute(string policy) : base(policy)
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}