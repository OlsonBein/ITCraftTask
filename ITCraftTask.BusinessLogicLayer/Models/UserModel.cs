namespace ITCraftTask.BusinessLogicLayer.Models
{
    public class UserModel: BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
