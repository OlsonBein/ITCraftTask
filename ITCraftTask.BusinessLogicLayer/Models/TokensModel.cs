﻿namespace ITCraftTask.BusinessLogicLayer.Models
{
    public class TokensModel: BaseModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
