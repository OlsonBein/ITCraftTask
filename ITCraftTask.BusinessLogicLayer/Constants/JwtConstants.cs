using System;
using System.Collections.Generic;
using System.Text;

namespace ITCraftTask.BusinessLogicLayer.Constants
{
    public class JwtConstants
    {
        public const string SigningSecurityKey = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n141faza";
        public const string ValidIssuer = "Server";
        public const string ValidAudience = "Client";
        public const string AccessToken = "AccessToken";
        public const string RefreshToken = "refreshtoken";
    }
}
