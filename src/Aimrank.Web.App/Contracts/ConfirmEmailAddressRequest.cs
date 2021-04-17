using System;

namespace Aimrank.Web.App.Contracts
{
    public class ConfirmEmailAddressRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}