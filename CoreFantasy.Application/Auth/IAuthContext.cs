using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFantasy.Application.Auth
{
    public interface IAuthContext
    {
        string IdentityId { get; }
        string Email { get; }

        bool IsInRole(string role);
    }

}
