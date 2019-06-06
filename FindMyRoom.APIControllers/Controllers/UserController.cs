using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindMyRoom.BusinessManager;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindMyRoom.APIControllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        UserManager usermanager = new UserManager();
        Acknowledgement<User> aknowledgement = new Acknowledgement<User>();
        public UserController(UserManager managerobject)
        {
            this.usermanager = managerobject;
        }
        //Post api/User
        [Route("PostUser")]
        [HttpPost]
        public IActionResult PostUser(HelperAddUser addUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(usermanager.PostUser(addUser));
            }
         
            catch(Exception)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "Something went wrong. Please try again later";
                return Ok(aknowledgement);
            }

        }
        [Route("GetUser")]
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            IEnumerable<User> users = usermanager.GetUsers();
            return users;
        }

        //It can be used for change password
        //Logic is implemented for updating password of a particular user
        [HttpGet]
        [Route("UpdatePassword/{email}/{password}")]

        public IActionResult ForgetPassword(string email, string password)
        {
            Acknowledgement<User> payload = new Acknowledgement<User>();
            try
            {
                //ForgetPasswordType forgetPasswordType = new ForgetPasswordType();
                if (email.Equals(null))
                {
                    //return BadRequest(password);
                    throw new Exception("Email should not be empty");
                }
                if (password.Equals(null))
                {
                    //return BadRequest(password);
                    throw new Exception("Password should not be empty");
                }
                payload.Message = usermanager.ForgetPasswordManager(email, password);
                payload.code = 1;
                payload.Set = null;
                if (payload.Message == "Email does not exist")
                {
                    //return BadRequest(email);
                    throw new Exception("Email does not exist");
                }
                if (payload.Message.Equals("Invalid Email"))
                {
                    throw new Exception("Invalid Email");
                }
                if (payload.Message.Equals("Invalid Password"))
                {
                    throw new Exception("Invalid Password");
                }
            }
            catch (Exception ex)
            {
                payload.code = 2;
                payload.Set = null;
                payload.Message = ex.Message;
            }
            return Ok(payload);
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Validate([FromBody] AuthenticateAdmin authenticateAdmin)
        {
            Acknowledgement<User> payload = new Acknowledgement<User>();
            try
            {
                payload = usermanager.Validate(authenticateAdmin.UserEmail, authenticateAdmin.UserPassword);
            }
            catch (Exception ex)
            {
                payload.code = 2;
                payload.Set = null;
                payload.Message = "Invalid Credentials"+ex.Message;
            }
            return Ok(payload);
        }
        [Route("fblogin")]
        [HttpPost]
        public IActionResult FbUser([FromBody] Fbuser fbuser)
        {
            Acknowledgement<User> payload = new Acknowledgement<User>();
            try
            {   
                payload = usermanager.fbUser(fbuser);
            }
            catch (Exception ex)
            {
                payload.Set = null;
                payload.code = 3;
                payload.Message = ex.Message;
            }
            return Ok(payload);
        }

        //It is used for sending email
        [HttpGet]
        [Route("SendEmail/{email}")]
        public IActionResult EmailPassword(string email)
        {
            Acknowledgement<User> payload = new Acknowledgement<User>();
            try
            {
                if (email==null)
                {
                    //return BadRequest(email);
                    throw new Exception("Email should not be empty");
                }
                payload.Message= usermanager.EmailPasswordManager(email);
                payload.code = 1;
                payload.Set = null;
                
                if (payload.Message==null)
                {
                    //return BadRequest(email);
                    throw new Exception("Email does not exist");
                }
                if (payload.Message.Equals("Invalid Input"))
                {
                    throw new Exception("Invalid Input");
                }
            }
            catch (Exception e)
            {
                payload.code = 2;
                payload.Set = null;
                payload.Message = e.Message;
            }
            return Ok(payload);
        }

        [Route("UserDetails/{UserId}")]
        [HttpGet]
        public IActionResult GetUserdata(int UserId)
        {
            Acknowledgement<UserUpdateHelp> aknowledgement = new Acknowledgement<UserUpdateHelp>();
            try
            {
                return Ok(usermanager.GetUser(UserId));
            }
            catch (Exception)
            {
                aknowledgement.code = 3;
                aknowledgement.Set = null;
                aknowledgement.Message = "Something went wrong. Please try again later";
                return Ok(aknowledgement);
            }
        }

        [Route("UpdateUser")]
        [HttpPut]

        public IActionResult UpdateUserData( [FromBody] UserPutHelp user)
        {
            try
            {
                return Ok(usermanager.UpdateUser(user));
            }
            catch (Exception)
            {
                aknowledgement.code = 3;
                aknowledgement.Set = null;
                aknowledgement.Message = "Something went wrong. Please try again later";
                return Ok(aknowledgement);
            }
        }
    }
}