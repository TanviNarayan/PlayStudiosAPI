using Microsoft.AspNetCore.Mvc;
using PlayStudiosApi.Interfaces;
using PlayStudiosApi.Infrastructure;
using System.Collections.Generic;
using PlayStudios.Model;

namespace PlayStudiosApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _noteRepository;
        public UserController(IUserRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [NoCache]
        [HttpPost]
        [Route("api/User")]
        public bool Post([FromBody]User user)
        {
            return _noteRepository.Insert(user);
        }

        //Get Login Details
        [HttpPost]
        [Route("api/User/LoginDetails")]
        public bool LoginDetails([FromBody]LoginData loginData)
        {
            return _noteRepository.GetLoginData(loginData);
        }

        // Reset Password Email
        [HttpPost]
        [Route("api/User/ResetPassSendingMail")]
        public bool ResetPassSendingMail([FromBody]ResetPassSendEmailData resetPassData)
        {
            return _noteRepository.ResetPassSendMail(resetPassData);
        }

        //Reset Password Verification
        [HttpPost]
        [Route("api/User/ResetPassVerification")]
        public bool ResetPassVerification([FromBody]ResetPassData resetPassData)
        {
            return _noteRepository.ResetPassVerification(resetPassData);
        }

        //Delete User
        [HttpGet]
        [Route("api/User/DeleteUser")]
        public bool DeleteUser(string email)
        {
            return _noteRepository.DeleteUser(email);
        }

        //Get All Users
        [HttpGet]
        [Route("api/User/GetAllUser")]
        public List<User> GetAllUser()
        {
            return _noteRepository.GetAllUser();
        }

        public string MyMethod(Microsoft.AspNetCore.Http.HttpContext context)
        {
            return $"{context.Request.Scheme}://{context.Request.Host}";
        }
    }
}
