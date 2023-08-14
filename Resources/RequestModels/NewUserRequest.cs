using Entities.Entities;
using Resources.Enums;

namespace Resources.RequestModels
{
    public class NewUserRequest
    {
        public int IdRol { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public int UserPhone { get; set; }

        public UserItem ToUserItem()
        {
            var userItem = new UserItem();

            userItem.IdRol = IdRol;
            userItem.UserName = UserName;
            userItem.InsertDate = DateTime.Now;
            userItem.IsActive = true;
            userItem.UserEmail= UserEmail;
            userItem.UserPhone = UserPhone;

            return userItem;
        }
    }
}
