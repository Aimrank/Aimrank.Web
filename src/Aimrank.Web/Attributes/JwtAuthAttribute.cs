using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Aimrank.Web.Attributes
{
    public class JwtAuthAttribute : AuthorizeAttribute
    {
        public JwtAuthAttribute(string policy = "") : base(policy)
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}