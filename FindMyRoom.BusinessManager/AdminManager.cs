using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using FindMyRoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FindMyRoom.BusinessManager
{
    public class AdminManager
    {
        AdminService deletepartner = new AdminService();
        public AdminManager()
        { }
        public AdminManager(AdminService adminService)
        {
            this.deletepartner = adminService;
        }
        AdminService update = new AdminService();
       // UserService signup = new UserService();
        public static string validations(UserModified user)
        {
           AdminService update1 = new AdminService();

            // Regex UserId = new Regex(@"^$");
            Regex Name = new Regex(@"^[a-zA-Z\s]*$");
            //Regex Email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            Regex MobileNumber = new Regex(@"[6/7/8/9]{1}[0-9]{9}$");
            //Regex Password = new Regex(@"(?=^.{6,20}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$");
            int Count1 = 0;
            string type="";

            foreach (var item in update1.GetUserDetails())
            {
               if ((item.UserId==user.UserId))
               {
                    Count1++;
                    type = item.UserType;
               }

            }
            if(Count1==1 && !(type.Equals("partner",StringComparison.OrdinalIgnoreCase)))
            {
                return "User is not a partner";
            }
            if(Count1==0)
            {
                return "UserID does not exist";
            }
            if (!Name.IsMatch(user.UserName))
            {
                return "Username is invalid";
            }
            else if (!MobileNumber.IsMatch(user.UserPhoneNumber))
            {
                return "Mobile number should start with 6/7/8/9 and should have 10 digits";
            }
            else if(!user.UserType.Equals("Partner", StringComparison.OrdinalIgnoreCase))
            {
                return "user type should be Partner";
            }
            else if (!(user.UserStatus.Equals("valid", StringComparison.OrdinalIgnoreCase) || user.UserStatus.Equals("invalid", StringComparison.OrdinalIgnoreCase)))
            {
                return "user status should be valid or invalid";
            }
            else 
            {
                return "success";
            }

        }

        //This method will call the get method in UserService 

        public virtual Acknowledgement<UserModified> GetPartners()
        {
            //AdminService update = new AdminService();

            try
            {
                List<UserModified> PartnerList = update.Getpartnerdata();
                Acknowledgement<UserModified> acknowledgement = new Acknowledgement<UserModified>();
                if (PartnerList == null)
                {
                    acknowledgement.code = 2;
                    acknowledgement.Set = PartnerList;
                    acknowledgement.Message = "Partnerdata loading failed";
                    return acknowledgement;
                }
                else
                {
            
                    acknowledgement.code = 0;
                    acknowledgement.Set = PartnerList;
                    acknowledgement.Message = "Partnerdata loaded successfully";
                    update.Getpartnerdata();
                    return acknowledgement;
                }
            }
            catch(Exception ex)
            {
                throw new RetrievingDataException(ex.Message);
            }
            
      }

        public virtual Acknowledgement<UserModified> GetUserDetails()
        {
            try
            {
                List<UserModified> UserList = update.GetUserDetails();
                Acknowledgement<UserModified> acknowledgement = new Acknowledgement<UserModified>();
                if (UserList == null)
                {
                    acknowledgement.code = 2;
                    acknowledgement.Set = UserList;
                    acknowledgement.Message = "Userdata loading failed";
                    return acknowledgement;
                }
                else
                {

                    acknowledgement.code = 0;
                    acknowledgement.Set = UserList;
                    acknowledgement.Message = "Userdata loaded successfully";
                    return acknowledgement;
                }
            }
            catch (Exception ex)
            {
                throw new RetrievingDataException(ex.Message);
            }
        }

        //This method will do validations for existance of email and type of partner and if not exists it inserts data.

        public virtual Acknowledgement<UserModified> UpdatePartner(UserModified user, int id)
        {
            Acknowledgement<UserModified> acknowledgement = new Acknowledgement<UserModified>();
            try
            {
                List<UserModified> partners = new List<UserModified>();
                partners.Add(user);
                User getuser = update.GetPartner(id);

                string validator = validations(user);
                if (!validator.Equals("success"))
                {
                    acknowledgement.code = 2;
                    acknowledgement.Set = partners;
                    acknowledgement.Message = validator;
                    return acknowledgement;
                }
                else
                {
                    string name = user.UserName.Trim();
                    string address = user.UserAddress.Trim();
                    if (name.Equals(""))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = partners;
                        acknowledgement.Message = "Full Name field should not empty";
                        return acknowledgement;
                    }
                    else if ((address.Equals("")))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = partners;
                        acknowledgement.Message = "Address field should not be empty";
                        return acknowledgement;

                    }
                    else if(!(user.UserEmail.Equals(getuser.UserEmail)))
                    {
                        acknowledgement.code = 2;
                        acknowledgement.Set = partners;
                        acknowledgement.Message = "Email id cannot be updated";
                        return acknowledgement;
                    }
                    else
                    {
                        acknowledgement.code = 0;
                        acknowledgement.Set = partners;
                        acknowledgement.Message = "Successfully updated.";
                        update.UpdatePartner(user, id);
                        return acknowledgement;
                    }
                }
            
                    //return acknowledgement;
              
                //List<User> partners = new List<User>();
                //partners.Add(user);

                //if (partners == null)
                //{
                //    acknowledgement.code = 2;
                //    acknowledgement.Set = partners;
                //    acknowledgement.Message = "Partnerdata loading failed";
                //    return acknowledgement;
                //}
                //acknowledgement.code = 0;
                //acknowledgement.Set = partners;
                //acknowledgement.Message = "Partnerdata loaded successfully";
                //update.UpdatePartner(user, id);
                //return acknowledgement;
               
            }
            catch (Exception ex)
            {
                throw new InsertingDataException(ex.Message);
            }
        }
        public virtual Acknowledgement<User> deletePartnerService(int UserId)
        {
            try
            {
                Acknowledgement<User> acknowledgement = new Acknowledgement<User>();
                //AdminService deletePartner = new AdminService();
                deletepartner.PartnerDelete(UserId);
                acknowledgement.code = 1;
                acknowledgement.Set = null;
                acknowledgement.Message = "Successfully deleted";
                return acknowledgement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}