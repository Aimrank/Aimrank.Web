using System;

namespace Aimrank.Web.Contracts.Requests
{
    public class RefreshJwtRequest
    {
        public Guid RefreshToken { get; set; }
        public string Jwt { get; set; }
    }
}