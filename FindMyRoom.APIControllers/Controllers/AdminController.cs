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
    public class AdminController: ControllerBase
    {
        AdminManager adminManager = new AdminManager();
        public AdminController(AdminManager adminManager)
        {
            this.adminManager = adminManager;
        }
        Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();

        [Route("GetUserDetails")]
        [HttpGet]
        public Acknowledgement<UserModified> GetUserDetails()
        {
            try
            {
                return adminManager.GetUserDetails();
            }
            catch(Exception)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "Something went wrong. Please try again later";
                return aknowledgement;
            }
        }

        [Route("ShowPartners")]
        [HttpGet]
        public Acknowledgement<UserModified> Getpartnerdata()
        {
            try
            {
                return adminManager.GetPartners();
            }
            catch (Exception)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "Something went wrong. Please try again later";
                return aknowledgement;
            }
        }

        [Route("UpdatePartner/{PartnerId}")]
        [HttpPut]
        public IActionResult PutPartner([FromRoute] int PartnerId, [FromBody] UserModified user)
        {
            Acknowledgement<UserModified> aknowledgement = new Acknowledgement<UserModified>();
            try
            {
                if (!(PartnerId == user.UserId))
                {
                    aknowledgement.code = 2;
                    aknowledgement.Set = null;
                    aknowledgement.Message = "PartnerId and UserId does not match";
                    return Ok(aknowledgement);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(adminManager.UpdatePartner(user, PartnerId));

            }
            catch (Exception)
            {
                aknowledgement.code = 2;
                aknowledgement.Set = null;
                aknowledgement.Message = "Something went wrong. Please try again later";
                return Ok(aknowledgement);
            }
        }
        [HttpDelete]
        [Route("DeletePartner/{PartnerId}")]
        public IActionResult DeletePartnerList(int PartnerId)
        {
            Acknowledgement<User> acknowledgement = new Acknowledgement<User>();
            try
            {

                return Ok(adminManager.deletePartnerService(PartnerId));
            }
            catch (Exception)
            {
                acknowledgement.code = 2;
                acknowledgement.Set = null;
                acknowledgement.Message = "No Partner found with given PartnerId to Delete";
                return Ok(acknowledgement);
            }
        }
    }
}
