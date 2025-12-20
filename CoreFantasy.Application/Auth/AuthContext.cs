using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFantasy.Application.Auth
{
    public class AuthContext : IAuthContext
    {
        public string IdentityId { get; init; }
        public string Email { get; init; }

        public IReadOnlyCollection<string> Roles { get; init; }

        public bool IsInRole(string role)
            => Roles.Contains(role);
    }

}
