﻿using System;

namespace Aimrank.Web.Contracts.Requests
{
    public class CreateServerRequest
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string[] Whitelist { get; set; }
    }
}