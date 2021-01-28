using System.Collections.Generic;

namespace Aimrank.Application.Commands.RefreshJwt
{
    public class AuthenticationErrorDto
    {
        public string FieldName { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}