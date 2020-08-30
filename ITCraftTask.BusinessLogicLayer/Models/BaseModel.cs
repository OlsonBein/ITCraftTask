using System.Collections.Generic;

namespace ITCraftTask.BusinessLogicLayer.Models
{
    public class BaseModel
    {
        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
