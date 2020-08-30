using Microsoft.AspNetCore.Identity;

namespace ITCraftTask.DataAccessLayer.Entities
{
    public class User : IdentityUser<long>
    {
        public string Name { get; set; }
    }
}
