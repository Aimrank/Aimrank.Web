using System;

namespace Aimrank.Web.Contracts
{
    public class ConfirmEmailAddressRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}