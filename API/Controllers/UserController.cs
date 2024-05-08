using API.Attributes;
using API.IServices;
using Data;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Resources.FilterModels;
using Resources.RequestModels;
using Security.IServices;
using System;
using System.Security.Authentication;
using System.Web.Http.Cors;
namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserSecurityService _userSecurityService;
        private readonly IUserService _userService;
        private readonly ServiceContext _serviceContext;
        public UserController(IUserSecurityService userSecurityService, IUserService userService, ServiceContext serviceContext)
        {
            _userSecurityService = userSecurityService;
            _userService = userService;
            _serviceContext = serviceContext;
        }
        [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpPost(Name = "LoginUser")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var usersData = _userService.GetAllUsers();
                UserItem user = usersData.Where(user => user.UserName == loginRequest.UserName).First();
                int userIdRol = user.IdRol;
                UserRolItem rol = _serviceContext.Set<UserRolItem>().Where(ur => ur.Id == userIdRol).FirstOrDefault();
                string roleName = rol?.Name;
                string userName = user.UserName;
                int userId = user.Id;
                string token = _userSecurityService.GenerateAuthorizationToken(loginRequest.UserName, loginRequest.UserPassword);
                return Ok(new Tuple<string, int, string, string, int>(token, userIdRol, roleName, userName, userId));
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
        }
        [EndpointAuthorize(AllowedUserRols = "Administrador")]
        [HttpPost(Name = "InsertUser")]
        public int InsertUser([FromBody] NewUserRequest newUserRequest)
        {
            return _userService.InsertUser(newUserRequest);
        }

          [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpGet(Name = "GetAllUsers")]
        public List<UserItem> GetAll()
        {
            return _userService.GetAllUsers();
        }

        [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpDelete(Name = "DeleteUser")]
        public void Delete([FromQuery] int id)
        {
            _userService.DeleteUser(id);
        }

        [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpGet(Name = "GetUsersById")]
        public List<UserItem> GetUserById([FromQuery] int id)
        {
            return _userService.GetUsersById(id);
        }
    }
}
