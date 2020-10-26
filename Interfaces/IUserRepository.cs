using PlayStudios.Model;
using System.Collections.Generic;

namespace PlayStudiosApi.Interfaces
{
    public interface IUserRepository
    {
        bool Insert(User user);
        bool GetLoginData(LoginData loginData);
        bool ResetPassSendMail(ResetPassSendEmailData resetPassData);
        bool ResetPassVerification(ResetPassData resetPassData);
        bool DeleteUser(string email);
        List<User> GetAllUser();
    }
}
